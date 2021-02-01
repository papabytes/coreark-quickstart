using System;
using AutoMapper;
using CoreArk.Packages.Core;
using CoreArk.Packages.Core.Interfaces;
using CoreArk.Packages.Security;
using CoreArk.Packages.Services;
using CoreArk.Packages.Web;
using Entity.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Services;
using Services.CompanySettingses.MappingProfiles;

namespace Api
{
    public class Startup
    {
        private readonly string _apiDescription;
        private readonly string _environment;
        private readonly string _version;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            _version = Environment.GetEnvironmentVariable("ASPNETCORE_APP_VERSION");
            _apiDescription = $"[{_environment}] Schréder Hyperion Api - ({_version})";
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddHttpContextAccessor();

            services.AddContextService();

            services.AddServices();

            //Adding Papabytes.Packages.Security - includes SecurityOptions
            services.AddSecurityServices(Configuration);

            services.AddJwtAuthentication(Configuration);

            services.AddScoped<IConfigurationAssemblyResolver, ConfigurationAssemblyResolver>();

            var connectionString = Configuration.GetConnectionString("DefaultConnectionString");

            services.AddDbContext<DataContext>(opts =>
            {
                opts.UseMySql(connectionString, db => db.MigrationsAssembly("Migrations.MySql"));
            });

            services.AddAutoMapper(typeof(CompanySettingsMappingProfile).Assembly);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(_version,
                    new OpenApiInfo
                    {
                        Title = _apiDescription,
                        Version = _version
                    });
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext dataContext)
        {
            dataContext.Database.Migrate();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/<{_version}/swagger.json", _apiDescription);
                c.RoutePrefix = string.Empty;
            });

            app.UseExceptionHandler("/error");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
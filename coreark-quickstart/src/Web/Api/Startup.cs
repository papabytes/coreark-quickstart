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
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
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

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "<Your Api Name>", Version = "v1"}); });

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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "<Your API> V1");
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
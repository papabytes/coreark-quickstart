using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using BlazorStyled;
using CoreArk.Packages.Security;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Portal.Clients;
using Portal.Services;

namespace Portal
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddLocalization();

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

            builder.Services.AddAuthorizationCore();
            
            builder.Services.AddSecurityServices(builder.Configuration);
            
            builder.Services.AddTransient(sp => 
                new HttpClient
                {
                    BaseAddress = new Uri(builder.Configuration.GetValue<string>("ApiBaseAddress"))
                });

            builder.Services.AddTransient<ApiHttpMessageHandler>();
            builder.Services.AddHttpClient<ApiClient>(client =>
                    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ApiBaseAddress")))
                .AddHttpMessageHandler<ApiHttpMessageHandler>();
            
            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddTransient<IConfigurationService, ConfigurationService>();

            builder.Services.AddBlazorStyled();
            
            await builder.Build().RunAsync();
        }
    }
}

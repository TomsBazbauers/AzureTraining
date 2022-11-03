using AzureApi;
using AzureApi.Configurations;
using AzureApi.Services;
using AzureApi.Services.Interfaces;
using DataProvider.Configurations;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;

[assembly: FunctionsStartup(typeof(DataProvider.Startup))]
namespace DataProvider
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services
                .AddRefitClient<IPublicApi>().ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.publicapis.org"));
            builder.Services
                .AddSingleton<IAzureConfiguration, AzureConfiguration>();

            builder.Services
                .AddScoped<IBlobStorageProvider, StorageProvider>();
            builder.Services
                .AddScoped<ILogStorageProvider, StorageProvider>();

            builder.Services
                .AddScoped<IBlobService, BlobService>();
            builder.Services
                .AddScoped<ILogService, LogService>();
        }
    }
}
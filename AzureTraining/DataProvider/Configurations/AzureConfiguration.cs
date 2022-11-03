using AzureApi.Configurations;
using System;

namespace DataProvider.Configurations
{
    public class AzureConfiguration : IAzureConfiguration
    {
        public string ConnectionString => Environment.GetEnvironmentVariable("UseDevelopmentStorage=true");

        public string BlobContainerName => Environment.GetEnvironmentVariable("azure-api-blob");

        public string AzureTableName => Environment.GetEnvironmentVariable("data");

        public string BlobFilePrefix => Environment.GetEnvironmentVariable("ApiResponse-");
    }
}
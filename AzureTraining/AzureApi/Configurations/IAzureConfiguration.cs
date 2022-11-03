namespace AzureApi.Configurations
{
    public interface IAzureConfiguration
    {
        string AzureTableName { get; }

        string BlobContainerName { get; }

        string BlobFilePrefix { get; }

        string ConnectionString { get; }
    }
}
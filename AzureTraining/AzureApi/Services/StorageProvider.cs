using Azure.Storage.Blobs;
using AzureApi.Configurations;
using AzureApi.Models;
using AzureApi.Services.Interfaces;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AzureApi.Services
{
    public class StorageProvider : ILogStorageProvider, IBlobStorageProvider
    {
        private readonly BlobContainerClient _blobContainerClient;
        private readonly CloudTable _cloudTable;
        private readonly IAzureConfiguration _azureConfig;

        public StorageProvider(IAzureConfiguration azureConfig)
        {
            _azureConfig = azureConfig;

            var client = new BlobServiceClient("UseDevelopmentStorage=true");

            try
            {
                _blobContainerClient = client.CreateBlobContainer(_azureConfig.BlobContainerName);
            }
            catch
            {
                _blobContainerClient = client.GetBlobContainerClient(_azureConfig.BlobContainerName);
            }

            var account = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
            var tableClient = account.CreateCloudTableClient(new TableClientConfiguration());

            _cloudTable = tableClient.GetTableReference(_azureConfig.AzureTableName);
            _cloudTable.CreateIfNotExists();
        }

        public string SaveBlob(Root response)
        {
            using var stream = new MemoryStream();
            using var streamWriter = new StreamWriter(stream);

            var content = JsonSerializer.Serialize(response);

            streamWriter.Write(content);
            streamWriter.Flush();
            stream.Seek(0, SeekOrigin.Begin);

            var name = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss");
            var fileName = $"{_azureConfig.BlobFilePrefix}{name}.json";

            _blobContainerClient.UploadBlob(fileName, stream);

            return name;
        }

        public async Task LogRequestAsync(Root response, string id)
        {
            var serialized = JsonSerializer.Serialize(response);
            var entry = new ApiResponseEntity(id, serialized, DateTime.Now);
            var operation = TableOperation.Insert(entry);

            await _cloudTable.ExecuteAsync(operation);
        }

        public IEnumerable<ApiResponseEntity> GetLogs(DateTime from, DateTime to)
        {
            var items = _cloudTable
                .ExecuteQuery(new TableQuery<ApiResponseEntity>())
                .Where(x => x.Timestamp >= from && x.Timestamp <= to);

            return items;
        }

        public async Task<string> GetBlobAsync(string id)
        {
            var fileName = $"data{id}.json";
            var blobClient = _blobContainerClient.GetBlobClient(fileName);

            using var stream = new MemoryStream();
            await blobClient.DownloadToAsync(stream);
            stream.Position = 0;

            using var streamReader = new StreamReader(stream);
            var response = await streamReader.ReadToEndAsync();

            return response;
        }
    }
}
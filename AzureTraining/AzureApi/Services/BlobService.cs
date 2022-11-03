using AzureApi.Models;
using AzureApi.Services.Interfaces;
using System.Threading.Tasks;

namespace AzureApi.Services
{
    public class BlobService : IBlobService
    {
        private readonly IBlobStorageProvider _storageProvider;

        public BlobService(IBlobStorageProvider provider)
        {
            _storageProvider = provider;
        }

        public Task<string> GetBlobAsync(string id)
        {
            return _storageProvider.GetBlobAsync(id);
        }

        public string SaveBlob(Root response)
        {
            return _storageProvider.SaveBlob(response);
        }
    }
}
using AzureApi.Models;
using System.Threading.Tasks;

namespace AzureApi.Services.Interfaces
{
    public interface IBlobStorageProvider
    {
        Task<string> GetBlobAsync(string id);

        string SaveBlob(Root response);
    }
}
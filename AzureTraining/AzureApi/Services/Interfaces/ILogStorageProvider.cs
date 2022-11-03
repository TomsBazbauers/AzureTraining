using AzureApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureApi.Services.Interfaces
{
    public interface ILogStorageProvider
    {
        Task<string> GetBlobAsync(string id);

        IEnumerable<ApiResponseEntity> GetLogs(DateTime from, DateTime to);

        Task LogRequestAsync(Root response, string id);
    }
}
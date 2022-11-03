using AzureApi.Models;
using AzureApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureApi.Services
{
    public class LogService : ILogService
    {
        private readonly ILogStorageProvider _logStorageProvider;

        public LogService(ILogStorageProvider provider)
        {
            _logStorageProvider = provider;
        }

        public Task LogRequestAsync(Root response, string id)
        {
            return _logStorageProvider.LogRequestAsync(response, id);
        }

        public IEnumerable<ApiResponseEntity> GetLogs(DateTime from, DateTime to)
        {
            return _logStorageProvider.GetLogs(from, to);
        }
    }
}
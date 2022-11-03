using AzureApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureApi.Services.Interfaces
{
    public interface ILogService
    {
        IEnumerable<ApiResponseEntity> GetLogs(DateTime from, DateTime to);

        Task LogRequestAsync(Root response, string id);
    }
}
using AzureApi;
using AzureApi.Models;
using AzureApi.Services.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider.Blob
{
    public class GetData
    {
        private IPublicApi _publicApi;
        private readonly IBlobService _blobService;
        private readonly ILogService _logService;

        public GetData(IPublicApi api, IBlobService blobService, ILogService logService)
        {
            _publicApi = api;
            _blobService = blobService;
            _logService = logService;
        }

        [FunctionName("ApiPull")]
        public async Task RunAsync([TimerTrigger("* */1 * * * *")] TimerInfo myTimer, ILogger log)
        {
            var result = await _publicApi.GetApi();
            var output = result.Entries.First();

            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            var id = _blobService.SaveBlob(result);

            await _logService.LogRequestAsync(result, id);
        }
    }
}
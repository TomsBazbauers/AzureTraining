using AzureApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DataProvider.Blob
{
    public class GetBlob
    {
        private readonly IBlobService _blobService;

        public GetBlob(IBlobService blobService)
        {
            _blobService = blobService;
        }

        [FunctionName("GetBlob")]
        public async Task<IActionResult> RunAsync(
            [HttpTrigger("get", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string id = req.Query["id"];
            var result = await _blobService.GetBlobAsync(id);

            return await Task.FromResult<IActionResult>(new ObjectResult(result));
        }
    }
}

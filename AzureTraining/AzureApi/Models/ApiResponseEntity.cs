using Microsoft.Azure.Cosmos.Table;
using System;

namespace AzureApi.Models
{
    public class ApiResponseEntity : TableEntity, ITableEntity
    {
        public ApiResponseEntity()
        { }

        public ApiResponseEntity(string id, string serializedResponse, DateTime executed)
        {
            RowKey = id;
            PartitionKey = id;
            Response = serializedResponse;
            Timestamp = executed;
        }

        public string Response { get; set; }
    }
}

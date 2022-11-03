using AzureApi.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureApi
{
    public interface IPublicApi
    {
        [Get("/random?auth=null")]
        Task<Root> GetApi();
    }
}
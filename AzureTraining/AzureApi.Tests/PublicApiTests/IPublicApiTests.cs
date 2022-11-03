using FluentAssertions;
using Refit;
using System.Threading.Tasks;
using Xunit;

namespace AzureApi.Tests.PublicApiTests
{
    public class IPublicApiTests
    {
        [Fact]
        public async Task GetApi_Async()
        {
            //Act
            var apiResponse = RestService.For<IPublicApi>("https://api.publicapis.org");
            var actual = await apiResponse.GetApi();

            //Assert
            actual.Should().NotBeNull();
        }
    }
}
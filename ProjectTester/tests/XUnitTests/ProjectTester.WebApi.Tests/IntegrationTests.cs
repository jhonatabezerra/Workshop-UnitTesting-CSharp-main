using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using ProjectTester.Domain.Models.Enum;
using ProjectTester.Domain.Models.Operations;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ProjectTester.WebApi.Tests
{
    public class IntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public IntegrationTests(WebApplicationFactory<Startup> factory) => _factory = factory;

        [Fact]
        public async Task GetAsync_WhenCallSwaggerPage_ShouldReturnHttp200()
        {
            // Arrange
            const string PATH = "/swagger/index.html";
            HttpClient client = _factory.CreateClient();

            // Act
            HttpResponseMessage response = await client.GetAsync(PATH);
            string context = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.NotEmpty(context);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAsync_WhenCallSwaggerPage_ShouldReturnSomeValue()
        {
            // Arrange
            Operations operationsExpected = new()
            {
                Operation = OperationTypes.BUY,
                UnitCost = 2,
                Quantity = 1,
            };
            const string PATH = "GetOperation";
            HttpClient client = _factory.CreateClient();

            // Act
            HttpResponseMessage response = await client.GetAsync(PATH);
            string context = await response.Content.ReadAsStringAsync();
            var jsonResult = JsonConvert.DeserializeObject<Operations>(context);

            // Assert
            Assert.NotEmpty(context);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(operationsExpected.Operation, jsonResult.Operation);
            Assert.Equal(operationsExpected.Quantity, jsonResult.Quantity);
            Assert.Equal(operationsExpected.UnitCost, jsonResult.UnitCost);
        }
    }
}
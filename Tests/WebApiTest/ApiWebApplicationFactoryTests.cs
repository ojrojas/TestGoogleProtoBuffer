using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Tests.WebApiTest
{
    [TestFixture]
    public class ApiWebApplicationFactoryTests
    {
        private ApiWebApplicationFactory _factory;
        private HttpClient _httpClient;
        private const string localhost = "https://localhost:5001";

        [OneTimeSetUp]
        public void CreateServerApi()
        {
            var config = new ConfigurationBuilder().Build();
            _factory = new ApiWebApplicationFactory();
            _httpClient = _factory.CreateClient();
            _factory.Server.BaseAddress = new Uri(localhost);
        }

        [Test]
        public async Task TestWhenStartingServerGrpc()
        {
            var requestPhrase = "Oscar";
            var requestPhrase2 = "Julian";
            var responseClient = await _httpClient.GetAsync($"{_factory.Server.BaseAddress}api/sayhello?request={requestPhrase}");
            var responseClient2 = await _httpClient.GetAsync($"{_factory.Server.BaseAddress}api/sayhello?request={requestPhrase2}");

            try
            {
                var response = responseClient.EnsureSuccessStatusCode();
                var response2 = responseClient2.EnsureSuccessStatusCode();
                var sayhelloresponse = await response.Content.ReadAsStringAsync();
                var sayhelloresponse2 = await response2.Content.ReadAsStringAsync();

                Console.WriteLine(sayhelloresponse);
                Assert.That(sayhelloresponse.Equals("Oscar"), Is.True);
                Assert.That(sayhelloresponse2.Equals("Jul i an"), Is.False);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
         

        

        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _factory.Dispose();
        }
    }
}

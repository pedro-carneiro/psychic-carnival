namespace ToggleApi.Tests.Integration
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Xunit;

    public class ToggleShould
    {
        private readonly HttpClient _client;

        public ToggleShould()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [Fact]
        public async Task ReturnNotFoundForUnknownId()
        {
            var response = await _client.GetAsync("/api/toggle/1012212312");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}

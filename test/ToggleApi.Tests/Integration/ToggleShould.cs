namespace ToggleApi.Tests.Integration
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using System.Net;
    using System.Net.Http;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class ToggleShould
    {
        private readonly HttpClient _client;

        public ToggleShould()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [Fact]
        public async Task GetNotFoundForUnknownId()
        {
            var response = await _client.GetAsync("/api/toggle/1012212312");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeleteNotFoundForUnknownId()
        {
            var response = await _client.DeleteAsync("/api/toggle/1012212312");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task UpdateNotFoundForUnknownId()
        {
            var toggle = new Toggle() { id = 1012212312, name = "name", defaultValue = false };

            var content = new StringContent(toJson(toggle), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync("/api/toggle/1012212312", content);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task UpdateBadRequestOnEmptyBody()
        {
            var content = new StringContent("", Encoding.UTF8, "application/json");
            var response = await _client.PutAsync("/api/toggle/13", content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateBadRequestWhenURLAndBodyIdMismatch()
        {
            var toggle = new Toggle() { id = 1012212312, name = "name", defaultValue = false };

            var content = new StringContent(toJson(toggle), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync("/api/toggle/13", content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateBadRequestOnEmptyBody()
        {
            var content = new StringContent("", Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/toggle", content);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public void CreateReturnsCreatedCodeAndResourcePayload()
        {
            var toggle = new Toggle() { name = "name", defaultValue = false };

            var content = new StringContent(toJson(toggle), Encoding.UTF8, "application/json");
            var response = _client.PostAsync("/api/toggle", content).Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var actualToggle = fromJson(responseContent);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.True(actualToggle.id > 0);
            Assert.Equal(toggle.name, actualToggle.name);
            Assert.Equal(toggle.defaultValue, actualToggle.defaultValue);
        }

        [Fact]
        public void CreatedResourceShowsInGetOperations()
        {
            var toggle = create(new Toggle() { name = "readable", defaultValue = true });

            var response = _client.GetAsync("/api/toggle/" + toggle.id).Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var actualToggle = fromJson(responseContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            assertToggle(toggle, actualToggle);

            response = _client.GetAsync("/api/toggle/").Result;
            responseContent = response.Content.ReadAsStringAsync().Result;
            var toggleList = fromJsonList(responseContent);

            actualToggle = toggleList.FirstOrDefault(t => t.id == toggle.id);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            assertToggle(toggle, actualToggle);
        }

        [Fact]
        public void DeletedResourceAbsentInGetOperations()
        {
            var toggle = create(new Toggle() { name = "deleted", defaultValue = true });

            var response = _client.DeleteAsync("/api/toggle/" + toggle.id).Result;
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            response = _client.GetAsync("/api/toggle/" + toggle.id).Result;
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            response = _client.GetAsync("/api/toggle/").Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var toggleList = fromJsonList(responseContent);

            var actualToggle = toggleList.FirstOrDefault(t => t.id == toggle.id);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Null(actualToggle);
        }

        [Fact]
        public void UpdatedResourceUpToDateInGetOperations()
        {
            var toggle = create(new Toggle() { name = "updated", defaultValue = false });

            toggle.defaultValue = true;
            var content = new StringContent(toJson(toggle), Encoding.UTF8, "application/json");
            var response = _client.PutAsync("/api/toggle/" + toggle.id, content).Result;
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            response = _client.GetAsync("/api/toggle/" + toggle.id).Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var actualToggle = fromJson(responseContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            assertToggle(toggle, actualToggle);

            response = _client.GetAsync("/api/toggle/").Result;
            responseContent = response.Content.ReadAsStringAsync().Result;
            var toggleList = fromJsonList(responseContent);

            actualToggle = toggleList.FirstOrDefault(t => t.id == toggle.id);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            assertToggle(toggle, actualToggle);
        }
        private string toJson(Toggle toggle)
        {
            return JsonConvert.SerializeObject(toggle);
        }

        private Toggle fromJson(string json)
        {
            return JsonConvert.DeserializeObject<Toggle>(json);
        }

        private IEnumerable<Toggle> fromJsonList(string jsonList)
        {
            return JsonConvert.DeserializeObject<IEnumerable<Toggle>>(jsonList);
        }

        private void assertToggle(Toggle expected, Toggle actual)
        {
            Assert.Equal(expected.id, actual.id);
            Assert.Equal(expected.name, actual.name);
            Assert.Equal(expected.defaultValue, actual.defaultValue);
        }

        private Toggle create(Toggle toggle)
        {
            var content = new StringContent(toJson(toggle), Encoding.UTF8, "application/json");
            var response = _client.PostAsync("/api/toggle", content).Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            return fromJson(responseContent);
        }

        private class Toggle
        {
            public long id { get; set; }
            public string name { get; set; }
            public bool defaultValue { get; set; }
        }
    }
}

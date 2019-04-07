using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace SimpleAction.Services.Activities.Tests.Integration.Controllers {
    public class HomeControllerTests {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        public HomeControllerTests () {
            _server = new TestServer (WebHost.CreateDefaultBuilder ().UseStartup<Startup> ());
            _client = _server.CreateClient ();
        }

        [Fact]
        public async Task home_controller_get_should_return_content () {
            var response = await _client.GetAsync("/");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            content.Should().BeEquivalentTo("Hello from Simple Action API!");
        }

    }
}
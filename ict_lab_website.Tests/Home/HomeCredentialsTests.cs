using ict_lab_website.Controllers;
using ict_lab_website.Models.Home;
using ict_lab_website.Models.ViewModels;
using ict_lab_website.Process;
using ict_lab_website.Tests.Fake_implementations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ict_lab_website.Tests.Home
{
    public class HomeCredentialsTests
    {
        private readonly ILogger<HomeController> logger;
        private readonly IOptions<ApiConfig> apiConfig;
        private readonly IApiCalls apiCalls;
        private readonly IApiCalls failingApiCalls;

        public HomeCredentialsTests()
        {
            this.logger = new FakeLogger<HomeController>();
            this.apiConfig = new FakeIOptions();
            this.apiCalls = new FakeCredentialsApiCalls();
            this.failingApiCalls = new FakeFailingApiCalls();
        }

        [Fact]
        public void LoginCheck_ShouldWorkWithWorkingApiCalls()
        {
            IHomeCredentials homeCredentials = new HomeCredentials(apiConfig, logger, apiCalls);
            CredentialsViewModel model = new CredentialsViewModel();
            Boolean expected = true;

            var stringJson = JsonConvert.SerializeObject(model);
            var rJson = JObject.Parse(stringJson);

            var actual = homeCredentials.LoginCredentials(rJson);

            var succeed = actual["Succeed"].Value<Boolean>();

            Assert.Equal(expected, succeed);
        }

        [Fact]
        public void LoginCheck_ShouldNotWorkWithWorkingWithFakeApiCalls()
        {
            IHomeCredentials homeCredentials = new HomeCredentials(apiConfig, logger, failingApiCalls);
            CredentialsViewModel model = new CredentialsViewModel();
            Boolean expected = false;

            var stringJson = JsonConvert.SerializeObject(model);
            var rJson = JObject.Parse(stringJson);

            var actual = homeCredentials.LoginCredentials(rJson);

            var succeed = actual["Succeed"].Value<Boolean>();

            Assert.Equal(expected, succeed);
        }

        [Fact]
        public void RegisterCheck_ShouldWorkWithWorkingApiCalls()
        {
            IHomeCredentials homeCredentials = new HomeCredentials(apiConfig, logger, apiCalls);
            CredentialsViewModel model = new CredentialsViewModel();
            Boolean expected = true;

            var stringJson = JsonConvert.SerializeObject(model);
            var rJson = JObject.Parse(stringJson);

            var actual = homeCredentials.RegisterCredentials(rJson);

            var succeed = actual["Succeed"].Value<Boolean>();

            Assert.Equal(expected, succeed);
        }

        [Fact]
        public void RegisterCheck_ShouldNotWorkWithWorkingWithFakeApiCalls()
        {
            IHomeCredentials homeCredentials = new HomeCredentials(apiConfig, logger, failingApiCalls);
            CredentialsViewModel model = new CredentialsViewModel();
            Boolean expected = false;

            var stringJson = JsonConvert.SerializeObject(model);
            var rJson = JObject.Parse(stringJson);

            var actual = homeCredentials.RegisterCredentials(rJson);

            var succeed = actual["Succeed"].Value<Boolean>();

            Assert.Equal(expected, succeed);
        }
    }
}

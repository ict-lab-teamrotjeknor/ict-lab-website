using ict_lab_website.Process;
using ict_lab_website.Tests.Users;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ict_lab_website.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ictlabwebsite.Controllers;
using ict_lab_website.Tests.Fake_implementations;
using ict_lab_website.Tests.Rooms;
using ict_lab_website.Models.Users;

namespace ict_lab_website.Tests.Users
{
    public class UsersTests
    {
		private readonly ILogger<UsersController> logger;
        private readonly IOptions<ApiConfig> apiConfig;
        private readonly IApiCalls failingApiCalls;
        private readonly IApiCalls usersApiCalls;

        public UsersTests()
        {
			this.apiConfig = new FakeIOptions();
			this.logger = new FakeLogger<UsersController>();
            this.failingApiCalls = new FakeFailingApiCalls();
			this.usersApiCalls = new FakeRoomsApiCalls();
        }
		[Fact]
		public void GetAllUsers_ShouldWork()
		{
			IUsers usersRepository = new UsersRepository(apiConfig, logger);
		}

		[Fact]
        public void GetAllUsers_ShouldReturnEmptyStringWithNotWorkingApiCalls()
		{
			
		}
    }
}

﻿using ict_lab_website.Process;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Xunit;
using ictlabwebsite.Controllers;
using ict_lab_website.Tests.Fake_implementations;
using ict_lab_website.Tests.Rooms;
using ict_lab_website.Models.Users;

namespace ict_lab_website.Tests.User
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
            IUsers usersRepository = new UsersRepository(apiConfig, logger, usersApiCalls);
            string users = usersRepository.GetAllUsers();

            //Check if type is a string which should be true
            Assert.IsType<string>(users);

            //Check if string is empty which should be false 
            Assert.False(users.Equals(""));
        }

        [Fact]
        public void GetAllUsers_ShouldReturnEmptyStringWithNotWorkingApiCalls()
        {

            IUsers usersRepository = new UsersRepository(apiConfig, logger, failingApiCalls);
            string users = usersRepository.GetAllUsers();

            //Check if string is empty which should be true
            Assert.True(users.Equals(""));

        }
    }
}

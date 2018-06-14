﻿using System;
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
			IUsers usersRepository = new UsersRepository(apiConfig, logger, usersApiCalls);
			string users = usersRepository.GetAllUsers();

			//Check if type is a string which should be true
			Assert.IsType<string>(users);

			//Check if string is empty which should be false 
			Assert.False(users.Equals(""));
		}

	}
}

using ict_lab_website.Controllers;
using ict_lab_website.Models;
using ict_lab_website.Models.Rooms;
using ict_lab_website.Process;
using ict_lab_website.Tests.Fake_implementations;
using ict_lab_website.Tests.Rooms;
using ict_lab_website.Tests.Schedule;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ict_lab_website.Tests.Rooms
{
    public class RoomRepositoryTests
    {
        private readonly ILogger<RoomsController> logger;
        private readonly IOptions<ApiConfig> apiConfig;
        private readonly IApiCalls failingApiCalls;
        private readonly IApiCalls roomsApiCalls;

        public RoomRepositoryTests()
        {
            this.apiConfig = new FakeIOptions();
            this.logger = new FakeLogger<RoomsController>();
            this.failingApiCalls = new FakeFailingApiCalls();
            this.roomsApiCalls = new FakeRoomsApiCalls();
        }

        [Fact]
        public void GetAll_ShouldWork()
        {
            IRepository<Room> roomRepository = new RoomRepository(apiConfig, logger, roomsApiCalls);

            List<Room> rooms = roomRepository.GetAll();

            Assert.NotEmpty(rooms);
            Assert.All(rooms, room => Assert.IsType<Room>(room));
            Assert.All(rooms, room => Assert.NotNull(room.Name));
        }

        [Fact] void GetAll_ShouldReturnEmptyListWithNotWorkingApiCalls()
        {
            IRepository<Room> roomRepository = new RoomRepository(apiConfig, logger, failingApiCalls);

            List<Room> rooms = roomRepository.GetAll();

            Assert.Empty(rooms);
        }
    }
}

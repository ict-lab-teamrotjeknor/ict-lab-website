using ict_lab_website.Controllers;
using ict_lab_website.Models.Schedule;
using ict_lab_website.Process;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ict_lab_website.Tests.Schedule
{
    public class RoomScheduleTests
    {
        private readonly ILogger<ScheduleController> logger;
        private readonly IOptions<ApiConfig> apiConfig;
        private readonly IApiCalls apiCalls;
        private readonly ISchedule roomSchedule;

        public RoomScheduleTests()
        {
            this.logger = new FakeLogger<ScheduleController>();
            this.apiConfig = new FakeIOptions();
            this.apiCalls = new FakeScheduleApiCalls();
        }

        [Theory]
        [ClassData(typeof(ScheduleTestDataGenerator))]
        public void GetDatesInSameWeek_ShouldWorkWithValidDate(DateTime dateTime)
        {
            ISchedule roomSchedule = new RoomSchedule(apiConfig, logger, apiCalls);

            List<DateTime> datesInSameWeek = roomSchedule.GetDatesInSameWeek(dateTime);

            Assert.True(datesInSameWeek.Count == 7);
            Assert.Contains<DateTime>(dateTime, datesInSameWeek);
        }

        [Theory]
        [ClassData(typeof(ScheduleTestDataGenerator))]
        public void GetWeek_ShouldWorkWithValidDates(DateTime dateTime)
        {
            ISchedule roomSchedule = new RoomSchedule(apiConfig, logger, apiCalls);
            string roomName = "H.1.308";

            var reservations = roomSchedule.GetWeek(dateTime, roomName);

            Assert.True(reservations.Count == 7);
        }

        [Theory]
        [ClassData(typeof(ScheduleTestDataGenerator))]
        public void GetDay_ShouldBeSubsetOfGetWeek(DateTime dateTime)
        {
            ISchedule roomSchedule = new RoomSchedule(apiConfig, logger, apiCalls);
            string roomName = "H.1.308";

            var reservationsForWeek = roomSchedule.GetWeek(dateTime, roomName);
            var reservationsForDay = roomSchedule.GetDay(dateTime, roomName);

            Assert.Contains(reservationsForDay, reservationsForWeek.Values);

        }

    }
}

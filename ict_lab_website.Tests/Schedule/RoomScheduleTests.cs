using ict_lab_website.Controllers;
using ict_lab_website.Models.Schedule;
using ict_lab_website.Process;
using ict_lab_website.Tests.Fake_implementations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ict_lab_website.Tests.Schedule
{
    public class RoomScheduleTests
    {
        private readonly ILogger<ScheduleController> logger;
        private readonly IOptions<ApiConfig> apiConfig;
        private readonly IApiCalls apiCalls;
        private readonly IApiCalls failingApiCalls;

        public RoomScheduleTests()
        {
            this.logger = new FakeLogger<ScheduleController>();
            this.apiConfig = new FakeIOptions();
            this.apiCalls = new FakeScheduleApiCalls();
            this.failingApiCalls = new FakeFailingApiCalls();
        }

        [Theory]
        [ClassData(typeof(ScheduleTestDataGenerator))]
        public void GetWeek_ShouldWorkWithValidDates(DateTime dateTime)
        {
            ISchedule roomSchedule = new RoomSchedule(apiConfig, logger, apiCalls);
            string roomName = "H.1.308";

            var reservations = roomSchedule.GetWeek(dateTime, roomName);
            Assert.True(reservations.Count == 7);
            Assert.All(reservations.Values, day => Assert.True(day.Values.Count == 15));
        }

        [Theory]
        [ClassData(typeof(ScheduleTestDataGenerator))]
        public void GetWeek_ShouldReturnEmptyDaysWithNotWorkingApiCalls(DateTime dateTime)
        {
            ISchedule roomSchedule = new RoomSchedule(apiConfig, logger, failingApiCalls);
            string roomName = "H.1.308";

            var reservations = roomSchedule.GetWeek(dateTime, roomName);

            Assert.All(reservations.Values, day => Assert.All(day.Values, reservation => Assert.Null(reservation)));
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

        [Fact]
        public void AddReservation_ShouldWorkWithValidReservation()
        {
            ISchedule roomSchedule = new RoomSchedule(apiConfig, logger, apiCalls);
            UploadableReservation reservation = new UploadableReservation();
            Boolean expected = true;

            Boolean actual = roomSchedule.AddReservation(reservation);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddReservation_ShouldFailWithNotWorkingApiCalls()
        {
            ISchedule roomSchedule = new RoomSchedule(apiConfig, logger, failingApiCalls);
            UploadableReservation reservation = new UploadableReservation();
            Boolean expected = false;

            Boolean actual = roomSchedule.AddReservation(reservation);

            Assert.Equal(expected, actual);
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
        public void GetNumberOfFreeTimeslots_ShouldWork(DateTime dateTime)
        {
            ISchedule roomSchedule = new RoomSchedule(apiConfig, logger, apiCalls);
            string roomName = "H.1.308";
            var reservations = roomSchedule.GetDay(dateTime, roomName);
            int expected = reservations.Where(r => r.Value == null).Count();

            int actual = roomSchedule.GetNumberOfFreeTimeslots(dateTime, roomName);

            Assert.Equal(expected, actual);
        }
    }
}

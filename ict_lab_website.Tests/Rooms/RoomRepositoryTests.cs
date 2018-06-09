using ict_lab_website.Controllers;
using ict_lab_website.Process;
using ict_lab_website.Tests.Schedule;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace ict_lab_website.Tests
{
    public class RoomRepositoryTests
    {
        private readonly ILogger<RoomsController> logger;
        private readonly IOptions<ApiConfig> apiConfig;

        public RoomRepositoryTests()
        {
            this.apiConfig = new FakeIOptions();
        }
    }
}

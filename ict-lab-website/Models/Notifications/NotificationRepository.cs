using ict_lab_website.Process;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ict_lab_website.Models.Notifications
{
    public class NotificationRepository
    {
        private readonly IApiCalls apiCalls;
        private readonly ApiConfig apiConfig;

        public NotificationRepository(IApiCalls apiCalls, IOptions<ApiConfig> apiConfig)
        {
            this.apiCalls = apiCalls;
            this.apiConfig = apiConfig.Value;
        }


    }
}

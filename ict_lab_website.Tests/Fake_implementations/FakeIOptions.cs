using ict_lab_website.Process;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace ict_lab_website.Tests.Fake_implementations
{
    class FakeIOptions : IOptions<ApiConfig>
    {
        public ApiConfig Value => new ApiConfig
        {
            Url = "http://145.24.222.103:8080",
            GetAllRooms = "/manage/getallrooms",
            GetWeek = "/schedule/getweek",
            GetUsers = "/manage/getusers",
            SignIn = "/authentication/signin",
            SignUp = "/authentication/signup",
            ChangeRole = "/authentication/changerole",
            AddRole = "/authentication/addrole",
            DeleteUser = "/manage/deleteuser",
            CheckRole = "/authentication/checkrole",
            UploadHour = "/schedule/uploadhour"
        };
    }
}

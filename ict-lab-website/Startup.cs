using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ict_lab_website.Models;
using ict_lab_website.Models.Rooms;
using ict_lab_website.Models.Schedule;
using ict_lab_website.Process;
using ict_lab_website.Models.Home;

namespace ict_lab_website
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.Configure<ApiConfig>(Configuration.GetSection("ApiConfig"));
            services.AddTransient<IRepository<Room>, RoomRepository>();
            services.AddTransient<ISchedule, RoomSchedule>();
			services.AddTransient<IHomeCredentials, HomeCredentials>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Rooms}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "schedule",
                    template:"{controller=Schedule}/{action=Index}/{roomName}/{dateTime}/{view?}");
            });
        }
    }
}

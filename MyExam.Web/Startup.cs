﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyExam.IServices;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace MyExam.Web
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
            //services.AddSession();

            //services.AddSingleton(typeof(IUserService), typeof(UserService));


            //注册服务和实现类
            Assembly asmServices = Assembly.Load("MyExam.Services");
            var serviceTypes = asmServices.GetTypes().Where(t => t.IsAbstract == false && typeof(IServiceTag).IsAssignableFrom(t));
            foreach (var serviceType in serviceTypes)
            {
                var intfTypes = serviceType.GetInterfaces()
                    .Where(t => typeof(IServiceTag).IsAssignableFrom(t));
                foreach (var intfType in intfTypes)
                {
                    services.AddSingleton(intfType, serviceType);
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
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
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

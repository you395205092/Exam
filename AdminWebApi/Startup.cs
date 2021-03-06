﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AdminWebApi.Filters;
using AdminWebApi.Others;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyExam.IServices;

namespace AdminWebApi
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
            ///session  
            services.AddSession();


            services.AddSingleton(typeof(ExceptionFilter));
            services.AddSingleton(typeof(AuthorizationFilter));


            services.AddMvc(opt =>
            {
                opt.Conventions.Insert(0, new NameSpaceVersionRoutingConvention());
                opt.Filters.Add(typeof(ExceptionFilter));
                opt.Filters.Add(typeof(ActionFilter));
                opt.Filters.Add(typeof(AuthorizationFilter));
            });

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
            }
            app.UseMvc();
            //app.usemvc(routes =>
            //{
            //    //扩展路由

            //    routes.maproute(
            //        name: "default",
            //        template: "api/v1/{controller}/{action}/{id?}",
            //        defaults: new { controller = "user", action = "you" });
            //});
            //session b必须
            app.UseSession();
        }
    }
}

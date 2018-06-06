using AdminWebApi.Others;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using MyExam.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;

namespace AdminWebApi.Filters
{
    public class AuthorizationFilter : IAuthorizationFilter
    {
        private readonly IAdminUserService _adminUserService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        private static readonly string _adminMobile;
        public AuthorizationFilter(IAdminUserService adminUserService, IHttpContextAccessor httpContextAccessor)
        {
            _adminUserService = adminUserService;
            _httpContextAccessor = httpContextAccessor;
        }

        static AuthorizationFilter()
        {
            IConfigurationBuilder configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            IConfigurationRoot configRoot = configuration.Build();
            _adminMobile = configRoot.GetSection("adminMobile").Value;
        }



        public void OnAuthorization(AuthorizationFilterContext context)
        {
            MethodInfo methodInfo = ((ControllerActionDescriptor)context.ActionDescriptor).MethodInfo;
            var attributeData = methodInfo.GetCustomAttributes<CheckPermissionsAttribute>(false);
            //判断Action是否标注CheckPermissionsAttribute
            if (!attributeData.Any())
            {
                return;
            }
            var adminId = _session.GetString("LoginId");
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AdminWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using MyExam.DTO;
using MyExam.IServices;
using static MyExam.CommonMVC.WebHelper;

namespace AdminWebApi.Controllers
{

    [Route("api/[controller]")]
    public class UserController : Controller
    {
        /// <summary>
        /// 后台用户服务对象
        /// </summary>
        private readonly IAdminUserService _adminUserService;

        public UserController(
            IAdminUserService adminUserService)
        {
            _adminUserService = adminUserService;
        }

        public IActionResult you()
        {
            return ApiResult("12333", "登陆成功。", (int)HttpStatusCode.OK);
        }
        public IActionResult Login([FromForm]LoginModel model)
        {
            AdminUserDTO adminUser = _adminUserService.GetByUserName(model.UserName);
            //判断数据是否为null
            if (adminUser == null)
            {
                return ApiResult(message: "用户名或密码错误，请重新登录！", httpStatusCode: (int)HttpStatusCode.Forbidden);
            }

            //判断用户是否为锁定状态
            if ((adminUser.LastLoginErrorDateTime - DateTime.Now)?.Minutes < 20 && adminUser.LoginErrorTimes > 5)
            {
                return ApiResult(message: "当前用户为锁定状态，不可登陆！", httpStatusCode: (int)HttpStatusCode.Unauthorized);
            }

            bool result = _adminUserService.CheckLogin(model.UserName, model.Password);
            if (result)
            {
                //重置登陆错误次数
                if (adminUser.LoginErrorTimes > 0)
                {
                    _adminUserService.ResetLoginError(adminUser.Id);
                    adminUser.LoginErrorTimes = 0;
                    adminUser.LastLoginErrorDateTime = null;
                }
                //将数据提交至redis
                //await StringSetAsync(RedisKeyPrefix.AdminUserId + adminUser.Id, JsonConvert.SerializeObject(adminUser));
                return ApiResult(adminUser.Id, "登陆成功。", (int)HttpStatusCode.OK);
            }
            else
            {
                adminUser.LoginErrorTimes += 1;
                adminUser.LastLoginErrorDateTime = DateTime.Now;
                //将数据提交至redis
                //await StringSetAsync(RedisKeyPrefix.AdminUserId + adminUser.Id, JsonConvert.SerializeObject(adminUser));

                var checkRes = _adminUserService.RecordLoginError(model.UserName);
                if (!checkRes)
                {
                    return ApiResult(message: "出错！", httpStatusCode: (int)HttpStatusCode.Unauthorized);
                }
                return ApiResult(message: "用户名或密码错误，请重新登录！", httpStatusCode: (int)HttpStatusCode.Unauthorized);
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using MyExam.CommonMVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MyExam.Web.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger _log;
        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _log = logger;
        }

        public void OnException(ExceptionContext context)
        {
            context.Exception.ToExceptionless().Submit();
            context.Result = new JsonResult(new ResultModel((int)HttpStatusCode.InternalServerError, "服务端错误！", null));
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            _log.LogError("未处理异常：", context.Exception);
        }
    }
}

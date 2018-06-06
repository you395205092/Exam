using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyExam.CommonMVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static MyExam.CommonMVC.WebHelper;

namespace AdminWebApi.Filters
{
    public class ActionFilter: IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            ModelStateDictionary modelState = context.ModelState;
            if (!modelState.IsValid)
            {
                context.Result = new JsonResult(new ResultModel((int)HttpStatusCode.Unauthorized, GetValidMsg(modelState), null));
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }
        }
    }
}

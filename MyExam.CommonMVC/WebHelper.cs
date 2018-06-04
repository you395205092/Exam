﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Net;

namespace MyExam.CommonMVC
{
    public static class WebHelper
    {
        /// <summary>
        /// JsonSerializerSettings对象
        /// </summary>
        public static JsonSerializerSettings JsonSerializerSettings { get; } = JsonSerializerSettingsProvider.CreateSerializerSettings();



        /// <summary>
        /// WebApi返回方法
        /// </summary>
        /// <param name="value">需返回的数据</param>
        /// <param name="message">提示信息</param>
        /// <param name="httpStatusCode">Http状态码</param>
        /// <param name="serializerSettings">序列化Json参数设置</param>
        /// <returns>序列化Json数据</returns>
        public static IActionResult ApiResult(object value = null, string message = null, int httpStatusCode = (int)HttpStatusCode.OK, JsonSerializerSettings serializerSettings = null)
        {
            JsonResult jsonResult;
            if (serializerSettings != null)
            {
                jsonResult = new JsonResult(new ResultModel(httpStatusCode, message, value), serializerSettings);
            }
            else
            {
                jsonResult = new JsonResult(new ResultModel(httpStatusCode, message, value));
            }
            jsonResult.StatusCode = httpStatusCode;
            return jsonResult;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace MyExam.CommonMVC
{
    /// <summary>
    /// 返回信息类
    /// </summary>
    public class ResultModel
    {
        /// <summary>
        /// 对象初始化构造函数
        /// </summary>
        /// <param name="status">Http状态码</param>
        /// <param name="message">提示信息</param>
        /// <param name="data">数据</param>
        public ResultModel(int status, string message, object data)
        {
            Status = status;
            Message = message;
            Data = data;
        }

        public ResultModel()
        {

        }

        /// <summary>
        /// Http状态码
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }
    }
}

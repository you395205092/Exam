using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminWebApi.Others
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class CheckPermissionsAttribute : Attribute
    {
        /// <summary>
        /// 权限检测构造函数
        /// </summary>
        /// <param name="permissionName">权限名称</param>
        public CheckPermissionsAttribute(string permissionName)
        {
            PermissionName = permissionName;
        }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string PermissionName { get; set; }
    }
}

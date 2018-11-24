using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Siteo.WebAPI.Attributes.Permissions
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class PermissionAttribute : Attribute
    {
        private string[] _permissionList;

        public PermissionAttribute(params string[] permissionList)
        {
            _permissionList = permissionList;
        }
        /// <summary>
        /// 资源名称
        /// </summary>
        public string[] PermissionList
        {
            get { return _permissionList; }
        }

        /// <summary>
        /// 资源描述
        /// </summary>
        public string Descript { get; set; }
    }
}
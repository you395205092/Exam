﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminWebApi.Models
{
    public class EditModel
    {
        public long Id { get; set; }


        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "密码为必填项！")]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "密码长度不正确！")]
        public string Password { get; set; }
    }
}

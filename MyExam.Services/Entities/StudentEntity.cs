using System;
using System.Collections.Generic;
using System.Text;

namespace MyExam.Services.Entities
{
    public class StudentEntity:BaseEntity
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public int LoginErrorTimes { get; set; }
        public DateTime? LastLoginErrorDateTime { get; set; }

    }
}

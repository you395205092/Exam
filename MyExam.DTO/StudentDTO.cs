using System;
using System.Collections.Generic;
using System.Text;

namespace MyExam.DTO
{
    public class StudentDTO:BaseDTO
    {
        public string UserName { get; set; }
        public int LoginErrorTimes { get; set; }
        public DateTime? LastLoginErrorDateTime { get; set; }
    }
}

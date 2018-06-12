using System;
using System.Collections.Generic;
using System.Text;

namespace MyExam.DTO
{
    public class SubjectDTO:BaseDTO
    {
        public string Name { get; set; }
        public int TypeClass { get; set; }
        public string ItemA { get; set; }
        public string ItemB { get; set; }
        public string ItemC { get; set; }
        public string ItemD { get; set; }
        public string ItemE { get; set; }
        public string ItemF { get; set; }
        public string Answer { get; set; }
    }
}

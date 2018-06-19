using System;
using System.Collections.Generic;
using System.Text;

namespace MyExam.DTO
{
    public class StuPaperShowDTO:BaseDTO
    {
        public long PaperId { get; set; }
        public SubjectDTO Subjects { get; set; }
        public long StuId { get; set; }
        public string StuName { get; set; }
        public string Answer { get; set; }
        public int Score { get; set; }
    }
}

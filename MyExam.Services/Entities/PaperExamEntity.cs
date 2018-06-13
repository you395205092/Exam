using System;
using System.Collections.Generic;
using System.Text;

namespace MyExam.Services.Entities
{
    public class PaperExamEntity:BaseEntity
    {
        public string Name { get; set; }
        public int Total { get; set; }
        public string PCount { get; set; }
        public string PScore { get; set; }
        public DateTime BTime { get; set; }
        public DateTime ETime { get; set; }


    }
}

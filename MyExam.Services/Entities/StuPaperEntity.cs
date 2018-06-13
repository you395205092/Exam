using System;
using System.Collections.Generic;
using System.Text;

namespace MyExam.Services.Entities
{
    public class StuPaperEntity : BaseEntity
    {
        public long StuId { get; set; }
        public long PaperId { get; set; }
        ///[{1,2,3},{3,4,5}]
        public string Question { get; set; }
        ///[{1,2,3},{3,4,5}]
        public string Answer { get; set; }
        /// <summary>
        /// {1,2,3}
        /// </summary>
        public string Score { get; set; }
    }
}

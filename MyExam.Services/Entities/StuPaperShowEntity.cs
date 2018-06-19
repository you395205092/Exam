using System;
using System.Collections.Generic;
using System.Text;

namespace MyExam.Services.Entities
{
    public class StuPaperShowEntity:BaseEntity
    {
        public virtual PaperExamEntity Papers { get; set; }
        public long PaperId { get; set; }
        public long SubjectId { get; set; }
        public long StuId { get; set; }
        public string Answer { get; set; }
        public int Score { get; set; }

    }
}

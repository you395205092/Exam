using System;
using System.Collections.Generic;
using System.Text;

namespace MyExam.Services.Entities
{
    public class SubjectEntity : BaseEntity
    {
        public string Name { get; set; }
        /// <summary>
        /// 0 单选题  1判断题  2多选题
        /// </summary>
        public int TypeId { get; set; }
        public string ItemA { get; set; }
        public string ItemB { get; set; }
        public string ItemC { get; set; }
        public string ItemD { get; set; }
        public string ItemE { get; set; }
        public string ItemF { get; set; }
        public string Answer { get; set; }

    }
}

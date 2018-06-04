using System;

namespace MyExam.DTO
{
    public abstract class BaseDTO
    {
        public long Id { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}

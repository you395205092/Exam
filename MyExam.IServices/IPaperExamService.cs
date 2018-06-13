using MyExam.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyExam.IServices
{
    public interface IPaperExamService:IServiceTag
    {
        long Add(PaperExamDTO dto);
        long Edit(PaperExamDTO dto);
        void Delete(long Id);
        PaperExamDTO[] GetAll();
        PaperExamDTO GetById(long Id);
    }
}

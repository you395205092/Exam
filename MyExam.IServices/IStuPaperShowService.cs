using MyExam.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyExam.IServices
{
    public interface IStuPaperShowService:IServiceTag
    {
        void Add(long PaperId,long SubjectId,long StuId,string Answer,int Score);
        void Delete(long PaperId,long StuId);
        StuPaperShowDTO[] GetAll(long PaperId,long StuId);
    }
}

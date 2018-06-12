using MyExam.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyExam.IServices
{
    public interface ISubjectService: IServiceTag
    {
        long Add(SubjectAddModel model);
        long Edit(SubjectEditModel model);
        bool IsExist(string Name);
        bool IsExist(long Id, string Name);
        SubjectDTO[] GetAll();
        SubjectDTO[] GetByTypeID(int TypeId);
        SubjectDTO GetById(long Id);

    }
}

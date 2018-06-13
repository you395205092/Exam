using MyExam.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyExam.IServices
{
    public interface IStudentService: IServiceTag
    {
        void Add(string userName, string password);
        bool Edit(long id, string password);
        bool CheckLogin(string userName, string password);
        bool CheckName(string userName);
        StudentDTO[] GetAll();
        StudentDTO GetByUserId(long Id);
        StudentDTO GetByUserName(string userName);

        void ResetLoginError(long id);
        bool RecordLoginError(string userName);
    }
}

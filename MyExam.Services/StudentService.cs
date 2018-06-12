using MyExam.DTO;
using MyExam.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyExam.Services
{
    public class StudentService : IStudentService
    {
        public void Add(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public bool CheckLogin(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public bool CheckName(string userName)
        {
            throw new NotImplementedException();
        }

        public bool Edit(long id, string password)
        {
            throw new NotImplementedException();
        }

        public StudentDTO[] GetAll()
        {
            throw new NotImplementedException();
        }

        public StudentDTO GetByUserName(string userName)
        {
            throw new NotImplementedException();
        }

        public bool RecordLoginError(string userName)
        {
            throw new NotImplementedException();
        }

        public void ResetLoginError(long id)
        {
            throw new NotImplementedException();
        }
    }
}

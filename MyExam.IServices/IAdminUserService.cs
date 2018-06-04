using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyExam.DTO;

namespace MyExam.IServices
{
    public interface IAdminUserService:IServiceTag
    {
        void AddUser(string userName, string password);
        bool Edit(long id, string password);
        bool CheckLogin(string userName, string password);
        bool CheckName(string userName);
        AdminUserDTO[] GetAll();
        AdminUserDTO GetByUserName(string userName);
        void ResetLoginError(long id);
        bool RecordLoginError(string userName);

        Task<(bool result, string msg)> RecordLoginErrorAsync(string userName);
    }
}

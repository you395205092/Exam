using Microsoft.EntityFrameworkCore;
using MyExam.Common;
using MyExam.DTO;
using MyExam.IServices;
using MyExam.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExam.Services
{
    public class AdminUserService :IAdminUserService
    {

        public void Add(string userName, string password)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                if (ctx.AdminUsers.Any(u => u.UserName == userName))
                {
                    throw new ApplicationException("账号已经存在");
                }
                AdminUserEntity adminUser = new AdminUserEntity();
                var salt = CommonHelper.CreateVerifyPassWord(5);
                adminUser.UserName = userName;
                adminUser.PasswordSalt = salt;
                adminUser.PasswordHash = CommonHelper.CalcMD5(salt + password);

                adminUser.IsDeleted = false;
                ctx.AdminUsers.Add(adminUser);
                ctx.SaveChanges();
            }
        }

        public bool CheckLogin(string userName, string password)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                var bs =new  BaseService<AdminUserEntity>(ctx);
                var data = bs.GetAll().FirstOrDefault(e => e.UserName == userName);
                if (data==null)
                {
                    return false;
                }
                var salt = data.PasswordSalt;
                return CommonHelper.CalcMD5(salt + password) == data.PasswordHash;
               
            }
        }
        public bool CheckName(string userName)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                var bs = new BaseService<AdminUserEntity>(ctx);
                return bs.GetAll().Any(e => e.UserName == userName);
            }
            
        }

        public bool Edit(long id, string password)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                var bs = new BaseService<AdminUserEntity>(ctx);
                var data= bs.GetById(id);
                var salt = data.PasswordSalt;
                data.PasswordHash = CommonHelper.CalcMD5(salt + password);
                return (ctx.SaveChanges() > 0) ? true : false;
            }
        }

        public AdminUserDTO[] GetAll()
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                var bs = new BaseService<AdminUserEntity>(ctx);
                var data = bs.GetAll();
                if (data==null)
                {
                    return null;
                }
                return data.Select(e => ToDTO(e)).ToArray();
            }
        }

        public AdminUserDTO GetByUserName(string userName)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                var bs = new BaseService<AdminUserEntity>(ctx);
                var data = bs.GetAll().Where(e => e.UserName == userName).FirstOrDefault();
                return ToDTO(data);
            }
        }

        public bool RecordLoginError(string userName)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                var bs = new BaseService<AdminUserEntity>(ctx);
                var data= bs.GetAll().SingleOrDefault(e => e.UserName == userName);
                if (data ==null)
                {
                    return false;
                }

                data.LastLoginErrorDateTime = DateTime.Now;
                data.LoginErrorTimes += 1;
                //ctx.Entry(adminUser).State = EntityState.Modified;
                ctx.SaveChanges();
                return true;
            }
        }

        public async Task<(bool result, string msg)> RecordLoginErrorAsync(string userName)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                var bs = new BaseService<AdminUserEntity>(ctx);
                var data = await bs.GetAll().SingleOrDefaultAsync(e => e.UserName == userName);
                data.LastLoginErrorDateTime = DateTime.Now;
                data.LoginErrorTimes += 1;
                ctx.Entry(data).State = EntityState.Modified;
                await ctx.SaveChangesAsync();
                return (true, null);

            }
        }

        public void ResetLoginError(long id)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                var bs = new BaseService<AdminUserEntity>(ctx);
                var data = bs.GetById(id);
                data.LoginErrorTimes = 0;
                ctx.SaveChanges();
            }
        }

        private AdminUserDTO ToDTO(AdminUserEntity ef)
        {
            AdminUserDTO dto = new AdminUserDTO();
            dto.CreateDateTime = ef.CreateDateTime;
            dto.Id = ef.Id;
            dto.LastLoginErrorDateTime = ef.LastLoginErrorDateTime;
            dto.LoginErrorTimes = ef.LoginErrorTimes;
            dto.UserName = ef.UserName;
            return dto;

        }
    }
}

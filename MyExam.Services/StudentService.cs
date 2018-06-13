using MyExam.Common;
using MyExam.DTO;
using MyExam.IServices;
using MyExam.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyExam.Services
{
    public class StudentService : IStudentService
    {
        public void Add(string userName, string password)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                StudentEntity ef = new StudentEntity();
                ef.UserName = userName;
                ef.PasswordHash = password;
                ctx.Students.Add(ef);
                ctx.SaveChanges();
            }
        }

        public bool CheckLogin(string userName, string password)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                BaseService<StudentEntity> bs = new BaseService<StudentEntity>(ctx);
                var data=bs.GetAll().SingleOrDefault(e => e.UserName == userName);
                if (data==null)
                {
                    return false;
                }

                string salt = data.PasswordSalt;
                string hash=CommonHelper.CalcMD5(salt+password);

                return hash == data.PasswordHash;

            }
        }

        public bool CheckName(string userName)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<StudentEntity> bs = new BaseService<StudentEntity>(ctx);
                return bs.GetAll().Any(e => e.UserName == userName);
            }
        }

        public bool Edit(long id, string password)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<StudentEntity> bs = new BaseService<StudentEntity>(ctx);
                var data = bs.GetById(id);
                if (data==null)
                {
                    return false;
                    throw new ArgumentNullException("找不到id为"+id);
                    
                }
                string salt = data.PasswordSalt;
                data.PasswordHash = CommonHelper.CalcMD5(salt + password);
                return true;
            }
        }

        public StudentDTO[] GetAll()
        {
            using (MyDbContext ctx= new MyDbContext())
            {
                BaseService<StudentEntity> bs = new BaseService<StudentEntity>(ctx);
                var data=bs.GetAll();
                if (data==null)
                {
                    return null;
                }
                return data.Select(e => ToDTO(e)).ToArray();

            }
        }

        public StudentDTO ToDTO(StudentEntity ef)
        {
            StudentDTO dto = new StudentDTO();
            dto.CreateDateTime = ef.CreateDateTime;
            dto.Id = ef.Id;
            dto.LastLoginErrorDateTime = ef.LastLoginErrorDateTime;
            dto.LoginErrorTimes = ef.LoginErrorTimes;
            dto.UserName = ef.UserName;
            return dto;
        }

        public StudentDTO GetByUserId(long Id)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                BaseService<StudentEntity> bs = new BaseService<StudentEntity>(ctx);
                var data = bs.GetById(Id);
                if (data==null)
                {
                    return null;
                }
                return ToDTO(data);
            }
        }

        public bool RecordLoginError(string userName)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                var bs = new BaseService<SubjectEntity>(ctx);
                var data = GetByUserName(userName);
                if (data == null)
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

        public void ResetLoginError(long id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                var bs = new BaseService<StudentEntity>(ctx);
                var data = bs.GetById(id);
                if (data==null)
                {
                    throw new ArgumentNullException("找不到id" + data.Id);
                }
                data.LoginErrorTimes = 0;
                ctx.SaveChanges();
            }
        }

        public StudentDTO GetByUserName(string userName)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<StudentEntity> bs = new BaseService<StudentEntity>(ctx);
                var data = bs.GetAll().SingleOrDefault(e=>e.UserName==userName);
                if (data == null)
                {
                    return null;
                }
                return ToDTO(data);
            }
        }
    }
}

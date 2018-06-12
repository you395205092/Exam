using MyExam.DTO;
using MyExam.IServices;
using MyExam.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyExam.Services
{
    public class SubjectService : ISubjectService
    {
        public long Add(SubjectAddModel model)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                if (IsExist(model.Name))
                {
                    throw new ApplicationException("题目已经存在！");
                }
                SubjectEntity ef = new SubjectEntity();
                ef.Name = model.Name;
                ef.ItemA = model.ItemA;
                ef.ItemB = model.ItemB;
                ef.ItemC = model.ItemC;
                ef.ItemD = model.ItemD;
                ef.ItemE = model.ItemE;
                ef.ItemF = model.ItemF;
                ef.Answer = model.Answer;
                ctx.Subjects.Add(ef);
                return ef.Id;
            }
        }

        public long Edit(SubjectEditModel model)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                if (IsExist(model.Id,model.Name))
                {
                    throw new ApplicationException("题目已经存在！");
                }
                BaseService<SubjectEntity> bs = new BaseService<SubjectEntity>(ctx);
                var data= bs.GetById(model.Id);
                if (data ==null)
                {
                    throw new ApplicationException("不存在id为"+model.Id+"的数据");
                }
                data.Name = model.Name;
                data.ItemA = model.ItemA;
                data.ItemB = model.ItemB;
                data.ItemC = model.ItemC;
                data.ItemD = model.ItemD;
                data.ItemE = model.ItemE;
                data.ItemF = model.ItemF;
                ctx.SaveChanges();
                return data.Id;
            }
        }

        public SubjectDTO ToDTO(SubjectEntity ef)
        {
            SubjectDTO dto = new SubjectDTO();
            dto.Answer = ef.Answer;
            dto.CreateDateTime = ef.CreateDateTime;
            dto.Id = ef.Id;
            dto.ItemA = ef.ItemA;
            dto.ItemB = ef.ItemB;
            dto.ItemC = ef.ItemC;
            dto.ItemD = ef.ItemD;
            dto.ItemE = ef.ItemE;
            dto.ItemF = ef.ItemF;
            return dto;
        }
        public SubjectDTO[] GetAll()
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                BaseService<SubjectEntity> bs = new BaseService<SubjectEntity>(ctx);
                var data = bs.GetAll();
                if (data==null)
                {
                    return null;
                }
                return data.Select(e => ToDTO(e)).ToArray();

            }
        }

        public SubjectDTO GetById(long Id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<SubjectEntity> bs = new BaseService<SubjectEntity>(ctx);
                var data = bs.GetById(Id);
                if (data == null)
                {
                    return null;
                }
                return ToDTO(data);

            }
        }

        public SubjectDTO[] GetByTypeID(int TypeId)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<SubjectEntity> bs = new BaseService<SubjectEntity>(ctx);
                var data = bs.GetAll().Where(e=>e.TypeId == TypeId);
                if (data == null)
                {
                    return null;
                }
                return data.Select(e => ToDTO(e)).ToArray();

            }
        }

        public bool IsExist(string Name)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                BaseService<SubjectEntity> bs = new BaseService<SubjectEntity>(ctx);
                
                return bs.GetAll().Any(e => e.Name == Name);
            }
        }

        public bool IsExist(long Id, string Name)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<SubjectEntity> bs = new BaseService<SubjectEntity>(ctx);

                return bs.GetAll().Any(e => e.Name == Name && e.Id!=Id);
            }
        }
    }
}

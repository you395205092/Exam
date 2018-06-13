using MyExam.DTO;
using MyExam.IServices;
using MyExam.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyExam.Services
{
    public class PaperExamService : IPaperExamService
    {
        public long Add(PaperExamDTO dto)
        {
            using (MyDbContext ctx= new MyDbContext())
            {
                PaperExamEntity ef = new PaperExamEntity();
                ef.Name = dto.Name;
                ef.PCount = dto.PCount;
                ef.PScore = dto.PScore;
                ef.Total = dto.Total;
                ctx.PaperExams.Add(ef);
                ctx.SaveChanges();
                return ef.Id;

            }
        }

        public void Delete(long Id)
        {
            using (MyDbContext ctx=new MyDbContext())
            {
                BaseService<PaperExamEntity> bs = new BaseService<PaperExamEntity>(ctx);
                bs.MarkDeleted(Id);
            }
        }

        public long Edit(PaperExamDTO dto)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<PaperExamEntity> bs = new BaseService<PaperExamEntity>(ctx);
                var data=bs.GetById(dto.Id);
                if (data==null)
                {
                    throw new ArgumentNullException("找不到id为"+dto.Id);
                }
                data.BTime = dto.BTime;
                data.ETime = dto.ETime;
                data.Name = dto.Name;
                data.PCount = dto.PCount;
                data.PScore = dto.PScore;
                data.Total = dto.Total;
                return data.Id;

            }
        }

        public PaperExamDTO ToDTO(PaperExamEntity ef)
        {
            PaperExamDTO dto = new PaperExamDTO();
            dto.BTime = ef.BTime;
            dto.CreateDateTime = ef.CreateDateTime;
            dto.ETime = ef.ETime;
            dto.Id = ef.Id;
            dto.Name = ef.Name;
            dto.PCount = ef.PCount;
            dto.PScore = ef.PScore;
            dto.Total = ef.Total;
            return dto;
        }

        public PaperExamDTO[] GetAll()
        {
            using (MyDbContext ctx= new MyDbContext())
            {
                BaseService<PaperExamEntity> bs = new BaseService<PaperExamEntity>(ctx);
                var data=bs.GetAll();
                if (data==null)
                {
                    return null;
                }
                return data.Select(e => ToDTO(e)).ToArray();
            }
        }

        public PaperExamDTO GetById(long Id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<PaperExamEntity> bs = new BaseService<PaperExamEntity>(ctx);
                var data = bs.GetById(Id);
                if (data == null)
                {
                    return null;
                }
                return ToDTO(data);
            }
        }
    }
}

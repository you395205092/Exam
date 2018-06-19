
using MyExam.DTO;
using MyExam.IServices;
using MyExam.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyExam.Services
{
  public class StuPaperShowService : IStuPaperShowService
    {
        private readonly ISubjectService _subjectService;
        private readonly IStudentService _studentService;

        public StuPaperShowService(
            ISubjectService subjectService, IStudentService studentService)
        {
            _subjectService = subjectService;
            _studentService = studentService;
        }


        public void Add(long PaperId, long SubjectId, long StuId, string Answer, int Score)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                StuPaperShowEntity ef = new StuPaperShowEntity();
                ef.Answer = Answer;
                ef.PaperId = PaperId;
                ef.Score = Score;
                ef.StuId = StuId;
                ef.SubjectId = SubjectId;
                ctx.StuPaperShows.Add(ef);
                ctx.SaveChanges();
                if (ef.Id < 0)
                {
                    throw new Exception("添加失败");
                }

            }
        }

        public void Delete(long PaperId, long StuId)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<StuPaperShowEntity> bs = new BaseService<StuPaperShowEntity>(ctx);
                var data = bs.GetAll().Where(e => e.PaperId == PaperId && e.StuId == StuId);

                foreach (var item in data)
                {
                    ctx.StuPaperShows.Remove(item);
                }
                ctx.SaveChanges();
            }
        }

        public StuPaperShowDTO[] GetAll(long PaperId, long StuId)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                BaseService<StuPaperShowEntity> bs = new BaseService<StuPaperShowEntity>(ctx);
                var data = bs.GetAll().Where(e => e.PaperId == PaperId && e.StuId == StuId);
                if (data ==null)
                {
                    return null;
                }
                else
                {
                    return data.Select(e => ToDTO(e)).ToArray();
                }
            }
        }

        public StuPaperShowDTO ToDTO(StuPaperShowEntity ef)
        {
            StuPaperShowDTO dto = new StuPaperShowDTO();
            dto.Id = ef.Id;
            dto.PaperId = ef.PaperId;
            dto.Score = ef.Score;
            dto.StuId = ef.StuId;

            var studentdata = _studentService.GetByUserId(ef.StuId);
            if (studentdata != null)
            {
                dto.StuName = studentdata.UserName;
            }
            var subjectdata = _subjectService.GetById(ef.SubjectId);
            if (subjectdata==null)
            {
                dto.Subjects = subjectdata;
            }
            return dto;
        }
    }
}

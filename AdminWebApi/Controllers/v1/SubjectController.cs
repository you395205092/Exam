using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyExam.CommonMVC;
using MyExam.DTO;
using MyExam.IServices;
using static MyExam.Common.CommonHelper;
using static MyExam.CommonMVC.WebHelper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdminWebApi.Controllers.v1
{
    [Produces("application/json")]
    public class SubjectController : Controller
    {
        private readonly  ISubjectService _subjectService; 

        public SubjectController(
            ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [AcceptVerbs(HttpMethodType.Get, Route = "v1/Subject/" + nameof(Add))]
        public IActionResult Add(SubjectAddModel model)
        {
            var data=_subjectService.Add(model);
            if (data>0)
            {
                return ApiResult(message: "添加成功！", httpStatusCode: (int)HttpStatusCode.OK);
            }
            else
            {
                return ApiResult(message: "添加失败！", httpStatusCode: (int)HttpStatusCode.Forbidden);
            }
        }
        [AcceptVerbs(HttpMethodType.Get, Route = "v1/Subject/" + nameof(GetById))]
        public IActionResult GetById(long Id)
        {
            var data = _subjectService.GetById(Id);
            return ApiResult(data, httpStatusCode: (int)HttpStatusCode.OK);
        }

        [AcceptVerbs(HttpMethodType.Get, Route = "v1/Subject/" + nameof(List))]
        public IActionResult List()
        {
            var data = _subjectService.GetAll();
            return ApiResult(data, httpStatusCode: (int)HttpStatusCode.OK);
        }

        [AcceptVerbs(HttpMethodType.Get, Route = "v1/Subject/" + nameof(List))]
        public IActionResult List(int TypeId)
        {
            var data = _subjectService.GetByTypeID(TypeId);
            return ApiResult(data, httpStatusCode: (int)HttpStatusCode.OK);
        }
    }
}

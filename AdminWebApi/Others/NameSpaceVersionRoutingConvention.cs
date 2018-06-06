using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdminWebApi.Others
{
    public class NameSpaceVersionRoutingConvention : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            IList<ControllerModel> controllers = application.Controllers;
            foreach (var controller in controllers)
            {
                //判断Controller上是否标注了[Route]
                bool hasRouteAttribute = controller.Selectors.Any(s => s.AttributeRouteModel != null);
                //如果标注了，则不由我处理
                if (hasRouteAttribute)
                {
                    continue;
                }

                //判断Controller的Namespace是否包含version
                Match matchVer = Regex.Match(controller.ControllerType.Namespace, @".v(\d+)");
                if (!matchVer.Success)
                {
                    continue;
                }
                string verNum = matchVer.Groups[1].Value;

                //计算这个Controller对应的路由路径
                string template = "v" + verNum + "/" + controller.ControllerName;
                foreach (ActionModel action in controller.Actions)
                {
                    controller.Selectors.Add(new SelectorModel
                    {
                        AttributeRouteModel = new AttributeRouteModel
                        {
                            Template = template + "/" + action.ActionName
                        }
                    });
                }
            }
        }
    }
}

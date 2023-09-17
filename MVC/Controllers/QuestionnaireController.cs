
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


using DAL;
using System.Collections.Generic;
using BLL;
using System.Text;
using DAL.Core;
using Core;
using DevExtreme.AspNet.Data;

namespace MVC.Controllers
{
    public class QuestionnaireController : BaseController
    {
        public ActionResult Index( string mode)
        {
            ViewBag.sessionId = base.Request.HttpContext.Connection.Id;
            ViewBag.Id = uow.GetRepository<Questionnaire>().FirstOrDefault(x => x.Main == true)?.Id;
            if (string.IsNullOrWhiteSpace(mode))
            {
                mode = "work";
            }
            ViewBag.Mode = mode;
            //if (id == null)
            //{
            //    ViewBag.Mode = "edit";
            //}
            return View("Index");
        }



        public IActionResult Get(int  Id)
        {
            var model = new QuestionnaireModel(Id);
            string json = base.GetJsonSafe(() => model.Get());
            return Content(json, "application/json");
        }




        public ActionResult SaveAnsvers(int? questionnaireId, string? sessionId)
        {
            var model = new QuestionnaireModel(questionnaireId);


			string json = base.GetJsonSafe(() => model.SaveAnsvers(base.Body(), GetCurrentUser(), sessionId));
            return Content("{}", "application/json");
        }


        private UserDTO GetCurrentUser()
        {
            return new UserDTO();
        }
    }
}


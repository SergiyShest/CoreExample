
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


using DAL;
using System.Collections.Generic;
using BLL;
using System.Text;
using DAL.Core;

namespace MVC.Controllers
{



    public class QuestionnaireController : BaseController
    {
        public ActionResult Index(int? id, string mode)
        {
            ViewBag.sessionId = base.Request.HttpContext.Connection.Id;
            ViewBag.Id = id;
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

        public ActionResult SaveAnsvers(int? questionnaireId, string? sessionId)
        {
            var model = new QuestionnaireModel(questionnaireId);


			string json = base.GetJsonSafe(() => model.SaveAnsvers(base.Body(), GetCurrentUser(), sessionId));
            return Content("", "application/json");
        }

        public ActionResult UploadFile(int? questionnaireId, string questionnaireName)
        {

            var file_ = "Request.Files[0]";

            var errors = new List<ObjectError>();
            try
            {
                //if (file_.ContentLength > 0)
                //{
                //    string _FileName = Path.GetFileName(file_.FileName);
                //    string _path = Path.Combine(Path.GetTempPath(), _FileName);
                //    file_.SaveAs(_path);

                //    if (questionnaireId == null)
                //    {
                //        var model = new Questionnaire(questionnaireId);
                //        model.Name = questionnaireName;
                //        model.Save(GetCurrentUser());
                //        questionnaireId = model.Id;
                //    }


                //    ExcelLoader loader = new ExcelLoader();
                //    var questionniare = loader.AddQuestions(_path, (int)questionnaireId);
                //    foreach (var error in loader.Errors)
                //    {
                //        errors.Add(new ObjectError("", error));
                //    }
                //    questionniare.Save(GetCurrentUser());

                //}


            }
            catch (Exception ex)
            {
                errors.Add(new ObjectError("", ex.GetAllMessages()));
            }

            var json = JsonConvert.SerializeObject(new { Errors = errors });
            return Content(json, "application/json");
        }

        private UserDTO GetCurrentUser()
        {
            return new UserDTO();
        }
    }
}


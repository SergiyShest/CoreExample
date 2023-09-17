
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


using DAL;
using System.Collections.Generic;
using BLL;
using DAL.Core;

namespace MVC.Controllers
{



    public class QuestionnaireEditController : BaseController
    {
        public ActionResult Index(int? id, int? patientId, string mode)
        {
            ViewBag.PatientId = patientId;
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

        public ActionResult GetQuestions(int? id)
        {
            var model = new QuestionnaireModel(id);
            string json = base.GetJsonSafe(() => model.Get());
            json = json.Replace("\"x_", "\"x-");

            return Content(json, "application/json");
        }

        public ActionResult GetAnsvers(int? questionnaireId, int? patientId)
        {

            var model = new QuestionnaireModel(questionnaireId);

            string json = base.GetJsonSafe(() => model.GetAnsvers(patientId));
            return Content(json, "application/json");
        }

        public ActionResult SaveQuestion(int? questionnaireId)
        {
            var model = new QuestionnaireModel(questionnaireId);
            var bodyStream = new StreamReader(HttpContext.Request.Body);
            bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
            var bodyText = bodyStream.ReadToEnd();
            var errors = model.SaveQuestion( uow ,bodyText, GetCurrentUser());


            var json = JsonConvert.SerializeObject(new { Errors = "errors" });
            return Content(json, "application/json");
        }

        public ActionResult SaveQuestionnaire(int? questionnaireId)
        {
            var model = new QuestionnaireModel(questionnaireId);
            var bodyStream = new StreamReader(HttpContext.Request.Body);
            bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
            var bodyText = bodyStream.ReadToEnd();
            var errors = model.SaveQuestionnaire(uow ,bodyText, GetCurrentUser());

            var json = JsonConvert.SerializeObject(new { Errors = "errors" });
            return Content(json, "application/json");
        }

        public ActionResult DeleteQuestion(int? questionnaireId, int? questionId)
        {
            var model = new QuestionnaireModel(questionnaireId);


            string json = base.GetJsonSafe(() => model.DeleteQuestion(questionId));

            return Content(json, "application/json");
        }

        public ActionResult SaveAnsvers(int? questionnaireId, int? patientId)
        {
            var model = new QuestionnaireModel(questionnaireId);
            var bodyStream = new StreamReader(HttpContext.Request.Body);
            bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
            var bodyText = bodyStream.ReadToEnd();
            string json = base.GetJsonSafe(() => model.SaveAnsvers(bodyText, GetCurrentUser(), patientId));
            return Content("json", "application/json");
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


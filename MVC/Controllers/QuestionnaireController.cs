
using BLL;
using Core;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    
    public class QuestionnaireController : BaseController
    {
        public QuestionnaireController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) { }

        public ActionResult Index(int? id)
        {
            ViewBag.sessionId = base.Request.HttpContext.Connection.Id;
            var qs = uow.GetRepository<Questionnaire>().GetAll();

            if (id != null) {
                   ViewBag.Id = qs.FirstOrDefault(x => x.Id == id && x.Enabled==true)?.Id;
                if (ViewBag.Id == null)
                {
                    //var error = "Not ";
                }
            }
            else { 
                  ViewBag.Id = qs.FirstOrDefault(x => x.Main == true)?.Id;
           }
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


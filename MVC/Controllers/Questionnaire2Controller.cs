
using BLL;
using Core;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Entity.Controllers
{
    
    public class Questionnaire2Controller : BaseController
    {
        public Questionnaire2Controller(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) { }


        public ActionResult Index(int? id)
        {
            ViewBag.sessionId = base.Request.HttpContext.Connection.Id;
			ViewBag.Lang = "";
			// var qs = uow.GetRepository<Questionnaire>().GetAll();

			// if (id != null) {
			//        ViewBag.Id = qs.FirstOrDefault(x => x.Id == id && x.Enabled==true)?.Id;
			//     if (ViewBag.Id == null)
			//     {
			//         //var error = "Not ";
			//     }
			// }
			// else { 
			//       ViewBag.Id = qs.FirstOrDefault(x => x.Main == true)?.Id;
			//}
			return View("Index");
        }
        [Route("en")]
        public ActionResult En(int? id)
        {
            ViewBag.sessionId = base.Request.HttpContext.Connection.Id;
            ViewBag.Lang = "en";
            return View("Index");
        }
		[Route("es")]
		public ActionResult Es(int? id)
        {
            ViewBag.sessionId = base.Request.HttpContext.Connection.Id;
			ViewBag.Lang = "es";
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

            if (sessionId != null)
            {
                var model = new QuestionnaireModel(questionnaireId);


                string json = base.GetJsonSafe(() => model.SaveAnsvers(base.Body(), GetCurrentUser(), sessionId));
            }
            return Content("{}", "application/json");
        }

        public ActionResult SaveUserCounter( string? sessionId)
        {
            var rep = uow.GetRepository<HitCounter>();

			var exists = rep.FirstOrDefault(x => x.SessionId == sessionId);
            if(exists == null)
            {
           
			string json = base.GetJsonSafe( () =>
                {
                    var answer= JsonConvert.DeserializeObject<HitCounter>(base.Body());
                    answer.Cdate = DateTime.Now;
                    rep.Create(answer); 
                }
                );

            }




            return Content("{}", "application/json");
        }

        private UserDTO GetCurrentUser()
        {
            return new UserDTO();
        }
    }
}



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
        public Questionnaire2Controller(IHttpContextAccessor httpContextAccessor) : 
            base(httpContextAccessor) { }


        public ActionResult Index()
        {
            ViewBag.sessionId = base.Request.HttpContext.Connection.Id;
			ViewBag.Lang = "";

			return View("Index");
        }
        public ActionResult Form()
        {
            ViewBag.sessionId = base.Request.HttpContext.Connection.Id;
			ViewBag.Lang = "";
			return View("Form");
        }


        public ActionResult End()
        {
            ViewBag.sessionId = base.Request.HttpContext.Connection.Id;
			return View("End");
        }




        public ActionResult SaveAnswer(string? sessionId)
        {
            if (sessionId == null) sessionId = Guid.NewGuid().ToString();
            {
                var model = new Answer2Model();
                string json = base.GetJsonSafe(() => model.SaveAnswers(base.Body(), GetCurrentUser(), sessionId));
            }

            return Ok(new { ok = true });

        }

        public ActionResult SaveUserCounter( string? sessionId)
        {
            var rep = uow.GetRepository<HitCounter>();
			var exists = rep.FirstOrDefault(x => x.SessionId == sessionId);
            if(exists == null)
            {
			string json = base.GetJsonSafe(() =>
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


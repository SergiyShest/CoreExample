using DAL.Core;
using DAL;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Entity.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Entity.Controllers
{   [Authorize]
    public class AnswerJournalController : BaseController
    {

     public  AnswerJournalController(IHttpContextAccessor httpContextAccessor):base(httpContextAccessor) { }

        public IActionResult Index()
        {
            return View();
        }
		
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions)
		{
            var request = Body();
			var answers = uow.GetRepository<vAnswer2>().GetAll();
            var dateFrom = base.HttpContext.Session.GetString("dateFrom");
            var dateTo = base.HttpContext.Session.GetString("dateTo");
            
            if (!string.IsNullOrWhiteSpace(dateFrom))
            {
                var dateFr = DateOnly.FromDateTime(DateTime.Parse(dateFrom));
                answers = answers.Where(x => x.Cdate > dateFr);
            }
            if (!string.IsNullOrWhiteSpace(dateTo))
            {
                var dateT = DateOnly.FromDateTime(DateTime.Parse(dateTo));
                answers = answers.Where(x => x.Cdate < dateT);
            }
            
            loadOptions.PrimaryKey = new[] { "Id" };
			loadOptions.PaginateViaPrimaryKey = true;
            try
            {
                return Json(await DataSourceLoader.LoadAsync(answers, loadOptions));
            }
            catch(Exception ex)
            {
			    return Json(answers.ToList());
            }
		}

        public async Task<IActionResult> SetFilter()
        {
            try
            {
                var body = JsonConvert.DeserializeObject<DateFromTo>(base.Body());

                base.HttpContext.Session.SetString("dateFrom", body.from);

                if (body.from == null)
                {
                    base.HttpContext.Session.Remove("dateFrom");
                }
                else
                {
                    base.HttpContext.Session.SetString("dateFrom", body.from);
                }


                if (body.to == null)
                {
                    base.HttpContext.Session.Remove("dateTo");
                }
                else
                {
                    base.HttpContext.Session.SetString("dateTo", body.to);
                }



                Console.WriteLine(base.Body());
                return Ok(new { Reload = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    error = ex.GetAllMessages()
                });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
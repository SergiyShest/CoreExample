using DAL.Core;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Entity.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using BLL;
using System.Globalization;
using NPOI.XWPF.UserModel;

namespace Entity.Controllers
{   [Authorize]
    public class HitCounterController : BaseController
    {

     public HitCounterController(IHttpContextAccessor httpContextAccessor):base(httpContextAccessor) { }

        public IActionResult Index()
        {
            return View();
        }


		public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions)
		{
            var answers = uow.GetRepository<HitCounter>().GetAll();
          
            var  dateFrom =  base.HttpContext.Session.GetString("dateFrom");
            var  dateTo =  base.HttpContext.Session.GetString("dateTo");
            if (!string.IsNullOrWhiteSpace(dateFrom))
            {       var dateFr = DateTime.Parse(dateFrom);
                    answers=answers.Where(x=>x.Cdate>DateTime.Parse(dateFrom));
            }
            if (!string.IsNullOrWhiteSpace(dateTo))
            {          
                var dateT = DateTime.Parse(dateTo);
                     answers= answers.Where(x=>x.Cdate<DateTime.Parse(dateTo));
            }
            var x = answers.ToList();

			loadOptions.PrimaryKey = new[] { "Id" };
			loadOptions.PaginateViaPrimaryKey = true;
			return Json(await DataSourceLoader.LoadAsync(answers, loadOptions));
		}


            public async Task<IActionResult> SetFilter()
            {
                try
                {
                    var body = JsonConvert.DeserializeObject<DateFromTo>( base.Body());

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
                    return Ok(new {Reload=true });
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

    public class DateFromTo
    {
        public string from { get; set; } = string.Empty;
        public DateTime? From { get { return CoreHelper.ParseRequestDate(from); } }

        public string to {get;set;} = string.Empty;
        public DateTime? To { get { return CoreHelper.ParseRequestDate(to); } }
    }
}
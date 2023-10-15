using Core;
using DAL;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Entity.Models;
using Newtonsoft.Json;
using System.Diagnostics;

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
			loadOptions.PrimaryKey = new[] { "Id" };
			loadOptions.PaginateViaPrimaryKey = true;
			return Json(await DataSourceLoader.LoadAsync(answers, loadOptions));
		}




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
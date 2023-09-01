﻿using Core;
using DAL;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MVC.Controllers
{
    public class AnswerJournalController : BaseController
    {


        public IActionResult Index()
        {
            return View();
        }

		public ActionResult GetAll(DataSourceLoadOptions loadOptions)
		{


			


		  var ansvers=	uow.GetRepository<Answer>().GetAll();


			//	var loadResult = DataSourceLoader.Load(ordersQuery, loadOptions);
			return Content(JsonConvert.SerializeObject(ansvers), "application/json");
		}

		public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions)
		{
			var ansvers = uow.GetRepository<Answer>().GetAll();

			loadOptions.PrimaryKey = new[] { "Id" };
			loadOptions.PaginateViaPrimaryKey = true;

			return Json(await DataSourceLoader.LoadAsync(ansvers, loadOptions));
		}



		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
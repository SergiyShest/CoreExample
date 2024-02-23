﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Core;


namespace Entity.Controllers
{
    public class GenericCardController<T> : BaseController where T : class, IEntity
	{
		public GenericCardController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) { }

		public virtual IActionResult Index(int?id)
		{
			ViewBag.Id = id;
			ViewBag.Mode = "edit";
			return View();
		}

		public virtual async Task<IActionResult> Get(int? id,string? mode)
		{ 
			T answer ;
			if (id == null) answer = Activator.CreateInstance<T>();
            else { 
			   answer  =  uow.GetRepository<T>().GetAll().First(x=>x.Id==id);
            }
			return Content(JsonConvert.SerializeObject(answer ), "application/json");
		}
    }

}

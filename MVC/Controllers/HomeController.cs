using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CookieReaders.Models;
using Microsoft.AspNetCore.Authorization;
using MVC.Models;
using Microsoft.AspNetCore.Hosting.Server;
using BLL;
using Core;
using Newtonsoft.Json;
using System.Text;
using MVC.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Web.Helpers;
using System.IO;
using DAL.Core;
using DAL;
using DevExtreme.AspNet.Data;

namespace CookieReaders.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {




        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions)
        {
            var answers = uow.GetRepository<Questionnaire>().GetAll();

            loadOptions.PrimaryKey = new[] { "Id" };
            loadOptions.PaginateViaPrimaryKey = true;
            return Json(await DataSourceLoader.LoadAsync(answers, loadOptions));
        }






        [HttpPost]
        public async Task<IActionResult> Upload(int? id)
        {
            var file_ = base.Request.Form.Files[0];
            string content = String.Empty;
            using (var memoryStream = new MemoryStream())
            {
                file_.CopyTo(memoryStream);
                memoryStream.Position = 0;
                using (var reader = new StreamReader(memoryStream))
                {
                    content = reader.ReadToEnd();
                }
            }
            try
            {
                var questionniare = JsonConvert.DeserializeObject<QuestionnaireBo>(content);
                if (questionniare == null) { throw new ApplicationException ("now file"); }
                var exists = uow.GetRepository<Questionnaire>().GetAll().Any(x => x.Name == questionniare.Name);
                if(exists) { throw new ApplicationException("Questionnaire with the same name already exists. Change  name please."); }
                if (id == null)
                {
                    questionniare.Id = null;
                    foreach (var q in questionniare.Questions)
                    { q.Id = null; }
                }
                questionniare.Save(null);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new {error = ex.GetAllMessages()});
            }

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok();
        }



        public FileResult Download(int id)
        {
            var questionnaire = uow.GetRepository<Questionnaire>().FirstOrDefault();

            var qustionniare = new QuestionnaireBo(questionnaire);
            var json = JsonConvert.SerializeObject(qustionniare, Formatting.Indented);
            byte[] fileBytes = Encoding.ASCII.GetBytes(json);
            string fileName = qustionniare.Name + ".json";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }


    }
}

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
using System.IO;
using DAL.Core;
using DAL;
using DevExtreme.AspNet.Data;
using System.Net.Http.Formatting;
using System.Net;
using NPOI.XWPF.UserModel;

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

        public async Task<IActionResult> Insert()
        {
            var body = base.Body();
            var form = new FormDataCollection(body);
            var values = form.Get("values");

            var newQ = new Questionnaire();

            JsonConvert.PopulateObject(values, newQ);
            uow.Save();

            return Ok();
        }

        public async Task<IActionResult> Update()
        {
            var  body= base.Body();
            var form = new FormDataCollection(body);
            var values = form.Get("values");
            var id = Convert.ToInt32(form.Get("key"));
            var quer = uow.GetRepository<Questionnaire>().FirstOrDefault(x=>x.Id==id,false );
            var prMain = quer.Main == true;
            JsonConvert.PopulateObject(values, quer);
            if (quer.Main == true && !prMain){ 

              var prevMain = uow.GetRepository<Questionnaire>().FirstOrDefault(x=>x.Main==true,false );
              if(prevMain!=null)
                prevMain.Main = false;
            }
            uow.Save();


            return Ok();
        }

        public async Task<IActionResult> Delete()
        {
            var body = base.Body();
            var form = new FormDataCollection(body);
            var id = Convert.ToInt32(form.Get("key"));
            uow.Delete<Questionnaire>(id);
            return Ok();
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
                if (questionniare == null) { throw new ApplicationException("now file"); }
                   var exists = uow.GetRepository<Questionnaire>().GetAll().Any(x => x.Name == questionniare.Name && x.Id != id);
                if (exists) { throw new ApplicationException("Questionnaire with the same name already exists. Change  name please."); }
                if (id != null)
                {
                    var row = uow.GetRepository<Questionnaire>().FirstOrDefault(x => x.Id == id);

                    if (row != null)
                    {
                        var q = new QuestionnaireBo(row) ;
                        q.Delete(uow ,null, false);
                    }
                }
                questionniare.Id = id;
                foreach (var q in questionniare.Questions)
                { q.Id = null; }
                questionniare.Save(uow,null);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.GetAllMessages() });
            }

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok();
        }

        public FileResult Download(int id)
        {
            var questionnaire = uow.GetRepository<Questionnaire>().FirstOrDefault(x=>x.Id==id);

            var qustionniare = new QuestionnaireBo(questionnaire);
            var json = JsonConvert.SerializeObject(qustionniare, Formatting.Indented);
            byte[] fileBytes = Encoding.ASCII.GetBytes(json);
            string fileName = qustionniare.Name + id+".json";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }


        public FileResult DownloadCss(int id)
        {
            var fileContent = System.IO.File.ReadAllText("wwwroot\\Scripts\\vue-apps\\css_questionnaire\\app.css");
            byte[] fileBytes = Encoding.ASCII.GetBytes(fileContent);
            string fileName = "qustionniareMain.css";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }




    }
}

using BLL;
using Core;
using DAL.Core;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Controllers;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace CookieReaders.Controllers
{
   [Authorize]
    public class HomeController : BaseController
    {
        QuestionnairesModel Model { get; set; }
        public HomeController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) 
        {
            Model = new QuestionnairesModel(uow);
            Model.UserName = base.UserName;
        }

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
            try
            {
                Model.InsertMain(base.Body());
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.GetAllMessages() });
            }
        }

        public async Task<IActionResult> Update()
        {
            try
            {
                Model.UpdateMain(base.Body());
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.GetAllMessages() });
            }
        }

        public async Task<IActionResult> Delete()
        {
            try
            {
                Model.Delete(Body());
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    error = ex.GetAllMessages()
                });
            }
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
                Model.Upload(id, content,true);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.GetAllMessages() });
            }
        }

        public FileResult Download(int id)
        {
            var questionnaire = uow.GetRepository<Questionnaire>().FirstOrDefault(x => x.Id == id);

            var qustionniare = new QuestionnaireBo(questionnaire);
            var json = JsonConvert.SerializeObject(qustionniare, Formatting.Indented);
            byte[] fileBytes = Encoding.ASCII.GetBytes(json);
            string fileName = qustionniare.Name + id + ".json";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public FileResult DownloadCss()
        {
            var fileContent = System.IO.File.ReadAllText("wwwroot\\Scripts\\vue-apps\\css_questionnaire\\app.css");
            byte[] fileBytes = Encoding.ASCII.GetBytes(fileContent);
            string fileName = "qustionniareMain.css";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
       [HttpPost]
        public async Task<IActionResult> UploadCss()
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
                System.IO.File.WriteAllText("wwwroot\\Scripts\\vue-apps\\css_questionnaire\\app.css", content);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.GetAllMessages() });
            }

        }
    }
}

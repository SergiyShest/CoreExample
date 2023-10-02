using BLL;
using Core;
using DAL;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using DAL.Core;
using System.Diagnostics;

namespace MVC.Controllers
{
    [Authorize]
    public class UserJournalController : BaseController
    {
        UserModel UserModel { get;  }
        AvaiableUserModel AvUserModel { get;  }

        public UserJournalController()
        {
            AvUserModel = new AvaiableUserModel(uow);
            UserModel = new UserModel(uow);
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions)
        {
            var answers = uow.GetRepository<User>().GetAll();

            loadOptions.PrimaryKey = new[] { "Id" };
            loadOptions.PaginateViaPrimaryKey = true;
            return Json(await DataSourceLoader.LoadAsync(answers, loadOptions));
        }
 
        public async Task<IActionResult> Delete()
        {
            try
            {
                UserModel.Delete(Body());
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
        
        public async Task<IActionResult> GetAvaiable(DataSourceLoadOptions loadOptions)
        {
            var answers = uow.GetRepository<AvaiableUser>().GetAll();

            loadOptions.PrimaryKey = new[] { "Id" };
            loadOptions.PaginateViaPrimaryKey = true;
            return Json(await DataSourceLoader.LoadAsync(answers, loadOptions));
        }

        public async Task<IActionResult> InsertAvaiable()
        {
            try
            {
                AvUserModel.Insert(base.Body());
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.GetAllMessages() });
            }
        }

        public async Task<IActionResult> UpdateAvaiable()
        {
            try
            {
                AvUserModel.Update(base.Body());
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.GetAllMessages() });
            }
        }

        public async Task<IActionResult> DeleteAvaiable()
        {
            try
            {
                AvUserModel.Delete(Body());
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


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Dynamic;
using NLog;
using BLL;
using DAL.Core;

namespace MVC.Controllers
{
    public class BaseController : Controller
    {
        protected Logger log = LogManager.GetCurrentClassLogger();

        protected UnitOfWork uow= new UnitOfWork();

        protected JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings();

        public string GetJsonSafe(Func<object> function)
        {
            dynamic expOb = new ExpandoObject();
            try
            {
                expOb.Item = function();
            }
            catch (Exception ex)
            {
                expOb.Errors = new List<ObjectError>() { new ObjectError("", ex.GetAllMessages()) };
                log.Error(ex);
            }
            var json = JsonConvert.SerializeObject(expOb, JsonSerializerSettings);
            return json;
        }

        public string GetJsonSafe(Action function)
        {
            dynamic expOb = new ExpandoObject();
            try
            {
                function();

            }
            catch (Exception ex)
            {
                expOb.Errors = new List<ObjectError>() { new ObjectError("", ex.GetAllMessages()) };
                log.Error(ex);

            }
            var json = JsonConvert.SerializeObject(expOb, JsonSerializerSettings);
            return json;
        }


    }
}


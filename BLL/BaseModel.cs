using DAL.Core;
using Newtonsoft.Json;
using NLog;

using System.Dynamic;


namespace BLL
{
    public abstract class BaseModel
	{
		public int? Id { get; set; }

		protected Logger log = LogManager.GetCurrentClassLogger();

		protected static void NullTest(object type, string name)
		{
			if (type == null) { throw new ArgumentNullException($"Parametr {name} can not be null"); }
		}


		public string GetJson(Func<object> function)
		{
           dynamic expOb = new ExpandoObject();
			string json;
			try
			{
				expOb.Item = function();
				json = JsonConvert.SerializeObject(expOb);
			}
			catch (Exception ex)
			{
				expOb.Errors = new List<ObjectError>() { new ObjectError("", ex.GetAllMessages()) };
				log.Error(ex);
				json = JsonConvert.SerializeObject(expOb);
			}
			return json;
		}

	    protected IUnitOfWorkEx UOW	= new UnitOfWork();
	}
}
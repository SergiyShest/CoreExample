
using Newtonsoft.Json;
using NLog;
using DAL;
using System.Dynamic;
using Core;

namespace Sasha.Lims.WebUI.Areas.Questions
{
	public class EditQuestionnairesModel
	{

		protected Logger log = LogManager.GetCurrentClassLogger();
       public int? Id { get; set; }

		IUnitOfWorkEx UOW;
		public EditQuestionnairesModel(int? id)
		{
			UOW = new UnitOfWork();
			Id = id;
		}

		public string Get( )
		{
			dynamic expOb = new ExpandoObject();
			string json=null;
			try
			{

				
			}
			catch (Exception ex)
			{
				expOb.Errors = new List<ObjectError>() { new ObjectError("", ex.GetAllMessages()) };
				log.Error(ex);
				json = JsonConvert.SerializeObject(expOb);
			}

			return json;
		}


		protected static void NullTest(object type, string name)
		{
			if (type == null) { throw new ArgumentNullException($"Parametr {name} can not be null"); }
		}




		public virtual string GetContextMenuItemsFromSettings()
		{
			dynamic expOb = new ExpandoObject();
			string json;
			try
			{
				var items = new List<JsContextMenuItem>();

					items = new List<JsContextMenuItem>()
					{

					new JsContextMenuItem(0){ Name = "Add ",    Slug= "addChaild" ,AvaiableCodes=new List<string>{ } },
					new JsContextMenuItem(0){ Name = "Edit",    Slug= "edit"    ,AvaiableCodes=new List<string>{ } } ,
					new JsContextMenuItem(0){ Name = "Delete" , Slug= "delete"   ,AvaiableCodes=new List<string>{ } },
					new JsContextMenuItem(0){ Name = "Copy"   , Slug= "copy"   ,AvaiableCodes=new List<string>{ } }
					};

				expOb.Item = items;

			}
			catch (Exception ex)
			{
				expOb.Errors = new List<ObjectError>() { new ObjectError("", ex.GetAllMessages()) };
				log.Error(ex);
			}
			var jsonResolver = new PropertyRenameAndIgnoreSerializerContractResolver();


			jsonResolver.RenameProperty(typeof(JsContextMenuItem), "Name", "name");
			jsonResolver.RenameProperty(typeof(JsContextMenuItem), "Slug", "slug");
			jsonResolver.RenameProperty(typeof(JsContextMenuItem), "NeedContent", "needContent");
			jsonResolver.RenameProperty(typeof(JsContextMenuItem), "AvaiableCodes", "avaiableCodes");

			var serializerSettings = new JsonSerializerSettings();
			serializerSettings.ContractResolver = jsonResolver;//
			json = JsonConvert.SerializeObject(expOb, serializerSettings);
			return json;
		}


	}


}

using Core;
using DAL;
using DAL.Core;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Dynamic;


namespace BLL
{
    public class EditQuestionModel: IVueModel
	{

		protected Logger log = LogManager.GetCurrentClassLogger();
		public int? Id { get; set; }



        IUnitOfWorkEx UOW;
		public EditQuestionModel(int? id)
		{
			UOW = new UnitOfWork();
			Id = id;
		}

		public string GetJson()
		{
			dynamic expOb = new ExpandoObject();
			string json;
			try
			{
				QuestionBo question;
				if (Id == null)
					question = new QuestionBo();
				else
				{
					question = new QuestionBo((int) Id);
				}
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

		public List<ObjectError> SetJson(string json)
		{
			var errors = new List<ObjectError>();
			var obj = JsonConvert.DeserializeObject(json);
			{

			}

			return errors;
		}

		public List<ObjectError> Save(UserDTO user)
		{
			var errors = new List<ObjectError>();
			return errors;
		}

		public List<ObjectError> Delete()
		{
			throw new NotImplementedException();
		}
	}


}
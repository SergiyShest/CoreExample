
using DAL;
using Newtonsoft.Json;
using NLog;

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Xml.Linq;


namespace Sasha.Lims.WebUI.Areas.Questions
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
				expOb.Item =function();
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


	public class QuestionnaireModel : BaseModel
	{





		public QuestionnaireModel(int? id)
		{

			Id = id;
		}

		public QuestionnaireBo Get()
		{
			return new QuestionnaireBo((int)Id);

		}

		public void DeleteQuestion(int? questionId)
		{
			NullTest(questionId, "questionId");
			UOW.GetRepository<A_prop>().Delete((int)questionId);

		}




		//public string GetQuestion(int id)
		//{
		//	dynamic expOb = new ExpandoObject();
		//	string json;
		//	try
		//	{

		//		Question question = new Question(id);
		//		expOb.question = question;
		//		json = JsonConvert.SerializeObject(expOb);

		//	}
		//	catch (Exception ex)
		//	{
		//		expOb.Errors = new List<ObjectError>() { new ObjectError("", ex.GetAllMessages()) };
		//		log.Error(ex);
		//		json = JsonConvert.SerializeObject(expOb);
		//	}

		//	return json;
		//}

		internal dynamic GetAnsvers(int? patientId)
		{
			NullTest(patientId, "patientId");

			var answers = GetAnswers((int)patientId);
			var answer = answers.FirstOrDefault(x => x.QuestionnarieId == Id);
			if (answer == null) return null;
			if (answer.QuestionnarieAnsvers == null) return null;
			return answer.QuestionnarieAnsvers;

		}




		public List<ObjectError> SaveQuestion(string bodyText, UserDTO user)
		{
			var err = new List<ObjectError>();
			try
			{
				var query = JsonConvert.DeserializeObject<Question>(bodyText);
				if (query.Id == 0 || query.Id == null)
				{
					query.QuestionnaireId = Id;
				}
				query.Save(user);
			}
			catch (Exception ex)
			{
				err.Add(new ObjectError("", ex.GetAllMessages()));
			}
			return err;
		}

		public List<ObjectError> SaveQuestionnaire(string bodyText, UserDTO user)
		{
			var err = new List<ObjectError>();
			try
			{
				var questionnaire = JsonConvert.DeserializeObject<QuestionnaireBo>(bodyText);
				if (questionnaire.ParentId == null)
				{
					var parent = UOW.GetRepository<A_prop>().FirstOrDefault(x => x.Code == "QUESTIONNAIRE_LIST");
					questionnaire.ParentId = parent.Id;
				}
				questionnaire.Save(user);
			}
			catch (Exception ex)
			{
				err.Add(new ObjectError("", ex.GetAllMessages()));
			}
			return err;
		}


		//public virtual string GetContextMenuItemsFromSettings()
		//{
		//	dynamic expOb = new ExpandoObject();
		//	string json;
		//	try
		//	{
		//		var items = new List<JsContextMenuItem>();

		//		items = new List<JsContextMenuItem>()
		//			{

		//			new JsContextMenuItem(0){ Name = "Add ",    Slug= "addChaild" ,AvaiableCodes=new List<string>{ } },
		//			new JsContextMenuItem(0){ Name = "Edit",    Slug= "edit"    ,AvaiableCodes=new List<string>{ } } ,
		//			new JsContextMenuItem(0){ Name = "Delete" , Slug= "delete"   ,AvaiableCodes=new List<string>{ } },
		//			new JsContextMenuItem(0){ Name = "Copy"   , Slug= "copy"   ,AvaiableCodes=new List<string>{ } }
		//			};

		//		expOb.Item = items;

		//	}
		//	catch (Exception ex)
		//	{
		//		expOb.Errors = new List<ObjectError>() { new ObjectError("", ex.GetAllMessages()) };
		//		log.Error(ex);
		//	}
		//	var jsonResolver = new PropertyRenameAndIgnoreSerializerContractResolver();


		//	jsonResolver.RenameProperty(typeof(JsContextMenuItem), "Name", "name");
		//	jsonResolver.RenameProperty(typeof(JsContextMenuItem), "Slug", "slug");
		//	jsonResolver.RenameProperty(typeof(JsContextMenuItem), "NeedContent", "needContent");
		//	jsonResolver.RenameProperty(typeof(JsContextMenuItem), "AvaiableCodes", "avaiableCodes");

		//	var serializerSettings = new JsonSerializerSettings();
		//	serializerSettings.ContractResolver = jsonResolver;//
		//	json = JsonConvert.SerializeObject(expOb, serializerSettings);
		//	return json;
		//}

		public void SaveAnsvers(string bodyText, UserDTO user, int? patientId)
		{

			if (!patientId.HasValue)
			{ throw new NullReferenceException("patientId can not be null"); }
			var answers = GetAnswers((int)patientId);

			var ansver = answers.FirstOrDefault(x => x.QuestionnarieId == Id);
			if (ansver == null) { ansver = new QuestionnarieAnswers((int)patientId, (int)Id); }
			ansver.QuestionnarieAnsvers = JsonConvert.DeserializeObject(bodyText);
			ansver.Save(user);
		}

		public List<QuestionnarieAnswers> GetAnswers(int patientId)
		{
			//	var comments = UOW.GetRepository<a_comment>().Where(x =>
			//	x.commentType_id == (int) CommentType.QuestionarieAnsvers &&
			//	x.table_id == (int) TableType.Patient &&
			//	x.row_id == (int) patientId
			//	).ToList();
			//	var answers = new List<QuestionnarieAnswers>();
			//	foreach (var commentRecord in comments)
			//	{
			//		answers.Add(new QuestionnarieAnswers(commentRecord));
			//	}
			//	return answers;
			//}
			return null;
		}


	}
}
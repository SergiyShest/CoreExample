
using Core;
using DAL;
using DAL.Core;
using Entity;
using Newtonsoft.Json;


namespace BLL
{

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
			UOW.GetRepository<Vjsf>().Delete((int)questionId);

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

		public dynamic GetAnsvers(int? patientId)
		{
			NullTest(patientId, "patientId");

			//var answers = GetAnswers((int)patientId);
			//var answer = answers.FirstOrDefault(x => x.QuestionnarieId == Id);
			//if (answer == null) return null;
			//if (answer.QuestionnarieAnsvers == null) return null;
			//return answer.QuestionnarieAnsvers;
			return null;
		}




		public List<ObjectError> SaveQuestion(IUnitOfWorkEx uow, string bodyText, UserDTO user)
		{
			var err = new List<ObjectError>();
			try
			{
				var query = JsonConvert.DeserializeObject<QuestionBo>(bodyText);
				if (query.Id == 0 || query.Id == null)
				{
					query.QuestionnaireId = Id;
				}
				query.Save(uow,user);
			}
			catch (Exception ex)
			{
				err.Add(new ObjectError("", ex.GetAllMessages()));
			}
			return err;
		}

		public List<ObjectError> SaveQuestionnaire(IUnitOfWorkEx uow, string bodyText, UserDTO user)
		{
			var err = new List<ObjectError>();
			try
			{
				var questionnaire = JsonConvert.DeserializeObject<QuestionnaireBo>(bodyText);
	
				questionnaire.Save(uow, user);
			}
			catch (Exception ex)
			{
				err.Add(new ObjectError("", ex.GetAllMessages()));
			}
			return err;
		}



		public void SaveAnswers(string bodyText, UserDTO user, string? sessionId)
		{

			if (string.IsNullOrEmpty(sessionId) )
			{ throw new NullReferenceException("sessionId can not be null"); }
		 	//var answers = GetAnswers(sessionId);

			// ansver = answers.FirstOrDefault(x => x.QuestionnarieId == Id);
			if (Id == null) 
			{ Id = -10; }
			var ansver = new AnswersBo(sessionId, (int)Id,  bodyText);

			

			ansver.Save();
		}
		public void SaveAnswers(string bodyText, UserDTO user, int? patientId)
		{

			if (!patientId.HasValue)
			{ throw new NullReferenceException("sessionId can not be null"); }
			//var answers = GetAnswers(patientId);

			//var ansver = answers.FirstOrDefault(x => x.QuestionnarieId == Id);
			//if (ansver == null) { ansver = new QuestionnarieAnswers(patientId, (int)Id); }
			//ansver.QuestionnarieAnsvers = JsonConvert.DeserializeObject(bodyText);
			//ansver.Save(user);
		}
		//public List<QuestionnarieAnswers> GetAnswers(string sessionId)
		//{
		//	//var comments = UOW.GetRepository<a_comment>().Where(x =>
		//	//x.commentType_id == (int)CommentType.QuestionarieAnsvers &&
		//	//x.table_id == (int)TableType.Patient &&
		//	//x.row_id == (int)patientId
		//	//).ToList();
		//	//var answers = new List<QuestionnarieAnswers>();
		//	//foreach (var commentRecord in comments)
		//	//{
		//	//	answers.Add(new QuestionnarieAnswers(commentRecord));
		//	//}
		//	//return answers;

		//	return null;
		//}
		//public List<QuestionnarieAnswers> GetAnswers(int patientId)
		//{
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
		//	return null;
		//}

	}
}
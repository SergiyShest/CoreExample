using Core;
using DAL;
using DAL.Core;
using Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace BLL
{
    public class AnswersBo
	{
		UnitOfWork _uow = new UnitOfWork();

		public string Name { get; set; }

		public int? QuestionnarieId { get; set; }

		public string Error { get; set; }

		List<Answer> Answers { get; set; } = new List<Answer>();


		#region constructors



		public AnswersBo(IEnumerable<Answer> records)
		{
			Answers = records.ToList();
		}

		public AnswersBo(string sessionId, int? questionnarieId, string bodyText)
		{
			Name = sessionId;
			QuestionnarieId = questionnarieId;

			dynamic ansversJson = JsonConvert.DeserializeObject(bodyText);

			foreach (var answer in ansversJson)
			{
				var models = (JObject)answer.answerModel;
				var pr = models.Properties();
				foreach (var item in pr)
				{
					int qId = answer.QuestionId;
					var ansv = _uow.GetRepository<Answer>().FirstOrDefault(x =>
						x.QuestionnarieId == questionnarieId &&
						x.SessionId == sessionId &&
						x.QuestionId == qId &&
						x.Name == item.Name);
					if (ansv == null)
						ansv = new Answer()
						{
							QuestionnarieId = questionnarieId,
							SessionId = sessionId,
							QuestionId = answer.QuestionId,
							Name = item.Name,


						};
					ansv.Value = item.Value.ToString();
					ansv.Cdate = DateOnly.FromDateTime(DateTime.Today);
					Answers.Add(ansv);
				}
			}

		}
		#endregion


		public void Save()
		{
			foreach (Answer a in Answers)
			{
				_uow.Save(a);
			}
		}




	}
}
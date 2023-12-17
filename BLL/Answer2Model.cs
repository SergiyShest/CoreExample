using DAL;
using Entity;
using Newtonsoft.Json;

namespace BLL
{
    public class Answer2Model : BaseModel
	{


        public void SaveAnswers(string bodyText, UserDTO user, string? sessionId)
        {
            dynamic answersJson = JsonConvert.DeserializeObject(bodyText);
            var answer = new SimpleAnswer();

            answer.QuestionnarieId  = 0;
            answer.SessionId  = sessionId;
            answer.UserName = answersJson.Name;
            answer.UserPhone = answersJson.Phone;
            answer.UserEmail = answersJson.Email;
            answer.Cdate = DateOnly.FromDateTime(DateTime.Today);
            answer.DateTime = DateTime.Today;

            var rep = base.UOW.GetRepository<SimpleAnswer>();
            rep.Create(answer);
            rep.Save();

        }
    }
}
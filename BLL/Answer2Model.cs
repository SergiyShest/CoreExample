using DAL;
using Entity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog;
using NLog.Fluent;
using NPOI.SS.Formula.Functions;

namespace BLL
{
    public class Answer2Model : BaseModel
	{
        Logger log = LogManager.GetCurrentClassLogger();

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
            log.Info($"\"{answersJson.Name}\";\"{answersJson.Phone}\";\"{answersJson.Email}\"");
            var rep = base.UOW.GetRepository<SimpleAnswer>();
            rep.Create(answer);
            rep.Save();

        }
    }
}
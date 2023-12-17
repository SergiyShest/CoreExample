using NUnit.Framework;
using DAL;

using Core;
using NUnit.Framework.Internal;
using BLL;
using DAL.Core;
using Sasha.Lims.Tests.TestCore;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Entity;
using System.Dynamic;

namespace Tests
{
    [TestFixture]
    public class QuestionnaireTests : BaseTest
    {

        #region common

        [Test]
        public void CreateQuestion()
        {
            var question = new QuestionBo() { Name = Guid.NewGuid().ToString() };
            question.Save(base.UOW,_user);

        }

        [Test]
        public void DeleteQuestion()
        {
            var question = new QuestionBo() { Name = "fffff" };
            question.Save(UOW,_user);

            question.Delete( UOW,_user);

        }

        [Test]
        public void GetQuestionnaires()
        {
            var questionnaireRecord = UOW.GetRepository<Questionnaire>().FirstOrDefault();
            var questionnaire = new QuestionnaireBo(questionnaireRecord);
            var question = new QuestionBo() { Name = Guid.NewGuid().ToString() };
            questionnaire.Questions.Add(question);
            questionnaire.Save(base.UOW,_user);


        }

        [Test]
        public void SaveQuestionnaire()
        {
            var questionnaire = createQuestionnaire(true, true);
            var newQuestionId = questionnaire.Questions.FirstOrDefault(x => x.Name == "Test").Id;

            var newQuestion = UOW.GetRepository<Vjsf>().FirstOrDefault(x => x.Id == newQuestionId);
            Assert.NotNull(newQuestion);
        }

        [Test]
        public void SaveAnswers()
        {
            var questionnaire = createQuestionnaire(false, true);
            var model = new QuestionnaireModel(questionnaire.Id);
            var bodyText =
                "[" +
                "{\"QuestionId\":1,\"answerModel\":{\"ListProp\":1},\"valid\":true}," +
                "{\"QuestionId\":2,\"answerModel\":{\"Age\":1},\"valid\":true}," +
                "{\"QuestionId\":7,\"answerModel\":{\"Phone\":'345-23-45-455-45'},\"valid\":true}" +
                "]";

            model.SaveAnswers(bodyText, _user, "sessionId");

        }


        #endregion

        [Test]
        public void LoadFromFileQuestionn()
        {
            var json = File.ReadAllText("g:\\source\\repos\\Core\\MVC\\Views\\Questionnaire\\src\\data.json");
            dynamic obj = JsonConvert.DeserializeObject(json);
            foreach (var x in obj.Item.Questions) { 

            var  jso = JsonConvert.SerializeObject(x, Formatting.Indented);
            var newObject = JsonConvert.DeserializeObject<QuestionBo>(jso);
            var  jso2 = JsonConvert.SerializeObject(newObject, Formatting.Indented);
            

            }
            // var questionnaireRecord = UOW.GetRepository<Questionnaire>().GetAll().ToList().OrderBy(x => x.Id).LastOrDefault();
            // var  questionnaire  = new QuestionnaireBo();





        }

        [Test]
        public void DeleteAddQuestionnaire()
        {
            var questionnaires = UOW.GetRepository<Questionnaire>().GetAll();
            foreach (var question in questionnaires) {
                var q = new QuestionnaireBo(question);
                q.Delete(base.UOW, _user);
            }

        }

        [Test]
        public void LoadFromFileQuestionnaireNew()
        {
            var json = File.ReadAllText("g:\\source\\repos\\Core\\MVC\\Views\\Questionnaire\\src\\data.json");
            File.WriteAllText("C:tmp\\json", json);
            var questionniare = JsonConvert.DeserializeObject<QuestionnaireBo>(json);
            questionniare.Id = null;
            foreach ( var q in questionniare.Questions) 
            { q.Id=null; }

           
            questionniare.Save(base.UOW, _user);
            var  jso1 = JsonConvert.SerializeObject(questionniare, Formatting.Indented);
            File.WriteAllText("C:tmp\\jso1", jso1);

            var qustionniare2 = new QuestionnaireBo(questionniare.Id);
            qustionniare2.Load();
            var  jso2 = JsonConvert.SerializeObject(qustionniare2, Formatting.Indented);
            File.WriteAllText("C:tmp\\jso2", jso2);
            // var questionnaireRecord = UOW.GetRepository<Questionnaire>().GetAll().ToList().OrderBy(x => x.Id).LastOrDefault();
            // var  questionnaire  = new QuestionnaireBo();

        }
        
        private QuestionnaireBo createQuestionnaire(bool withQuestion = true,bool cteateNew=false)
        {
			var questionnaire = new QuestionnaireBo() { Name = Guid.NewGuid().ToString()};
            if (!cteateNew)
            {
                var questionnaireRecord = UOW.GetRepository<Questionnaire>().FirstOrDefault();
                questionnaire = new QuestionnaireBo(questionnaireRecord);
            }
            if (withQuestion)
            {
                var question = new QuestionBo() { Name = "Test" };
                questionnaire.Questions.Add(question);
            }
            questionnaire.Save(base.UOW,_user);
            return questionnaire;
        }

    }


}


public sealed class DynObject : DynamicObject
{
    private readonly Dictionary<string, object> _properties;

    public DynObject(Dictionary<string, object> properties)
    {
        _properties = properties;
    }

    public override IEnumerable<string> GetDynamicMemberNames()
    {
        return _properties.Keys;
    }

    public override bool TryGetMember(GetMemberBinder binder, out object result)
    {
        if (_properties.ContainsKey(binder.Name))
        {
            result = _properties[binder.Name];
            return true;
        }
        else
        {
            result = null;
            return false;
        }
    }

    public override bool TrySetMember(SetMemberBinder binder, object value)
    {
        if (_properties.ContainsKey(binder.Name))
        {
            _properties[binder.Name] = value;
            return true;
        }
        else
        {
            return false;
        }
    }
}
using NUnit.Framework;

using Newtonsoft.Json;
using DAL;
using Sasha.Lims.WebUI.Areas.Questions;
using Core;
using NUnit.Framework.Internal;
using BLL;

namespace Tests
{
    [TestFixture]
    public class QuestionnaireTests 

    {
        IUnitOfWorkEx UOW= new UnitOfWork();
        UserDTO _user = new UserDTO();


         [Test]
        public void CreateQuestion()
        {
            var question = new QuestionBo();
            question.Save(_user);

        }

         [Test]
        public void DeleteQuestion()
        {
            var question = new QuestionBo();
            question.Save(_user);

            question.Delete(_user);

        }


        [Test]
        public void GetQuestionnaires()
        {
            var questionnaireRecord = UOW.GetRepository<Questionnaire>().FirstOrDefault();
            var questionnaire = new QuestionnaireBo(questionnaireRecord);
            var question = new QuestionBo();
            questionnaire.Questions.Add(question);
            questionnaire.Save(_user);
        }

        [Test]
        public void SaveQuestionnaire()
        {
            var questionnaire = createQuestionnaire();
            var newQuestionId = questionnaire.Questions.FirstOrDefault(x => x.Name == "Test").Id;

            var newQuestion = UOW.GetRepository<Vjsf>().FirstOrDefault(x => x.Id == newQuestionId);
            Assert.NotNull(newQuestion);
        }



        [Test]
        public void SaveAnsvers()
        {
            var questionnaire = createQuestionnaire();
            var patientId = UOW.GetRepository<c_patient>().GetAll().FirstOrDefault().id;
            var model = new QuestionnaireModel(questionnaire.Id);
            model.SaveAnsvers("{bodyText:'bodyText'}", _user, patientId);
            var ansvers = model.GetAnswers((int)patientId);
            var answer = ansvers.FirstOrDefault(x => x.QuestionnarieId == questionnaire.Id);
            Assert.NotNull(answer);

        }



       // [Test]
       // public void LoadFromFileQuestionnaire()
       // {

       //     var questionnaireRecord = UOW.GetRepository<A_prop>().Where(x => x.Code == "QUESTIONNAIRE").ToList().OrderBy(x => x.Id).LastOrDefault();
       //     ExcelLoader loader = new ExcelLoader();

       ////     var questionniare = loader.AddQuestions("c:/Users/Sergey.DESKTOP-6MTKSUM/Documents/TestQuestionnaire.xlsx", questionnaireRecord.id);
       // //    questionniare.Save(_user);



       // }


        private QuestionnaireBo createQuestionnaire(bool withQuestion = true)
        {
            var questionnaireRecord = UOW.GetRepository<Questionnaire>().FirstOrDefault();
            var questionnaire = new QuestionnaireBo(questionnaireRecord);
            if (withQuestion)
            {
                var question = new QuestionBo() { Name = "Test" };
                questionnaire.Questions.Add(question);
            }
            questionnaire.Save(_user);
            return questionnaire;
        }


    }

    internal class ExcelLoader
    {
    }

    //[TestFixture]
    //public class loadTests
    //{

    //    //[Test]
    //    //public void LoadFromFileQuestionnaire()
    //    //{

    //    //	ExcelLoader loader = new ExcelLoader();
    //    //    var questions =	loader.LoadQuestions("c:/Users/Sergey.DESKTOP-6MTKSUM/Documents/TextInput.xlsx");
    //    //	Assert.Greater(questions.Count(),0);	
    //    //          Assert.AreEqual(0,loader.Errors.Count,string.Join(";",loader.Errors));

    //    //}

    //    [Test]
    //    public void LoadFromFileQuestionnaireGroup()
    //    {

    //        ExcelLoader loader = new ExcelLoader();
    //        var questionGroups = loader.LoadQuestions("c:/Users/Sergey.DESKTOP-6MTKSUM/Documents/TestQuestionnaire1.xlsx");
    //        Assert.Greater(questionGroups.Count(), 0);
    //        Assert.AreEqual(0, loader.Errors.Count, string.Join(";", loader.Errors));
    //        var shema = JsonConvert.SerializeObject(questionGroups[0].Schema, new Formatting());
    //        var options = JsonConvert.SerializeObject(questionGroups[0].Options, new Formatting());
    //    }

    //    [Test]
    //    public void LoadFromFileQuestionnaireGroupFull()
    //    {

    //        ExcelLoader loader = new ExcelLoader();
    //        var questionGroups = loader.LoadQuestions("c:/Users/Sergey.DESKTOP-6MTKSUM/Documents/TestQuestionnaire.xlsx");
    //        Assert.Greater(questionGroups.Count(), 0);
    //        Assert.AreEqual(0, loader.Errors.Count, string.Join(";", loader.Errors));
    //        var shema = JsonConvert.SerializeObject(questionGroups[0].Schema, new Formatting());
    //        var options = JsonConvert.SerializeObject(questionGroups[0].Options, new Formatting());
    //    }
    //}




}

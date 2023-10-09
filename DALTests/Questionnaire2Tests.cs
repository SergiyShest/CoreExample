using BLL;
using Core;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Sasha.Lims.Tests.TestCore;

namespace Tests
{
    [TestFixture]
    public class Questionnaire2Tests: BaseTest
    {
        [Test]
        public void GetQuestionnaires()
        {


            var questionnaireRecord = new Questionnaire() { Name = Guid.NewGuid().ToString() };
            var questionnaire = new QuestionnaireBo(questionnaireRecord);
            var question = new QuestionBo() { Name = Guid.NewGuid().ToString() };
            questionnaire.Questions.Add(question);
            question.Images.Add(new);

            questionnaire.Save(base.UOW, _user);
          
        }

    }


}

using NUnit.Framework;

using Newtonsoft.Json;
using NUnit.Framework.Internal;
using BLL;

namespace Tests
{
    [TestFixture]
    public class loadTests
    {

        //[Test]
        //public void LoadFromFileQuestionnaire()
        //{

        //	ExcelLoader loader = new ExcelLoader();
        //    var questions =	loader.LoadQuestions("c:/Users/Sergey.DESKTOP-6MTKSUM/Documents/TextInput.xlsx");
        //	Assert.Greater(questions.Count(),0);	
        //          Assert.AreEqual(0,loader.Errors.Count,string.Join(";",loader.Errors));

        //}

        [Test]
        public void LoadFromFileQuestionnaireGroup()
        {

            ExcelLoader loader = new ExcelLoader();
            var questionGroups = loader.LoadQuestions("c:/Users/Sergey.DESKTOP-6MTKSUM/Documents/TestQuestionnaire1.xlsx");
            Assert.Greater(questionGroups.Count(), 0);
            Assert.AreEqual(0, loader.Errors.Count, string.Join(";", loader.Errors));
            var shema = JsonConvert.SerializeObject(questionGroups[0].Schema, new Formatting());
            var options = JsonConvert.SerializeObject(questionGroups[0].Options, new Formatting());
        }

        [Test]
        public void LoadFromFileQuestionnaireGroupFull()
        {

            ExcelLoader loader = new ExcelLoader();
            var questionGroups = loader.LoadQuestions("c:/Users/Sergey.DESKTOP-6MTKSUM/Documents/TestQuestionnaire.xlsx");
            Assert.Greater(questionGroups.Count(), 0);
            Assert.AreEqual(0, loader.Errors.Count, string.Join(";", loader.Errors));
            var shema = JsonConvert.SerializeObject(questionGroups[0].Schema, new Formatting());
            var options = JsonConvert.SerializeObject(questionGroups[0].Options, new Formatting());
        }
    }




}

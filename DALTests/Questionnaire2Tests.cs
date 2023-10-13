using BLL;
using Core;
using DAL.Core;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Sasha.Lims.Tests.TestCore;

namespace Tests
{
    [TestFixture]
    public class Questionnaire2Tests: BaseTest
    {
        [TestCase(true)]
		[TestCase(false)]
		public void SaveImageSimple(bool withSave)
		{
			var im = saveImage(withSave);
			
			var uow = new UnitOfWork();
			var exists = uow.GetRepository<QuestionImage>().GetAll().Any(x => x.Name == im.name);
			Assert.That(exists);

		}


		[TestCase(true)]
		[TestCase(false)]
		public void DeleteImageWithqQuestionnaire(bool withSave)
		{
			var im = saveImage(withSave);
          im.questionnaire.Delete(base.UOW, _user);

			var uow = new UnitOfWork();
			var exists = uow.GetRepository<QuestionImage>().GetAll().Any(x => x.Name == im.name);
			Assert.That(!exists);

		}




        [TestCase(true)]
		[TestCase(false)]
		public void SaveImage(bool withSave)
		{
			var res = saveImage(true);//получил questionnaire c одним изображением
			var uow = new UnitOfWork();
	
			var json = JsonConvert.SerializeObject(res.questionnaire);
			var questionnaire = JsonConvert.DeserializeObject<QuestionnaireBo>(json);
			
			var name = Guid.NewGuid().ToString();
			questionnaire.Questions[0].QuestionImages.Add(new QuestionImage() { Name = name, Path = "path" });
			json = JsonConvert.SerializeObject(questionnaire);	

		    var model = new QuestionnaireModel(res.questionnaire.Id);
			model.SaveQuestionnaire(uow, json,null);


			var exists = uow.GetRepository<QuestionImage>().GetAll().Any(x => x.Name == res.name);
			Assert.That(exists);

		}







		private (QuestionnaireBo questionnaire, string name )saveImage(bool withSave)
		{
			var questionnaireRecord = new Questionnaire() { Name = Guid.NewGuid().ToString() };
			var questionnaire = new QuestionnaireBo(questionnaireRecord);
			var question = new QuestionBo() { Name = Guid.NewGuid().ToString() };
			questionnaire.Questions.Add(question);
			var name = Guid.NewGuid().ToString();
			question.QuestionImages.Add(new QuestionImage() { Name = name, Path = "path" });

			questionnaire.Save(base.UOW, _user,withSave);
			 if(withSave) base.UOW.Save();
			return (questionnaire, name );
		}


	}


}

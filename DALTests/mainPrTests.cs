using Microsoft.VisualStudio.TestTools.UnitTesting;


using DAL;

using Core;
using DAL.Core;
using Entity;
//using NUnit.Framework;

namespace Tests
{
    [TestClass()]
 
    public class mainPrTests
    {
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        UnitOfWork UOW = new UnitOfWork();

       [TestMethod()]
        public void UowTestUses()
        {
         var UOW = new UnitOfWork();
            {
               var r =  UOW.GetRepository<User>();
                var vjcfs = r.GetAll().ToList();
                logger.Debug("Список объектов:");
                foreach (var u in vjcfs)
                {
                    logger.Debug($"{u.Id}.{u.Name} - {u.Email}");
                }
            }
         }
    
    
        [TestMethod()]
        public void UowTestRead()
        {
         
            {
               var r =  UOW.GetRepository<Vjsf>();
                var vjcfs = r.GetAll().ToList();
                logger.Debug("Список объектов:");
                foreach (var u in vjcfs)
                {
                    logger.Debug($"{u.Id}.{u.Name} - {u.Code}");
                }
            }
         }

        [TestMethod()]
        public void UowTestCreate()
        {

            {
                var r = UOW.GetRepository<Vjsf>();
                var vjsf = new Vjsf() { Name="Name",Code="Code",QuestionnaireId=0};
                r.Create(vjsf);
                r.Save();
 
            }
        }

        [TestMethod()]
        public void UowTestCreate2()
        {
            {
                var r = UOW.GetRepository<Questionnaire>();
                var vjsf = new Questionnaire() { Name = "Name1", Text = "Code1"  };
                r.Create(vjsf);
                r.Save();
            }
        }

        [TestMethod()]
        public void UowTestCreateAnswer()
        {

            {
                var r = UOW.GetRepository<Answer>();
                var ansver  = new Answer() { Name = "Name1", Value = "Code1",QuestionnarieId=99  };
                r.Create(ansver);
                r.Save();

            }
        }
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Sasha.Lims.WebUI.Areas.Questions;
using Core;
//using NUnit.Framework;

namespace ConsoleDB.Tests
{
    [TestClass()]
  
    public class mainPrTests
    {  NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        [TestMethod()]
        public void maTest()
        {

            using (QContext db = new QContext())
            {
                // получаем объекты из бд и выводим на консоль
                var vjcfs = db.Users.ToList();
                logger.Debug("Список объектов:");
                foreach (var u in vjcfs)
                {
                    logger.Debug($"{u.Id}.{u.Name} - {u.UserOstId}- {u.Email}");
                }
            }

          using (QContext db = new QContext())
            {
                // получаем объекты из бд и выводим на консоль
                var vjcfs = db.Vjsf.ToList();
                logger.Debug("Список схем:");
                foreach (var u in vjcfs)
                {
                    logger.Debug($"{u.Id}.{u.Name} - {u.Code}");
                }
            }

        }

      
    
    
        [TestMethod()]
        public void UowTestRead()
        {
         var UOW = new UnitOfWork();
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
            var UOW = new UnitOfWork();
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
            var UOW = new UnitOfWork();
            {
                var r = UOW.GetRepository<Questionnaire>();
                var vjsf = new Questionnaire() { Name = "Name1", Code = "Code1"  };
                r.Create(vjsf);
                r.Save();

            }
        }

        [TestMethod()]
        public void UowTestCreateAnswer()
        {
            var UOW = new UnitOfWork();
            {
                var r = UOW.GetRepository<Answer>();
                var ansver  = new Answer() { Name = "Name1", Value = "Code1",QuestionnarieId=99  };
                r.Create(ansver);
                r.Save();

            }
        }
    }
}
using NUnit.Framework;
using NUnit.Framework.Internal;
using Sasha.Lims.Tests.TestCore;
using Entity;
using System.Dynamic;
using DAL.Core;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static NPOI.POIFS.Crypt.CryptoFunctions;

namespace Tests
{
    [TestFixture]
    //создание модели любого типа
    public class NotesForAnswerTests : BaseTest
    {

        [Test]
        public void LoadTest()
        {
            var r = UOW.GetRepository<AnswerNote>();
            var vjcfs = r.GetAll().ToList();
            log.Debug("Список объектов:");
            foreach (var u in vjcfs)
            {
                log.Debug($"{u.Id}.{u.Note} - {u.AnswerId}");
            }
        }

        [Test]
        public void AddTest()
        {
            var r = UOW.GetRepository<AnswerNote>();
            var an = new AnswerNote() { AnswerId = 1,Note="note", Cdate=DateTime.Now,Ldate=DateTime.Now};
            r.Create(an);
            r.Save();

            var items = r.GetAll().ToList();
            log.Debug("Список объектов:");
            foreach (var u in items)
            {
                log.Debug($"{u.Id}.{u.Note} - {u.AnswerId}");
            }
        }




		//[TestCase(Сase.add)]
		//[TestCase(Сase.add)]
		//[TestCase(Сase.add)]
		//[TestCase(Сase.add)]
		//[TestCase(Сase.add)]
		//[TestCase(Сase.add)]
         [TestCase(Сase.edit)]
//        [TestCase(Сase.remove)]
        public void AddFromModelTest(Сase cases)
        {
            UnitOfWork.RemembeNewEntity = false;
			var answer = UOW.GetRepository<SimpleAnswer>().FirstOrDefault();
            var model = new MVC.Models.AnswerNodesModel((int)answer.Id); 
            var notes = UOW.GetRepository<AnswerNote>().Where(x=>x.AnswerId == answer.Id).ToList();
            var note="Доброе утро "+Guid.NewGuid().ToString();
            if (notes.Count == 0)   { 
                        model.AddNote(note);
                        notes = UOW.GetRepository<AnswerNote>().Where(x=>x.AnswerId == answer.Id).ToList();
                    }
            
            int? nodeId = null;
            switch (cases)
            {
                case Сase.add: model.AddNote(note);break;

                case Сase.edit: nodeId = notes[0].Id; model.EditNote((int)nodeId, note);break;
                case Сase.remove: 
                        nodeId = notes[0].Id;
                        model.DeleteNote((int)nodeId);  break;
            }
            

             notes = UOW.GetRepository<AnswerNote>().Where(x=>x.AnswerId == answer.Id).ToList();

         switch (cases)
            {
                case Сase.add: 
                case Сase.edit:
                     Assert.True(notes.Any(x=>x.Note==note));
                    ;break;
                case Сase.remove: Assert.False(notes.Any(x => x.Id == nodeId)) ; break;
            }
           

        }

      
    }
    public  enum Сase { add,edit,remove}

}

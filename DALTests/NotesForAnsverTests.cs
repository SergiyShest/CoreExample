using NUnit.Framework;
using NUnit.Framework.Internal;
using Sasha.Lims.Tests.TestCore;
using Entity;
using System.Dynamic;
using DAL.Core;
using Newtonsoft.Json;

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


        [Test]
        public void AddFromModelTest()
        {
            var r = UOW.GetRepository<AnswerNote>();
            var an = new AnswerNote() { AnswerId = 1, Note = "note", Cdate = DateTime.Now, Ldate = DateTime.Now };
            r.Create(an);
            r.Save();

            var items = r.GetAll().ToList();
            log.Debug("Список объектов:");
            foreach (var u in items)
            {
                log.Debug($"{u.Id}.{u.Note} - {u.AnswerId}");
            }
        }

    }
}

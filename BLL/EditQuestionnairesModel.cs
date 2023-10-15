
using Newtonsoft.Json;
using NLog;
using DAL;
using System.Dynamic;
using Core;
using DAL.Core;
using System.Net.Http.Formatting;
using Entity;

namespace BLL
{
    public class QuestionnairesModel
    {
        public string UserName { get; set; }
        protected Logger log = LogManager.GetCurrentClassLogger();
        public int? Id { get; set; }

        IUnitOfWorkEx UOW;

        public QuestionnairesModel(IUnitOfWorkEx uow)
        {
            UOW = uow;
        }

        public void InsertMain(string body)
        {
            var form = new FormDataCollection(body);
            var values = form.Get("values");

            var newQ = new Questionnaire();

            JsonConvert.PopulateObject(values, newQ);
            CheckName(null, newQ.Name);
            UOW.Save(newQ);
        }

        public void UpdateMain(string body)
        {
            var form = new FormDataCollection(body);
            var values = form.Get("values");
            var id = Convert.ToInt32(form.Get("key"));

            var quest = UOW.GetRepository<Questionnaire>().FirstOrDefault(x => x.Id == id, false);
            if (id == null) { throw new Exception($"Questionnaire with id ={id} dont exists"); }
            var prMain = quest.Main == true;
            JsonConvert.PopulateObject(values, quest);
            if (quest.Main == true && !prMain)
            {

                var prevMain = UOW.GetRepository<Questionnaire>().FirstOrDefault(x => x.Main == true, false);
                if (prevMain != null)
                    prevMain.Main = false;
            }
            CheckName(id, quest.Name);
            UOW.Save();

        }

        public void Delete(string body)
        {
            var form = new FormDataCollection(body);
            var values = form.Get("values");
            var id = Convert.ToInt32(form.Get("key"));
            var bo = new QuestionnaireBo(id);
            bo.Delete(UOW, null);

        }

        public void Upload(int? id,string content,bool deleteQuestions)
        {
            var questionniare = JsonConvert.DeserializeObject<QuestionnaireBo>(content);
            if (questionniare == null) { throw new ApplicationException("now file"); }
            CheckName(id, questionniare.Name);
            if (id != null)
            {
                var row = UOW.GetRepository<Questionnaire>().FirstOrDefault(x => x.Id == id);

                if (row != null)
                {
                    var q = new QuestionnaireBo(row);
                    if(deleteQuestions)
                        foreach (var question in q.Questions)
                        {
                            question.Delete(UOW, null, false);
                        }
                }
            }
            questionniare.Id = id;
            foreach (var q in questionniare.Questions)
            { q.Id = null; }
            questionniare.Save(UOW, null);
        }

        private void CheckName(int? id, string? name)
        {
            var exists = UOW.GetRepository<Questionnaire>().GetAll().Any(x => x.Name == name && x.Id != id);
            if (exists) { throw new ApplicationException("Questionnaire with the same name already exists. Change  name please."); }
        }
        
        #region not used

        protected static void NullTest(object type, string name)
        {
            if (type == null) { throw new ArgumentNullException($"Parametr {name} can not be null"); }
        }

        public virtual string GetContextMenuItemsFromSettings()
        {
            dynamic expOb = new ExpandoObject();
            string json;
            try
            {
                var items = new List<JsContextMenuItem>();

                items = new List<JsContextMenuItem>()
                    {

                    new JsContextMenuItem(0){ Name = "Add ",    Slug= "addChaild" ,AvaiableCodes=new List<string>{ } },
                    new JsContextMenuItem(0){ Name = "Edit",    Slug= "edit"    ,AvaiableCodes=new List<string>{ } } ,
                    new JsContextMenuItem(0){ Name = "Delete" , Slug= "delete"   ,AvaiableCodes=new List<string>{ } },
                    new JsContextMenuItem(0){ Name = "Copy"   , Slug= "copy"   ,AvaiableCodes=new List<string>{ } }
                    };

                expOb.Item = items;

            }
            catch (Exception ex)
            {
                expOb.Errors = new List<ObjectError>() { new ObjectError("", ex.GetAllMessages()) };
                log.Error(ex);
            }
            var jsonResolver = new PropertyRenameAndIgnoreSerializerContractResolver();


            jsonResolver.RenameProperty(typeof(JsContextMenuItem), "Name", "name");
            jsonResolver.RenameProperty(typeof(JsContextMenuItem), "Slug", "slug");
            jsonResolver.RenameProperty(typeof(JsContextMenuItem), "NeedContent", "needContent");
            jsonResolver.RenameProperty(typeof(JsContextMenuItem), "AvaiableCodes", "avaiableCodes");

            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = jsonResolver;//
            json = JsonConvert.SerializeObject(expOb, serializerSettings);
            return json;
        }
        #endregion

    }
}
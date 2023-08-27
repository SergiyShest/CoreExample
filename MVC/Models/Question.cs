using Core;
using DAL;
using Newtonsoft.Json;
using System.Dynamic;

namespace Sasha.Lims.WebUI.Areas.Questions
{
    public class Question : BaseObj<Vjsf>
    {
        public string Name { get; set; }

        public string Code { get; set; }


        public dynamic Schema
        {

            get
            {
                if (Record.Schema == null)
                    Record.Schema = getDefaultShema();
                return Record.Schema;
            }
            set { Record.Schema = value; }
        }

        private dynamic getDefaultShema()
        {
            dynamic dynObj = new ExpandoObject();
            dynObj.type = "object";
            dynObj.properties = new ExpandoObject();
            dynamic ListProp = new ExpandoObject();

            ListProp.type = "number";
            ListProp.title = "Choose the right Ansver";
            ListProp.x_display = "radio";
            ListProp.x_fromData = "context.Items";
            ListProp.x_itemKey = "Id";
            ListProp.x_itemTitle = "Name";
            ListProp.description = "This description is used as a help message.";

            dynObj.properties.ListProp = ListProp;


            return dynObj;
        }

        public dynamic Options
        {
            get
            {
                if (Record.Options == null)
                    Record.Options = getDefaultOptions();
                return Record.Options;
            }
            set { Record.Options = value; }
        }

        private dynamic getDefaultOptions()
        {
            dynamic dynObj = new ExpandoObject();
            dynamic context = new ExpandoObject();
            context.Items = new List<NamedInt>()
            {
                new NamedInt { Id = 1, Name = "Ansver 1" },
                new NamedInt { Id = 2, Name = "Ansver 2" },
                new NamedInt { Id = 3, Name = "Ansver 3" }
            };
            dynObj.context = context;
            dynObj.selectAll = true;
            return dynObj;
        }

        #region Constructors
        public Question(int? id)

        {
            this.Id = id;
            Code = "QUESTION";
        }
        public Question(Vjsf record) : base(record)
        {
            Code = "QUESTION";
        }
        public Question() : base()
        {
            Code = "QUESTION";
        }
        #endregion

        internal string Error { get; private set; }



        public int? QuestionnaireId 
        { get { return Record.QuestionnaireId; } 
         set { Record.QuestionnaireId = value; } 
        }

        public override void Save(UserDTO user)
        {
            Code = "QUESTION";
            if (Id <= 0)
            {
                SetId(0);
            }
            base.Save(user);
        }

    }
}


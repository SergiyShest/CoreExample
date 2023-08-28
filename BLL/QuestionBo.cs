using Core;
using DAL;
using Newtonsoft.Json;
using System.Dynamic;

namespace BLL {
    public class QuestionBo : BaseObj<Vjsf>
    {
        public string Name { get { return Record?.Name; } 
            set { Record.Name = value; } }

        public string Code { get { return Record?.Code; } 
            set { Record.Code = value; } }

		public int? Order { get { return Record?.Description; } 
            set { Record.Description = value; } }

		public dynamic Schema
        {
            get
            {
                if (Record.Schema == null)
                    Record.Schema = getDefaultShema();
                return JsonConvert.DeserializeObject(Record.Schema);
            }
            set { 
                Record.Schema = value;
            }
        }

        private string getDefaultShema()
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
            return   JsonConvert.SerializeObject( dynObj);
        }

        public dynamic Options
        {
            get
            {
                if (Record.Options == null)
                    Record.Options = getDefaultOptions();
                  return JsonConvert.DeserializeObject(Record.Options); 
            }
            set { Record.Options = value; }
        }

        private string getDefaultOptions()
        {
            dynamic dynObj = new ExpandoObject();
            dynamic context = new ExpandoObject();
            context.Items = new List<NamedInt>()
            {
                new NamedInt { Id = 1, Name = "Ansver first" },
                new NamedInt { Id = 2, Name = "Ansver second" },
                new NamedInt { Id = 3, Name = "Ansver next" }
            };
            dynObj.context = context;
            dynObj.selectAll = true;
			return JsonConvert.SerializeObject(dynObj);
		}

		internal string Error { get; private set; }



		public int? QuestionnaireId
		{
			get { return Record.QuestionnaireId; }
			set { Record.QuestionnaireId = value; }
		}

		public string Description
		{
			get { return Record?.Code; }
			set { Record.Code = value; }
		}



		#region Constructors
		public QuestionBo(int? id)

        {
            this.Id = id;
            Code = "QUESTION";
        }
        public QuestionBo(Vjsf record) : base(record)
        {
            Code = "QUESTION";
        }
        public QuestionBo() : base()
        {
            Code = "QUESTION";
        }
        #endregion



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


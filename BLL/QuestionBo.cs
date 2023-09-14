using Core;
using DAL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Dynamic;
using System.Text.Json.Nodes;

namespace BLL
{
    [JsonObject]
    public class QuestionBo : BaseObj<Vjsf>
    {
        #region Properties
        #region Name
        public string Name
        {
            get { return Record?.Name; }
            set { Record.Name = value; }
        }
        #endregion

        #region Text

        public string Text
        {
            get { return Record?.Code; }
            set { Record.Code = value; }
        }
        #endregion

        #region Order
        public int? Order
        {
            get { return Record?.Order; }
            set { Record.Order = value; }
        }
        #endregion

        #region NextButtonText
        public string? NextButtonText
        {
            get { return Record?.NextButtonText; }
            set { Record.NextButtonText = value; }
        }

        #endregion

        #region PrevButtonText
        public string? PrevButtonText
        {
            get { return Record?.PrevButtonText; }
            set { Record.PrevButtonText = value; }
        }

        #endregion
        
        #region BackGroundColor
        public string? BackGroundColor
        {
            get { return Record?.BackGroundColor; }
            set { Record.BackGroundColor = value; }
        }
        #endregion

        #region ForeColor
        public string? ForeColor
        {
            get { return Record?.ForeColor; }
            set { Record.ForeColor = value; }
        }
        #endregion

        #region Font
        public string? Font
        {
            get { return Record?.Font; }
            set { Record.Font = value; }
        }
        #endregion

        #region Schema

        public dynamic Schema
        {
            get
            {
                if (Record.Schema == null)
                    Record.Schema = getDefaultShema();
                return JsonConvert.DeserializeObject(Record.Schema);
            }
            set
            {
                if (value != null && value.GetType() == typeof(JObject))
                { 
                    Record.Schema = value.ToString(); 
                }
                else
                {
                    Record.Schema = value;
                }
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
            return JsonConvert.SerializeObject(dynObj);
        }

        #endregion

        #region Options

        public dynamic Options
        {
            get
            {
                if (Record.Options == null)
                    Record.Options = getDefaultOptions();
                return JsonConvert.DeserializeObject(Record.Options);
            }
            set
            {
                if (value != null && value.GetType() == typeof(JObject))
                {
                    Record.Options = value.ToString();
                }
                else
                    Record.Options = value;
            }
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

        #endregion

        #region QuestionnaireId

        public int? QuestionnaireId
        {
            get { return Record.QuestionnaireId; }
            set { Record.QuestionnaireId = value; }
        }

        #endregion

        #region Description

        public string Description
        {
            get { return Record?.Description; }
            set { Record.Description = value; }
        }

        #endregion

        internal string Error { get; private set; }

        #endregion
       
        #region Constructors
        public QuestionBo(int? id)

        {
            this.Id = id;

        }
        public QuestionBo(Vjsf record) : base(record)
        {

        }
        public QuestionBo() : base()
        {

        }
        #endregion

        public override void Save(UserDTO user)
        {

            if (Id <= 0)
            {
                SetId(0);
            }
            base.Save(user);
        }


    }
}


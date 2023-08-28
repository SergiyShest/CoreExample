using Core;
using DAL;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using NLog.Fluent;
using Sasha.Lims.WebUI.Areas.Questions;
using System.Xml.Linq;

namespace BLL
{
    public interface IVueModel
    {
    }

    /// <summary>
    /// Comment
    /// </summary>
    public class Comment : BaseObj<questionnarieAnswer>
    {
        #region Fields and properties

        #endregion
        #region constructors
        public Comment() : base()
        {
        }
        public Comment(int? id) : base(id)
        {
        }
        public Comment(questionnarieAnswer record) : base(record)
        {
        }
        #endregion

    }



    public class QuestionnarieAnswers : Comment
    {

        public int QuestionnarieId
        {
            get { return JsonAttrOb.QuestionnarieId; }
            set { JsonAttrOb.QuestionnarieId = value; }
        }

        public dynamic QuestionnarieAnsvers
        {
            get { return JsonAttrOb.QuestionnarieAnsvers; }
            set { JsonAttrOb.QuestionnarieAnsvers = value; }
        }
        public string Error { get; set; }

        #region constructors

        public QuestionnarieAnswers(int? id) : base(id)
        {
        }

        public QuestionnarieAnswers(questionnarieAnswer record) : base(record)
        {
            //if (record.commentType_id != (int)CommentType.QuestionarieAnsvers)
            //{
            //    throw new ApplicationException("commentType_id mast be equals" + (int)CommentType.QuestionarieAnsvers);
            //}
            //if (record.table_id != (int)TableType.Patient)
            //{
            //    throw new ApplicationException("table_id mast be equals" + (int)TableType.Patient);
            //}
        }

        public QuestionnarieAnswers(int patientId, int questonnarieId)
        {
            //Record.table_id = (int)TableType.Patient;
            //Record.commentType_id = (int)CommentType.QuestionarieAnsvers;
            //Record.row_id = patientId;
            //this.QuestionnarieId = questonnarieId;

        }
        #endregion

        JsonAttr _jsonAttr;
        private JsonAttr JsonAttrOb
        {
            get
            {
                if (_jsonAttr == null)
                {
                    if (!string.IsNullOrWhiteSpace(Record.Value))
                    {
                        try
                        {
                            _jsonAttr = JsonConvert.DeserializeObject<JsonAttr>(Record.Value);
                        }
                        catch (Exception ex)
                        {
                            var message = $"Error while read  item (field value must be in correct json format) table a_userField id={Id} ){ex}";

                            Log.Error(message);
                            this.Error = message;
                            _jsonAttr = new JsonAttr();
                        }
                    }
                    else
                    {
                        _jsonAttr = new JsonAttr();
                    }

                }
                return _jsonAttr;
            }
        }

        public override void Save(UserDTO user)
        {
            Record.Value = JsonConvert.SerializeObject(JsonAttrOb);
            base.Save(user);
        }


        class JsonAttr
        {

            public int QuestionnarieId { get; set; }
            public dynamic QuestionnarieAnsvers { get; set; }
        }


    }

}
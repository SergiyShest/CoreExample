

using Core;
using System.Text.Json.Serialization;

namespace Sasha.Lims.WebUI.Areas.Questions
{
    public class ObjectError
    {

        public ObjectError(string objName, string error)
        {
            Name = objName;
            Errors = new List<string> { error };
        }

        public ObjectError(IBaseObject obBaseObject, List<string> errors)
        {
            Object = obBaseObject;
            Errors = errors;
        }

        public ObjectError(IBaseObject obBaseObject, Exception ex)
        {
            Object = obBaseObject;
            this.ex = ex;
            Errors = new List<string> { ex.Message };
        }

        [JsonIgnore]
        public IBaseObject Object { get; set; }

        public int? Id
        {
            get { return Object?.Id; }
        }

        string _name;
        private Exception ex;

        public string Name
        {
            get
            {
                if (_name == null)
                {
                    var namedObj = Object as INamed;
                    if (namedObj != null)
                    {
                        { _name = $" id={namedObj.Name} "; }
                    }
                    else { _name = $" id={Object?.Id} "; }
                }

                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public List<string> Errors { get; set; }


    }



}
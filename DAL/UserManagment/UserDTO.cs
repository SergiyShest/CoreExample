using Core;
using DAL.Core;
using NLog.Fluent;

namespace DAL
{
    public class UserDTO
    {
        public string Id { get; set; }
        public int UserId { get; set; }
    }

    public class JsContextMenuItem
    {

        public string Error { get; set; }

        private IEntity _property;
        #region properties
        public int Id
        {
            get { return (int)_property.Id; }
        }
        string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Slug
        {
            get
            {
                return dopAttr.Slug;
            }
            set
            {
                dopAttr.Slug = value;
            }
        }

        public int? Order
        {
            get { return dopAttr.Order; }
        }

        private DopAttr _dopAttr;
        internal DopAttr dopAttr
        {
            get
            {
                if (_dopAttr == null)
                {
                 //   if (!string.IsNullOrWhiteSpace(_property.value))
                    {
                        try
                        {
                           // _dopAttr = JsonConvert.DeserializeObject<DopAttr>(_property.value);
                        }
                        catch (Exception ex)
                        {
                           var message =
                               $"Error while read menu item (field value must be in correct json format) table a_prop  id={_property.Id} ){ex}";

                            Log.Error(message);
                            Error = message;
                            _dopAttr = new DopAttr();
                            //throw new Exception(message);
                        }
                    }
                   // else
                    {
                        _dopAttr = new DopAttr();
                    }
                }
                return _dopAttr;
            }

            set
            {
                _dopAttr = value;

            }
        }

        public List<string> AvaiableCodes
        {
            get
            {
                return dopAttr.AvaiableCodes;
            }
            set
            {
                dopAttr.AvaiableCodes = value;
            }
        }

        public bool NeedContent
        {
            get
            {
                return dopAttr.NeedContent;
            }
            set
            {
                dopAttr.NeedContent = value;
            }
        }

        public bool Disabled
        {
            get
            {
                return dopAttr.Disabled;
            }
            set
            {
                dopAttr.Disabled = value;
            }
        }



        #endregion

        #region constructor
        public JsContextMenuItem()
        {//PropsType typeId
          //  _property = new a_prop();
          //  _property.prop_id = (int)typeId;
          //  _property.code = "CONTEXT_MENU_ITEM";
        }



        public JsContextMenuItem(int propId)
        {
        //    _property = prop;
        }

        #endregion


        public void SerializeValue()
        {
           // _property.value = JsonConvert.SerializeObject(dopAttr);
        }

        public void Save(IUnitOfWorkEx uow)
        {
            SerializeValue();
           // var rep = uow.GetRepository<a_prop>();
            //rep.Save(_property);
        }


        internal class DopAttr
        {
            public string Slug;

           public int Order { get; set; } = 0;

            public bool Disabled { get; set; } = false;

            public List<string> AvaiableCodes { get; set; }
            public bool NeedContent { get; set; }
        }
    }


}
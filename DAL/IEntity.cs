using DAL;
using DAL.Core;
using NLog;

using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Core

{
    public interface INamed
    {
        string Name { get; }
    }
    public interface INamedInt : INamed
    { 
        int Id { get; }
    }


    public interface IEntity
    { 
        int? Id { get; set; }
    }

    public class NamedInt : INamedInt
    {
        public NamedInt() { }
        public NamedInt(int? id, string name)
        {
            Id = (int)id;
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            var name = Name.Replace(" ", "");

            return name;
        }
    }



    public class NamedValue : INamed
    {
        public NamedValue() { }
        public NamedValue(string name, object value)
        {
            Value = value;
            Name = name;
        }
        public object Value { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            var name = Name.Replace(" ", "");
            return name;
        }
    }



    public interface IBaseObject
    {
        int? Id { get; }

        IEntity Record { get; }

        void Save(UserDTO user);

        void Delete(UserDTO user);

        List<string> Validate(bool throwEx = false);


    }




    public abstract class BaseObj<T> : IBaseObject where T : class, IEntity, new()
    {

      protected  IUnitOfWorkEx _uow = new UnitOfWork();

        #region Fields and properties



        #region Logger

        private static Logger _log = LogManager.GetCurrentClassLogger();
        protected Logger Log => _log;

        #endregion

        #region Id
        int? _id;
        public virtual int? Id
        {
            get
            {
                if (_record != null)
                {
                    return _record.Id;
                }
                return _id;
            }

            set
            {
                _id = value;
                if (_record != null && value != null)
                {
                    _record.Id = (int)value;
                }
            }

        }

        public virtual void SetId(int id)
        {
            _id = id;
            if (_record != null)
                _record.Id = id;
        }


        #endregion

        #region DbProperties
        protected List<NamedValue> DbValues { get; set; }

        PropertyInfo[] _dbProperties;
        PropertyInfo[] GetProperties(T record)
        {

            if (_dbProperties == null && record != null)
            {
                _dbProperties = typeof(T).GetProperties();
            }
            return _dbProperties;

        }


        #endregion

        #region Record

        protected T _record;
        [JsonIgnore]
        public bool IsSerialased { get; set; }



        [JsonIgnore]
        public T Record
        {
            get
            {
                if (_record == null)
                {
                    T record=null;
                    if (Id != null)
                    {

                        record = _uow.Get<T>((int)Id);
                        DbValues = RecordValues(record);
                        if (record == null)
                        {
                            throw new ApplicationException($"Dont find id {Id} for type {typeof(T).FullName}");
                        }


                    }
                    else
                    {
                        record = new T();
                    }
                    _record = record;
                }
                return _record;
            }
            set
            {
                if (value != null && IsSerialased)
                {
                    SetProperties(value);
                }
                _record = value;
                //if (_record != null)
                //{
                //	Id = _record.id;
                //}
            }
        }
        [JsonIgnore]
        IEntity IBaseObject.Record => Record;



        /// <summary>
        /// установка свойств которые были установлены в объекте который не был начитан из базы, но он не новый
        /// </summary>
        protected virtual void SetProperties(T record)
        {

        }

        #endregion

        #endregion

        #region constructors

        public BaseObj()
        {
        }

        public BaseObj(int? id)
        {
            if (id.HasValue)
                SetId((int)id);
        }

        protected BaseObj(T record, bool useRecordValues = false)
        {
            UseRecordValues = useRecordValues;
            Record = record;
            DbValues = RecordValues(record);
        }

        #endregion

        #region Methods

        public virtual void Load()
        {
            var x = Record; //
        }

        public virtual void Save(UserDTO user)
        {
            if (IsSerialased)
            {
                if (Id == null || Id == 0)
                {
                    Record = new T();
                }
                else
                {
                    Record = _uow.Get<T>((int)Id);
                }
                IsSerialased = false;
            }
            var valResult = Validate(true);

            if (valResult.Count > 0)
            {
                string error = string.Join(Environment.NewLine, valResult);
                throw new ValidationException(error);
            }
            _uow.Save(Record);
            Id = Record.Id;
        }

        public virtual void Delete(UserDTO user)
        {
            if (Record.Id != null)
            {
              _uow.Delete<T>((int)Record.Id);
            }
           
        }

        public virtual List<string> Validate(bool throwEx = false)
        {
            
            return ValidationHelper.Validate(Record, throwEx);
            return null;
        }

        /// <summary>
        /// if true? then save Db Values and work Changes ToDo:Check maybe same functional have in entyty
        /// </summary>
        protected bool UseRecordValues;

        List<NamedValue> RecordValues(T record)
        {
            if (!UseRecordValues) return null;
            if (record == null) return null;
            var copy = new List<NamedValue>();
            var props = GetProperties(record);
            foreach (var pr in props)
            {
                var val = pr.GetValue(record);
                copy.Add(new NamedValue(pr.Name, val));
            }

            return copy;
        }


        //public List<ChangedValue> Changes()
        //{
        //    if (_record == null || DbValues == null) return null;
        //    var changes = new List<ChangedValue>();
        //    var props = GetProperties(Record);
        //    foreach (var pr in props)
        //    {
        //        var dbValue = DbValues.FirstOrDefault(x => x.Name == pr.Name)?.Value;
        //        var value = pr.GetValue(Record);

        //        if (!object.Equals(dbValue, value))

        //        {
        //            changes.Add(new ChangedValue(pr.Name, value, dbValue));
        //        }
        //    }

        //    return changes;
        //}


        #endregion

    }

    public class c_patient : IEntity
    {
        public int? id { get; set; }
        public int? Id { get; set; }
    }

}
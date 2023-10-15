
using Newtonsoft.Json;
using NLog;
using Entity;
using DAL.Core;
using System.Net.Http.Formatting;
using Core;

namespace BLL
{
    public class AvaiableUserModel: BaseModel<AvaiableUser>
    {
        public AvaiableUserModel(IUnitOfWorkEx uow) : base(uow) { }
    }

    public class UserModel: BaseModel<User>
    {
        public UserModel(IUnitOfWorkEx uow) : base(uow) { }
    }


    public class BaseModel<T> where T : class, IEntity, new()
    {

        protected Logger log = LogManager.GetCurrentClassLogger();
        public int? Id { get; set; }

        IUnitOfWorkEx UOW;

        public BaseModel(IUnitOfWorkEx uow)
        {
            UOW = uow;
        }

        public virtual void Insert(string body)
        {
            var form = new FormDataCollection(body);
            var values = form.Get("values");

            var newQ = new T();

            JsonConvert.PopulateObject(values, newQ);
            UOW.Save(newQ);
        }

        public virtual void Update(string body)
        {
            var form = new FormDataCollection(body);
            var values = form.Get("values");
            var id = Convert.ToInt32(form.Get("key"));

            var quest = UOW.GetRepository<T>().FirstOrDefault(x => x.Id == id, false);

            JsonConvert.PopulateObject(values, quest);
  
            UOW.Save();

        }

        public virtual void Delete(string body)
        {
            var form = new FormDataCollection(body);
            var id = Convert.ToInt32(form.Get("key"));

            UOW.GetRepository<T>().Delete(id);

        }

 
    }
}
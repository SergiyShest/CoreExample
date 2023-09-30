//using Microsoft.VisualStudio.TestTools.UnitTesting;
using CookieReaders.Providers.Repositories;
using CookieReaders.Providers;
using DAL;
using DAL.Core;
using NUnit.Framework;
using Sasha.Lims.Tests.TestCore;
using CookieReaders.Models;

namespace Tests
{
    public class UserTests: BaseTest
    {

        [Test]
        public void UowTestUsers()
        {
         
                var r = UOW.GetRepository<AvaiableUser>();
                var vjcfs = r.GetAll().ToList();
                log.Debug("Список объектов:");
                foreach (var u in vjcfs)
                {
                    log.Debug($"{u.Id}.{u.Comment} - {u.Email}");
                }
        }

        [Test]
        public void RegisterUniqueUser()
        {
            var avUser = UOW.GetRepository<AvaiableUser>().FirstOrDefault(x => x.Email == "ff");
            if (avUser == null)
            {
                avUser = new AvaiableUser() {Email = "ff" };
                UOW.GetRepository<AvaiableUser>().Create(avUser);
            }
            var  userRepository = new UserRepository();
            RegisterVm model= new RegisterVm() { Password="123",ConfirmPassword="123",EmailAddress="ff"};
            var user = userRepository.Register(model);
            Assert.Throws<ApplicationException>(()=>userRepository.Register(model));//register user second time

        }

        [Test]
        public void TryRegisterUserNotInAvaiableList()
        { 

         var user=   UOW.GetRepository<AvaiableUser>().FirstOrDefault(x=>x.Email=="ff");
           if(user!=null)
            {
                UOW.GetRepository<AvaiableUser>().Remove(user);
            }
            var  userRepository = new UserRepository();
            RegisterVm model= new RegisterVm() { Password="123",ConfirmPassword="123",EmailAddress="ff"};

            Assert.Throws<ApplicationException>(() => userRepository.Register(model));//register not in list


        }
    }
}
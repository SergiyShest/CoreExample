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
    public class UserTests : BaseTest
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
            CreateAvaiableUserIfNotExists("ff");
            RemoveUserIfExists();
            var userRepository = new UserRepository();


            RegisterVm model = new RegisterVm() { Password = "123", ConfirmPassword = "123", EmailAddress = "ff" };
            var user = userRepository.Register(model);
            Assert.Throws<ApplicationException>(() => userRepository.Register(model));//register user second time

        }

     

        [Test]
        public void TryRegisterUserNotInAvaiableList()
        {
           
              RemoveAvaiableUserIfExists("ff");
              RemoveUserIfExists();
            var userRepository = new UserRepository();
            RegisterVm model = new RegisterVm() { Password = "123", ConfirmPassword = "123", EmailAddress = "ff" };

            Assert.Throws<ApplicationException>(() => userRepository.Register(model));//register not in list


        }
   private void RemoveUserIfExists()
        {
            var existsUser = UOW.GetRepository<User>().FirstOrDefault(x => x.Email.ToLower() == "ff");
            if (existsUser != null)
            {
                UOW.GetRepository<User>().Remove(existsUser);
            }
        }



        private void CreateAvaiableUserIfNotExists(string name)
        {
            var avUser = UOW.GetRepository<AvaiableUser>().FirstOrDefault(x => x.Email == name.ToLower());
            if (avUser == null)
            {
                avUser = new AvaiableUser() { Email = "ff" };
                UOW.GetRepository<AvaiableUser>().Create(avUser);
            }
        }

        private void RemoveAvaiableUserIfExists(string name)
        {
            var avUser = UOW.GetRepository<AvaiableUser>().FirstOrDefault(x => x.Email.ToLower() == name.ToLower());
            if (avUser != null)
            {
                UOW.GetRepository<AvaiableUser>().Remove(avUser);
            }
        }
    }
}
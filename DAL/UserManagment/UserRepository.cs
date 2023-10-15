using System;
using System.Linq;
using CookieReaders.Models;

using DAL;
using DAL.Core;
using Entity;

namespace CookieReaders.Providers.Repositories
{
    public interface IUserRepository
    {
        UserItem Register(RegisterVm model);
        UserItem Validate(LoginVm model);
    }

    public class UserRepository : IUserRepository
    {
        private readonly UnitOfWork _db;

        public IRepos<AvaiableUser> AvaiableUsers { get { return _db.GetRepository<AvaiableUser>(); } }

        public IRepos<User> UsersRep { get { return _db.GetRepository<User>(); } }

        public UserRepository()
        {
            _db = new UnitOfWork();
        }

        public UserItem Validate(LoginVm model)
        {
            var emailRecords = UsersRep.Where(x => x.Email == model.EmailAddress);

            var results = emailRecords.AsEnumerable()
            .Where(m => m.PasswordHash == Hasher.GenerateHash(model.Password, m.Salt))
            .Select(m => new UserItem
            {
                UserId = m.Id,
                EmailAddress = m.Email,
                Name = m.Name,
                CreatedUtc = m.Cdate
            });

            return results.FirstOrDefault();
        }

        public UserItem Register(RegisterVm model)
        {
           var avaiable = AvaiableUsers.FirstOrDefault(x => x.Email.ToLower() == model.EmailAddress.ToLower());
            if (avaiable == null) {
                throw new ApplicationException("User with email "+model.EmailAddress+ " Not in the list of allowed users!");
            }

            var emailRecords = UsersRep.FirstOrDefault(x => x.Email == model.EmailAddress);
            if (emailRecords != null) {
                throw new ApplicationException("User with email "+model.EmailAddress+" already exists!");
            }

            var salt = Hasher.GenerateSalt();
            var hashedPassword = Hasher.GenerateHash(model.Password, salt);

            var user = new User
            {
                Email = model.EmailAddress,
                PasswordHash = hashedPassword,
                Salt = salt,
                Name = model.Name,
                Cdate = DateTime.UtcNow
            };

            UsersRep.Create(user);
            UsersRep.Save();

            return new UserItem
            {
                UserId = user.Id,
                EmailAddress = user.Email,
                Name = user.Name,
                CreatedUtc = user.Cdate
            };
        }
    }
}
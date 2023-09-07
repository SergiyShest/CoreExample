using System;
using System.Linq;
using CookieReaders.Models;

using DAL;
using DAL.Core;

namespace CookieReaders.Providers.Repositories
{
    public interface IUserRepository
    {
        CookieUserItem Register(RegisterVm model);
        CookieUserItem Validate(LoginVm model);
    }

    public class UserRepository : IUserRepository
    {
        private readonly UnitOfWork _db;

        public IRepos<User> UsersRep { get { return _db.GetRepository<User>(); } }

        public UserRepository()
        {
            _db = new UnitOfWork();
        }

        public CookieUserItem Validate(LoginVm model)
        {
            var emailRecords = UsersRep.Where(x => x.Email == model.EmailAddress);

            var results = emailRecords.AsEnumerable()
            .Where(m => m.PasswordHash == Hasher.GenerateHash(model.Password, m.Salt))
            .Select(m => new CookieUserItem
            {
                UserId = m.Id,
                EmailAddress = m.Email,
                Name = m.Name,
                CreatedUtc = m.CDate
            });

            return results.FirstOrDefault();
        }

        public CookieUserItem Register(RegisterVm model)
        {
            var salt = Hasher.GenerateSalt();
            var hashedPassword = Hasher.GenerateHash(model.Password, salt);

            var user = new User
            {
//                Id = Guid.NewGuid(),
                Email = model.EmailAddress,
                PasswordHash = hashedPassword,
                Salt = salt,
                Name = "Some User",
                CDate = DateTime.UtcNow
            };

            UsersRep.Create(user);
            UsersRep.Save();

            return new CookieUserItem
            {
                UserId = user.Id,
                EmailAddress = user.Email,
                Name = user.Name,
                CreatedUtc = user.CDate
            };
        }
    }
}
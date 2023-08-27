

namespace DAL;

public class UsersService
{
     NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

    public List<User> GetUsers()
    {
<<<<<<< HEAD
        List<DAL.User > users=new List<DAL.User>();

        using (QContext db = new QContext())
=======
        List<DAL.User> users = new List<DAL.User>();
        using (var db = new KorfinContext())
>>>>>>> 76eca1be0fbef6e8b49a9ddfcc88158acbe126e8
        {
            // получаем объекты из бд и выводим на консоль
            users = db.Users.ToList();
            logger.Debug("Список объектов:");
            foreach (DAL.User u in users)
            {
                logger.Debug($"{u.Id}.{u.Name} - {u.Email}");
            }
        }
        return users;
    }

}
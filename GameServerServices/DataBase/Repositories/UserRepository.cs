using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using GameServerServices.DataBase.Contexts;
using GameServerServices.DataBase.Models;

namespace GameServerServices.DataBase.Repositories
{
    public class UserRepository : IRepository<User>
    {
        
        public UserRepository(IDataContext dataContext)
        {
            UserSet = dataContext.Set<User>();
        }
        protected DbSet<User> UserSet { get; private set; }
        public IQueryable<User> Get() => UserSet;

        public void Add(User user)
        {
            if (user == null)
            {
                throw new ArgumentException();
            }

            UserSet.Attach(user);
            UserSet.Add(user);
        }

        public void Update(int userId, User newUser)
        {

            var userToChange = UserSet.Find(userId);

            if (userToChange != null)
            {
                UserSet.Attach(userToChange);
                userToChange.Email = newUser.Email;
                userToChange.Nickname = newUser.Nickname;
                userToChange.PasswordHash = newUser.PasswordHash;
            }
        }

        public void Remove(int userId)
        {
            var userToRemove =  UserSet.Find(userId);

            if (userToRemove != null)
            {
                UserSet.Attach(userToRemove);
                UserSet.Remove(userToRemove);
            }
        }

        public async Task RemoveAsync(int userId)
        {
            var userToRemove = await UserSet.FindAsync(userId);

            if (userToRemove != null)
            {
                UserSet.Attach(userToRemove);
                UserSet.Remove(userToRemove);
            }
        }


        public async Task UpdateAsync(int userId, User newUser)
        {
            var userToChange = await UserSet.FindAsync(userId);

            if (userToChange != null)
            {
                UserSet.Attach(userToChange);
                userToChange.Email = newUser.Email;
                userToChange.Nickname = newUser.Nickname;
                userToChange.PasswordHash = newUser.PasswordHash;
            }
        }
    }
}
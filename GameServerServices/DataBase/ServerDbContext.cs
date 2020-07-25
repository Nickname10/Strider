using System.Data.Entity;
using GameServerServices.DataBase.Models;

namespace GameServerServices.DataBase
{
    public class ServerDbContext : DbContext
    {
        public ServerDbContext() : base("MsSqlServer")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Character> Characters { get; set; }
    }
}
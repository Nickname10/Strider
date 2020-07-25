using System.Data.Entity;
using System.Threading.Tasks;

namespace GameServerServices.DataBase.Contexts
{
    public class DataContext : IDataContext
    {
        private readonly ServerDbContext _appContext;

        public DataContext(ServerDbContext appDbContext)
        {
            _appContext = appDbContext;
        }

        public Task SaveChangesAsync() => _appContext.SaveChangesAsync();
        public void SaveChanges() => _appContext.SaveChanges();
        public DbSet<T> Set<T>() where T : class => _appContext.Set<T>();
    }
}
using System.Data.Entity;
using System.Threading.Tasks;

namespace GameServerServices.DataBase.Contexts
{
    public interface IDataContext
    {
        DbSet<T> Set<T>() where T : class;
        Task SaveChangesAsync();
    }
}
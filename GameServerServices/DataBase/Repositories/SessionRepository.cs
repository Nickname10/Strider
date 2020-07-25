using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using GameServerServices.DataBase.Contexts;
using GameServerServices.DataBase.Models;

namespace GameServerServices.DataBase.Repositories
{
    public class SessionRepository:IRepository<Session>
    {

        public SessionRepository(IDataContext dataContext)
        {
            SessionSet = dataContext.Set<Session>();
        }
        protected DbSet<Session> SessionSet { get; private set; }
        public IQueryable<Session> Get() => SessionSet;

        public void Add(Session session)
        {
            if (session == null)
            {
                throw new ArgumentException();
            }

            SessionSet.Attach(session);
            SessionSet.Add(session);
        }

        public async Task RemoveAsync(int sessionId)
        {
            var sessionToRemove = await SessionSet.FindAsync(sessionId);

            if (sessionToRemove != null)
            { 
                SessionSet.Attach(sessionToRemove);
                SessionSet.Remove(sessionToRemove);
            }
        }


        public async Task UpdateAsync(int sessionId, Session session)
        {
            var sessionToChange = await SessionSet.FindAsync(sessionId);

            if (sessionToChange != null)
            {
                SessionSet.Attach(sessionToChange);
                sessionToChange.StartTime = session.StartTime;
                sessionToChange.EndTime = session.EndTime;
                sessionToChange.MaxPlayers = session.MaxPlayers;
                sessionToChange.Name = session.Name;
                sessionToChange.Creator = session.Creator;
                sessionToChange.UserId = session.UserId;
            }
        }

        public void Dispose()
        {

        }
    }
}
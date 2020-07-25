using System;
using GameServerServices.DataBase.Contexts;
using GameServerServices.DataBase.Repositories;

namespace GameServerServices.DataBase
{
    public class WorkUnit
    {
        private static WorkUnit _instance;
        
        public static WorkUnit Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                            _instance = new WorkUnit();
                    }
                }
                return _instance;
            }
        }

        private static readonly object SyncRoot = new object();
        private readonly DataContext _dataContext;
        public UserRepository Users { get; }
        public MessageRepository Messages { get; }
        public CharacterRepository Characters { get; }

        private WorkUnit()
        {
            _dataContext = new DataContext(new ServerDbContext());
            Users = new UserRepository(_dataContext);
            Messages = new MessageRepository(_dataContext);
            Characters = new CharacterRepository(_dataContext);


        }

        public void Save() => _dataContext.SaveChanges();
        public void SaveAsync() => _dataContext.SaveChangesAsync();
    }
}
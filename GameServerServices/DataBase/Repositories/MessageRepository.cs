using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using GameServerServices.DataBase.Contexts;
using GameServerServices.DataBase.Models;

namespace GameServerServices.DataBase.Repositories
{
    public class MessageRepository:IRepository<Message>
    {

        public MessageRepository(IDataContext dataContext)
        {
            MessageSet = dataContext.Set<Message>();
        }
        protected DbSet<Message> MessageSet { get; private set; }
        public IQueryable<Message> Get() => MessageSet;

        public void Add(Message message)
        {
            if (message == null)
            {
                throw new ArgumentException();
            }

            MessageSet.Attach(message);
            MessageSet.Add(message);
        }

        public void Update(int messageId, Message newMessage)
        {
            var messageToChange = MessageSet.Find(messageId);

            if (messageToChange != null)
            {
                MessageSet.Attach(messageToChange);
                messageToChange.DateTime = newMessage.DateTime;
                messageToChange.Text = newMessage.Text;
            }
        }

        public void Remove(int messageId)
        {
            var messageToRemove = MessageSet.Find(messageId);

            if (messageToRemove != null)
            {
                MessageSet.Attach(messageToRemove);
                MessageSet.Remove(messageToRemove);
            }
        }

        public async Task RemoveAsync(int messageId)
        {
            var messageToRemove = await MessageSet.FindAsync(messageId);

            if (messageToRemove != null)
            {
                MessageSet.Attach(messageToRemove);
                MessageSet.Remove(messageToRemove);
            }
        }


        public async Task UpdateAsync(int messageId, Message newMessage)
        {
            var messageToChange = await MessageSet.FindAsync(messageId);

            if (messageToChange != null)
            {
                MessageSet.Attach(messageToChange);
                messageToChange.DateTime = newMessage.DateTime;
                messageToChange.Text = newMessage.Text;
            }
        }

    }
}
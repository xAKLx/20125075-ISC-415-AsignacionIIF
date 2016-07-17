using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class MessageRepository
    {
        MessageContext context = null;

        public MessageRepository(MessageContext db)
        {
            context = db;
        }

        public void Add(Message m)
        {
            context.Messages.Add(m);
            context.SaveChanges();
        }

        public ICollection<Message> Find(string sender, string receiver)
        {
            var list = new List<Message>();

            foreach (var message in context.Messages)
                if (message.Sender == sender && message.Receiver == receiver)
                    list.Add(message);

            return list;
        }

        public IEnumerable GetMessages()
        {
            return context.Messages;
        }
    }
}
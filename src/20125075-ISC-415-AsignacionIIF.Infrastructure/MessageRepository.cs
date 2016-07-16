using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _20125075_ISC_415_AsignacionIIF.Core;
using _20125075_ISC_415_AsignacionIIF.Core.Interfaces;

namespace _20125075_ISC_415_AsignacionIIF.Infrastructure
{
    public class MessageRepository : IMessageRepository
    {
        DataContext context = new DataContext();

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

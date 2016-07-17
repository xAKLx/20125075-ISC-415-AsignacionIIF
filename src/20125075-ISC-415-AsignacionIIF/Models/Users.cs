using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20125075_ISC_415_AsignacionIIF.Models
{
    public class Users
    {
        private static Users uniqueInstance = null;

        public Dictionary<String, DateTime> userList = new Dictionary<String, DateTime>();
        //public Dictionary<Tuple<String, String>, List<Message>> userMessages = new Dictionary<Tuple<String, String>, List<Message>>();
        public Dictionary<String, String> userImages = new Dictionary<String, String>();
        

        private Users()
        {

        }

        public static Users getUniqueInstance()
        {
            if (null == uniqueInstance)
                uniqueInstance = new Users();
            return uniqueInstance;
        }

        public ICollection<Message> getOrderedMessages(String user1, String user2, MessageContext db)
        {
            MessageRepository repo = new MessageRepository(db);

            var list = new List<Message>();

            list.AddRange(repo.Find(user1, user2));
            list.AddRange(repo.Find(user2, user1));

            return list.OrderBy(o => o.Date).ToList();
        }

        public bool addMessage(String sender, String receiver, String content, MessageContext db)
        {
            MessageRepository repo = new MessageRepository(db);

            repo.Add(new Message()
            {
                Sender = sender,
                Receiver = receiver,
                Content = content,
                Date = DateTime.Now
            });

            return true;
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20125075_ISC_415_AsignacionIIF.Core.Interfaces
{
    interface IMessageRepository
    {
        void Add(Message m);

        IEnumerable GetMessages();

        ICollection<Message> Find(String sender, String receiver);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20125075_ISC_415_AsignacionIIF.Models
{
    public class Message
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Content { get; set; }

		public Message(string to, string from, string content)
        {
            To = to;
            From = from;
            Content = content;
        }
    }
}

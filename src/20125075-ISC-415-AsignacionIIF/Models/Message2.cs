using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _20125075_ISC_415_AsignacionIIF.Models
{
    public class olddMessage
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Content { get; set; }

		public olddMessage(string to, string from, string content)
        {
            To = to;
            From = from;
            Content = content;
        }
    }
}

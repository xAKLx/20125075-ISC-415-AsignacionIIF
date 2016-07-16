using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20125075_ISC_415_AsignacionIIF.Core
{
    public class Message
    {
        [Key]
        [Required]
        [MinLength(3)]
        public String Sender { get; set; }

        [Key]
        [Required]
        [MinLength(3)]
        public String Receiver { get; set; }

        [Required]
        public String Content { get; set; }

        [Key]
        [Required]
        public DateTime Date { get; set; }
    }
}

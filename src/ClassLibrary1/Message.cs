using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
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
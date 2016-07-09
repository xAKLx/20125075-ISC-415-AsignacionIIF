using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _20125075_ISC_415_AsignacionIIF.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Name")]
        [MinLength(3)]
        public string Name { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Raymax.Models
{
    public class Terms
    {
        [Key]
        public int ID { get; set; }
        [Display(Name ="Terms & Condition")]
        public string TermsANDPolicy { get; set; }
    }
}

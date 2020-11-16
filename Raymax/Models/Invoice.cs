using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Raymax.Models
{
    public class Invoice
    {
        [Key]
        [Display(Name = "Invoice Number")]
        public int ID { get; set; }
        [Required]
        [Display(Name ="Customer Name")]
        public string CustomerName { get; set; }
        public string Address { get; set; }
        [Required]
        public string Contact { get; set; }
        [Display(Name = "Date")]
        public DateTime Date_Created { get; set; }
        
        [Display(Name = "Pay")]
        public double Amount_Paid { get; set; }
        [Display(Name = "Change")]
        public double Amount_Due { get; set; }
        public double Subtotal { get; set; }
        [Display(Name = "VAT Rate")]
        public double TaxRate { get; set; }
        [Display(Name = "VAT Amount")]
        public double TaxAmount { get; set; }
        [Display(Name = "Total")]
        public double GrandTotal { get; set; }
        public List<Product> Products { get; set; }
    }
}

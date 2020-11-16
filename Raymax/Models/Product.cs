using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Raymax.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Product ID")]
        public string ProductID { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }

        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
    }
}

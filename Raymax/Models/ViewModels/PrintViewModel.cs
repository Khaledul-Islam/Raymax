using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raymax.Models.ViewModels
{
    public class PrintViewModel
    {
        public Invoice Invoice { get; set; }
        public IEnumerable<Terms> Terms { get; set; }
    }
}

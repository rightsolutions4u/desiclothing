using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DesiClothing4u.Common.Models
{
    public class VendorProduct
    { 
        [Key]
        public int ProductId { get; set; }
        public Vendor Vendor { get; set; }
        public IEnumerable<Product> Product { get; set; }
        public IEnumerable<Category> Category { get; set; }
        public IEnumerable<Picture> Picture { get; set; }
       
    }
}

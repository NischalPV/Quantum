using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quantum.Web.Areas.Products.Models
{
    public class ProductViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string HSNCode { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Quantum.Core.Entities
{
    public class Product : BaseEntityFull<string>
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string HSNCode { get; set; }
        
        [Required]
        public double Price { get; set; }
        
        public string Description { get; set; }

        public Product()
        {
            Id = Guid.NewGuid().ToString();
        }

        public Product(string createdById)
        {
            CreatedById = createdById;
        }

        public Product(string name, string hsnCode, double price, string description, string modifiedById)
        {
            Name = name;
            HSNCode = hsnCode;
            Price = price;
            Description = description;
            ModifiedById = modifiedById;
            ModifiedDate = DateTime.UtcNow;
        }
    }
}

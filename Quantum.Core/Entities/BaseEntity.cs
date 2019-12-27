using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Quantum.Core.Entities
{
    public class BaseEntity<T>
    {
        public T Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime? ModifiedDate { get; set; }

        public BaseEntity()
        {
            CreatedDate = DateTime.UtcNow;
            IsActive = true;
        }
    }

    public class BaseEntityFull<T> : BaseEntity<T>
    {
        //[ForeignKey(nameof(CreatedBy))]
        public string CreatedById { get; set; }

        //[ForeignKey(nameof(ModifiedBy))]
        public string ModifiedById { get; set; }
        [NotMapped]
        public virtual ApplicationUser CreatedBy { get; set; }
        
        [NotMapped]
        public virtual ApplicationUser ModifiedBy { get; set; }

    }
}

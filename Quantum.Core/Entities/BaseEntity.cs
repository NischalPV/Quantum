using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Quantum.Core.Entities
{
    public class BaseEntityStripped<T>
    {
        public T Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime? ModifiedDate { get; set; }

        public BaseEntityStripped()
        {
            CreatedDate = DateTime.UtcNow;
            IsActive = true;
        }
    }

    public class BaseEntityFull<T>
    {
        public T Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime? ModifiedDate { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        public string CreatedById { get; private set; }

        [ForeignKey(nameof(ModifiedBy))]
        public string ModifiedById { get; set; }

        public virtual ApplicationUser CreatedBy { get; set; }
        public virtual ApplicationUser ModifiedBy { get; set; }

        public BaseEntityFull()
        {
            CreatedDate = DateTime.UtcNow;
            IsActive = true;
        }

    }
}

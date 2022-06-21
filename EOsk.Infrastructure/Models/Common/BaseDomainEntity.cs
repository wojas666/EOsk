using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOsk.Infrastructure.Models.Common
{
    public abstract class BaseDomainEntity
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime DateCreated { get; set; }

        public string? CratedBy { get; set; }

        public DateTime LastDateModified { get; set; }

        public string? LastModifiedBy { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ProjectPrintDos.Models
{
    public class PaymentType
    {
        [Key]
        public int PaymentTypeID { get; set; }
        [Required]
        [StringLength(20)]
        public string Type { get; set; }
        [Required]
        [StringLength(20)]
        public string AccountNumber { get; set; }
        [Required]
        public int IsActive { get; set; }
        [Required]
        public int IsPrimary { get; set; }

        // ForeignKey
        [Required]
        public ApplicationUser User { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
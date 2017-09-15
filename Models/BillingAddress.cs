using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ProjectPrintDos.Models
{
    public class BillingAddress
    {
        [Key]
        public int BillingAddressID { get; set; }

        [Required]
        public string Street { get; set; }
        
        public int? Unit { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required(ErrorMessage = "Valid Zip is Required")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Zip Code")]
        public int ZipCode { get; set; }

        public bool IsDefault { get; set; }
        
        [Required]
        public ApplicationUser User { get; set; }

        public virtual ICollection<Order> Order { get; set; }

    }

}
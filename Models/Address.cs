using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace projectPrint.Models
{
    public class Address
    {
        [Key]
        public int AddressID { get; set; }

        [Required]
        public string Street { get; set; }
        
        [Required]
        public int Unit { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required(ErrorMessage = "Valid Zip is Required")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Zip Code")]
        public int ZipCode { get; set; }

        public int ? IsBillingAddress { get; set; }
        public int ? IsShippingAddress { get; set; }
        public int ? IsDefault { get; set; }
        
        [Required]
        public ApplicationUser User { get; set; }

        public virtual ICollection<Order> Order { get; set; }

    }

}
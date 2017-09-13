using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ProjectPrintDos.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "First Name")]
        [StringLength(40)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string LastName { get; set; }

        public virtual ICollection<BillingAddress> BillingAddresses { get; set; }
        public virtual ICollection<ShippingAddress> ShippingAddresses { get; set; }
        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<PaymentType> PaymentType { get; set; }
    }
}

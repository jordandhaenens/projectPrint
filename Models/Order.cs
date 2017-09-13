using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ProjectPrintDos.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        public int ? IsFulfilled { get; set; } 
        [Required]
        public ApplicationUser User { get; set; }
        
        // ForeignKey
        // This property will be set in a ViewModel
        public int ? PaymentTypeID { get; set; }
        public PaymentType PaymentType { get; set; }
        
        // ForeignKey
        // This property will be set in a ViewModel
        public int ? ShippingAddressID { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        
        // ForeignKey
        // This property will be set in a ViewModel
        public int ? BillingAddressID { get; set; }
        public BillingAddress BillingAddress { get; set; }

        public virtual ICollection<CompositeProduct> CompositeProduct { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace projectPrint.Models
{
    public class ProductType
    {
        [Key]
        public int ProductTypeID { get; set; }

        // Type and Size
        [Required]
        public string Title { get; set; }

        [Required]
        public string BaseColor { get; set; }
        
        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Cost { get; set ;}

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        public virtual ICollection<CompositeProduct> CompositeProduct { get; set; }
    }
}
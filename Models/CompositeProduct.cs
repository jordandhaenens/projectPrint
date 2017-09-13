using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ProjectPrintDos.Models
{
    public class CompositeProduct
    {
        [Key]
        public int CompositeProductID { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }

        // ForeignKey
        public int ? ProductTypeID { get; set; }
        public ProductType ProductType { get; set; }

        // ForeignKey
        public int ? InkID { get; set; }
        public Ink Ink { get; set; }

        // ForeignKey
        public int ? ScreenID { get; set; }
        public Screen Screen { get; set; }

        // ForeignKey
        public int ? OrderID { get; set; }
        public Order Order { get; set; }

    }
}
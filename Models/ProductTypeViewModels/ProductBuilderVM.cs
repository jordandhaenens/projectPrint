using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ProjectPrintDos.Models;
using ProjectPrintDos.Data;

namespace ProjectPrintDos.Models.ProductTypeViewModels
{
    public class ProductBuilderVM
    {
        // I need to present all the screens
        public IEnumerable<Screen> Screens { get; set; }

        // I need to present all the inks
        public IEnumerable<Ink> Inks { get; set; }

        // I need to present the single product type that was clicked on
        public ProductType ProductType { get; set; }

        // Pass in DB, ProductType
        public ProductBuilderVM(ApplicationDbContext ctx, ProductType pt)
        {
            Screens = ctx.Screen.Where(s => s.Quantity > 1);
            Inks = ctx.Ink.Where(i => i.Quantity > 1);
            ProductType = pt;
        }
    }
}
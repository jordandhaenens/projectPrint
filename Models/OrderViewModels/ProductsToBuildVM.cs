using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Linq;
using System.Collections;
using ProjectPrintDos.Data;
using Microsoft.EntityFrameworkCore;

namespace ProjectPrintDos.Models.OrderViewModels
{
    public class ProductsToBuildVM
    {
        public IEnumerable<CompositeProduct> Products { get; set; }
        public IEnumerable<CompositeProduct> OrderedProducts { get; set; }


        public ProductsToBuildVM(ApplicationDbContext ctx)
        {
            Products = ctx.CompositeProduct
                .Include(c => c.Order)
                .Include(c => c.Screen)
                .Include(c => c.Ink)
                .Include(c => c.ProductType)
                .Where(c => c.Order.IsFulfilled == null && c.Order.PaymentTypeID != null);

            // This works but needs optimization to list by ordering count or priority
            OrderedProducts = Products.OrderBy(p => p.ScreenID).ThenBy(p => p.InkID);
                

        }
    }
}
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
    public class ProductsToBuild
    {
        public IEnumerable<CompositeProduct> Products { get; set; }
        public IEnumerable<CompositeProduct> OrderedProducts { get; set; }


        public ProductsToBuild(ApplicationDbContext ctx)
        {
            Products = ctx.CompositeProduct
                .Include(cp => cp.Order)
                .Where(cp => cp.Order.IsFulfilled == null && cp.Order.PaymentTypeID != null);

            // This works but needs optimization to list by ordering count or priority
            OrderedProducts = Products.OrderBy(p => p.ScreenID).ThenBy(p => p.InkID);
                

        }
    }
}
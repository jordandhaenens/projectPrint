using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Collections.Generic;
using ProjectPrintDos.Models;
using ProjectPrintDos.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ProjectPrintDos.Models.OrderViewModels
{
    public class ViewCartVM
    {
        // All CompositeProducts on the selected order
        public IEnumerable<CompositeProduct> userProducts { get; set; }
        public double ? ShoppingCartTotal { get; set; }

        public ViewCartVM(ApplicationDbContext ctx, Order order)
        {
            // userProducts = order.CompositeProduct;
            userProducts = ctx.CompositeProduct
                .Include(cp => cp.ProductType)
                .Include(cp => cp.Ink)
                .Include(cp => cp.Screen)
                .Where(cp => cp.OrderID == order.OrderID);

            // Get the total price of all items in cart 
            ShoppingCartTotal = userProducts.Sum(cp => cp.Price);
        }
    }
}
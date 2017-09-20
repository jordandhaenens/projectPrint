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
    public class CloseOrderVM
    {
        public ShippingAddress DefaultShippingAddress { get; set; }
        public BillingAddress DefaultBillingAddress { get; set; }
        public PaymentType DefaultPaymentType { get; set; }
        public ApplicationUser User { get; set; }
        public Order Order { get; set; }
        public IEnumerable<CompositeProduct> UserProducts { get; set; }
        public double? ShoppingCartTotal { get; set; }

        public CloseOrderVM(ApplicationDbContext ctx, int orderID, ApplicationUser user)
        {
            DefaultShippingAddress = ctx.ShippingAddress.SingleOrDefault(sa => sa.User == user && sa.IsDefault == true);
           
            DefaultBillingAddress = ctx.BillingAddress.SingleOrDefault(ba => ba.User == user && ba.IsDefault == true);
           
            DefaultPaymentType = ctx.PaymentType.SingleOrDefault(pt => pt.User == user && pt.IsActive == true && pt.IsPrimary == true);
           
            User = user;
           
            Order = ctx.Order.SingleOrDefault(o => o.OrderID == orderID && o.User == user);
           
            UserProducts = ctx.CompositeProduct
                .Include(cp => cp.ProductType)
                .Include(cp => cp.Ink)
                .Include(cp => cp.Screen)
                .Where(cp => cp.OrderID == orderID);
           
            ShoppingCartTotal = UserProducts.Sum(cp => cp.Price);
        }

    }
}
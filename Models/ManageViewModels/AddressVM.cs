using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectPrintDos.Data;

namespace ProjectPrintDos.Models.ManageViewModels
{
    public class AddressVM
    {
        public IEnumerable<BillingAddress> BillingAddresses { get; set; }
        public IEnumerable<ShippingAddress> ShippingAddresses { get; set; }

        public AddressVM(ApplicationDbContext ctx, ApplicationUser user)
        {
            BillingAddresses =  ctx.BillingAddress.Where(ba => ba.User == user).ToList();
            ShippingAddresses = ctx.ShippingAddress.Where(sa => sa.User == user).ToList();
        }
    }
}
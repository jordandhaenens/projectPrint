using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectPrintDos.Data;

namespace ProjectPrintDos.Models.ManageViewModels
{
    // This class is authored by Jordan Dhaenens
    // This View Model is used in the Manage Controller / action PaymentTypes()
    public class GetPaymentTypesVM
    {
        public IEnumerable<PaymentType> UserPaymentTypes { get; set; }

        public GetPaymentTypesVM(ApplicationDbContext ctx, ApplicationUser user)
        {
            UserPaymentTypes = ctx.PaymentType.Where(pt => pt.User == user && pt.IsActive == true).ToList();
        }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ProjectPrintDos.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ProjectPrintDos.Models;

namespace ProjectPrintDos.Models.PaymentViewModels
{
    public class PaymentDeleteVM
    {
        public PaymentType PaymentType { get; set; }
        public Order Order { get; set; }
        public PaymentDeleteVM(ApplicationDbContext ctx, int id)
        {
            this.PaymentType = ctx.PaymentType.SingleOrDefault(pt => pt.PaymentTypeID == id);
            this.Order = ctx.Order.FirstOrDefault(o => o.PaymentTypeID == id);
        }
    }
}
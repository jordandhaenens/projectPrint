using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectPrintDos.Data;

namespace ProjectPrintDos.Models.ManageViewModels
{
    public class InventoryVM
    {
        public IEnumerable<Screen> Screens { get; set; }
        public IEnumerable<Ink> Inks { get; set; }
        public IEnumerable<ProductType> ProductTypes { get; set; }

        public InventoryVM(ApplicationDbContext ctx)
        {
            Screens = ctx.Screen.OrderBy(s => s.Quantity).ToList();
            Inks = ctx.Ink.OrderBy(i => i.Quantity).ToList();
            ProductTypes = ctx.ProductType.OrderBy(pt => pt.BaseColor).ToList();
            // ProductTypes = 
            //     from p in ctx.ProductType
            //     group p by p.BaseColor into g
            //     select new ProductType
            //     {
            //         ProductTypeID = g.Key;
            //     };
        }
    }
}
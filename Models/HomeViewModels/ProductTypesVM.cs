using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ProjectPrintDos.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ProjectPrintDos.Models;

namespace ProjectPrintDos.Models.HomeViewModels
{
    public class ProductTypesVM
    {
        public IEnumerable<ProductType> ProductTypes { get; set; }

        public ProductTypesVM(ApplicationDbContext ctx)
        {
            ProductTypes = ctx.ProductType.Where(pt => pt.Quantity > 1);
        }
    }
}
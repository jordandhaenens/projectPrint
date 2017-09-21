using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectPrintDos.Data;
using ProjectPrintDos.Models;
using ProjectPrintDos.Models.ProductTypeViewModels;

namespace ProjectPrintDos.Controllers
{
    public class ProductTypeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductTypeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;    
            _userManager = userManager;
        }

        // This action was modified by Jordan Dhaenens
        // GET: ProductType
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductType.OrderBy(pt => pt.BaseColor).ToListAsync());
        }

        // GET: ProductType/Details/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductType
                .SingleOrDefaultAsync(m => m.ProductTypeID == id);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        // This action was authored by Jordan Dhaenens
        // This action returns the Customer facing ProductType details
        // GET: ProductType/ProductDetail/4
        public async Task<IActionResult> ProductDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationUser user = await _userManager.GetUserAsync(User);
            ProductType productType = await _context.ProductType.SingleOrDefaultAsync(pt => pt.ProductTypeID == id && pt.Quantity > 1);
            if (productType == null)
            {
                return NotFound();
            }

            ProductBuilderVM model = new ProductBuilderVM(_context, productType);
            

            return View(model);
        }


        // GET: ProductType/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        
        // POST: ProductType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductTypeID,Title,Description,BaseColor,Cost,Price,Quantity,Img")] ProductType productType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productType);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(productType);
        }

        // GET: ProductType/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductType.SingleOrDefaultAsync(m => m.ProductTypeID == id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        // POST: ProductType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductTypeID,Description,Title,BaseColor,Cost,Price,Quantity,Img")] ProductType productType)
        {
            if (id != productType.ProductTypeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductTypeExists(productType.ProductTypeID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(productType);
        }

        // GET: ProductType/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductType
                .SingleOrDefaultAsync(m => m.ProductTypeID == id);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        // POST: ProductType/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productType = await _context.ProductType.SingleOrDefaultAsync(m => m.ProductTypeID == id);
            _context.ProductType.Remove(productType);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProductTypeExists(int id)
        {
            return _context.ProductType.Any(e => e.ProductTypeID == id);
        }
    }
}

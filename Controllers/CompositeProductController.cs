using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectPrintDos.Data;
using ProjectPrintDos.Models;

namespace ProjectPrintDos.Controllers
{
    public class CompositeProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompositeProductController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: CompositeProduct
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CompositeProduct.Include(c => c.Ink).Include(c => c.Order).Include(c => c.ProductType).Include(c => c.Screen);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CompositeProduct/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compositeProduct = await _context.CompositeProduct
                .Include(c => c.Ink)
                .Include(c => c.Order)
                .Include(c => c.ProductType)
                .Include(c => c.Screen)
                .SingleOrDefaultAsync(m => m.CompositeProductID == id);
            if (compositeProduct == null)
            {
                return NotFound();
            }

            return View(compositeProduct);
        }

        // GET: CompositeProduct/Create
        public IActionResult Create()
        {
            ViewData["InkID"] = new SelectList(_context.Ink, "InkID", "InkID");
            ViewData["OrderID"] = new SelectList(_context.Order, "OrderID", "UserId");
            ViewData["ProductTypeID"] = new SelectList(_context.ProductType, "ProductTypeID", "BaseColor");
            ViewData["ScreenID"] = new SelectList(_context.Screen, "ScreenID", "Title");
            return View();
        }

        // POST: CompositeProduct/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompositeProductID,DateCreated,ProductTypeID,InkID,ScreenID,OrderID")] CompositeProduct compositeProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(compositeProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["InkID"] = new SelectList(_context.Ink, "InkID", "InkID", compositeProduct.InkID);
            ViewData["OrderID"] = new SelectList(_context.Order, "OrderID", "UserId", compositeProduct.OrderID);
            ViewData["ProductTypeID"] = new SelectList(_context.ProductType, "ProductTypeID", "BaseColor", compositeProduct.ProductTypeID);
            ViewData["ScreenID"] = new SelectList(_context.Screen, "ScreenID", "Title", compositeProduct.ScreenID);
            return View(compositeProduct);
        }

        // GET: CompositeProduct/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compositeProduct = await _context.CompositeProduct.SingleOrDefaultAsync(m => m.CompositeProductID == id);
            if (compositeProduct == null)
            {
                return NotFound();
            }
            ViewData["InkID"] = new SelectList(_context.Ink, "InkID", "InkID", compositeProduct.InkID);
            ViewData["OrderID"] = new SelectList(_context.Order, "OrderID", "UserId", compositeProduct.OrderID);
            ViewData["ProductTypeID"] = new SelectList(_context.ProductType, "ProductTypeID", "BaseColor", compositeProduct.ProductTypeID);
            ViewData["ScreenID"] = new SelectList(_context.Screen, "ScreenID", "Title", compositeProduct.ScreenID);
            return View(compositeProduct);
        }

        // POST: CompositeProduct/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompositeProductID,DateCreated,ProductTypeID,InkID,ScreenID,OrderID")] CompositeProduct compositeProduct)
        {
            if (id != compositeProduct.CompositeProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compositeProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompositeProductExists(compositeProduct.CompositeProductID))
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
            ViewData["InkID"] = new SelectList(_context.Ink, "InkID", "InkID", compositeProduct.InkID);
            ViewData["OrderID"] = new SelectList(_context.Order, "OrderID", "UserId", compositeProduct.OrderID);
            ViewData["ProductTypeID"] = new SelectList(_context.ProductType, "ProductTypeID", "BaseColor", compositeProduct.ProductTypeID);
            ViewData["ScreenID"] = new SelectList(_context.Screen, "ScreenID", "Title", compositeProduct.ScreenID);
            return View(compositeProduct);
        }

        // GET: CompositeProduct/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compositeProduct = await _context.CompositeProduct
                .Include(c => c.Ink)
                .Include(c => c.Order)
                .Include(c => c.ProductType)
                .Include(c => c.Screen)
                .SingleOrDefaultAsync(m => m.CompositeProductID == id);
            if (compositeProduct == null)
            {
                return NotFound();
            }

            return View(compositeProduct);
        }

        // POST: CompositeProduct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var compositeProduct = await _context.CompositeProduct.SingleOrDefaultAsync(m => m.CompositeProductID == id);
            _context.CompositeProduct.Remove(compositeProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CompositeProductExists(int id)
        {
            return _context.CompositeProduct.Any(e => e.CompositeProductID == id);
        }
    }
}

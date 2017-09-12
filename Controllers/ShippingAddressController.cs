using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projectPrint.Data;
using projectPrint.Models;

namespace projectPrint.Controllers
{
    public class ShippingAddressController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShippingAddressController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: ShippingAddress
        public async Task<IActionResult> Index()
        {
            return View(await _context.ShippingAddress.ToListAsync());
        }

        // GET: ShippingAddress/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingAddress = await _context.ShippingAddress
                .SingleOrDefaultAsync(m => m.ShippingAddressID == id);
            if (shippingAddress == null)
            {
                return NotFound();
            }

            return View(shippingAddress);
        }

        // GET: ShippingAddress/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShippingAddress/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShippingAddressID,Street,Unit,City,State,ZipCode,IsDefault")] ShippingAddress shippingAddress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shippingAddress);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(shippingAddress);
        }

        // GET: ShippingAddress/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingAddress = await _context.ShippingAddress.SingleOrDefaultAsync(m => m.ShippingAddressID == id);
            if (shippingAddress == null)
            {
                return NotFound();
            }
            return View(shippingAddress);
        }

        // POST: ShippingAddress/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShippingAddressID,Street,Unit,City,State,ZipCode,IsDefault")] ShippingAddress shippingAddress)
        {
            if (id != shippingAddress.ShippingAddressID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shippingAddress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShippingAddressExists(shippingAddress.ShippingAddressID))
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
            return View(shippingAddress);
        }

        // GET: ShippingAddress/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingAddress = await _context.ShippingAddress
                .SingleOrDefaultAsync(m => m.ShippingAddressID == id);
            if (shippingAddress == null)
            {
                return NotFound();
            }

            return View(shippingAddress);
        }

        // POST: ShippingAddress/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shippingAddress = await _context.ShippingAddress.SingleOrDefaultAsync(m => m.ShippingAddressID == id);
            _context.ShippingAddress.Remove(shippingAddress);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ShippingAddressExists(int id)
        {
            return _context.ShippingAddress.Any(e => e.ShippingAddressID == id);
        }
    }
}

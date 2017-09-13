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
    public class BillingAddressController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BillingAddressController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: BillingAddress
        public async Task<IActionResult> Index()
        {
            return View(await _context.BillingAddress.ToListAsync());
        }

        // GET: BillingAddress/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billingAddress = await _context.BillingAddress
                .SingleOrDefaultAsync(m => m.BillingAddressID == id);
            if (billingAddress == null)
            {
                return NotFound();
            }

            return View(billingAddress);
        }

        // GET: BillingAddress/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BillingAddress/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BillingAddressID,Street,Unit,City,State,ZipCode,IsDefault")] BillingAddress billingAddress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(billingAddress);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(billingAddress);
        }

        // GET: BillingAddress/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billingAddress = await _context.BillingAddress.SingleOrDefaultAsync(m => m.BillingAddressID == id);
            if (billingAddress == null)
            {
                return NotFound();
            }
            return View(billingAddress);
        }

        // POST: BillingAddress/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BillingAddressID,Street,Unit,City,State,ZipCode,IsDefault")] BillingAddress billingAddress)
        {
            if (id != billingAddress.BillingAddressID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(billingAddress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillingAddressExists(billingAddress.BillingAddressID))
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
            return View(billingAddress);
        }

        // GET: BillingAddress/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billingAddress = await _context.BillingAddress
                .SingleOrDefaultAsync(m => m.BillingAddressID == id);
            if (billingAddress == null)
            {
                return NotFound();
            }

            return View(billingAddress);
        }

        // POST: BillingAddress/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var billingAddress = await _context.BillingAddress.SingleOrDefaultAsync(m => m.BillingAddressID == id);
            _context.BillingAddress.Remove(billingAddress);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool BillingAddressExists(int id)
        {
            return _context.BillingAddress.Any(e => e.BillingAddressID == id);
        }
    }
}

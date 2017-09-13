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
    public class PaymentTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentTypeController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: PaymentType
        public async Task<IActionResult> Index()
        {
            return View(await _context.PaymentType.ToListAsync());
        }

        // GET: PaymentType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentType = await _context.PaymentType
                .SingleOrDefaultAsync(m => m.PaymentTypeID == id);
            if (paymentType == null)
            {
                return NotFound();
            }

            return View(paymentType);
        }

        // GET: PaymentType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PaymentType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentTypeID,Type,AccountNumber,IsActive,IsPrimary")] PaymentType paymentType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paymentType);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(paymentType);
        }

        // GET: PaymentType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentType = await _context.PaymentType.SingleOrDefaultAsync(m => m.PaymentTypeID == id);
            if (paymentType == null)
            {
                return NotFound();
            }
            return View(paymentType);
        }

        // POST: PaymentType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentTypeID,Type,AccountNumber,IsActive,IsPrimary")] PaymentType paymentType)
        {
            if (id != paymentType.PaymentTypeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentTypeExists(paymentType.PaymentTypeID))
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
            return View(paymentType);
        }

        // GET: PaymentType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentType = await _context.PaymentType
                .SingleOrDefaultAsync(m => m.PaymentTypeID == id);
            if (paymentType == null)
            {
                return NotFound();
            }

            return View(paymentType);
        }

        // POST: PaymentType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paymentType = await _context.PaymentType.SingleOrDefaultAsync(m => m.PaymentTypeID == id);
            _context.PaymentType.Remove(paymentType);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PaymentTypeExists(int id)
        {
            return _context.PaymentType.Any(e => e.PaymentTypeID == id);
        }
    }
}

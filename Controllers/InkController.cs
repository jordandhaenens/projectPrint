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
    public class InkController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InkController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Ink
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ink.ToListAsync());
        }

        // GET: Ink/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ink = await _context.Ink
                .SingleOrDefaultAsync(m => m.InkID == id);
            if (ink == null)
            {
                return NotFound();
            }

            return View(ink);
        }

        // GET: Ink/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ink/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InkID,Title,Cost,Price,Quantity")] Ink ink)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ink);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(ink);
        }

        // GET: Ink/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ink = await _context.Ink.SingleOrDefaultAsync(m => m.InkID == id);
            if (ink == null)
            {
                return NotFound();
            }
            return View(ink);
        }

        // POST: Ink/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InkID,Title,Cost,Price,Quantity")] Ink ink)
        {
            if (id != ink.InkID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ink);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InkExists(ink.InkID))
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
            return View(ink);
        }

        // GET: Ink/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ink = await _context.Ink
                .SingleOrDefaultAsync(m => m.InkID == id);
            if (ink == null)
            {
                return NotFound();
            }

            return View(ink);
        }

        // POST: Ink/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ink = await _context.Ink.SingleOrDefaultAsync(m => m.InkID == id);
            _context.Ink.Remove(ink);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool InkExists(int id)
        {
            return _context.Ink.Any(e => e.InkID == id);
        }
    }
}

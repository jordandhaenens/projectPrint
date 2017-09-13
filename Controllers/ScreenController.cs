using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectPrintDos.Data;
using ProjectPrintDos.Models;

namespace ProjectPrintDos.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ScreenController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ScreenController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Screen
        public async Task<IActionResult> Index()
        {
            return View(await _context.Screen.ToListAsync());
        }

        // GET: Screen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var screen = await _context.Screen
                .SingleOrDefaultAsync(m => m.ScreenID == id);
            if (screen == null)
            {
                return NotFound();
            }

            return View(screen);
        }

        // GET: Screen/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Screen/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScreenID,Title,Cost,Price,Quantity")] Screen screen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(screen);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(screen);
        }

        // GET: Screen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var screen = await _context.Screen.SingleOrDefaultAsync(m => m.ScreenID == id);
            if (screen == null)
            {
                return NotFound();
            }
            return View(screen);
        }

        // POST: Screen/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScreenID,Title,Cost,Price,Quantity")] Screen screen)
        {
            if (id != screen.ScreenID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(screen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScreenExists(screen.ScreenID))
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
            return View(screen);
        }

        // GET: Screen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var screen = await _context.Screen
                .SingleOrDefaultAsync(m => m.ScreenID == id);
            if (screen == null)
            {
                return NotFound();
            }

            return View(screen);
        }

        // POST: Screen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var screen = await _context.Screen.SingleOrDefaultAsync(m => m.ScreenID == id);
            _context.Screen.Remove(screen);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ScreenExists(int id)
        {
            return _context.Screen.Any(e => e.ScreenID == id);
        }
    }
}

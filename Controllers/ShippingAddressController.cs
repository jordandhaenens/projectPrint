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

namespace ProjectPrintDos.Controllers
{
    [Authorize]
    public class ShippingAddressController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ShippingAddressController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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

        // This method is authored by Jordan Dhaenens
        // POST: ShippingAddress/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShippingAddressID,Street,Unit,City,State,ZipCode,IsDefault")] ShippingAddress shippingAddress)
        {
            ModelState.Remove("User");
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                shippingAddress.User = user;
                _context.Add(shippingAddress);
                await _context.SaveChangesAsync();
                return RedirectToAction("GetAddresses", "Manage");
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
            ModelState.Remove("User");
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (id != shippingAddress.ShippingAddressID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    shippingAddress.User = user;
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
                return RedirectToAction("GetAddresses", "Manage");
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
            return RedirectToAction("GetAddresses", "Manage");
        }

        private bool ShippingAddressExists(int id)
        {
            return _context.ShippingAddress.Any(e => e.ShippingAddressID == id);
        }
    }
}

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
        // public async Task<IActionResult> Index()
        // {
        //     return View(await _context.ShippingAddress.ToListAsync());
        // }

        // This action is modified by Jordan Dhaenens to restrict access to address details if User is not owner
        // GET: ShippingAddress/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ApplicationUser user = await _userManager.GetUserAsync(User);

            var shippingAddress = await _context.ShippingAddress
                .SingleOrDefaultAsync(m => m.ShippingAddressID == id && m.User == user);
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
                // Check DB for default ShippingAddress and set to false
                ShippingAddress formerDefaultShippingAddress = await _context.ShippingAddress.SingleOrDefaultAsync(sa => sa.IsDefault == true);
                if (shippingAddress.IsDefault == true && formerDefaultShippingAddress != null)
                {
                    formerDefaultShippingAddress.IsDefault = false;
                    _context.Update(formerDefaultShippingAddress);

                    shippingAddress.User = user;
                    _context.Add(shippingAddress);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("GetAddresses", "Manage");
                }
                else
                {
                    shippingAddress.User = user;
                    _context.Add(shippingAddress);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("GetAddresses", "Manage");
                }
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
            ApplicationUser user = await _userManager.GetUserAsync(User);

            var shippingAddress = await _context.ShippingAddress.SingleOrDefaultAsync(m => m.ShippingAddressID == id && m.User == user);
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
                    // Check DB for default ShippingAddress and set to false if one exists
                    ShippingAddress formerDefaultShippingAddress = await _context.ShippingAddress.SingleOrDefaultAsync(sa => sa.IsDefault == true);
                    if (shippingAddress.IsDefault == true && formerDefaultShippingAddress != null)
                    {
                        formerDefaultShippingAddress.IsDefault = false;
                        _context.Update(formerDefaultShippingAddress);

                        shippingAddress.User = user;
                        _context.Update(shippingAddress);
                        await _context.SaveChangesAsync();
                    }
                    // User is changing the current IsDefault address to false
                    else if (shippingAddress.IsDefault == false && formerDefaultShippingAddress.ShippingAddressID == id)
                    {
                        ShippingAddress updatedAddress = _context.ShippingAddress.SingleOrDefault(ba => ba.ShippingAddressID == id);
                        updatedAddress.IsDefault = false;
                        _context.Update(updatedAddress);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        shippingAddress.User = user;
                        _context.Update(shippingAddress);
                        await _context.SaveChangesAsync();
                    }
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

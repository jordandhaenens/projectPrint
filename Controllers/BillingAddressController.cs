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
    public class BillingAddressController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BillingAddressController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context; 
            _userManager = userManager;
        }

        // GET: BillingAddress
        // public async Task<IActionResult> Index()
        // {
        //     return View(await _context.BillingAddress.ToListAsync());
        // }

        // This action is modified by Jordan Dhaenens to restrict access to address details if User is not owner
        // GET: BillingAddress/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ApplicationUser user = await _userManager.GetUserAsync(User);

            var billingAddress = await _context.BillingAddress
                .SingleOrDefaultAsync(m => m.BillingAddressID == id && m.User == user);
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

        // This method is authored by Jordan Dhaenens
        // POST: BillingAddress/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BillingAddressID,Street,Unit,City,State,ZipCode,IsDefault")] BillingAddress billingAddress)
        {
            ModelState.Remove("User");
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                // Check DB for default BillingAddress and set to false if one exists
                BillingAddress formerDefaultBillingAddress = await _context.BillingAddress.SingleOrDefaultAsync(ba => ba.IsDefault == true);
                if (billingAddress.IsDefault == true && formerDefaultBillingAddress != null)
                {
                    formerDefaultBillingAddress.IsDefault = false;
                    _context.Update(formerDefaultBillingAddress);

                    billingAddress.User = user;
                    _context.Add(billingAddress);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("GetAddresses", "Manage");
                }
                else
                {
                    billingAddress.User = user;
                    _context.Add(billingAddress);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("GetAddresses", "Manage");
                }
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
            ApplicationUser user = await _userManager.GetUserAsync(User);

            var billingAddress = await _context.BillingAddress.SingleOrDefaultAsync(m => m.BillingAddressID == id && m.User == user);
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
            ModelState.Remove("User");
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (id != billingAddress.BillingAddressID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Check DB for default BillingAddress and set to false if one exists
                    BillingAddress formerDefaultBillingAddress = await _context.BillingAddress.SingleOrDefaultAsync(ba => ba.IsDefault == true);
                    // User is setting the address to default and another address is already assigned that status
                    if (billingAddress.IsDefault == true && formerDefaultBillingAddress != null)
                    {
                        formerDefaultBillingAddress.IsDefault = false;
                        _context.Update(formerDefaultBillingAddress);

                        billingAddress.User = user;
                        _context.Update(billingAddress);
                        await _context.SaveChangesAsync();
                    }
                    // User is changing the current IsDefault address to false
                    else if (billingAddress.IsDefault == false && formerDefaultBillingAddress.BillingAddressID == id)
                    {
                        BillingAddress updatedAddress = _context.BillingAddress.SingleOrDefault(ba => ba.BillingAddressID == id);
                        updatedAddress.IsDefault = false;
                        _context.Update(updatedAddress);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        billingAddress.User = user;
                        _context.Update(billingAddress);
                        await _context.SaveChangesAsync();
                    }
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
                return RedirectToAction("GetAddresses", "Manage");
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
            return RedirectToAction("GetAddresses", "Manage");
        }

        private bool BillingAddressExists(int id)
        {
            return _context.BillingAddress.Any(e => e.BillingAddressID == id);
        }
    }
}

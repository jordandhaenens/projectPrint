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
using ProjectPrintDos.Models.PaymentViewModels;

namespace ProjectPrintDos.Controllers
{
    [Authorize]
    public class PaymentTypeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PaymentTypeController(ApplicationDbContext context, UserManager<ApplicationUser> user)
        {
            _context = context;    
            _userManager = user;
        }

        // GET: PaymentType
        public async Task<IActionResult> Index()
        {
            return View(await _context.PaymentType.ToListAsync());
        }

        // This action was modified to ensure that the current User can only access their PaymentTypes
        // GET: PaymentType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ApplicationUser user = await _userManager.GetUserAsync(User);

            var paymentType = await _context.PaymentType
                .SingleOrDefaultAsync(m => m.PaymentTypeID == id && m.User == user);
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

        // This action is authored by Jordan Dhaenens
        // POST: PaymentType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentTypeID,Type,AccountNumber,IsActive,IsPrimary")] PaymentType paymentType)
        {
            ModelState.Remove("User");
            ModelState.Remove("IsActive");
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                // Check DB for primary PaymentType and set to false if one exists
                PaymentType formerPrimaryPaymentType = await _context.PaymentType.SingleOrDefaultAsync(pt => pt.IsPrimary == true);
                if (paymentType.IsPrimary == true && formerPrimaryPaymentType != null)
                {
                    formerPrimaryPaymentType.IsPrimary = false;
                    _context.Update(formerPrimaryPaymentType);

                    paymentType.IsActive = true;
                    paymentType.User = user;
                    _context.Add(paymentType);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("PaymentTypes", "Manage");
                }
                else
                {
                    paymentType.IsActive = true;
                    paymentType.User = user;
                    _context.Add(paymentType);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("PaymentTypes", "Manage");
                }
            }
            return View(paymentType);
        }

        // Jordan Dhaenens modified this action to filter out inactive PaymentTypes and ensure ownership
        // GET: PaymentType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ApplicationUser user = await _userManager.GetUserAsync(User);

            var paymentType = await _context.PaymentType.SingleOrDefaultAsync(m => m.PaymentTypeID == id && m.User == user);
            if (paymentType == null || paymentType.IsActive != true)
            {
                return NotFound();
            }
            return View(paymentType);
        }

        // This action is modified by Jordan Dhaenens to prevent browser developer tools from tampering with the IsActive property
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

            ModelState.Remove("User");
            ModelState.Remove("IsActive");
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                try
                {
                    // Check DB for primary PaymentType and set to false if one exists
                    PaymentType formerPrimaryPaymentType = await _context.PaymentType.SingleOrDefaultAsync(pt => pt.IsPrimary == true);
                    if (paymentType.IsPrimary == true && formerPrimaryPaymentType != null)
                    {
                        formerPrimaryPaymentType.IsPrimary = false;
                        _context.Update(formerPrimaryPaymentType);

                        paymentType.IsActive = true;
                        paymentType.User = user;
                        _context.Update(paymentType);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        paymentType.IsActive = true;
                        paymentType.User = user;
                        _context.Update(paymentType);
                        await _context.SaveChangesAsync();
                    }
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
                return RedirectToAction("PaymentTypes", "Manage");
            }
            return View(paymentType);
        }


        // This action is modified by Jordan Dhaenens to verify that paymentType.IsActive == true
        // GET: PaymentType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            if (id == null)
            {
                return NotFound();
            }

            var paymentType = await _context.PaymentType
                .SingleOrDefaultAsync(m => m.PaymentTypeID == id);
            if (paymentType == null || paymentType.IsActive != true || paymentType.User != user)
            {
                return NotFound();
            }

            return View(paymentType);
        }


        // This action is authored by Jordan Dhaenens
        // This action deletes a paymentType from the DB unless it is associated with an Order
        // POST: PaymentType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            PaymentDeleteVM modelVM = new PaymentDeleteVM(_context, id);
            
            if (modelVM.Order == null)
            {
                // No instance of this PaymentTypeId exists in Order table. OK to erase
                _context.PaymentType.Remove(modelVM.PaymentType);
            }
            else
            {
                try
                {
                    // ?Does modelVM.PaymentType have a User? Check once the ability to create orders is in place
                    modelVM.PaymentType.IsActive = false;
                    _context.Update(modelVM.PaymentType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentTypeExists(modelVM.PaymentType.PaymentTypeID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("PaymentTypes", "Manage");
        }

        private bool PaymentTypeExists(int id)
        {
            return _context.PaymentType.Any(e => e.PaymentTypeID == id);
        }
    }
}

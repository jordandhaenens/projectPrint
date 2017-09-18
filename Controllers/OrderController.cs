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
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;    
            _userManager = userManager;
        }

        // GET: Order
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Order.Include(o => o.BillingAddress).Include(o => o.PaymentType).Include(o => o.ShippingAddress);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.BillingAddress)
                .Include(o => o.PaymentType)
                .Include(o => o.ShippingAddress)
                .SingleOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // This action is authored by Jordan Dhaenens
        // This action creates a CompositeProduct from the User input and adds it to the User open order. If User doesn't have an open order, an open order will be created and the CompositeProduct will be added to it. 
        // POST: Order/BuildProductAddToOrder
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BuildProductAddToOrder(int inkID, int screenID, int productTypeID)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            // Build CompositeProduct
            CompositeProduct compositeProduct = new CompositeProduct(){ProductTypeID = productTypeID, DateCreated = DateTime.Now};
            if (inkID != 0 && screenID != 0)
            {
                compositeProduct.InkID = inkID;
                compositeProduct.ScreenID = screenID;
            }

            // Check for open User order. If none, create new Order
            Order order = await _context.Order.SingleOrDefaultAsync(o => o.PaymentTypeID == null && o.User == user);
            if (order == null)
            {
                // Create new Order
                Order newOrder = new Order()
                {
                    User = user,
                };
                _context.Add(newOrder);
                await _context.SaveChangesAsync();
                // Add OrderID to CompositeProduct
                compositeProduct.OrderID = newOrder.OrderID;
            }
            else
            {
                // Add OrderID to CompositeProduct
                compositeProduct.OrderID = order.OrderID;
            }

            _context.Add(compositeProduct);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }


        // GET: Order/Create
        public IActionResult Create()
        {
            ViewData["BillingAddressID"] = new SelectList(_context.Set<BillingAddress>(), "BillingAddressID", "City");
            ViewData["PaymentTypeID"] = new SelectList(_context.PaymentType, "PaymentTypeID", "AccountNumber");
            ViewData["ShippingAddressID"] = new SelectList(_context.ShippingAddress, "ShippingAddressID", "City");
            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderID,IsFulfilled,PaymentTypeID,ShippingAddressID,BillingAddressID")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["BillingAddressID"] = new SelectList(_context.Set<BillingAddress>(), "BillingAddressID", "City", order.BillingAddressID);
            ViewData["PaymentTypeID"] = new SelectList(_context.PaymentType, "PaymentTypeID", "AccountNumber", order.PaymentTypeID);
            ViewData["ShippingAddressID"] = new SelectList(_context.ShippingAddress, "ShippingAddressID", "City", order.ShippingAddressID);
            return View(order);
        }

        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.SingleOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["BillingAddressID"] = new SelectList(_context.Set<BillingAddress>(), "BillingAddressID", "City", order.BillingAddressID);
            ViewData["PaymentTypeID"] = new SelectList(_context.PaymentType, "PaymentTypeID", "AccountNumber", order.PaymentTypeID);
            ViewData["ShippingAddressID"] = new SelectList(_context.ShippingAddress, "ShippingAddressID", "City", order.ShippingAddressID);
            return View(order);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderID,IsFulfilled,PaymentTypeID,ShippingAddressID,BillingAddressID")] Order order)
        {
            if (id != order.OrderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderID))
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
            ViewData["BillingAddressID"] = new SelectList(_context.Set<BillingAddress>(), "BillingAddressID", "City", order.BillingAddressID);
            ViewData["PaymentTypeID"] = new SelectList(_context.PaymentType, "PaymentTypeID", "AccountNumber", order.PaymentTypeID);
            ViewData["ShippingAddressID"] = new SelectList(_context.ShippingAddress, "ShippingAddressID", "City", order.ShippingAddressID);
            return View(order);
        }

        // GET: Order/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.BillingAddress)
                .Include(o => o.PaymentType)
                .Include(o => o.ShippingAddress)
                .SingleOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.SingleOrDefaultAsync(m => m.OrderID == id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.OrderID == id);
        }
    }
}

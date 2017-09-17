using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectPrintDos.Data;
using ProjectPrintDos.Models;
using ProjectPrintDos.Models.HomeViewModels;

namespace ProjectPrintDos.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        // This action is authored by Jordan Dhaenens
        // This action grabs all ProuctTypes from the DB whose quantity is not zero and displays them
        // GET: Home/Products
        public IActionResult Products()
        {
            ProductTypesVM model = new ProductTypesVM(_context);
            return View(model);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

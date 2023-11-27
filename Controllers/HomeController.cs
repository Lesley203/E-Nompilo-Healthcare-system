using E_Nompilo_Healthcare_system.Areas.Identity.Data;
using E_Nompilo_Healthcare_system.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace E_Nompilo_Healthcare_system.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HealthcareDbContext _context;
        public HomeController(HealthcareDbContext dbContext,ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = dbContext;
        }

        public IActionResult Service()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Team()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(Get_In_Touch_Contacts model)
        {
           
            if (ModelState.IsValid)
            {
                _context.In_Touch_Contacts.Add(model);
                _context.SaveChanges();
                TempData["AlertMessageel"] = " Thank you for contacting Us!.We'll get in touch with you as soon as possible!";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Test()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
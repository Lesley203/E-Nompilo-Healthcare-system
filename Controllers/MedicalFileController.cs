using E_Nompilo_Healthcare_system.Areas.Identity.Data;
using E_Nompilo_Healthcare_system.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Nompilo_Healthcare_system.Controllers
{
    public class MedicalFileController : Controller
    {

        private readonly HealthcareDbContext _context;
        public MedicalFileController(HealthcareDbContext dbContext)
        {

            _context = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MedicalFileModel fileModel)
        {
            if (ModelState.IsValid)
            {
                _context.medFile.Add(fileModel);
                _context.SaveChanges();
                return RedirectToAction("Index", "Dashboard");
            }
            return View(fileModel);
        }
    }
}

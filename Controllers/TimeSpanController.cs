using E_Nompilo_Healthcare_system.Areas.Identity.Data;
using E_Nompilo_Healthcare_system.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Nompilo_Healthcare_system.Controllers
{


    public class TimeSpanController : Controller
    {
        private readonly HealthcareDbContext _context;
        private readonly UserManager<HealthcareSystemUser> _userManager;
        public TimeSpanController(HealthcareDbContext dbContext, UserManager<HealthcareSystemUser> userManager)
        {
            this._userManager = userManager;
            _context = dbContext;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Index_List()
        {
            var list = _context.Get_Time;
            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(TimeSpanModel model)

        {
            if (ModelState.IsValid)
            {
                _context.Get_Time.Add(model);
                _context.SaveChanges();

                return RedirectToAction("Index_List");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index_delete(int Id)

        {

           



                var booking = _context.Get_Time
                    .FirstOrDefault(a => a.ID == Id);


                if (booking == null)
                {

                    return NotFound();
                }

                _context.Get_Time.Remove(booking);
                await _context.SaveChangesAsync();


                return RedirectToAction("Index_List");
          
        }
    }
}

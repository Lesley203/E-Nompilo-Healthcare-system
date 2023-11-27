using E_Nompilo_Healthcare_system.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Nompilo_Healthcare_system.Controllers
{
    public class PreNatalController : Controller
    {
        private readonly HealthcareDbContext _context;
        private readonly UserManager<HealthcareSystemUser> _userManager;
        public PreNatalController(HealthcareDbContext dbContext, UserManager<HealthcareSystemUser> userManager)
        {
            this._userManager = userManager;
            _context = dbContext;
        }


        public async Task<ActionResult> Patient()
        {
            var userI = await this._userManager.GetUserAsync(User);
            if (userI == null)
            {
                return NotFound();
            }
            string lastName = userI.LastName;
            string gender = userI.Gender;
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            ViewBag.Role = roleClaim;
            ViewData["LastNameUser"] = gender + " " + lastName;
            return View();
        }
        public IActionResult Referral()
        {
            return View();
        }
       
        public IActionResult FetusTracking()
        {
            return View();
        }

       
      







    }

}

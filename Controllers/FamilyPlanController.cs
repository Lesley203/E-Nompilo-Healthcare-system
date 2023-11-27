using E_Nompilo_Healthcare_system.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Nompilo_Healthcare_system.Controllers
{
    public class FamilyPlanController : Controller
    {
        private readonly HealthcareDbContext _context;
        private readonly UserManager<HealthcareSystemUser> _userManager;
        public FamilyPlanController(HealthcareDbContext dbContext, UserManager<HealthcareSystemUser> userManager)
        {
            this._userManager = userManager;
            _context = dbContext;
        }

        public async Task<IActionResult> FamilyPlanning()
        {

            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            ViewBag.Role = roleClaim;
            var userI = await this._userManager.GetUserAsync(User);
            if (userI == null)
            {
                return NotFound();
            }
            string lastName = userI.LastName;
            string gender = userI.Gender;

            ViewData["LastNameUser"] = gender + " " + lastName;

            return View();
        }
       

        public IActionResult Prescription()
        {
            return View();
        }

        public IActionResult Tracking_Menstration()
        {
            return View();
        }
        //public IActionResult Screening()
        //{
        //    return View();
        //}
        public IActionResult Educational_Resources()
        {
            return View();
        }
        public IActionResult FeedbackandReviews()
        {
            return View();
        }
        public IActionResult Prescriptions()
        {
            return View();
        }
        public IActionResult Insurance()
        {
            return View();
        }
        public IActionResult Counselling()
        {
            return View();
        }

    }
    }

using E_Nompilo_Healthcare_system.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Nompilo_Healthcare_system.Controllers
{
    public class BookingAppointmentController : Controller
    {
        private readonly HealthcareDbContext _context;
        private readonly UserManager<HealthcareSystemUser> _userManager;
        public BookingAppointmentController(HealthcareDbContext dbContext, UserManager<HealthcareSystemUser> userManager)
        {
            this._userManager = userManager;
            _context = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}

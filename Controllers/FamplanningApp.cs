using Microsoft.AspNetCore.Mvc;
using E_Nompilo_Healthcare_system.Areas.Identity.Data;
using E_Nompilo_Healthcare_system.Data.ViewModel;

namespace E_Nompilo_Healthcare_system.Controllers
{
    public class FamplanningApp : Controller
    {
        private readonly HealthcareDbContext _context;
        public FamplanningApp(HealthcareDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var app = _context.Appointments;
            var pat = _context.Users;
            var model = from a in app
                        join p in pat on a.Id equals p.Id
                        where a.DateofAppointment.Date >= DateTime.Now.Date && a.TypeOfAppointment == "Family Planning"
                        select new AppointmentViewcs
                        {
                            DateofAppointment = a.DateofAppointment,
                            PatientName = p.FirstName + " " + p.LastName,
                            Notes = a.Notes,
                            Time = a.Time,
                            FirstTimeVisit = a.FirstTimeVisit
                        };
            return View(model.ToList());
        }
    }
}

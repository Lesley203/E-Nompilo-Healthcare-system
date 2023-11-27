using E_Nompilo_Healthcare_system.Areas.Identity.Data;
using E_Nompilo_Healthcare_system.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace E_Nompilo_Healthcare_system.Controllers
{


    public class VaccinationController : Controller
    {
        private readonly HealthcareDbContext _context;
        private readonly UserManager<HealthcareSystemUser> _userManager;
        public VaccinationController(HealthcareDbContext dbContext, UserManager<HealthcareSystemUser> userManager)
        {
            this._userManager = userManager;
            _context = dbContext;
        }

        public IActionResult Nurse_Vac(string searchEmail)
        {
            ViewData["CurrentFilter"] = searchEmail;
            var solutions = from b in _context.Users
                            select b;
            if (!String.IsNullOrEmpty(searchEmail))
            {
                solutions = solutions.Where(b => b.Email.Contains(searchEmail));
            }
            return View(solutions);
        }

        [HttpGet]
        public async Task<IActionResult> Get_Vaccination_nurse(string? Id)
        {
            var userWithDetails = _context.Get_Vaccinateds
               .FirstOrDefault(u => u.Id == Id);


            ViewBag.TimeList = await _context.Vaccine_Information.ToListAsync();
            return View(userWithDetails);
        }

        [HttpGet]
        public IActionResult Vaccination_comments_view_P(int Vaccination_FeddbackId)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userWithDetails = _context.Vaccinations_Comments
               .FirstOrDefault(u => u.Vaccination_FeddbackId == Vaccination_FeddbackId && u.Id == user);

            return View(userWithDetails);
        }
        [HttpGet]
        public IActionResult Vaccination_comments(int Vaccination_FeddbackId, string? Id )
        {
            var userWithDetails = _context.Vaccinations_Comments
               .FirstOrDefault(u => u.Vaccination_FeddbackId == Vaccination_FeddbackId && u.Id == Id);

            return View(userWithDetails);
        }
        [HttpGet]
        public IActionResult Vaccination_comments_view_N(int Vaccination_FeddbackId, string? Id)
        {
            var userWithDetails = _context.Vaccinations_Comments
               .FirstOrDefault(u => u.Vaccination_FeddbackId == Vaccination_FeddbackId && u.Id == Id);

            return View(userWithDetails);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Vaccination_comments(int Vaccination_FeddbackId, string Id, Vaccination_Feedback_commentsModel model)
        {
             model.Id = Id;
                         
            if (ModelState.IsValid)
            {
                _context.Vaccinations_Comments.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction("Report_Review_Nurse");
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Get_Vaccination_nurse(string Id, Get_VaccinatedModel model)
        {
            model.Id = Id;

            if (ModelState.IsValid)
            {
                _context.Get_Vaccinateds.Add(model);
                _context.SaveChanges();

                return RedirectToAction("ManageVaccination");
            }
            return View(model);
        }

        [HttpGet]
        public async  Task<IActionResult> ManageVaccination(int? SearchId)
        {
            var userI = await this._userManager.GetUserAsync(User);

            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            ViewBag.Role = roleClaim;

            if (userI == null)
            {

                return NotFound();

            }


            string lastName = userI.LastName;
            string gender = userI.Gender;



            ViewData["LastNameUser"] = gender + " " + lastName;

            var solutions = from b in _context.Get_Vaccinateds
                            select b;

            // Sort appointments by DateofAppointment in ascending order
            solutions = solutions
                .OrderBy(b => b.Date)
                .Where(x => x.RecStatus == 'A');

            if (SearchId.HasValue) // Check if SearchId has a value
            {
                // Filter appointments by BookingId
                solutions = solutions.Where(b => b.VaccineId == SearchId);
            }

            return View(solutions);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete_Vaccine(int? VaccineId)
        {

            try
            {

                var booking = _context.Get_Vaccinateds.Find(VaccineId);
                if (booking == null)
                {

                    return NotFound();
                }

                _context.Get_Vaccinateds.Remove(booking);
                _context.SaveChanges();

                TempData["AlertMessagLe"] = "Vaccines Process has been deleted";
                return RedirectToAction("ManageVaccination");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting appointment: {ex.Message}");
                return RedirectToAction("ManageVaccination");
            }
        }
        public IActionResult ChangeStatus(int id, VaccineStatus newStatus)
        {
            var booking = _context.Get_Vaccinateds.Find(id);
            if (booking != null)
            {
                booking.Status = newStatus;
                booking.IsCompleted = true;
                _context.SaveChanges();

               
            }
            return RedirectToAction("ManageVaccination");
        }
        public async Task<IActionResult> Patient()
        {
            
            var userI = await this._userManager.GetUserAsync(User);
            if (userI == null)
            {
                return NotFound();
            }
            string lastName = userI.LastName;
            string gender = userI.Gender;

            ViewData["LastNameUser"] = gender + " " + lastName;

            ViewData["LastName"] = gender + " " + lastName;
            return View();
        }

        public IActionResult Survey()
        {
            return View();
        }
        public IActionResult Report_Feedback()
        {
            // Get the currently logged-in user's information (you need to implement this)
           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Report_Feedback(Vaccine_reportingModel viewModel)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Check if the user has already submitted feedback
            var existingFeedback = _context.vaccine_Reportings.FirstOrDefault(x => x.Id == user);

            if (existingFeedback != null)
            {
                TempData["AlertMessagLe"] = "You have already submitted feedback. You can only do it once.";
                return RedirectToAction("Patient", "Vaccination",viewModel); // Return the same view with the error message.

            }
            else if (ModelState.IsValid)
            {

                viewModel.Id = user;

                // Create a new instance of Vaccine_reportingModel and populate it with the data from the ViewModel.
                _context.vaccine_Reportings.Add(viewModel);
                _context.SaveChanges();
                TempData["AlertMessagL"] = "Your form has been successfully submitted.";
                return RedirectToAction("Patient", "Vaccination"); // Redirect to a success page or another action.
            }

            // If ModelState is not valid, redisplay the form with validation errors.
            return View(viewModel);
        }
        public IActionResult FilterByDateAnd_Scre(DateTime startDate, DateTime endDate)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var filteredUsers = _context.VaccineScreening
                .Include(u => u.User)
                .Where(u => u.GetDateSurvay >= startDate && u.GetDateSurvay <= endDate && u.Id == user)
                 .OrderBy(a => a.GetDateSurvay)
                .ToList();

            return View("Report_Review_Screening", filteredUsers);
        }
        public IActionResult Report_Review_Screening()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var list = _context.VaccineScreening.Where(a => a.Id == user).OrderBy(a => a.GetDateSurvay);
            return View(list);
        }
        public IActionResult Report_Review_Screening_1st()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var list = _context.VaccineScreening.Where(a => a.Id == user).OrderBy(a => a.GetDateSurvay);
            return View(list);
        }
        public IActionResult Report_Review()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var list = _context.vaccine_Reportings.Where(a => a.Id == user);
            return View(list);
        }

        public IActionResult Report_Review_Nurse(string searchEmail)
        {
            
            var list = _context.vaccine_Reportings.Include(a => a.HCUser);
            ViewData["CurrentFilter"] = searchEmail;
            var solutions = from b in _context.vaccine_Reportings.Include(a => a.HCUser)
                select b;
            if (!String.IsNullOrEmpty(searchEmail))
            {
                solutions = solutions.Where(b => b.HCUser.Email.Contains(searchEmail));
            }
            return View(solutions);
        }
    }
}

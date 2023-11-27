
using E_Nompilo_Healthcare_system.Areas.Identity.Data;
using E_Nompilo_Healthcare_system.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace E_Nompilo_Healthcare_system.Controllers
{
    public class ReportsController : Controller
    {

        private readonly HealthcareDbContext _context;
        private readonly UserManager<HealthcareSystemUser> _userManager;
        public ReportsController(HealthcareDbContext dbContext, UserManager<HealthcareSystemUser> userManager)
        {
            this._userManager = userManager;
            _context = dbContext;
        }
        public async Task<IActionResult> Walk_In_ReportIndex()
        {
            var userII = await this._userManager.GetUserAsync(User);
            string lastName = userII.LastName;
            string gender = userII.Gender;
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            ViewBag.Role = roleClaim;
            ViewData["LastNameUser"] = gender + " " + lastName;
            return View();
        }


        //[HttpGet]
        //public async Task<IActionResult> _SelfDiagnosPartialReport()
        //{

        //    var currentUser = await _userManager.GetUserAsync(User);

        //    // Retrieve the self-history records for the current user and specific DiagnosisId
        //    //var selfHistoryRecords = _context.selfHistories
        //    //    //Include(a => a.SelfDiag)
        //    //    .Where(history => history.Id == currentUser.Id)
        //    //    .OrderBy(history => history.GetDateTime)
        //    //    .Select(history => new SelfHistoryModel
        //    //    {

        //    //        SelfDigId = history.SelfDigId,
        //    //        GetDateTime = history.GetDateTime

        //    //        // Map other properties as needed
        //    //        /* SelfDiagUser = currentUser.UserName*/ // Store the user's name
        //    //    });


        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> DeleteItem(int itemToDeleteId)
        //{
        //    // Find the item to delete based on its ID
        //    var itemToDelete =  _context.selfHistories.FirstOrDefault(h => h.SelfDigId == itemToDeleteId);

        //    if (itemToDelete == null)
        //    {
        //        return NotFound(); // Or handle the case where the item doesn't exist
        //    }

        //    // Remove the item from the database
        //    _context.selfHistories.Remove(itemToDelete);
        //    await _context.SaveChangesAsync();

        //    return RedirectToAction("_SelfDiagnosPartialReport"); // Redirect back to the list
        //}

        [HttpPost]
        public async Task<IActionResult> DeleteSelected(List<int> selectedItems)
        {
            if (selectedItems != null && selectedItems.Any())
            {
                // Find and remove the selected items from the database
                var itemsToDelete = _context.selfHistories.Where(h => selectedItems.Contains(h.SelfDigId));
                _context.selfHistories.RemoveRange(itemsToDelete);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("_SelfDiagnosPartialReport"); // Redirect back to the list
        }


        [HttpGet]
        public async Task<IActionResult> _SelfDiagnosPartialReport()
        {
            var userII = await this._userManager.GetUserAsync(User);
            string lastName = userII.LastName;
            string gender = userII.Gender;
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            ViewBag.Role = roleClaim;
            ViewData["LastNameUser"] = gender + " " + lastName;
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Find the SelfHistoryModel record(s) for the user
            var selfHistoryList = _context.selfHistories
                .Where(u => u.Id == user)
                .ToList();

            // Create a list to store related SelfDiagnosModel records
            var relatedSelfDiagnosList = new List<Combined_diagnosis>();

            // Iterate through SelfHistoryModel records and fetch related SelfDiagnosModel records
            foreach (var selfHistory in selfHistoryList)
            {
                if(selfHistory == null)
                {
                    return NotFound();
                }
                var viewModel = new Combined_diagnosis
                {
                    SelfDiagnosModel = _context.selfDiagnos
                        .FirstOrDefault(sd => sd.DiagnosisId == selfHistory.DiagnosisId),
                    SelfHistoryModel = selfHistory
                };

                relatedSelfDiagnosList.Add(viewModel);
            }
            return View(relatedSelfDiagnosList);
        }
        
        public IActionResult Med_Details(string Id)
        {

            var userWithDetails = _context.Users
                .Include(u => u.MedicalFileModel)
                .FirstOrDefault(u => u.Id == Id);

            if (userWithDetails == null)
            {
                return NotFound();
            }

            return View(userWithDetails);
        }
        [HttpGet]
        public IActionResult Nurse_Screen_Report()
        {

            var userWithDetails = _context.VaccineScreening
                 .Include(u => u.User)
                  .OrderBy(a => a.GetDateSurvay);
                

            if (userWithDetails == null)
            {
                return NotFound();
            }

            return View(userWithDetails);
        }
        [HttpGet]
        public IActionResult Admin_Report()
        {

            return View();
        }
        [HttpGet]
        public IActionResult Admin_Users_Report()
        {
            var users = _context.Users.ToList();
            return View(users);

        }
        [HttpGet]
        public IActionResult Nurse_Report()
        {

            return View();

        }
        public IActionResult FilterByDateAndNurse_Scre(DateTime startDate, DateTime endDate)
        {
            var filteredUsers = _context.VaccineScreening
                .Include(u => u.User)
                .Where(u => u.GetDateSurvay >= startDate && u.GetDateSurvay <= endDate)
                 .OrderBy(a => a.GetDateSurvay)
                .ToList();

            return View("Nurse_Screen_Report", filteredUsers);
        }
        public IActionResult FilterByDateAndNurse(DateTime startDate, DateTime endDate)
        {
            var filteredUsers = _context.Get_Vaccinateds
                .Where(u => u.Date >= startDate && u.Date <= endDate)
                 .OrderBy(a => a.Date)
                .ToList();

            return View("Nurse_Report_Sample_A", filteredUsers);
        }

        [HttpGet]
        public IActionResult Nurse_Report_Sample_Vacc()
        {
            var list = _context.Vaccine_Information;
            return View(list);
        }

        public IActionResult Pharm_Report_Sample_Med()
        {
            var list = _context.add_Medications;
            return View(list);
        }

        [HttpGet]
        public IActionResult Nurse_Report_Sample_A()
        {
            var list = _context.Get_Vaccinateds.Include(a => a.MainUser);
            return View(list); 
        }

        public IActionResult FilterByDateAndBooking(DateTime startDate, DateTime endDate)
        {
            var filteredAppointments = _context.Appointments
                .Where(a => a.DateofAppointment >= startDate && a.DateofAppointment <= endDate)
                .OrderBy(a => a.DateofAppointment) // Optional: You can order the results by date if needed
                .ToList();

            var filteredViewModels = filteredAppointments.Select(appointment => new Combined_BookingModel
            {
                HUser = _context.Users.FirstOrDefault(u => u.Id == appointment.Id),
                AppointmentModel = appointment
            }).ToList();

            return View("BookingReport", filteredViewModels);
        }
        public IActionResult FilterByDateAndRefil_Request(DateTime startDate, DateTime endDate)
        {
            var filteredAppointments = _context.refillrequests
                .Where(a => a.RequestDate >= startDate && a.RequestDate <= endDate)
                .OrderBy(a => a.RequestDate) // Optional: You can order the results by date if needed
                .ToList();

            var filteredViewModels = filteredAppointments.Select(appointment => new PhamacistRefillModel
            {
                HUser = _context.Users.FirstOrDefault(u => u.Id == appointment.Id),
                RefillrequestModel = appointment
            }).ToList();

            return View("BookingReport", filteredViewModels);
        }

        public IActionResult FilterByDateAndEmail(DateTime startDate, DateTime endDate)
        {
            var filteredUsers = _context.Users
                .Where(u => u.Date_AccountCreated >= startDate && u.Date_AccountCreated <= endDate )
                .ToList();

            return View("Admin_Users_Report", filteredUsers);
        }

        [HttpGet]
        public IActionResult BookingReport()
        {
           

         
            var selfHistoryList = _context.Appointments
                .OrderBy(b => b.DateofAppointment)
                .ToList();

           
            var relatedSelfDiagnosList = new List<Combined_BookingModel>();


            foreach (var selfHistory in selfHistoryList)
            {
                if (selfHistory == null)
                {
                    return NotFound();
                }
                var viewModel = new Combined_BookingModel
                {
                    HUser = _context.Users
                        .FirstOrDefault(sd => sd.Id == selfHistory.Id),
                    AppointmentModel = selfHistory
                };

                relatedSelfDiagnosList.Add(viewModel);
            }
            return View(relatedSelfDiagnosList);
        }
        [HttpGet]
        public IActionResult Doctor_User_Report()
        {
           
            return View();

        }

        [HttpGet]
        public IActionResult Pharmacist_User_Report()
        {

            return View();

        }

        [HttpGet]
        public IActionResult Pharmacist_Report()
        {



            var selfHistoryList = _context.refillrequests
                .OrderBy(b => b.RequestDate)
                .ToList();


            var relatedSelfDiagnosList = new List<PhamacistRefillModel>();


            foreach (var selfHistory in selfHistoryList)
            {
                if (selfHistory == null)
                {
                    return NotFound();
                }
                var viewModel = new PhamacistRefillModel
                {
                    HUser = _context.Users
                        .FirstOrDefault(sd => sd.Id == selfHistory.Id),
                    RefillrequestModel = selfHistory
                };

                relatedSelfDiagnosList.Add(viewModel);
            }
            return View(relatedSelfDiagnosList);
        }
    }
}







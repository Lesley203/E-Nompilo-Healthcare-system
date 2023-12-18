
using E_Nompilo_Healthcare_system.Areas.Identity.Data;
using E_Nompilo_Healthcare_system.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace E_Nompilo_Healthcare_system.Controllers
{
    public class Walk_InController : Controller
    {
        private readonly HealthcareDbContext _context;
        private readonly UserManager<HealthcareSystemUser> _userManager;
        public Walk_InController(HealthcareDbContext dbContext, UserManager<HealthcareSystemUser> userManager)
        {
            this._userManager = userManager;
            _context = dbContext;
        }




        //Patient Walk-in Dashboard
        public async Task<IActionResult> Index()
        {
            var userI = await this._userManager.GetUserAsync(User);
            string lastName = userI.LastName;
            string gender = userI.Gender;
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            ViewBag.Role = roleClaim;
            ViewData["LastNameUser"] = gender + " " + lastName;
            return View();
        }

        //Admin Booking Appointment 
      

        [HttpGet]
        public async Task<IActionResult> AdminManageQueue()
        {

            var userI = await this._userManager.GetUserAsync(User);
            string lastName = userI.LastName;
            string gender = userI.Gender;

            ViewData["LastNameUser"] = gender + " " + lastName;

            var aptList = _context.Appointments.Where(x => x.RecStatus == 'A')
                .ToList();
            return View(aptList);
        }


        public async Task<IActionResult> AdminCreateAppointment(string Id)
        {
            var userI = await this._userManager.GetUserAsync(User);
            string lastName = userI.LastName;
            string gender = userI.Gender;
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            ViewBag.Role = roleClaim;
            ViewData["LastNameUser"] = gender + " " + lastName;
            var userWithDetails = _context.Appointments
               .FirstOrDefault(u => u.Id == Id);
            ViewBag.TimeList = await _context.Get_Time.ToListAsync();
            return View(userWithDetails);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AdminCreateAppointment(string Id, AppointmentModel appointMdl)
        {

            appointMdl.Id = Id;
            if (ModelState.IsValid)
            {
                _context.Appointments.Add(appointMdl);
                _context.SaveChanges();

                return RedirectToAction("AdminAppointment");
            }
            return View(appointMdl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update_Appointment(AppointmentModel model)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    var existingAppointment = _context.Appointments.FirstOrDefault(a => a.BookingId == model.BookingId);

                    if (existingAppointment == null)
                    {

                        return NotFound();
                    }

                    existingAppointment.DateofAppointment = model.DateofAppointment;
                    existingAppointment.TypeOfAppointment = model.TypeOfAppointment;
                    existingAppointment.Time = model.Time;
                    
                    existingAppointment.Notes = model.Notes;

                    
                    _context.SaveChanges();

                    TempData["AlertMessage"] = "Appointment updated successfully!";
                    return RedirectToAction("AppointmentDetailsTab");
                }

                
                return View(model);
            }
            catch (Exception)
            {
               
                return View(model); 
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete_Appointment(int BookingId)
        {
            try
            {
                
                var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

               
                var appointmentToDelete = _context.Appointments
                    .FirstOrDefault(a => a.BookingId == BookingId && a.Id == currentUser);

                if (appointmentToDelete == null)
                {
                   
                    return NotFound();
                }

                
                _context.Appointments.Remove(appointmentToDelete);
                _context.SaveChanges();

                TempData["AlertMessage"] = "Appointment deleted successfully!";
                return RedirectToAction("AppointmentDetailsTab");
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"Error deleting appointment: {ex.Message}");
                return RedirectToAction("AppointmentDetailsTab"); 
            }
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete_Appointment_queue(int BookingId)
        {
            try
            {
                
                var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

                
                var appointmentToDelete = _context.Appointments
                    .FirstOrDefault(a => a.BookingId == BookingId && a.Id == currentUser);

                if (appointmentToDelete == null)
                {
                   
                    return NotFound();
                }

               
                _context.Appointments.Remove(appointmentToDelete);
                _context.SaveChanges();

                TempData["AlertMessage"] = "Appointment deleted successfully!";
                return RedirectToAction("PatientQueueManage");
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error deleting appointment: {ex.Message}");
                return RedirectToAction("AppointmentDetailsTab"); 
            }
        }
        [HttpGet]
        public async Task<IActionResult> Update_Appointment_Admin(int BookingId, string Id)
        {
            var userI = await this._userManager.GetUserAsync(User);
            string lastName = userI.LastName;
            string gender = userI.Gender;
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            ViewBag.Role = roleClaim;
            ViewData["LastNameUser"] = gender + " " + lastName;

            var userWithDetails = _context.Appointments.FirstOrDefault(m => m.BookingId == BookingId && m.Id == Id);
            if (userWithDetails != null)
            {
                DateTime DateOfAppointment = userWithDetails.DateofAppointment;
                string startTime = userWithDetails.Time;
               

                ViewData["DateOfAppointment"] = DateOfAppointment;
                ViewData["startTime"] = startTime;
              
                

            }
            ViewData["BookingId"] = BookingId;
            ViewBag.TimeList = await _context.Get_Time.ToListAsync();
            return View(userWithDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update_Appointment_Admin(AppointmentModel model)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    var existingAppointment = _context.Appointments.FirstOrDefault(a => a.BookingId == model.BookingId && a.Id == model.Id);

                    if (existingAppointment == null)
                    {

                        return NotFound();
                    }

                    existingAppointment.DateofAppointment = model.DateofAppointment;
                  
                    existingAppointment.Time = model.Time;
                  
                   


                    _context.SaveChanges();

                    TempData["AlertMessage"] = "Postponed ";
                    return RedirectToAction("AdminAppointment");
                }


                return View(model);
            }
            catch (Exception)
            {

                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Update_Appointment(int BookingId)
        {

            var userII = await this._userManager.GetUserAsync(User);
            string lastName = userII.LastName;
            string gender = userII.Gender;
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            ViewBag.Role = roleClaim;
            ViewData["LastNameUser"] = gender + " " + lastName;


            var userI = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userWithDetails = _context.Appointments.FirstOrDefault(m => m.BookingId == BookingId && m.Id == userI);
            if (userWithDetails != null)
            {
                DateTime DateOfAppointment = userWithDetails.DateofAppointment;
                string startTime = userWithDetails.Time;
                
                string TypeOfAppt = userWithDetails.TypeOfAppointment;
                string Notes = userWithDetails.Notes;




                ViewData["DateOfAppointment"] = DateOfAppointment;
                ViewData["startTime"] = startTime;
                
                ViewData["TypeOfAppt"] = TypeOfAppt;
                ViewData["Notes"] = Notes;

            }
            ViewData["BookingId"] = BookingId;
            ViewBag.TimeList = await _context.Get_Time.ToListAsync();
            return View(userWithDetails);
        }



        public IActionResult ChangeStatus(int id, BookingStatus newStatus)
        {
            var booking = _context.Appointments.Find(id);
            if (booking != null)
            {
                booking.Status = newStatus;
                booking.IsCompleted = true;
                _context.SaveChanges();

                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (newStatus == BookingStatus.Complete && booking.Id == currentUserId)
                {
                    _context.Appointments.Remove(booking);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("AdminManageQueue");
        }

        [HttpGet]
        public async Task<IActionResult> AdminAppointment(int? SearchId)
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

            var solutions = from b in _context.Appointments
                            select b;

            // Sort appointments by DateofAppointment in ascending order
            solutions = solutions
                .OrderBy(b => b.DateofAppointment)
                .Where(x=>x.RecStatus=='A');

            if (SearchId.HasValue) // Check if SearchId has a value
            {
                // Filter appointments by BookingId
                solutions = solutions.Where(b => b.BookingId == SearchId);
            }

            return View(solutions);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete_Appointmenttt(int? BookingId)
        {

            try
            {
                
                var booking =  _context.Appointments.Find(BookingId);
                if (booking == null)
                {
                   
                    return NotFound();
                }

                _context.Appointments.Remove(booking);
                 _context.SaveChanges();

                TempData["AlertMessage"] = "Removed from the queue!";
                return RedirectToAction("AdminManageQueue");
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error deleting appointment: {ex.Message}");
                return RedirectToAction("AdminManageQueue"); 
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete_Appointmenttt_doc(int? BookingId)
        {

            try
            {


                var booking = _context.Appointments.Find(BookingId);
                if (booking == null)
                {

                    return NotFound();

                }

                _context.Appointments.Remove(booking);
                _context.SaveChanges();

                TempData["AlertMessage"] = "Removed from the queue!";
                return RedirectToAction("DoctorManageQueue");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting appointment: {ex.Message}");
                return RedirectToAction("DoctorManageQueue");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete_Appointmentt(int? BookingId)
        {

            try
            {
                
                var booking =  _context.Appointments.Find(BookingId);
                if (booking == null)
                {
                   
                    return NotFound();
                }

                _context.Appointments.Remove(booking);
                 _context.SaveChanges();

                TempData["AlertMessage"] = "Appointment deleted successfully!";
                return RedirectToAction("AdminAppointment");
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error deleting appointment: {ex.Message}");
                return RedirectToAction("AdminAppointment"); 
            }
        }


        
        public async Task<IActionResult> BookingAppointForm()
        {
            var userI = await this._userManager.GetUserAsync(User);
            string lastName = userI.LastName;
            string gender = userI.Gender;
            
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            ViewBag.Role = roleClaim;
            
            ViewData["LastNameUser"] = gender + " " + lastName;
            ViewBag.TimeList = await _context.Get_Time.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BookingAppointForm(AppointmentModel appointMdl)
        {

            //if (appointMdl.BookingId != null)
            //{
            //    TempData["AlertMessagee"] = "Failed to book, You already have a booking!";
            //    return RedirectToAction("AppointmentDetailsTab");
            //}

           var user = User.FindFirstValue(ClaimTypes.NameIdentifier);

            appointMdl.Id = user;

            
            
            if (ModelState.IsValid)
            {
                _context.Appointments.Add(appointMdl);
                _context.SaveChanges();
                TempData["AlertMessage"] = "Appointment Booked Successfully....!";
                TempData["AlertMess"] = "You Now placed In a Queue. Track your Booking Queue status.";
                return RedirectToAction("AppointmentDetailsTab");

            }
            return View(appointMdl);
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AppointmentDetails()
        {
            var userI = await this._userManager.GetUserAsync(User);
            string email = userI.Email;
            string firstName = userI.FirstName;
            string lastName = userI.LastName;
            string gender = userI.Gender;
            string MobileNum = userI.PhoneNumb;
           
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            ViewBag.Role = roleClaim;
           
            ViewData["PhoneNumb"] = MobileNum;
            ViewData["LastNameWlk"] = lastName + " " + firstName;
            ViewData["Email"] = email;
            ViewData["LastNameUser"] = gender + " " + lastName;
           
            if (userI == null)
            {
                return RedirectToAction("Walk_In", "Account"); 
            }


            var user = userI.Id;

            var aptList = _context.Appointments
                .Where(item => item.Id == user).ToList();

            //var aptList = _context.Appointments./*Where(item => item.BookingId.ToString() == userI.Id).*/ToList();
            List<AppointmentModel> list = new List<AppointmentModel>();
            if (aptList != null)
            {

                foreach (var apt in aptList)
                {
                    var AppointmentModel = new AppointmentModel()
                    {
                        BookingId = apt.BookingId,
                        DateofAppointment = apt.DateofAppointment,
                        ConsultingPerson = apt.ConsultingPerson,
                        Time = apt.Time,
                        
                        TypeOfAppointment = apt.TypeOfAppointment,
                        Notes = apt.Notes,
                        FirstTimeVisit = apt.FirstTimeVisit
                    };
                    list.Add(AppointmentModel);
                }
                return View(list);
            }
            return View(list);
        }

        
       


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AppointmentDetailsTab()
        {
          
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            ViewBag.Role = roleClaim;
            


            var userI = await this._userManager.GetUserAsync(User);
            string email = userI.Email;
            string firstName = userI.FirstName;
            string lastName = userI.LastName;
            string gender = userI.Gender;
            string MobileNum = userI.PhoneNumb;
            ViewData["LastNameUser"] = gender + " " + lastName;
            ViewData["PhoneNumb"] = MobileNum;
            ViewData["LastName"] = lastName + " " + firstName;
            ViewData["Email"] = email;


            var user = userI.Id;

            var aptList = _context.Appointments
                .Where(item => item.Id == user )
                .Where(x => x.RecStatus == 'A')
                .ToList();
            List<AppointmentModel> list = new List<AppointmentModel>();
            if (aptList != null)
            {

                foreach (var apt in aptList)
                {
                    var AppointmentModel = new AppointmentModel()
                    {
                        BookingId = apt.BookingId,
                        DateofAppointment = apt.DateofAppointment,
                        ConsultingPerson = apt.ConsultingPerson,
                        Time = apt.Time,
                       
                        TypeOfAppointment = apt.TypeOfAppointment,
                        Notes = apt.Notes,
                        FirstTimeVisit = apt.FirstTimeVisit
                    };
                    list.Add(AppointmentModel);

                }
                return View(list);
            }
            return View(list);
        }

        public async Task<IActionResult> SelfSearch_Booking(string SearchId)
        {
          
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            ViewBag.Role = roleClaim;
          
            var userI = await this._userManager.GetUserAsync(User);
            string lastName = userI.LastName;
            string gender = userI.Gender;

            ViewData["LastNameUser"] = gender + " " + lastName;
            ViewData["CurrentFilter"] = SearchId;
            var solutions = from b in _context.Appointments
                            select b;
            if (!String.IsNullOrEmpty(SearchId))
            {
                solutions = solutions.Where(b => b.Id.Contains(SearchId));
            }
            return View(solutions);
        }
 
        public async Task<IActionResult> PatientQueueManage()
        {
            
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            ViewBag.Role = roleClaim;
            
            var userI = await this._userManager.GetUserAsync(User);
            string lastName = userI.LastName;
            string gender = userI.Gender;

            ViewData["LastNameUser"] = gender + " " + lastName;

            var user = userI.Id;

            var aptList = _context.Appointments
                .Where(item => item.Id == user)
                 .Where(x => x.RecStatus == 'A')
                .ToList();
            return View(aptList);
        }


        public async Task<IActionResult> SelfSearch(string SearchString)
        {

            
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            ViewBag.Role = roleClaim;
          

            var userI = await this._userManager.GetUserAsync(User);
            string lastName = userI.LastName;
            string gender = userI.Gender;

            ViewData["LastNameUser"] = gender + " " + lastName;
            ViewData["CurrentFilter"] = SearchString;
            var solutions = from b in _context.selfDiagnos
                            select b;
            if (!String.IsNullOrEmpty(SearchString))
            {
                solutions = solutions.Where(b => b.SymptomName.Contains(SearchString));
            }
            return View(solutions);
        }

        public async Task<IActionResult> SelfDiagnos()
        {
            
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            ViewBag.Role = roleClaim;
        
            var userI = await this._userManager.GetUserAsync(User);
            string lastName = userI.LastName;
            string gender = userI.Gender;

            ViewData["LastNameUser"] = gender + " " + lastName;

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SelfDiagnos(SelfDiagnosModel selfDiagnoss)
        {
            if (selfDiagnoss == null) { return NotFound(); }

            if (ModelState.IsValid)
            {
                _context.selfDiagnos.Add(selfDiagnoss);
                _context.SaveChanges();

                return RedirectToAction("Diagnos_list");
            }
            else
                return View(selfDiagnoss);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SelfHistoryDiagnosis(int DiagnosisId, SelfHistoryModel model)
        {

            var currentUser = await _userManager.GetUserAsync(User);

            model.Id = currentUser.Id;
            model.DiagnosisId = DiagnosisId;
           

            if (ModelState.IsValid)
            {
                _context.selfHistories.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Illness_Treatment", "Walk_In", new { id = DiagnosisId });
            }

            return View(model);
        }
        public async Task<IActionResult> Illness_Treatment(int id)
        {
            var userII = await this._userManager.GetUserAsync(User);
            string lastName = userII.LastName;
            string gender = userII.Gender;
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            ViewBag.Role = roleClaim;
            ViewData["LastNameUser"] = gender + " " + lastName;
            var illnessTreatment = _context.illnessTreatments.SingleOrDefault(t => t.DiagnosisId == id);

            if (illnessTreatment == null)
            {
                return NotFound();
            }

            return View(illnessTreatment);
        }
       

        public async Task<IActionResult> DoctorManageQueue()
        {
           
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            ViewBag.Role = roleClaim;
        
            var userI = await this._userManager.GetUserAsync(User);
            string lastName = userI.LastName;
            string gender = userI.Gender;

            ViewData["LastNameUser"] = gender + " " + lastName;

           

            var aptList = _context.Appointments
                 .Where(x => x.RecStatus == 'A')
                .ToList();
            return View(aptList);
        }

        public IActionResult DeleteApt(int id)
        {
            var booking = _context.Appointments.Find(id);
            if (booking != null)
            {
                _context.Appointments.Remove(booking);
                _context.SaveChanges();
            }
            return RedirectToAction("DoctorManageQueue");
        }
        
        [HttpGet]
        public async Task<IActionResult> Appointment_Reschedule([Bind("BookingId", "Id")] int? BookingId)
        {
            var userII = await this._userManager.GetUserAsync(User);
            string lastName = userII.LastName;
            string gender = userII.Gender;
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            ViewBag.Role = roleClaim;
            ViewData["LastNameUser"] = gender + " " + lastName;
            var obj = _context.Appointments.Find(BookingId);


            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Appointment_Reschedule([Bind("BookingId,Id")] AppointmentModel model)
        {

            _context.Appointments.Update(model);
            _context.SaveChanges();
            return RedirectToAction("AppointmentDetailsTab");

        }


        public async Task<IActionResult> Acces_medical_file()
        {
            
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            ViewBag.Role = roleClaim;
         
            var userId = await this._userManager.GetUserAsync(User);
            string lastName = userId.LastName;
            string gender = userId.Gender;
            string firstname = userId.FirstName;

            ViewData["LastNameUser"] = gender + " " + lastName;
            ViewData["LastName"] = lastName;
            ViewData["FirstName"] = firstname;

            var userI = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userWithDetails = _context.medFile.Where(m => m.Id == userI).FirstOrDefault();
            if (userWithDetails != null)
            {
                string IdentityNo = userWithDetails.IdentityNo;
                string Address1 = userWithDetails.Address1;
                string Address2 = userWithDetails.Address2;
                string City = userWithDetails.City;
                string Country = userWithDetails.Country;
                string postalCode = userWithDetails.postalCode;
                string KinFirstName = userWithDetails.KinFirstName;
                string Relationship = userWithDetails.KinRelationship;
                string KinEmail = userWithDetails.KinEmail;
                string ContactKin = userWithDetails.KinContact;
                string KinAddress1 = userWithDetails.KinAddress1;
                string KinAddress2 = userWithDetails.KinAddress2;
                string postalCodeKin = userWithDetails.KinPostalcode;
                string TakingMed = userWithDetails.TakingMed;
                string allergies = userWithDetails.allergies;
                string MedicalInfor = userWithDetails.MedicalInfor;


                ViewData["IdentityNo"] = IdentityNo;
                ViewData["Address1"] = Address1;
                ViewData["Address2"] = Address2;
                ViewData["City"] = City;
                ViewData["Country"] = Country;
                ViewData["postalCode"] = postalCode;
                ViewData["KinFirstName"] = KinFirstName;
                ViewData["Relationship"] = Relationship;
                ViewData["KinEmail"] = KinEmail;
                ViewData["ContactKin"] = ContactKin;
                ViewData["KinAddress1"] = KinAddress1;
                ViewData["KinAddress2"] = KinAddress2;
                ViewData["postalCodeKin"] = postalCodeKin;
                ViewData["TakingMed"] = TakingMed;
                ViewData["allergies"] = allergies;
                ViewData[" MedicalInfor"] = MedicalInfor;
            }

            return View();
        }


        public async Task<IActionResult> Access_medical_file_Edit()
        {
         
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            ViewBag.Role = roleClaim;
           
            var userId = await this._userManager.GetUserAsync(User);
            string lastName = userId.LastName;
            string gender = userId.Gender;
            string firstname = userId.FirstName;

            ViewData["LastNameUser"] = gender + " " + lastName;
            ViewData["LastName"] = lastName;
            ViewData["FirstName"] = firstname;

            var userI = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userWithDetails = _context.medFile.Where(m => m.Id == userI).FirstOrDefault();
            if (userWithDetails != null)
            {
                string IdentityNo = userWithDetails.IdentityNo;
                string Address1 = userWithDetails.Address1;
                string Address2 = userWithDetails.Address2;
                string City = userWithDetails.City;
                string Country = userWithDetails.Country;
                string postalCode = userWithDetails.postalCode;
                string KinFirstName = userWithDetails.KinFirstName;
                string Relationship = userWithDetails.KinRelationship;
                string KinEmail = userWithDetails.KinEmail;
                string ContactKin = userWithDetails.KinContact;
                string KinAddress1 = userWithDetails.KinAddress1;
                string KinAddress2 = userWithDetails.KinAddress2;
                string postalCodeKin = userWithDetails.KinPostalcode;
                string TakingMed = userWithDetails.TakingMed;
                string allergies = userWithDetails.allergies;
                string MedicalInfor = userWithDetails.MedicalInfor;


                ViewData["IdentityNo"] = IdentityNo;
                ViewData["Address1"] = Address1;
                ViewData["Address2"] = Address2;
                ViewData["City"] = City;
                ViewData["Country"] = Country;
                ViewData["postalCode"] = postalCode;
                ViewData["KinFirstName"] = KinFirstName;
                ViewData["Relationship"] = Relationship;
                ViewData["KinEmail"] = KinEmail;
                ViewData["ContactKin"] = ContactKin;
                ViewData["KinAddress1"] = KinAddress1;
                ViewData["KinAddress2"] = KinAddress2;
                ViewData["postalCodeKin"] = postalCodeKin;
                ViewData["TakingMed"] = TakingMed;
                ViewData["allergies"] = allergies;
                ViewData[" MedicalInfor"] = MedicalInfor;
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Access_medical_file_Edit(MedicalFileModel viewModel)
        {

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                if (user == null)
                {
                    return NotFound(); 
                }

                var medicalFile = _context.medFile.FirstOrDefault(m => m.Id == user.Id);

                if (medicalFile == null)
                {
                    return NotFound(); 
                }

                medicalFile.Address1 = viewModel.Address1;
                medicalFile.Address2 = viewModel.Address2;
                medicalFile.City = viewModel.City;
                medicalFile.postalCode = viewModel.postalCode;
                medicalFile.Country = viewModel.Country;
                medicalFile.KinFirstName = viewModel.KinFirstName;
                medicalFile.KinRelationship = viewModel.KinRelationship;
                medicalFile.KinAddress1 = viewModel.KinAddress1;
                medicalFile.KinAddress2 = viewModel.KinAddress2;
                medicalFile.KinContact = viewModel.KinContact;
                medicalFile.KinEmail = viewModel.KinEmail;
                medicalFile.KinPostalcode = viewModel.KinPostalcode;
                medicalFile.TakingMed = viewModel.TakingMed;
                medicalFile.allergies = viewModel.allergies;
                medicalFile.MedicalInfor = viewModel.MedicalInfor;
                

                _context.SaveChanges(); 

                return RedirectToAction("Acces_medical_file");
            }

            return View(viewModel);

        }

        public async Task<IActionResult> Diagnos_list()
        {
            var userII = await this._userManager.GetUserAsync(User);
            string lastName = userII.LastName;
            string gender = userII.Gender;
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            ViewBag.Role = roleClaim;
            ViewData["LastNameUser"] = gender + " " + lastName;
            IEnumerable<SelfDiagnosModel> model = _context.selfDiagnos;
            return View(model);

        }


        public async Task<IActionResult> Treat_diagnos_add(int DiagnosisId)
        {
            var userII = await this._userManager.GetUserAsync(User);
            string lastName = userII.LastName;
            string gender = userII.Gender;
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            ViewBag.Role = roleClaim;
            ViewData["LastNameUser"] = gender + " " + lastName;
            var model = new IllnessTreatmentModel { DiagnosisId = DiagnosisId };
            ViewData["DiagnosisId"] = DiagnosisId; 
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Treat_diagnos_ad(int DiagnosisId, IllnessTreatmentModel model)
        {
            model.DiagnosisId = DiagnosisId; 

            if (ModelState.IsValid)
            {
                _context.illnessTreatments.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Diagnos_list", new { DiagnosisId }); 
            }

            return View(model);
        }

    }
}

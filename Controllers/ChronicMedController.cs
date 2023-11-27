using E_Nompilo_Healthcare_system.Areas.Identity.Data;
using E_Nompilo_Healthcare_system.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;

namespace E_Nompilo_Healthcare_system.Controllers
{
    public class ChronicMedController : Controller
    {
        private readonly HealthcareDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<HealthcareSystemUser> _userManager;
        public ChronicMedController(HealthcareDbContext dbContext, IEmailSender emailSender, UserManager<HealthcareSystemUser> userManager)
        {
            this._userManager = userManager;
            _context = dbContext;
            _emailSender = emailSender;
        }

        public async Task<IActionResult> ChronicMed()
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

        public IActionResult Exemle()
        {
            return View();
        }

        //Pharmacist Adding Medication
        public async Task<IActionResult> Add_Medication()
        {

            var userI = await this._userManager.GetUserAsync(User);
            string lastName = userI.LastName;
            string gender = userI.Gender;

            ViewData["LastNameUser"] = gender + " " + lastName;
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add_Medication(Add_Medication_Model model)
        {

            if (model == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _context.add_Medications.Add(model);
                _context.SaveChanges();
                TempData["AlertMessage"] = "Added Successfully....!";
                return RedirectToAction("Medication_List");
            }
            return View(model);
        }

        //list of medication
        [HttpGet]
        public IActionResult Medication_List(string searchName)
        {
          
            ViewData["CurrentFilter"] = searchName;
            var solutions = from b in _context.add_Medications
                            select b;
            if (!String.IsNullOrEmpty(searchName))
            {
                solutions = solutions.Where(b => b.MedicationName.Contains(searchName));
            }
            return View(solutions);
        }


        // GET: Update Refill request
        [HttpGet]
        public IActionResult UpdateMedication(int? MedicationId)
        {
            if (MedicationId == null)
            {
                return NotFound();
            }
          
            var addmedication = _context.add_Medications.FirstOrDefault(a => a.MedicationId == MedicationId);
            if (addmedication == null)
            {
                return NotFound();
            }

            return View(addmedication);
        }
        // For Update medication
        // POST: add medication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateMedication(Add_Medication_Model addmedication)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    var existingAddmedication = _context.add_Medications.FirstOrDefault(a => a.MedicationId == addmedication.MedicationId);



                    if (existingAddmedication == null)
                    {
                        return NotFound();
                    }
                    existingAddmedication.MedicationName = addmedication.MedicationName;

                    existingAddmedication.Manufacturer = addmedication.Manufacturer;
                    existingAddmedication.ActiveIngredient = addmedication.ActiveIngredient;
                    existingAddmedication.Dosage = addmedication.Dosage;
                    existingAddmedication.DosageForm = addmedication.DosageForm;
                    existingAddmedication.Description = addmedication.Description;

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Medication_List));
                }
                return View(addmedication);
            }
            catch (DbUpdateConcurrencyException)
            {
                return View(addmedication);
            }

        }


        // Delete medication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<IActionResult> Delete_Addmedication(int? MedicationId)
        {
            try
            {

                


                var AddmedicationToDelete = _context.add_Medications
                    .FirstOrDefault(a => a.MedicationId == MedicationId );

                if (AddmedicationToDelete == null)
                {

                    return NotFound();
                }


                _context.add_Medications.Remove(AddmedicationToDelete);
                await _context.SaveChangesAsync();

                TempData["AlertMessage"] = "Medication deleted successfully!";
                return RedirectToAction(nameof(Medication_List));
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting RequestPres: {ex.Message}");
                return RedirectToAction("Medication_List");
            }





        }



        //Patient request prescription
        public async Task<IActionResult> Request_Prescription()
        {
            var userI = await this._userManager.GetUserAsync(User);
            string lastName = userI.LastName;
            string gender = userI.Gender;

            ViewData["LastNameUser"] = gender + " " + lastName;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Request_Prescription(RequestPrescripModel request)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Email = User.FindFirstValue(ClaimTypes.Email);

            request.Id = user;



            if (ModelState.IsValid)
            {
                _context.requestPrescrips.Add(request);
                try
                {
                    await _emailSender.SendEmailAsync("mulaloricky@gmail.com", "Refill Request Feedback", "Refill Rquest has been sent");

                }

                catch (Exception ex)
                {

                }
                _context.SaveChanges();
               
                TempData["AlertMessage"] = "Request Successfully....!";
                return RedirectToAction("requestPresrip_List");
            }
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> requestPresrip_List()
        {
            var user = await this._userManager.GetUserAsync(User);
            var aptList = _context.requestPrescrips.Where(a => a.Id == user.Id).ToList();
            List<RequestPrescripModel> list = new List<RequestPrescripModel>();
            if (aptList != null)
            {

                foreach (var apt in aptList)
                {
                    var RequestPrescripModel = new RequestPrescripModel()
                    {
                        PrescripId = apt.PrescripId,
                        MedicalHistory = apt.MedicalHistory,
                        CurrentSymptoms = apt.CurrentSymptoms,
                        ChronicConditions = apt.CurrentSymptoms,
                        Allergies = apt.Allergies,

                    };
                    list.Add(RequestPrescripModel);
                }
                return View(list);
            }
            return View(list);

        }
        //Doctor list of prescription request 
        [HttpGet]
        public IActionResult DoctorrequestPresrip_List()
        {
           

            var Request = _context.requestPrescrips.Include(a => a.HCUser).ToList();
            return View(Request);

        }
        //list of available medications
        [HttpGet]
        public IActionResult DoctorMedication_List()
        {
            var aptList = _context.add_Medications.ToList();
            List<Add_Medication_Model> list = new List<Add_Medication_Model>();
            if (aptList != null)
            {

                foreach (var apt in aptList)
                {
                    var Add_Medication_Model = new Add_Medication_Model()
                    {
                        MedicationId = apt.MedicationId,
                        MedicationName = apt.MedicationName,
                        Manufacturer = apt.Manufacturer,
                        ActiveIngredient = apt.ActiveIngredient,
                        Dosage = apt.Dosage,
                        DosageForm = apt.DosageForm,
                        Description = apt.Description,
                        DateCreated = apt.DateCreated,
                    };
                    list.Add(Add_Medication_Model);
                }
                return View(list);
            }
            return View(list);

        }

        public async Task<IActionResult> Refillrequest( string Id)
        {
            var userI = await this._userManager.GetUserAsync(User);
            string lastName = userI.LastName;
            string firstName = userI.FirstName;
            string gender = userI.Gender;

            ViewData["LastNameUser"] = gender + " " + lastName;
           
            
            var req = new RefillrequestModel { Id = Id };

            // Dropdown list of Medication
            ViewBag.MedicationList = await _context.add_Medications.ToListAsync();

            return View(req);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Refillrequest(  RefillrequestModel requeste)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var Email = User.FindFirstValue(ClaimTypes.Email);
            requeste.Id = user;

            //requeste.PrescriptionId = PrescriptionId;
           

            if (ModelState.IsValid)
            {
                _context.refillrequests.Add(requeste);
               
                _context.SaveChanges();
                TempData["AlertMessage"] = "Refill Successfully....!";
                return RedirectToAction("RequestRefill");
            }
            return View(requeste);
        }
        //List of Refillrequest 
        [HttpGet]
        public async Task<IActionResult> RequestRefill()
        {
            var user = await this._userManager.GetUserAsync(User);

           

            var aptList = _context.refillrequests.Where(x => x.Id == user.Id).ToList();
            if (aptList == null)
            {
                return NotFound();
            }
            return View(aptList);

        }

        // GET: Update Refill request
        [HttpGet]
        public IActionResult Update_RefillRequest(int? RefillId)
        {
            if (RefillId == null)
            {
                return NotFound();
            }

            var addmedication = _context.refillrequests.FirstOrDefault(a => a.RefillId == RefillId);
            if (addmedication == null)
            {
                return NotFound();
            }

            return View(addmedication);
        }

        // Update Refill Request
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update_RefillRequest(RefillrequestModel model)
        {

            try
            {

                if (ModelState.IsValid)
                {

                    var existingAppointment = _context.refillrequests.FirstOrDefault(a => a.RefillId == model.RefillId);

                    if (existingAppointment == null)
                    {

                        return NotFound();
                    }

                    existingAppointment.MedicationName = model.MedicationName;
                    existingAppointment.RequestedQuantity = model.RequestedQuantity;
                    existingAppointment.DosageForm = model.DosageForm;
                   
                    existingAppointment.Notes = model.Notes;


                    _context.SaveChanges();

                    TempData["AlertMessage"] = "Refill Request updated successfully!";
                    return RedirectToAction("RequestRefill");
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
        public async Task<IActionResult> Delete_RefillRequest(int RefillId)
        {
            try
            {

                


                var appointmentToDelete = _context.refillrequests
                    .FirstOrDefault(a => a.RefillId == RefillId);

                if (appointmentToDelete == null)
                {

                    return NotFound();
                }


                _context.refillrequests.Remove(appointmentToDelete);
                await _context.SaveChangesAsync();

                TempData["AlertMessage"] = "Refill Request deleted successfully!";
                return RedirectToAction("RequestRefill");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting Refill Request: {ex.Message}");
                return RedirectToAction("RequestRefill");
            }
        }

        public IActionResult ChangeStatus(int id, RefillStatus newStatus)
        {
            var refill = _context.refillrequests.Find(id);
            if (refill != null)
            {

                refill.Statuss = newStatus;
                refill.IsCompleted = true;
                _context.SaveChanges();



            }
            return RedirectToAction("PharmRequestRefill");
        }
        //pharmacist refillrequest LIST
        [HttpGet]
        public IActionResult PharmRequestRefill(int PrescripId)
        {


            var selfHistoryList = _context.refillrequests
              .ToList();


            var relatedSelfDiagnosList = new List<RefillrequestModel>();


            foreach (var selfHistory in selfHistoryList)
            {
                if (selfHistory == null)
                {
                    return NotFound();
                }
                var viewModel = new RefillrequestModel
                {
                    HCUser = _context.Users
                        .FirstOrDefault(sd => sd.Id == selfHistory.Id),
                   MedicationName = selfHistory.MedicationName,
                   RequestDate = selfHistory.RequestDate,
                   RequestedQuantity = selfHistory.RequestedQuantity,
                   Notes = selfHistory.Notes,
                   DosageForm = selfHistory.DosageForm,
                   RefillId = selfHistory.RefillId,
                   IsCompleted = selfHistory.IsCompleted,
                   Statuss = selfHistory.Statuss,
                   
                };

                relatedSelfDiagnosList.Add(viewModel);
            }
            return View(relatedSelfDiagnosList);



        }
        //Create prescription
        [HttpGet]
        public async Task<IActionResult> Prescription(int PrescripId, string? Id)
        {
            var userI = await this._userManager.GetUserAsync(User);
            string lastName = userI.LastName;
            string firstName = userI.FirstName;
            string gender = userI.Gender;

            ViewData["Firstname_D"] = firstName;
            ViewData["LastNameUser"] = gender + " " + lastName;
            var prescription = _context.requestPrescrips.Where(a => a.PrescripId == PrescripId && a.Id == Id).Include(a => a.HCUser).FirstOrDefault();
            if (prescription != null)
            {
                ViewBag.Patient = prescription.HCUser.FirstName + " " + prescription.HCUser.LastName;
            }
            ViewBag.ID = PrescripId;

            // Dropdown list of Medication
            ViewBag.MedicationList = await _context.add_Medications.ToListAsync();
         


            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Prescription(string? Id, PrescriptionModel requeste)
        {
            //Add the Doctor Mde the Perscriptuion
           
            requeste.Id = Id;
            if (ModelState.IsValid)
            {
                _context.prescriptions.Add(requeste);
                _context.SaveChanges();
              

                return RedirectToAction("prescriptions");
            }
            return View(requeste);
        }
        //List of  DoctorPrescriptions 
        [HttpGet]
        public IActionResult prescriptions()
        {
            var Requested = _context.prescriptions.Include(a => a.HUser).Include(a => a.Prescription_Request).Include(a => a.Prescription_Request.HCUser).ToList();
            return View(Requested);

           






        }

        // GET: Update prescription
        [HttpGet]
        public IActionResult UpdatePrescription(int? PrescriptionId)
        {
            if (PrescriptionId == null)
            {
                return NotFound();
            }
           
            var prescription = _context.prescriptions.FirstOrDefault(a => a.PrescriptionId == PrescriptionId );
            if (prescription == null)
            {
                return NotFound();
            }

            return View(prescription);
        }
        // For Prescription Update 
        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdatePrescription(PrescriptionModel prescription)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var existingPrescription = _context.prescriptions.FirstOrDefault(a => a.PrescriptionId == prescription.PrescriptionId);



                    if (existingPrescription == null)
                    {
                        return NotFound();
                    }
                    existingPrescription.MedicationName = prescription.MedicationName;
                    existingPrescription.Dosageamount = prescription.Dosageamount;
                    existingPrescription.Frequency = prescription.Frequency;
                    existingPrescription.PrescriptionExpirationDate = prescription.PrescriptionExpirationDate;
                   
                    existingPrescription.Comment = prescription.Comment;
              

                    _context.SaveChanges();
                    TempData["AlertMessage"] = "Prescriptions unpdated successfully!";
                    return RedirectToAction(nameof(prescriptions));
                }
                return View(prescription);
            }
            catch (DbUpdateConcurrencyException)
            {
                return View(prescription);
            }

        }

        //List of Prescriptions in patient view
        [HttpGet]
        public async Task<IActionResult> Patientprescriptions()
        {
            var user = await this._userManager.GetUserAsync(User);
            var Requested = _context.prescriptions
                               .Where(m => m.Id == user.Id)
                               .Include(a => a.HUser)
                              
                               .Include(a => a.Prescription_Request)
                               .Include(a => a.Prescription_Request.HCUser)
                               .ToList();

            return View(Requested);

        }

        // GET: Patient Details Prescription
        [HttpGet]
        public IActionResult Prescription_Details(int? PrescriptionId)
        {
            if (PrescriptionId == null)
            {
                return NotFound();
            }

            // Find the request prescription by PrescripId
            var prescription = _context.prescriptions.FirstOrDefault(a => a.PrescriptionId == PrescriptionId);

            if (prescription == null)
            {
                return NotFound();
            }

            return View(prescription);
        }
        // List of refillrequest for patients
        [HttpGet]
        public IActionResult RefillrequestPatient()
        {
          

            var Requested = _context.refillrequests.Include(a => a.HCUser).ToList();
            return View(Requested);

        }



        // GET: RequestPrescrip Edit
        [HttpGet]
        public IActionResult EditRequestPrescrption(int? PrescripId )
        {
            if (PrescripId == null)
            {
                return NotFound();
            }
           
            var requestPrescrip = _context.requestPrescrips.FirstOrDefault(a => a.PrescripId == PrescripId);
            if (requestPrescrip == null)
            {
                return NotFound();
            }

            return View(requestPrescrip);
        }
        // For Request Prescription Edit
        // POST: RequestPrescrip/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditRequestPrescrption(RequestPrescripModel requestPrescrip)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var existingRequest_presc = _context.requestPrescrips.FirstOrDefault(a => a.PrescripId == requestPrescrip.PrescripId);
                   
                    

                    if (existingRequest_presc == null )
                    {
                        return NotFound();
                    }

                    existingRequest_presc.MedicalHistory = requestPrescrip.MedicalHistory;
                    existingRequest_presc.CurrentSymptoms = requestPrescrip.CurrentSymptoms;
                    existingRequest_presc.ChronicConditions = requestPrescrip.ChronicConditions;
                    existingRequest_presc.Allergies = requestPrescrip.Allergies;
                    existingRequest_presc.Notes = requestPrescrip.Notes;
                    
                     _context.SaveChanges();
                    TempData["AlertMessage"] = "Request Prescription updated successfully!";
                    
                    return RedirectToAction(nameof(requestPresrip_List));
                }
               return View(requestPrescrip);
            }
            catch (DbUpdateConcurrencyException) {
                return View(requestPrescrip);
            }
            
        }

        // GET: RequestPrescrip/requestPresrip_Details/5
        [HttpGet]
        public IActionResult requestPresrip_Details(int? PrescripId)
        {
            if (PrescripId == null)
            {
                return NotFound();
            }

            // Find the request prescription by PrescripId
            var requestPrescrip = _context.requestPrescrips.FirstOrDefault(a => a.PrescripId == PrescripId);

            if (requestPrescrip == null)
            {
                return NotFound();
            }

            return View(requestPrescrip);
        }


        // Delete Request Prescription
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_RequestPrescription(int? PrescripId)
        {
            try
            {

                


                var RequestPresToDelete = _context.requestPrescrips
                    .FirstOrDefault(a => a.PrescripId == PrescripId );

                if (RequestPresToDelete == null)
                {

                    return NotFound();
                }


                _context.requestPrescrips.Remove(RequestPresToDelete);
                await _context.SaveChangesAsync();

                TempData["AlertMessage"] = "Request Prescription deleted successfully!";
                return RedirectToAction("requestPresrip_List");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting RequestPres: {ex.Message}");
                return RedirectToAction("requestPresrip_List");
            }
        }

      
}   }






   
   





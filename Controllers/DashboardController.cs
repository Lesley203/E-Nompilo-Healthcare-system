using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using E_Nompilo_Healthcare_system.Areas.Identity.Pages.Account;
using Microsoft.AspNetCore.Mvc.Rendering;
using E_Nompilo_Healthcare_system.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Claims;
using E_Nompilo_Healthcare_system.Models;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace E_Nompilo_Healthcare_system.Controllers
{
    public class DashboardController : Controller
    {

        private readonly UserManager<HealthcareSystemUser> _userManager;
        private readonly HealthcareDbContext _context;
        private readonly IEmailSender _emailSender;
        public DashboardController(HealthcareDbContext cotext, UserManager<HealthcareSystemUser> userManager, IEmailSender emailSender)

        {

            _context = cotext;
            _emailSender = emailSender;
            this._userManager = userManager;

        }



        //public async Task<IActionResult> Index()
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    var roles = await _userManager.GetRolesAsync(user);

        //    return View(roles);
        //}

        //public IActionResult Index()
        //{
        //    var firstNameClaim = User.FindFirst(ClaimTypes.GivenName)?.Value;
        //    var genderClaim = User.FindFirst(ClaimTypes.Gender)?.Value;
        //    var emailClaim = User.FindFirst(ClaimTypes.Email)?.Value;
        //    var lastClaim = User.FindFirst(ClaimTypes.Surname)?.Value;
        //    var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;

        //    ViewBag.FirstName = firstNameClaim;
        //    ViewBag.LastName = lastClaim;
        //    ViewBag.Email = emailClaim;
        //    ViewBag.Role = roleClaim;
        //    ViewBag.Gender = genderClaim;



        //    return View();
        //}

        public async Task<IActionResult> Index()
        {

            //await _emailSender.SendEmailAsync("mafhuwaronewa62@gmail.com", "Confirm your email",
            //            $"Logged IN.");
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            ViewBag.Role = roleClaim;

            int userCount = _context.Users.Count();
            ViewBag.UserCount = userCount;

            var userI = await this._userManager.GetUserAsync(User);
            if (userI == null)
            {
                return NotFound();
            }
            string lastName = userI.LastName;
            string firstName = userI.FirstName;
            string gender = userI.Gender;

            ViewData["LastNameUser"] = gender + " " + lastName;
         
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var file_check = _context.medFile.Where(m => m.Id == user).FirstOrDefault();

            if (file_check != null)
            {

                TempData["FileChecked"] = "True";
            }
            else
            {
                TempData["FileChecked"] = null;
            }

            return View();
           
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(MedicalFileModel fileModel)
        {

            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
             
            fileModel.Id = user;
            if (ModelState.IsValid)
            {
                _context.medFile.Add(fileModel);
                _context.SaveChanges();
                TempData["AlertMessag"] = " Your File is Successfully Created and Store in our database....!";
                return RedirectToAction("Index", "Dashboard");
            }
            
            return View(fileModel);
        }
        //[HttpGet]
        //public IActionResult Admin_Medical_File(string searchEmail)
        //{
        //    var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var file_check = _context.medFile.Where(m => m.Id == user).FirstOrDefault();

        //    if (file_check != null)
        //    {

        //        TempData["FileChecked"] = "True";
        //    }
        //    else
        //    {
        //        IEnumerable<HealthcareSystemUser> usersWithoutFile = _context.Users.Where(u => !_context.medFile.Any(mf => mf.Id == u.Id));
        //        TempData["FileChecked"] = null;
        //        TempData["UsersWithoutFile"] = usersWithoutFile;
        //    }

        //    ViewData["CurrentFilter"] = searchEmail;
        //    var solutions = from b in _context.Users
        //                    select b;
        //    if (!String.IsNullOrEmpty(searchEmail))
        //    {
        //        solutions = solutions.Where(b => b.Email.Contains(searchEmail));
        //    }
        //    return View(solutions);

        //}

        public IActionResult Admin_Medical_File(string searchEmail)
        {


            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var file_check = _context.medFile.Where(m => m.Id == user).FirstOrDefault();


            // Assuming you want to filter users by email when searching
            ViewData["CurrentFilter"] = searchEmail;

            IEnumerable<HealthcareSystemUser> solutions;

            if (string.IsNullOrEmpty(searchEmail))
            {
                solutions = _context.Users;
            }
            else
            {
                solutions = _context.Users.Where(u => u.Email.Contains(searchEmail));
            }


            // Now, get the users without files
            IEnumerable<HealthcareSystemUser> usersWithoutFile = _context.Users
                .Where(u => !_context.medFile.Any(mf => mf.Id == u.Id));

            // Filter usersWithoutFile by email if a searchEmail is provided
            if (!string.IsNullOrEmpty(searchEmail))
            {
                usersWithoutFile = usersWithoutFile.Where(u => u.Email.Contains(searchEmail));
            }

            TempData["UsersWithoutFile"] = usersWithoutFile;
            TempData["FileChecked"] = file_check != null ? "True" : null;
            var ino = _context.medFile.Any(mf => mf.Id == user);
            TempData["File"] = ino;

            return View();
        }



        public IActionResult CreateMedicalFile(string Id)
        {

            var model = new MedicalFileModel { Id = Id }; // You may need to adjust this based on your MedicalFile model properties
            return View( model);
        }

       


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateMedicalFile(string Id, MedicalFileModel model)
        {

            var userWithoutFile = _context.Users
                .Include(u => u.MedicalFiles)
                .FirstOrDefault(u => u.Id == Id);

            if (userWithoutFile == null)
            {

                return NotFound();
            }

            model.Id = Id;
            if (ModelState.IsValid)
            {
                userWithoutFile.MedicalFiles.Add(model);
                _context.SaveChanges();

                return RedirectToAction("Admin_Medical_File");
            }

            return View(model);
        }

        public IActionResult UserDetails(string Id)
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
        public IActionResult System_User(string searchEmail)
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

        //[HttpGet]
        //public IActionResult System_User_Book()
        //{
        //    var users = _context.Users.ToList(); // Retrieve the list of users
        //    return View(users);
        //}
        [HttpGet]
        public async Task<IActionResult> System_User_Book(string searchEmail)
        {
            var userI = await this._userManager.GetUserAsync(User);
            string lastName = userI.LastName;
            string gender = userI.Gender;

            ViewData["LastNameUser"] = gender + " " + lastName;
            ViewData["CurrentFilter"] = searchEmail;
            var solutions = from b in _context.Users
                            select b;
            if (!String.IsNullOrEmpty(searchEmail))
            {
                solutions = solutions.Where(b => b.Email.Contains(searchEmail))
                                     .Where(b => b.Email.Contains(searchEmail));
            }
            return View(solutions);
        }

        public async Task<IActionResult> Admin_medical_file_Edit(string Id)
        {
            var userId = await this._userManager.GetUserAsync(User);
            string lastName = userId.LastName;
            string gender = userId.Gender;
            string firstname = userId.FirstName;

            ViewData["LastNameUser"] = gender + " " + lastName;
            ViewData["LastName"] = lastName;
            ViewData["FirstName"] = firstname;


            var userWithDetails = _context.medFile.Where(m => m.Id == Id).FirstOrDefault();
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
            var model = new MedicalFileModel { Id = Id }; // You may need to adjust this based on your MedicalFile model properties
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Admin_medical_file_Edit(string Id, MedicalFileModel viewModel)
        {

            if (ModelState.IsValid)
            {
               

                if (Id == null)
                {
                    return NotFound(); // Handle not found case
                }

                var medicalFile = _context.medFile.FirstOrDefault(m => m.Id == Id);

                if (medicalFile == null)
                {
                    return NotFound(); // Handle not found case
                }

                // Update the properties from the ViewModel
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
                // Update other properties as needed

                _context.SaveChanges(); // Save changes to the database

                return RedirectToAction("UserDetails", new { Id }); // Redirect to the list of MedicalFiles or another appropriate action
            }

            return View(viewModel);

        }

        
    }

    
}

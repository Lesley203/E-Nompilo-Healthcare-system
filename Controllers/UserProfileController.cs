using E_Nompilo_Healthcare_system.Areas.Identity.Data;
using E_Nompilo_Healthcare_system.Models;
using E_Nompilo_Healthcare_system.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using System.Security.Claims;

namespace E_Nompilo_Healthcare_system.Controllers
{
    public class UserProfileController : Controller
    {

        private readonly UserManager<HealthcareSystemUser> _userManager;
        private readonly HealthcareDbContext _context;
        private readonly IWebHostEnvironment WebHostEnvironment;

        public  UserProfileController(HealthcareDbContext cotext, UserManager<HealthcareSystemUser> userManager,IWebHostEnvironment webHostEnvironment)

        {

            _context = cotext;
            this._userManager = userManager;
            WebHostEnvironment= webHostEnvironment;

        }
        // GET: UserProfileController
        public async Task<ActionResult> Index()
        {


            var userId =  User.FindFirstValue(ClaimTypes.NameIdentifier); // Implement this method to get the current user's ID
            var userI = await this._userManager.GetUserAsync(User);

           
            string gender = userI.Gender;

           

            string firstName = userI.FirstName;
            string userid = userI.Id;
            string lastName = userI.LastName;
            string Email = userI.Email;
            string phonenumb = userI.PhoneNumb;
           
            ViewData["LastName"] = lastName;
            ViewData["FirstName"] = firstName;
            ViewData["Email"] = Email;
            ViewData["PhoneNumber"] = phonenumb;
            ViewData["userId"] = userid;
            

            ViewData["LastNameUser"] = gender + " " + lastName;

            return View();
            
        }

        // GET: UserProfileController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserProfileController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserProfileController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult ProfilePic(AppUserImage userPic)
        //{
        //    string stringFileName = UploadFile(userPic);
        //    var AppUser = new ProfilePicModel
        //    {

        //        ImagePath = stringFileName
        //    };
        //    _context.ProfileImage.Add(AppUser);
        //    _context.SaveChanges();

        //    return RedirectToAction("Index");
        //}



        // GET: UserProfileController/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit()
        {

            var userI = await this._userManager.GetUserAsync(User);
          
            string gender = userI.Gender;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Implement this method to get the current user's ID
            string firstName = userI.FirstName;
            string userid = userI.Id;
            string lastName = userI.LastName;
            string Email = userI.Email;
            string phonenumb = userI.PhoneNumb;
            

            ViewData["LastName"] = lastName;
            ViewData["FirstName"] = firstName;
            
            ViewData["Email"] = Email;
            ViewData["PhoneNumber"] = phonenumb;
            ViewData["userId"] = userid;
            ViewData["LastNameUser"] = gender + " " + lastName;
            //var user = await this._userManager.FindByIdAsync(id);
            //if (user == null)
            //{
            //    ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
            //    return View("NotFound");
            //}

            //var model = new EditUserVieModel
            //{
            //    Id = user.Id,
            //    Email = user.Email,
            //    FirstName = user.FirstName,
            //    LastName = user.LastName,
            //    PhoneNumb = user.PhoneNumb
            //};
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(HealthcareSystemUser user)
        {

            var userI = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUser = await _userManager.GetUserAsync(User);
            
            currentUser.FirstName = user.FirstName;
            currentUser.Email = user.Email;
            currentUser.LastName = user.LastName;
            
            currentUser.PhoneNumb = user.PhoneNumb;

            var result = await _userManager.UpdateAsync(currentUser);
            await _context.SaveChangesAsync();
            if (result.Succeeded)
            {

                return RedirectToAction("Index");
            }
            return View();
        }


        private string UploadFile(AppUserImage userPic)
        {
            string? fileName = null;
            if (userPic.ImagePath != null)
            {
                string uploadDir = Path.Combine(WebHostEnvironment.WebRootPath, "ImagesPro");
                fileName = Guid.NewGuid().ToString() + "_" + userPic.ImagePath.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    userPic.ImagePath.CopyTo(fileStream);
                }
            }
            return fileName;
        }
    


        // POST: UserProfileController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit(int id, IFormCollection collection)
        //{

        //    var userI = await this._userManager.GetUserAsync(User);
        //    string firstName = userI.LastName;
        //    string lastName = userI.FirstName;
        //    string gender = userI.Gender;

        //    ViewData["LastName"] = gender + " " + lastName;

        //    try
        //    {

               
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: UserProfileController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserProfileController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

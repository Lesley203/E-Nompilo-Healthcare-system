using E_Nompilo_Healthcare_system.Areas.Identity.Data;
using E_Nompilo_Healthcare_system.Areas.Identity.Pages.Account;
using E_Nompilo_Healthcare_system.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace E_Nompilo_Healthcare_system.Controllers
{
    public class Add_New_PatientController : Controller
    {
        private readonly HealthcareDbContext _context;
        private readonly SignInManager<HealthcareSystemUser> _signInManager;
        private readonly UserManager<HealthcareSystemUser> _userManager;
        private readonly IUserStore<HealthcareSystemUser> _userStore;
        

        public Add_New_PatientController(HealthcareDbContext dbContext, UserManager<HealthcareSystemUser> userManager, 
            SignInManager<HealthcareSystemUser> signInManager, IUserStore<HealthcareSystemUser> userStore)
        {
            _userManager = userManager;
            _context = dbContext;
            _userStore = userStore;
           
            _signInManager = signInManager;
        }
      
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add_New_Patient()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_New_Patient(AddUserModel model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) == null)
            {
                if (ModelState.IsValid)
                {
                    var user = new HealthcareSystemUser();
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    user.EmailConfirmed = true;
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.PhoneNumb = model.PhoneNumb;
                    user.Gender = model.Gender;
                    user.DateofBirth = model.DateofBirth;
                    await _userStore.SetUserNameAsync(user, model.Email, CancellationToken.None);
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "PATIENT");
                        return RedirectToAction("Admin_Medical_File", "Dashboard");   
                    }
                }
                
            }
           

            return View(model);
        }
    }
}
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using E_Nompilo_Healthcare_system.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using static System.Formats.Asn1.AsnWriter;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using E_Nompilo_Healthcare_system.Controllers;
using System.Timers;
using System.Security.Claims;
using Newtonsoft.Json.Serialization;
using E_Nompilo_Healthcare_system.Image;
using System.Data;

namespace E_Nompilo_Healthcare_system.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
       
        private readonly SignInManager<HealthcareSystemUser> _signInManager;
        private readonly UserManager<HealthcareSystemUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserStore<HealthcareSystemUser> _userStore;
        private readonly IUserEmailStore<HealthcareSystemUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;


        public RegisterModel(
            UserManager<HealthcareSystemUser> userManager,
            IUserStore<HealthcareSystemUser> userStore,
            SignInManager<HealthcareSystemUser> signInManager,
            ILogger<RegisterModel> logger,
            RoleManager<IdentityRole> roleManager,
            IEmailSender emailSender
            )

        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _roleManager = roleManager;
            _emailSender = emailSender;
          
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }



        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }


        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>

             

            [Required]
            [StringLength(50)]
            [DataType(DataType.Text)]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [StringLength(50)]
            [DataType(DataType.Text)]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            //[Required]
            //[Range(0, 99999)]
            //public int SCN { get; set; }


            

            [Required]
            [StringLength(50)]
            [DataType(DataType.Text)]
            public string Gender { get; set; }



            [Required(ErrorMessage = "Phone Number is required")]
            [Display(Name = "Phone Number")]
            [DataType(DataType.PhoneNumber)]
            [RegularExpression(@"^1?[0-9]{10}$", ErrorMessage = "Not a valid Phone number")]
            public string PhoneNumb { get; set; }

            [Required]
            [Display(Name = "Date of Birth")]
            [DataType(DataType.DateTime)]
            public System.DateTime DateofBirth { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }


            //[Required]
            //public string SelectedRole { get; set; }


            //public IEnumerable<SelectListItem> RolesList { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();


             //Input = new InputModel
             //{
             //    RolesList = _roleManager.Roles.Select(x =>x.Name).Select(i=>new SelectListItem{
             //        Text = i,
             //        Value  = i
             //    })
             //};      
        }
        //private async Task ConfigureDefaultUsersAndRoles(IServiceProvider serviceProvider)
        //{
        //    var userManager = serviceProvider.GetRequiredService<UserManager<User>();
        //    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        //    // Create default roles if they don't exist
        //    if (!await roleManager.RoleExistsAsync("Admin"))
        //    {
        //        await roleManager.CreateAsync(new IdentityRole("Admin"));
        //    }

        //    // Create a default user if they don't exist
        //    var defaultUser = await _userManager.FindByNameAsync("@example.com");
        //    if (defaultUser == null)
        //    {
        //        defaultUser = new User
        //        {
        //            UserName = "default@example.com",
        //            Email = "default@example.com"
        //        };
        //        await _userManager.CreateAsync(defaultUser, "YourPassword123"); // Replace with desired password

        //        // Assign the user to a role if needed
        //        await userManager.AddToRoleAsync(defaultUser, "Admin");
        //    }
        //}
        public async Task<IActionResult> OnPostAsync( string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid  )
            {

                var user = CreateUser();
                
                user.FirstName = Input.FirstName;
                user.LastName = Input.LastName;
                user.Email = Input.Email;
               user.Gender = Input.Gender;
                user.PhoneNumb = Input.PhoneNumb;
                user.DateofBirth = Input.DateofBirth;
                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    await _userManager.AddToRoleAsync(user, "PATIENT");



                    await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Surname, Input.LastName));

                    await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.GivenName, Input.FirstName));


                    await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Gender, Input.Gender));


                    //var roleExists = await _roleManager.RoleExistsAsync( Input.Role);
                    //if (roleExists)
                    //{
                    //    await _userManager.AddToRoleAsync(user, Input.Role);
                    //}
                    //else
                    //{
                    //    ModelState.AddModelError("", "The selected role does not exist.");
                    //    // You can handle the error accordingly, e.g., display an error message to the user.
                    //    // You may also redirect the user to a different view or take any other appropriate action.

                    //}
                    //bool roleExists = await _roleManager.RoleExistsAsync("PATIENT");
                    //if (!roleExists)
                    //{
                    //    // Handle the case when the role does not exist
                    //    return RedirectToAction("Index", "Home");
                    //}
                    //else
                    //{






                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }

                   
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private HealthcareSystemUser CreateUser()
        {

            try
            {
                return Activator.CreateInstance<HealthcareSystemUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(HealthcareSystemUser)}'. " +
                    $"Ensure that '{nameof(HealthcareSystemUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<HealthcareSystemUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<HealthcareSystemUser>)_userStore;
        }
    }
}

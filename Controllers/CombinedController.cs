using E_Nompilo_Healthcare_system.Areas.Identity.Data;
using E_Nompilo_Healthcare_system.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Nompilo_Healthcare_system.Controllers
{
    public class CombinedController : Controller
    {
        private readonly HealthcareDbContext _context;
        private readonly UserManager<HealthcareSystemUser> _userManager;
        public CombinedController(HealthcareDbContext dbContext, UserManager<HealthcareSystemUser> userManager)
        {
            this._userManager = userManager;
            _context = dbContext;
        }



    }
}

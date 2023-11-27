using Microsoft.AspNetCore.Mvc;
using E_Nompilo_Healthcare_system.Areas.Identity.Data;
using E_Nompilo_Healthcare_system.Areas.Identity.Pages.Account;
using E_Nompilo_Healthcare_system.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace E_Nompilo_Healthcare_system.Controllers
{
    public class FamDoctor : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    
        public IActionResult PatientOverview()
        {
            return View();
        }
        public IActionResult AppointmentManagement()
        {
            return View();
        }
        public IActionResult ContraceptiveOptions()
        {
            return View();
        }
        public IActionResult ReferralSystem()
        {
            return View();
        }
    }
}
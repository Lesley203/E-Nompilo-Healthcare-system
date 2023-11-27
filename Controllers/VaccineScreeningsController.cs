using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Nompilo_Healthcare_system.Areas.Identity.Data;
using E_Nompilo_Healthcare_system.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Security.Claims;

namespace E_Nompilo_Healthcare_system.Controllers
{
    public class VaccineScreeningsController : Controller
    {
        private readonly HealthcareDbContext _context;
        private readonly IEmailSender _emailSender;
        
        public object ClaimType { get; set; }   
       

        public VaccineScreeningsController(HealthcareDbContext context, IEmailSender _emailSender)
        {
            _context = context;
            _emailSender = _emailSender;
        }

        // GET: VaccineScreenings
        public IActionResult Survay_Vacc()
        {
            
           return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete_Vaccine(int VaccineId)
        {

            try
            {

                var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);


                var booking = _context.Get_Vaccinateds
                    .FirstOrDefault(a => a.VaccineId == VaccineId && a.Id == currentUser);

               
                if (booking == null)
                {

                    return NotFound();
                }

                _context.Get_Vaccinateds.Remove(booking);
                _context.SaveChanges();

                TempData["AlertMessagLe"] = "Vaccines Process has been canceled";
                return RedirectToAction("Get_Vaccinated_List");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting appointment: {ex.Message}");
                return RedirectToAction("Get_Vaccinated_List");
            }
        }
        public async Task<IActionResult> Get_Vaccinated()
        {
            ViewBag.TimeList = await _context.Vaccine_Information.ToListAsync();
            return View();
        }

        public IActionResult Get_Vaccinated_List()
        {
            
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var list = _context.Get_Vaccinateds.Where(a => a.Id == user && a.RecStatus == 'A');
            return View(list);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Get_Vaccinated(Get_VaccinatedModel model)
        {

            if(model == null)
            {
                TempData["AlertMessagL"] = "You Can not get vaccinated until the current one its finished";
                return RedirectToAction("Get_Vaccinated_List");
            }
            
           
            if (ModelState.IsValid)
            {

                var user = User.FindFirstValue(ClaimTypes.NameIdentifier);

                model.Id = user;

                // Save the model to the database
                _context.Add(model);
                await _context.SaveChangesAsync(); // Use await to wait for the database operation to complete
                TempData["AlertMessage"] = "Get Your Vaccine by Booking Appointment for";
                return RedirectToAction("Get_Vaccinated_List");
                
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Survay_Vacc(VaccineScreening model)
        {
            if (ModelState.IsValid)
            {
                var user = User.FindFirstValue(ClaimTypes.NameIdentifier);

                model.Id = user;

                // Save the model to the database
                _context.Add(model);
                await _context.SaveChangesAsync(); // Use await to wait for the database operation to complete

                if (model.ShowAlert)
                {
                    TempData["AlertMessagee"] = "YOU ARE NOT ELIGIBLE TO BE VACCINATED. PLEASE CONSULT A DOCTOR FIRST!";
                    // Show alert message
                    return RedirectToAction("Survay_Vacc");
                }
                else
                {
                    return RedirectToAction("Get_Vaccinated");
                }
            }

            // If the model is not valid, return to the form with errors
            return View("Survay_Vacc", model);
        }


    }
}

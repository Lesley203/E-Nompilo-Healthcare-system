using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Nompilo_Healthcare_system.Areas.Identity.Data;
using E_Nompilo_Healthcare_system.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;

namespace E_Nompilo_Healthcare_system.Controllers
{
    public class Fam_Screening2Controller : Controller
    {
        private readonly HealthcareDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<HealthcareSystemUser> _userManager;

        public Fam_Screening2Controller(HealthcareDbContext context, IEmailSender emailSender, UserManager<HealthcareSystemUser> userManager)
        {
            this._userManager = userManager;
            _context = context;
            _emailSender = emailSender;
        }
        //public async Task<IActionResult> Index()
        //{
        //    var userI = await this._userManager.GetUserAsync(User);
        //    string firstName = userI.FirstName;
        //    string lastName = userI.LastName;
        //    var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
        //    ViewBag.Role = roleClaim;
        //    var healthcareDbContext = _context.ContraceptivesRefill.Include(c => c.MainUser);

        //    if (user == null)
        //    {
        //        // Handle the case where the user is not authenticated
        //        return RedirectToAction("Login"); // Redirect to the login page or display an error message
        //    }
        //    var userRefills = _context.ContraceptivesRefill.Where(cr => cr.PatientID == user).ToList();
        //    return View(await healthcareDbContext.ToListAsync());
        //}
        // GET: Fam_Screening2
        public async Task<IActionResult> Index()
        {
            var userI = await this._userManager.GetUserAsync(User);
            string firstName = userI.FirstName;
            string lastName = userI.LastName;
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            ViewBag.Role = roleClaim;
            
            var healthcareDbContext = _context.Fam_Screening.Include(c => c.MainUser).Where(a => a.PatientID == user);
            if (user == null)
            {
                // Handle the case where the user is not authenticated
                return RedirectToAction("Login"); // Redirect to the login page or display an error message
            }
            var userRefills = _context.Fam_Screening.Where(cr => cr.PatientID == user).ToList();
            return View(await healthcareDbContext.ToListAsync());
        }

        public async Task<IActionResult> All_Screenings()
        {
            var healthcareDbContext = _context.Fam_Screening.Include(f => f.MainUser);
            return View(await healthcareDbContext.ToListAsync());
        }


        // GET: Fam_Screening2/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Fam_Screening == null)
            {
                return NotFound();
            }

            var fam_Screening = await _context.Fam_Screening
                .Include(f => f.MainUser)
                .FirstOrDefaultAsync(m => m.ScreeningID == id);
            if (fam_Screening == null)
            {
                return NotFound();
            }

            return View(fam_Screening);
        }

        // GET: Fam_Screening2/Create
        public IActionResult Create()
        {
            return View();
        }

        public HealthcareDbContext Get_context()
        {
            return _context;
        }

        // POST: Fam_Screening2/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScreeningID,Date,PatientID,Status,Question1,Question2,Question3,Question4,Question5,Question6,Question7,Question8,Question9,Question10")] Fam_Screening fam_Screening)
        {

            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Email = User.FindFirstValue(ClaimTypes.Email);
            fam_Screening.PatientID = user;

            int total = 0;
            if (ModelState.IsValid)
            {
                total += Convert.ToInt32(fam_Screening.Question1);
                total += Convert.ToInt32(fam_Screening.Question2);
                total += Convert.ToInt32(fam_Screening.Question3);
                total += Convert.ToInt32(fam_Screening.Question4);
                total += Convert.ToInt32(fam_Screening.Question5);
                total += Convert.ToInt32(fam_Screening.Question6);
                total += Convert.ToInt32(fam_Screening.Question7);
                total += Convert.ToInt32(fam_Screening.Question8);
                total += Convert.ToInt32(fam_Screening.Question9);
                total += Convert.ToInt32(fam_Screening.Question10);
                if (total < 30)
                {
                    TempData["Result"] = "You are not fit for contraceptives";
                }
                else if (total > 30 && total < 51)
                {
                    TempData["Results"] = "Birth control Pills may work for you";
                }
                else if (total > 51 && total <65)
                {
                    TempData["Results"] = "1 year injection can work for you";
                }
                else if (total > 65 && total < 75)
                {
                    TempData["Results"] = "Implant(Nexplanon) may work for you";
                }
                else if (total > 75 && total < 80)
                {
                    TempData["Results"] = "Sterilization may work for you";
                }
                else
                {
                    TempData["Results"] = "3 year injection can work for you";
                }
                fam_Screening.Total = total;
                _context.Add(fam_Screening);
                try
                {
                    await _emailSender.SendEmailAsync("s221442782@mandela.ac.za", "Screening Results", "Screening has been done");
                    var alert = new Alert()
                    {
                        Message = "Screening has been done succesfully",
                        IntendedUser = user,
                        Role = "notification",
                    };
                    _context.Add(alert);
                    await _context.SaveChangesAsync();
                }
                catch(Exception ex)
                {
                //    var admins = _context.UserRoles.Join(_context.Roles);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id", fam_Screening.PatientID);
            return View(fam_Screening);
        }

        // GET: Fam_Screening2/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Fam_Screening == null)
            {
                return NotFound();
            }

            var fam_Screening = await _context.Fam_Screening.FindAsync(id);
            if (fam_Screening == null)
            {
                return NotFound();
            }
            ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id", fam_Screening.PatientID);
            return View(fam_Screening);
        }

        // POST: Fam_Screening2/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScreeningID,Date,PatientID,Status,Question1,Question2,Question3,Question4,Question5,Question6,Question7,Question8,Question9,Question10")] Fam_Screening fam_Screening)
        {
            if (id != fam_Screening.ScreeningID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fam_Screening);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Fam_ScreeningExists(fam_Screening.ScreeningID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id", fam_Screening.PatientID);
            return View(fam_Screening);
        }

        // GET: Fam_Screening2/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Fam_Screening == null)
            {
                return NotFound();
            }

            var fam_Screening = await _context.Fam_Screening
                .Include(f => f.MainUser)
                .FirstOrDefaultAsync(m => m.ScreeningID == id);
            if (fam_Screening == null)
            {
                return NotFound();
            }

            return View(fam_Screening);
        }

        // POST: Fam_Screening2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Fam_Screening == null)
            {
                return Problem("Entity set 'HealthcareDbContext.Fam_Screening'  is null.");
            }
            var fam_Screening = await _context.Fam_Screening.FindAsync(id);
            if (fam_Screening != null)
            {
                _context.Fam_Screening.Remove(fam_Screening);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Fam_ScreeningExists(int id)
        {
          return (_context.Fam_Screening?.Any(e => e.ScreeningID == id)).GetValueOrDefault();
        }
    }
}

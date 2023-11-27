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
using Microsoft.AspNetCore.Identity;

namespace E_Nompilo_Healthcare_system.Controllers
{
    public class ContraceptivesRefillsController : Controller
    {
        private readonly HealthcareDbContext _context;
        private readonly UserManager<HealthcareSystemUser> _userManager;

        public ContraceptivesRefillsController(HealthcareDbContext context, UserManager<HealthcareSystemUser> userManager)
        {
            this._userManager = userManager;
            _context = context;
        }

        // GET: ContraceptivesRefills
        public async Task<IActionResult> Index()
        {
           
            var userI = await this._userManager.GetUserAsync(User);
            string lastName = userI.LastName;
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            ViewBag.Role = roleClaim;
            var healthcareDbContext = _context.ContraceptivesRefill.Include(c => c.MainUser).Where(a => a.PatientID == user);
            if (user == null)
            {
                // Handle the case where the user is not authenticated
                return RedirectToAction("Login"); // Redirect to the login page or display an error message
            }
            var userRefills = _context.ContraceptivesRefill.Where(cr => cr.PatientID == user).ToList();
            return View(await healthcareDbContext.ToListAsync());
        }
        public async Task<IActionResult> ManageContraceptives()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var healthcareDbContext = _context.ContraceptivesRefill.Include(c => c.MainUser);
            return View(await healthcareDbContext.ToListAsync());
        }
        public async Task<IActionResult> ContraceptivesApprove(int? ID)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var health = _context.ContraceptivesRefill.Include(c => c.MainUser).Where(a => a.RefillID == ID).FirstOrDefault();
            health.Status = "Approved";
            _context.ContraceptivesRefill.Update(health);
            await _context.SaveChangesAsync();
            TempData["AlertMessagee"] = "Request for Refill has been Approved!!!";
            return RedirectToAction(nameof(ManageContraceptives
         ));
            TempData["Success"] = "Refill has been Approved";
        }

        public async Task<IActionResult> ContraceptivesDecline(int? ID)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var health = _context.ContraceptivesRefill.Include(c => c.MainUser).Where(a => a.RefillID == ID).FirstOrDefault();
            health.Status = "Regected";
            _context.ContraceptivesRefill.Update(health);
            await _context.SaveChangesAsync();
            TempData["AlertMessagee"] = "Request for Refill has been Declined";
            return RedirectToAction(nameof(ManageContraceptives
           ));
        }

   
        public async Task<IActionResult> Reporting()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.Date = DateTime.Now.ToString("dd/MMMM/yyyy");
            ViewBag.Time = DateTime.Now.ToString("HH:MM");

            var healthcareDbContext = _context.ContraceptivesRefill.Include(c => c.MainUser).Where(a => a.PatientID == user); 
            return View(await healthcareDbContext.ToListAsync());
        }

        // GET: ContraceptivesRefills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ContraceptivesRefill == null)
            {
                return NotFound();
            }

            var contraceptivesRefill = await _context.ContraceptivesRefill
                .Include(c => c.MainUser)
                .FirstOrDefaultAsync(m => m.RefillID == id);
            if (contraceptivesRefill == null)
            {
                return NotFound();
            }

            return View(contraceptivesRefill);
        }

        // GET: ContraceptivesRefills/Create
        public IActionResult Create()
        {

            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
   
            ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: ContraceptivesRefills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContraceptivesRefill contraceptivesRefill)
        {
            var healthcareDbContext = _context.ContraceptivesRefill.Include(c => c.MainUser);
            var userI = await this._userManager.GetUserAsync(User);
            string firstName = userI.FirstName;
            string lastName = userI.LastName;
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Email = User.FindFirstValue(ClaimTypes.Email);
            contraceptivesRefill.PatientID = user;

            if (ModelState.IsValid)
            {
                _context.Add(contraceptivesRefill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id", contraceptivesRefill.PatientID);
            return View(contraceptivesRefill);
        }

        // GET: ContraceptivesRefills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ContraceptivesRefill == null)
            {
                return NotFound();
            }

            var contraceptivesRefill = await _context.ContraceptivesRefill.FindAsync(id);
            if (contraceptivesRefill == null)
            {
                return NotFound();
            }
            ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id", contraceptivesRefill.PatientID);
            return View(contraceptivesRefill);
        }

        // POST: ContraceptivesRefills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  ContraceptivesRefill contraceptivesRefill)
        {
            if (id != contraceptivesRefill.RefillID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    contraceptivesRefill.Status = "Rescheduled";
                    _context.Update(contraceptivesRefill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContraceptivesRefillExists(contraceptivesRefill.RefillID))
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
            ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id", contraceptivesRefill.PatientID);
            return View(contraceptivesRefill);
        }

        // GET: ContraceptivesRefills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ContraceptivesRefill == null)
            {
                return NotFound();
            }

            var contraceptivesRefill = await _context.ContraceptivesRefill
                .Include(c => c.MainUser)
                .FirstOrDefaultAsync(m => m.RefillID == id);
            if (contraceptivesRefill == null)
            {
                return NotFound();
            }

            return View(contraceptivesRefill);
        }

        // POST: ContraceptivesRefills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ContraceptivesRefill == null)
            {
               
                return Problem("Entity set 'HealthcareDbContext.ContraceptivesRefill'  is null.");
            }
            var contraceptivesRefill = await _context.ContraceptivesRefill.FindAsync(id);
            if (contraceptivesRefill != null)
            {
                
                _context.ContraceptivesRefill.Remove(contraceptivesRefill);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContraceptivesRefillExists(int id)
        {
          return (_context.ContraceptivesRefill?.Any(e => e.RefillID == id)).GetValueOrDefault();
        }
    }
}

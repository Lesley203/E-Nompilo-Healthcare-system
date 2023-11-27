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
using Microsoft.AspNetCore.Authorization;

namespace E_Nompilo_Healthcare_system.Controllers
{

    [Authorize]
    public class CounsellingsController : Controller
    {
        private readonly HealthcareDbContext _context;

        public CounsellingsController(HealthcareDbContext context)
        {
            _context = context;
        }

        // GET: Counsellings
        public async Task<IActionResult> Index()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var healthcareDbContext = _context.Counselling.Include(c => c.MainUser).Where(a => a.PatientID == user);
            return View(await healthcareDbContext.ToListAsync());
        }
        public async Task<IActionResult> All_Counsellings()
        {
            var healthcareDbContext = _context.Counselling.Include(c => c.MainUser);
            return View(await healthcareDbContext.ToListAsync());
        }
       // public async Task<IActionResult> CounsellingApprove(string? PatientID)
       // {
       //     var health = _context.Counselling.Include(c => c.MainUser).Where(a => a.PatientID == PatientID).FirstOrDefault();
       //     health.Status = "Approved";
       //     _context.Counselling.Update(health);
       //     await _context.SaveChangesAsync();
       //     return RedirectToAction(nameof(Index
       //         ));
       // }
       // public async Task<IActionResult> CouncellingDecline(int? PatientID)
       // {
       //     var health = _context.Counselling.Include(c => c.MainUser).Where(a => a.ID == PatientID).FirstOrDefault();
       //     health.Status = "Regected";
       //     _context.Counselling.Update(health);
       //     await _context.SaveChangesAsync();
       //     return RedirectToAction(nameof(Index
       //));
       // }

        // public async Task<IActionResult> CouncellingsReschedule(int? ID)
        // {
        //     var health = _context.Counselling.Include(c => c.MainUser).Where(a => a.PatientID == ID).FirstOrDefault();
        //     health.Status = "Rescheduled";
        //     _context.Counselling.Update(health);
        //     await _context.SaveChangesAsync();
        //     return RedirectToAction(nameof(Index
        // ));
        // }

        // GET: Counsellings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Counselling == null)
            {
                return NotFound();
            }

            var counselling = await _context.Counselling
                .Include(c => c.MainUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (counselling == null)
            {
                return NotFound();
            }

            return View(counselling);
        }

        // GET: Counsellings/Create
        public IActionResult Create()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.Con = _context.Counselling.Where(a => a.PatientID == user).ToList();
            //ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Counsellings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(/*[Bind("Id,PatientID,Date,Notes")]*/ Counselling counselling)
        {


            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            counselling.PatientID = user;
            if (ModelState.IsValid)
            {
                _context.Add(counselling);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id", counselling.PatientID);
            return View(counselling);
        }

        // GET: Counsellings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Counselling == null)
            {
                return NotFound();
            }

            var counselling = await _context.Counselling.FindAsync(id);
            if (counselling == null)
            {
                return NotFound();
            }
            ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id", counselling.PatientID);
            return View(counselling);
        }

        // POST: Counsellings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PatientID,Date,Notes")] Counselling counselling)
        {
            if (id != counselling.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(counselling);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CounsellingExists(counselling.Id))
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
            ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id", counselling.PatientID);
            return View(counselling);
        }

        // GET: Counsellings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Counselling == null)
            {
                return NotFound();
            }

            var counselling = await _context.Counselling
                .Include(c => c.MainUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (counselling == null)
            {
                return NotFound();
            }

            return View(counselling);
        }

        // POST: Counsellings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Counselling == null)
            {
                return Problem("Entity set 'HealthcareDbContext.Counselling'  is null.");
            }
            var counselling = await _context.Counselling.FindAsync(id);
            if (counselling != null)
            {
                _context.Counselling.Remove(counselling);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CounsellingExists(int id)
        {
          return (_context.Counselling?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

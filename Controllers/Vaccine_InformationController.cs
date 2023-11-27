using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Nompilo_Healthcare_system.Areas.Identity.Data;
using E_Nompilo_Healthcare_system.Models;

namespace E_Nompilo_Healthcare_system.Controllers
{
    public class Vaccine_InformationController : Controller
    {
        private readonly HealthcareDbContext _context;

        public Vaccine_InformationController(HealthcareDbContext context)
        {
            _context = context;
        }

        // GET: Vaccine_Information
        public IActionResult Vaccine_List_Patient(string VAccineName)
        {
            
            ViewData["CurrentFilter"] = VAccineName;
            var solutions = from b in _context.Vaccine_Information
                            select b;
            if (!String.IsNullOrEmpty(VAccineName))
            {
                solutions = solutions.Where(b => b.VaccineName.Contains(VAccineName));
            }
            return View(solutions);
        }

        public IActionResult Vaccine_Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Vaccine_Create(Vaccine_Information vaccine_)
        {
            if(vaccine_ == null)
            {
                return NotFound();

            }
            if (ModelState.IsValid)
            {
                _context.Vaccine_Information.Add(vaccine_);
                _context.SaveChanges();
                TempData["AlertMessages"] = "Vaccine information is successfully created! ";
                return RedirectToAction("Vaccine_list");
            }
            return View();
        }
        public IActionResult Vaccine_list(string searchEmail)
        {
            
            ViewData["CurrentFilter"] = searchEmail;
            var solutions = from b in _context.Vaccine_Information
                            select b;
            if (!String.IsNullOrEmpty(searchEmail))
            {
                solutions = solutions.Where(b => b.VaccineName.Contains(searchEmail));
            }
            return View(solutions);
        }
        // GET: Vaccine_Information/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null || _context.Vaccine_Information == null)
            {
                return NotFound();
            }

            var vaccine_Information = await _context.Vaccine_Information
                .FirstOrDefaultAsync(m => m.InformationID == id);
            if (vaccine_Information == null)
            {
                return NotFound();
            }

            return View(vaccine_Information);
        }

        // GET: Vaccine_Information/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vaccine_Information/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        

        // GET: Vaccine_Information/Edit/5
        public async Task<IActionResult> VaccineInfo_Edit(int? id)
        {
            if (id == null || _context.Vaccine_Information == null)
            {
                return NotFound();

            }

            var vaccine_Information = await _context.Vaccine_Information.FindAsync(id);
            if (vaccine_Information == null)
            {
                return NotFound();
            }
            return View(vaccine_Information);
        }

        // POST: Vaccine_Information/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VaccineInfo_Edit(int id,  Vaccine_Information vaccine_Information)
        {
            if (id != vaccine_Information.InformationID)
            {

                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vaccine_Information);
                    TempData["AlertMessagesL"] = "Vaccine information is currently updated!";
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Vaccine_InformationExists(vaccine_Information.InformationID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Vaccine_list));
            }
            return View(vaccine_Information);
        }

        // GET: Vaccine_Information/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vaccine_Information == null)
            {
                return NotFound();
            }

            var vaccine_Information = await _context.Vaccine_Information
                .FirstOrDefaultAsync(m => m.InformationID == id);
            if (vaccine_Information == null)
            {
                return NotFound();
            }

            return View(vaccine_Information);
        }

        // POST: Vaccine_Information/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VaccineDelete(int id)
        {
            if (_context.Vaccine_Information == null)
            {
                return Problem("Entity set 'HealthcareDbContext.Vaccine_Information'  is null.");
            }
            var vaccine_Information = await _context.Vaccine_Information.FindAsync(id);
            if (vaccine_Information != null)
            {
                _context.Vaccine_Information.Remove(vaccine_Information);
            }
            
            await _context.SaveChangesAsync();
            TempData["AlertMessaget"] = "Vaccine has been deleted successfully!";
            return RedirectToAction(nameof(Vaccine_list));
        }

        private bool Vaccine_InformationExists(int id)
        {
          return (_context.Vaccine_Information?.Any(e => e.InformationID == id)).GetValueOrDefault();
        }
    }
}

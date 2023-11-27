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
    public class Resourses1Controller : Controller
    {
        private readonly HealthcareDbContext _context;

        public Resourses1Controller(HealthcareDbContext context)
        {
            _context = context;
        }

        // GET: Resourses1
        public async Task<IActionResult> Index()
        {
              return _context.Resourse != null ? 
              View(await _context.Resourse.ToListAsync()) :
              Problem("Entity set 'HealthcareDbContext.Resourse'  is null.");
        }
        public async Task<IActionResult> Info()
        {
            return _context.Resourse != null ?
            View(await _context.Resourse.ToListAsync()) :
            Problem("Entity set 'HealthcareDbContext.Resourse'  is null.");
        }
        // GET: Resourses1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Resourse == null)
            {
                return NotFound();
            }

            var resourse = await _context.Resourse
                .FirstOrDefaultAsync(m => m.InformationID == id);
            if (resourse == null)
            {
                return NotFound();
            }

            return View(resourse);
        }

        // GET: Resourses1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Resourses1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InformationID,ContraceptiveMethods,Effectiveness,HealthBenefits,SideEffects,Reversibility,AvailabilityandAccess,ImageData,ImageType")] Resourse resourse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resourse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(resourse);
        }

        // GET: Resourses1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Resourse == null)
            {
                return NotFound();
            }

            var resourse = await _context.Resourse.FindAsync(id);
            if (resourse == null)
            {
                return NotFound();
            }
            return View(resourse);
        }

        // POST: Resourses1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InformationID,ContraceptiveMethods,Effectiveness,HealthBenefits,SideEffects,Reversibility,AvailabilityandAccess,ImageData,ImageType")] Resourse resourse)
        {
            if (id != resourse.InformationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resourse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResourseExists(resourse.InformationID))
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
            return View(resourse);
        }

        // GET: Resourses1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Resourse == null)
            {
                return NotFound();
            }

            var resourse = await _context.Resourse
                .FirstOrDefaultAsync(m => m.InformationID == id);
            if (resourse == null)
            {
                return NotFound();
            }

            return View(resourse);
        }

        // POST: Resourses1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Resourse == null)
            {
                return Problem("Entity set 'HealthcareDbContext.Resourse'  is null.");
            }
            var resourse = await _context.Resourse.FindAsync(id);
            if (resourse != null)
            {
                _context.Resourse.Remove(resourse);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResourseExists(int id)
        {
          return (_context.Resourse?.Any(e => e.InformationID == id)).GetValueOrDefault();
        }
    }
}

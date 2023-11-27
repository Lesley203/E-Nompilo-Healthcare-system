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
    public class ResoursesController : Controller
    {
        private readonly HealthcareDbContext _context;

        public ResoursesController(HealthcareDbContext context)
        {
            _context = context;
        }

        // GET: Resourses
        public async Task<IActionResult> Index()
        {
              return _context.Resourses != null ? 
                          View(await _context.Resourses.ToListAsync()) :
                          Problem("Entity set 'HealthcareDbContext.Resourses'  is null.");
        }
        public async Task<IActionResult> Information()
        {
            var healthcareDbContext = _context.Resourses.Include(c => c.InformationID);
            return View(await healthcareDbContext.ToListAsync());

            //return _context.Resourses != null ?
            //View(await _context.Resourses.ToListAsync()) :
            //Problem("Entity set 'HealthcareDbContext.Resourses'  is null.");
        }

        // GET: Resourses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Resourses == null)
            {
                return NotFound();
            }

            var resourses = await _context.Resourses
                .FirstOrDefaultAsync(m => m.InformationID == id);
            if (resourses == null)
            {
                return NotFound();
            }

            return View(resourses);
        }

        // GET: Resourses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Resourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Resourses resourses, IFormFile imagefile)
        {
            //resourses.DateCreated = DateTime.Now.ToString("dd/MMMM/yyyy");
            //resourses.Status = "Active";

            if (ModelState.IsValid)
            {
                if(imagefile != null && imagefile.Length > 0)
                    {
                        using ( var memorystream = new MemoryStream())
                        {
                            await imagefile.CopyToAsync(memorystream);

                        resourses.ImageData = memorystream.ToArray();

                        resourses.ImageType = imagefile.ContentType;

                    }
                    }
                
                
                _context.Add(resourses);
                await _context.SaveChangesAsync();

            }
            return View(resourses);
        }

        // GET: Resourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Resourses == null)
            {
                return NotFound();
            }

            var resourses = await _context.Resourses.FindAsync(id);
            if (resourses == null)
            {
                return NotFound();
            }
            return View(resourses);
        }

        // POST: Resourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InformationID,ContraceptiveMethods,Effectiveness,HealthBenefits,SideEffects,Reversibility,AvailabilityandAccess,Image")] Resourses resourses)
        {
            if (id != resourses.InformationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resourses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResoursesExists(resourses.InformationID))
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
            return View(resourses);
        }

        // GET: Resourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Resourses == null)
            {
                return NotFound();
            }

            var resourses = await _context.Resourses
                .FirstOrDefaultAsync(m => m.InformationID == id);
            if (resourses == null)
            {
                return NotFound();
            }

            return View(resourses);
        }

        // POST: Resourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Resourses == null)
            {
                return Problem("Entity set 'HealthcareDbContext.Resourses'  is null.");
            }
            var resourses = await _context.Resourses.FindAsync(id);
            if (resourses != null)
            {
                _context.Resourses.Remove(resourses);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResoursesExists(int id)
        {
          return (_context.Resourses?.Any(e => e.InformationID == id)).GetValueOrDefault();
        }
    }
}

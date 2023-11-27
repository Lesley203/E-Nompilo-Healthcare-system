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
    public class Pre_Natal_Care_AlertController : Controller
    {
        private readonly HealthcareDbContext _context;

        public Pre_Natal_Care_AlertController(HealthcareDbContext context)
        {
            _context = context;
        }

        // GET: Pre_Natal_Care_Alert
        public async Task<IActionResult> Index()
        {
              return _context.Pre_Natal_Care_Alert != null ? 
                          View(await _context.Pre_Natal_Care_Alert.ToListAsync()) :
                          Problem("Entity set 'HealthcareDbContext.Pre_Natal_Care_Alert'  is null.");
        }

        // GET: Pre_Natal_Care_Alert/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pre_Natal_Care_Alert == null)
            {
                return NotFound();
            }

            var pre_Natal_Care_Alert = await _context.Pre_Natal_Care_Alert
                .FirstOrDefaultAsync(m => m.AlertID == id);
            if (pre_Natal_Care_Alert == null)
            {
                return NotFound();
            }

            return View(pre_Natal_Care_Alert);
        }

        // GET: Pre_Natal_Care_Alert/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pre_Natal_Care_Alert/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AlertID,Message,Date,status,LastView,Role,IntendedUser")] Pre_Natal_Care_Alert pre_Natal_Care_Alert)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pre_Natal_Care_Alert);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pre_Natal_Care_Alert);
        }

        // GET: Pre_Natal_Care_Alert/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pre_Natal_Care_Alert == null)
            {
                return NotFound();
            }

            var pre_Natal_Care_Alert = await _context.Pre_Natal_Care_Alert.FindAsync(id);
            if (pre_Natal_Care_Alert == null)
            {
                return NotFound();
            }
            return View(pre_Natal_Care_Alert);
        }

        // POST: Pre_Natal_Care_Alert/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AlertID,Message,Date,status,LastView,Role,IntendedUser")] Pre_Natal_Care_Alert pre_Natal_Care_Alert)
        {
            if (id != pre_Natal_Care_Alert.AlertID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pre_Natal_Care_Alert);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Pre_Natal_Care_AlertExists(pre_Natal_Care_Alert.AlertID))
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
            return View(pre_Natal_Care_Alert);
        }

        // GET: Pre_Natal_Care_Alert/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pre_Natal_Care_Alert == null)
            {
                return NotFound();
            }

            var pre_Natal_Care_Alert = await _context.Pre_Natal_Care_Alert
                .FirstOrDefaultAsync(m => m.AlertID == id);
            if (pre_Natal_Care_Alert == null)
            {
                return NotFound();
            }

            return View(pre_Natal_Care_Alert);
        }

        // POST: Pre_Natal_Care_Alert/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pre_Natal_Care_Alert == null)
            {
                return Problem("Entity set 'HealthcareDbContext.Pre_Natal_Care_Alert'  is null.");
            }
            var pre_Natal_Care_Alert = await _context.Pre_Natal_Care_Alert.FindAsync(id);
            if (pre_Natal_Care_Alert != null)
            {
                _context.Pre_Natal_Care_Alert.Remove(pre_Natal_Care_Alert);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Pre_Natal_Care_AlertExists(int id)
        {
          return (_context.Pre_Natal_Care_Alert?.Any(e => e.AlertID == id)).GetValueOrDefault();
        }
    }
}

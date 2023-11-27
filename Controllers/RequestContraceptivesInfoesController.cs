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

namespace E_Nompilo_Healthcare_system.Controllers
{
    public class RequestContraceptivesInfoesController : Controller
    {
        private readonly HealthcareDbContext _context;

        public RequestContraceptivesInfoesController(HealthcareDbContext context)
        {
            _context = context;
        }

        // GET: RequestContraceptivesInfoes
        public async Task<IActionResult> Index()
        {
            var healthcareDbContext = _context.RequestContraceptivesInfo.Include(r => r.MainUser);
            return View(await healthcareDbContext.ToListAsync());
        }
        public async Task<IActionResult> Contaceptives_Info()
        {
            var healthcareDbContext = _context.RequestContraceptivesInfo.Include(r => r.MainUser);
            return View(await healthcareDbContext.ToListAsync());
        }
        public async Task<IActionResult> ContraceptiveReporting()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.Date = DateTime.Now.ToString("dd/MMMM/yyyy");
            ViewBag.Time = DateTime.Now.ToString("HH:MM");

            var healthcareDbContext = _context.RequestContraceptivesInfo.Include(r => r.MainUser);
            return View(await healthcareDbContext.ToListAsync());
        }

        // GET: RequestContraceptivesInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RequestContraceptivesInfo == null)
            {
                return NotFound();
            }

            var requestContraceptivesInfo = await _context.RequestContraceptivesInfo
                .Include(r => r.MainUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requestContraceptivesInfo == null)
            {
                return NotFound();
            }

            return View(requestContraceptivesInfo);
        }

        // GET: RequestContraceptivesInfoes/Create
        public IActionResult Create()
        {
            ViewBag.Inforam = _context.Resourse.ToList();
            ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PatientID,Date,Contraceptivetype,AdditionalInfo")] RequestContraceptivesInfo requestContraceptivesInfo)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Email = User.FindFirstValue(ClaimTypes.Email);
            requestContraceptivesInfo.PatientID = user;

            if (ModelState.IsValid)
            {
                _context.Add(requestContraceptivesInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id", requestContraceptivesInfo.PatientID);
            return View(requestContraceptivesInfo);
        }

        // GET: RequestContraceptivesInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RequestContraceptivesInfo == null)
            {
                return NotFound();
            }

            var requestContraceptivesInfo = await _context.RequestContraceptivesInfo.FindAsync(id);
            if (requestContraceptivesInfo == null)
            {
                return NotFound();
            }
            ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id", requestContraceptivesInfo.PatientID);
            return View(requestContraceptivesInfo);
        }

        // POST: RequestContraceptivesInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PatientID,Date,Contraceptivetype,AdditionalInfo")] RequestContraceptivesInfo requestContraceptivesInfo)
        {
            if (id != requestContraceptivesInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(requestContraceptivesInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestContraceptivesInfoExists(requestContraceptivesInfo.Id))
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
            ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id", requestContraceptivesInfo.PatientID);
            return View(requestContraceptivesInfo);
        }

        // GET: RequestContraceptivesInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RequestContraceptivesInfo == null)
            {
                return NotFound();
            }

            var requestContraceptivesInfo = await _context.RequestContraceptivesInfo
                .Include(r => r.MainUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requestContraceptivesInfo == null)
            {
                return NotFound();
            }

            return View(requestContraceptivesInfo);
        }

        // POST: RequestContraceptivesInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RequestContraceptivesInfo == null)
            {
                return Problem("Entity set 'HealthcareDbContext.RequestContraceptivesInfo'  is null.");
            }
            var requestContraceptivesInfo = await _context.RequestContraceptivesInfo.FindAsync(id);
            if (requestContraceptivesInfo != null)
            {
                _context.RequestContraceptivesInfo.Remove(requestContraceptivesInfo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequestContraceptivesInfoExists(int id)
        {
          return (_context.RequestContraceptivesInfo?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

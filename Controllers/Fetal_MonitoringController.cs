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
    public class Fetal_MonitoringController : Controller
    {
        private readonly HealthcareDbContext _context;
        private readonly IEmailSender _emailSender;
        private string? user;

        public object ClaimType { get; private set; }
        public Fetal_MonitoringController(HealthcareDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        // GET: Fetal_Monitoring
        public async Task<IActionResult> Index()
        {
            var healthcareDbContext = _context.Fetal_Monitoring.Include(f => f.MainUser).Where(a => a.PatientID == user); ;
            return View(await healthcareDbContext.ToListAsync());
        }

        // GET: Fetal_Monitoring/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Fetal_Monitoring == null)
            {
                return NotFound();
            }

            var fetal_Monitoring = await _context.Fetal_Monitoring
                .Include(f => f.MainUser)
                .FirstOrDefaultAsync(m => m.FetalMonitorID == id);
            if (fetal_Monitoring == null)
            {
                return NotFound();
            }

            return View(fetal_Monitoring);
        }

        // GET: Fetal_Monitoring/Create
        public IActionResult Create()
        {
            ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Fetal_Monitoring/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Fetal_Monitoring fetal_Monitoring)
        {

            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var email = User.FindFirstValue(ClaimTypes.Email);
            fetal_Monitoring.PatientID = user;
            int total = 0;
            if (ModelState.IsValid)
            {
                total += Convert.ToInt32(fetal_Monitoring.Question1);
                total += Convert.ToInt32(fetal_Monitoring.Question2);
                total += Convert.ToInt32(fetal_Monitoring.Question3);
                total += Convert.ToInt32(fetal_Monitoring.Question4);
                total += Convert.ToInt32(fetal_Monitoring.Question5);
                total += Convert.ToInt32(fetal_Monitoring.Question6);
                total += Convert.ToInt32(fetal_Monitoring.Question7);
                total += Convert.ToInt32(fetal_Monitoring.Question8);
                total += Convert.ToInt32(fetal_Monitoring.Question9);
                total += Convert.ToInt32(fetal_Monitoring.Question10);
                total += Convert.ToInt32(fetal_Monitoring.Question11);
                if(total == 0)
                {
                    TempData["Results"] = "Error: No choice selected, Please make a selection before submitting.";
                }
                else if (total < 40)
                {
                    TempData["Results"] = "Sorry, It seems like you were not informed and proactive in monitoring your baby's movements and heart rate, not that you will be referred to someone who will assist you with this matter look up to hear from us, thank you. ";

                }
                else if (total > 40)
                {
                    TempData["Results"] = "Congratulations on your proactive approach to your prenatal care! Your commitment to staying informed and engaged throughout your pregnancy is truly commendable. Your dedication to your own health and the well-being of your baby is a testament to your strength and love as a mother.";

                }
                fetal_Monitoring.Total = total;
                _context.Add(fetal_Monitoring);
                try
                {
                    await _emailSender.SendEmailAsync("Patient@gamail.com", "Fetal Monitoring Results", "You have successfully monitored the baby");
                    var pre_Natal_Care_Alert = new Pre_Natal_Care_Alert()
                    {

                        Message = "Nutritions has been tracked Successfully",
                        IntendedUser = user,
                        Role = "notification",
                    };
                    _context.Pre_Natal_Care_Alert.Add(pre_Natal_Care_Alert);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                }
                 

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id", fetal_Monitoring.PatientID);
            return View(fetal_Monitoring);
        }

        // GET: Fetal_Monitoring/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Fetal_Monitoring == null)
            {
                return NotFound();
            }

            var fetal_Monitoring = await _context.Fetal_Monitoring.FindAsync(id);
            if (fetal_Monitoring == null)
            {
                return NotFound();
            }
            ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id", fetal_Monitoring.PatientID);
            return View(fetal_Monitoring);
        }

        // POST: Fetal_Monitoring/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FetalMonitorID,Date,PatientID,Status,Question1,Question2,Question3,Question4,Question5,Question6,Question7,Question8,Question9,Question10,Question11,Total")] Fetal_Monitoring fetal_Monitoring)
        {
            if (id != fetal_Monitoring.FetalMonitorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fetal_Monitoring);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Fetal_MonitoringExists(fetal_Monitoring.FetalMonitorID))
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
            ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id", fetal_Monitoring.PatientID);
            return View(fetal_Monitoring);
        }

        // GET: Fetal_Monitoring/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Fetal_Monitoring == null)
            {
                return NotFound();
            }

            var fetal_Monitoring = await _context.Fetal_Monitoring
                .Include(f => f.MainUser)
                .FirstOrDefaultAsync(m => m.FetalMonitorID == id);
            if (fetal_Monitoring == null)
            {
                return NotFound();
            }

            return View(fetal_Monitoring);
        }

        // POST: Fetal_Monitoring/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Fetal_Monitoring == null)
            {
                return Problem("Entity set 'HealthcareDbContext.Fetal_Monitoring'  is null.");
            }
            var fetal_Monitoring = await _context.Fetal_Monitoring.FindAsync(id);
            if (fetal_Monitoring != null)
            {
                _context.Fetal_Monitoring.Remove(fetal_Monitoring);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Fetal_MonitoringExists(int id)
        {
            return (_context.Fetal_Monitoring?.Any(e => e.FetalMonitorID == id)).GetValueOrDefault();
        }
    }
}

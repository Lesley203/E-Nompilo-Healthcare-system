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
    public class Ultra_Sound_ImageController : Controller
    {
        private readonly HealthcareDbContext _context;
        private readonly IEmailSender _emailSender;

        public object ClaimType { get; private set; }

        public Ultra_Sound_ImageController(HealthcareDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        // GET: Ultra_Sound_Image
        public async Task<IActionResult> Index()
        {
            var healthcareDbContext = _context.Ultra_Sound_Image.Include(u => u.MainUser);
            return View(await healthcareDbContext.ToListAsync());
        }

        // GET: Ultra_Sound_Image/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ultra_Sound_Image == null)
            {
                return NotFound();
            }

            var ultra_Sound_Image = await _context.Ultra_Sound_Image
                .Include(u => u.MainUser)
                .FirstOrDefaultAsync(m => m.NutritionTrackingID == id);
            if (ultra_Sound_Image == null)
            {
                return NotFound();
            }

            return View(ultra_Sound_Image);
        }

        // GET: Ultra_Sound_Image/Create
        public IActionResult Create()
        {
            ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Ultra_Sound_Image/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ultra_Sound_Image ultra_Sound_Image)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var email = User.FindFirstValue(ClaimTypes.Email);
            ultra_Sound_Image.PatientID = user;
            int total = 0;
            if (ModelState.IsValid)
            {

                total += Convert.ToInt32(ultra_Sound_Image.Question1);
                total += Convert.ToInt32(ultra_Sound_Image.Question2);
                total += Convert.ToInt32(ultra_Sound_Image.Question3);
                total += Convert.ToInt32(ultra_Sound_Image.Question4);

                if (total <= 20)
                {
                    TempData["Results"] = "Welcome to Pre-Natal Care.During the early stages of pregnancy, detailed ultrasound images may not be available due to the small embryo size. Please focus on confirming the pregnancy, estimating gestational age, and checking for the gestational sac. More detailed images will be available in later stages of pregnancy.";
                    TempData["Image"] = 1;
                }
                else if (total > 20 && total <= 40)
                {
                    TempData["Results"] = "fetal heartbeat: FHR: 120 bpm)";
                    TempData["Image"] = 2;
                }
                else if (total > 40)
                {
                    TempData["Results"] = "Fetal measurements :CRL: 4.5 cm, fetal position: Vertex presentation, fetal heart rate: FHR: 140 bpm, anatomical findings: Heart,Spine, and clinical notes: No visible abnormalities detected.";
                    TempData["Image"] = 3;
                }
                ultra_Sound_Image.Total = total;
                _context.Add(ultra_Sound_Image);
                try
                {
                    await _emailSender.SendEmailAsync("Patient@gamail.com", "Ultrasound Image Results", "Your ultrasound has successfully sent to you");
                    var alert = new Alert()
                    {
                        Message = "Nutritions has been tracked Successfully",
                        IntendedUser = user,
                        Role = "notification",
                    };
                    //_context.Alert.Add(alert);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                }



                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id", ultra_Sound_Image.PatientID);
            return View(ultra_Sound_Image);
        }

        // GET: Ultra_Sound_Image/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ultra_Sound_Image == null)
            {
                return NotFound();
            }

            var ultra_Sound_Image = await _context.Ultra_Sound_Image.FindAsync(id);
            if (ultra_Sound_Image == null)
            {
                return NotFound();
            }
            ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id", ultra_Sound_Image.PatientID);
            return View(ultra_Sound_Image);
        }

        // POST: Ultra_Sound_Image/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NutritionTrackingID,Date,PatientID,Status,Question1,Question2,Question3,Question4")] Ultra_Sound_Image ultra_Sound_Image)
        {
            if (id != ultra_Sound_Image.NutritionTrackingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ultra_Sound_Image);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Ultra_Sound_ImageExists(ultra_Sound_Image.NutritionTrackingID))
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
            ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id", ultra_Sound_Image.PatientID);
            return View(ultra_Sound_Image);
        }

        // GET: Ultra_Sound_Image/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ultra_Sound_Image == null)
            {
                return NotFound();
            }

            var ultra_Sound_Image = await _context.Ultra_Sound_Image
                .Include(u => u.MainUser)
                .FirstOrDefaultAsync(m => m.NutritionTrackingID == id);
            if (ultra_Sound_Image == null)
            {
                return NotFound();
            }

            return View(ultra_Sound_Image);
        }

        // POST: Ultra_Sound_Image/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ultra_Sound_Image == null)
            {
                return Problem("Entity set 'HealthcareDbContext.Ultra_Sound_Image'  is null.");
            }
            var ultra_Sound_Image = await _context.Ultra_Sound_Image.FindAsync(id);
            if (ultra_Sound_Image != null)
            {
                _context.Ultra_Sound_Image.Remove(ultra_Sound_Image);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Ultra_Sound_ImageExists(int id)
        {
            return (_context.Ultra_Sound_Image?.Any(e => e.NutritionTrackingID == id)).GetValueOrDefault();
        }
    }
}

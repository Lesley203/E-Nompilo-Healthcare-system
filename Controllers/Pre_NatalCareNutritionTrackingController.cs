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
using System.ComponentModel.DataAnnotations;

namespace E_Nompilo_Healthcare_system.Controllers
{
    public class Pre_NatalCareNutritionTrackingController : Controller
    {
        private readonly HealthcareDbContext _context;
        private readonly IEmailSender _emailSender;

        public object ClaimType { get; private set; }

        public Pre_NatalCareNutritionTrackingController(HealthcareDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        // GET: Pre_NatalCareNutritionTracking
        public async Task<IActionResult> Index()
        {

            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var healthcareDbContext = _context.Pre_NatalCareNutritionTracking.Include(p => p.MainUser).Where(a => a.PatientID == user);
            return View(await healthcareDbContext.ToListAsync());
        }
        public async Task<IActionResult> TrackingHistory()
        {

            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var healthcareDbContext = _context.Pre_NatalCareNutritionTracking.Include(p => p.MainUser);
            return View(await healthcareDbContext.ToListAsync());
        }

        // GET: Pre_NatalCareNutritionTracking/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pre_NatalCareNutritionTracking == null)
            {
                return NotFound();
            }

            var pre_NatalCareNutritionTracking = await _context.Pre_NatalCareNutritionTracking
                .Include(p => p.MainUser)
                .FirstOrDefaultAsync(m => m.NutritionTrackingID == id);
            if (pre_NatalCareNutritionTracking == null)
            {
                return NotFound();
            }

            return View(pre_NatalCareNutritionTracking);
        }

        // GET: Pre_NatalCareNutritionTracking/Create
        public IActionResult Create()
        {
            ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Pre_NatalCareNutritionTracking/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pre_NatalCareNutritionTracking pre_NatalCareNutritionTracking)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var email = User.FindFirstValue(ClaimTypes.Email);
            pre_NatalCareNutritionTracking.PatientID = user;
            int total = 0;
            
            if (ModelState.IsValid )
            {
              

                total += Convert.ToInt32(pre_NatalCareNutritionTracking.Question1);
                total += Convert.ToInt32(pre_NatalCareNutritionTracking.Question2);
                total += Convert.ToInt32(pre_NatalCareNutritionTracking.Question3);
                total += Convert.ToInt32(pre_NatalCareNutritionTracking.Question4);
                total += Convert.ToInt32(pre_NatalCareNutritionTracking.Question5);
                total += Convert.ToInt32(pre_NatalCareNutritionTracking.Question6);
                total += Convert.ToInt32(pre_NatalCareNutritionTracking.Question7);
                total += Convert.ToInt32(pre_NatalCareNutritionTracking.Question8);
                total += Convert.ToInt32(pre_NatalCareNutritionTracking.Question9);
                total += Convert.ToInt32(pre_NatalCareNutritionTracking.Question10);
                total += Convert.ToInt32(pre_NatalCareNutritionTracking.Question11);
                total += Convert.ToInt32(pre_NatalCareNutritionTracking.Question12);
                if(total == 0)
                {
                    TempData["Results"] = "Error: No choice selected, Please make a selection before submitting.";
                    TempData["Image"] = 1;
                }
                else if (total < 30)
                {
                    TempData["Results"] = "Poor Nutrition, Reduce your intake of processed and sugary food, take prenatal care vitamins and consume food rich in Iron or with calcium to prevent chances of anaemia, pre-enclampsia, haemorrrphage and developmental delays of your child.";
                    TempData["Image"] = 2;
                }
                else if (total > 30 && total < 59)
                {
                    TempData["Results"] = "well-done,  you're already making changes for the better in your nutrition. With continued effort, you'll achieve the level of nutrition you desire for a healthy pregnancy.";
                    TempData["Image"] = 3;
                }
                else if (total > 59)
                {
                    TempData["Results"] = "Congratulations, you're doing an excellent job with your prenatal nutrition. You're efforts are not only benefiting you but also ensuring the best possible start for your baby.";
                    TempData["Image"] = 4;
                }
                pre_NatalCareNutritionTracking.Total = total;
                _context.Add(pre_NatalCareNutritionTracking);
                try
                {
                    await _emailSender.SendEmailAsync("Patient@gamail.com", "Nutrition Tracking Results", "Nutritions Has been tracked completely");
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
            ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id", pre_NatalCareNutritionTracking.PatientID);
            return View(pre_NatalCareNutritionTracking);
        }

        // GET: Pre_NatalCareNutritionTracking/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pre_NatalCareNutritionTracking == null)
            {
                return NotFound();
            }

            var pre_NatalCareNutritionTracking = await _context.Pre_NatalCareNutritionTracking.FindAsync(id);
            if (pre_NatalCareNutritionTracking == null)
            {
                return NotFound();
            }
            ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id", pre_NatalCareNutritionTracking.PatientID);
            return View(pre_NatalCareNutritionTracking);
        }

        // POST: Pre_NatalCareNutritionTracking/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NutritionTrackingID,Date,PatientID,Status,Question1,Question2,Question3,Question4,Question5,Question6,Question7,Question8,Question9,Question10,Question11,Question12")] Pre_NatalCareNutritionTracking pre_NatalCareNutritionTracking)
        {
            if (id != pre_NatalCareNutritionTracking.NutritionTrackingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pre_NatalCareNutritionTracking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Pre_NatalCareNutritionTrackingExists(pre_NatalCareNutritionTracking.NutritionTrackingID))
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
            ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id", pre_NatalCareNutritionTracking.PatientID);
            return View(pre_NatalCareNutritionTracking);
        }

        // GET: Pre_NatalCareNutritionTracking/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pre_NatalCareNutritionTracking == null)
            {
                return NotFound();
            }

            var pre_NatalCareNutritionTracking = await _context.Pre_NatalCareNutritionTracking
                .Include(p => p.MainUser)
                .FirstOrDefaultAsync(m => m.NutritionTrackingID == id);
            if (pre_NatalCareNutritionTracking == null)
            {
                return NotFound();
            }

            return View(pre_NatalCareNutritionTracking);
        }

        // POST: Pre_NatalCareNutritionTracking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pre_NatalCareNutritionTracking == null)
            {
                return Problem("Entity set 'HealthcareDbContext.Pre_NatalCareNutritionTracking'  is null.");
            }
            var pre_NatalCareNutritionTracking = await _context.Pre_NatalCareNutritionTracking.FindAsync(id);
            if (pre_NatalCareNutritionTracking != null)
            {
                _context.Pre_NatalCareNutritionTracking.Remove(pre_NatalCareNutritionTracking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Pre_NatalCareNutritionTrackingExists(int id)
        {
            return (_context.Pre_NatalCareNutritionTracking?.Any(e => e.NutritionTrackingID == id)).GetValueOrDefault();
        }
    }
}

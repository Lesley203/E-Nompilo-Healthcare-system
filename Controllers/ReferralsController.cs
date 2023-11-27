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
    public class ReferralsController : Controller
    {
        private readonly HealthcareDbContext dbContext;

        public ReferralsController(HealthcareDbContext db)
        {
            dbContext = db;
        }


        public IActionResult Index()
        {
            IEnumerable<Referrals>? ListRaferral = dbContext.referrals.Include(a => a.MainUsers);

            return View(ListRaferral);
        }
        public async Task<IActionResult> MyRefferals()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IEnumerable<Referrals>? ListRaferral = dbContext.referrals.Include(a => a.MainUsers).Where(a => a.Patient == user);
            var pre_Natal_Care_Alert = dbContext.Pre_Natal_Care_Alert.Where(a => a.IntendedUser == user).OrderByDescending(a => a.Date).ToList();
            if (pre_Natal_Care_Alert.Count > 0)
            {
                ViewBag.Pre_Natal_Care_Alert = pre_Natal_Care_Alert;
                TempData["Alerts"] = "Not Null";
            }

            return View(ListRaferral);



        }
        public async Task<IActionResult> PreNatalCareReport()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.Date = DateTime.Now.ToString("dd/MMMM/yyyy");
            ViewBag.Time = DateTime.Now.ToString("HH:mm");
            IEnumerable<Referrals>? ListRaferral = dbContext.referrals.Include(a => a.MainUsers).Where(a => a.Patient == user);
            var pre_Natal_Care_Alert = dbContext.Pre_Natal_Care_Alert.Where(a => a.IntendedUser == user).OrderByDescending(a => a.Date).ToList();
            if (pre_Natal_Care_Alert.Count > 0)
            {
                ViewBag.Pre_Natal_Care_Alert = pre_Natal_Care_Alert;
                TempData["Alerts"] = "Not Null";
            }

            return View(ListRaferral);

        }


        public IActionResult Create()
        {
            ViewBag.Patinet = (from U in dbContext.Users
                               join UR in dbContext.UserRoles on U.Id equals UR.UserId
                               join R in dbContext.Roles on UR.RoleId equals R.Id
                               where R.Name == "PATIENT"
                               select U).ToList();

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Referrals Ref)
        {
            if (ModelState.IsValid)
            {
                dbContext.referrals.Add(Ref);

                dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));

            }
            return View(Ref);
        }


        public async Task<IActionResult> Edit(int? ID)
        {
            if (ID == null || dbContext.referrals == null)
            {
                return NotFound();
            }

            var obj = await dbContext.referrals.FindAsync(ID);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int ID, Referrals Ref)
        {
            if (ID != Ref.ID)
            {
                return NotFound();
            }

            
  
            if (ModelState.IsValid)
            {
                try
                {
                    dbContext.Update(Ref);
                    await dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExampleExists(Ref.ID))
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
            return View(Ref);
        }


        public async Task<IActionResult> Delete(int? ID)
        {
            if (ID == null || dbContext.referrals == null)
            {
                return NotFound();
            }

            var obj = await dbContext.referrals
                .FirstOrDefaultAsync(m => m.ID == ID);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var obj = await dbContext.referrals.FindAsync(id);
            if (obj != null)
            {
                dbContext.referrals.Remove(obj);
            }

            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExampleExists(int ID)
        {
            return (dbContext.referrals?.Any(e => e.ID == ID)).GetValueOrDefault();
        }
    }
}

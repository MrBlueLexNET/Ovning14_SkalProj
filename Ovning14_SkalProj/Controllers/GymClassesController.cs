using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ovning14_SkalProj.Core.ViewModels;
using Ovning14_SkalProj.Data;
using Ovning14_SkalProj.Core.Entities;
using Ovning14_SkalProj.Models;
using System.Security.Claims;
using Ovning14_SkalProj.Extensions;

namespace Ovning14_SkalProj.Controllers
{
    public class GymClassesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public GymClassesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        [Authorize]

        //BookingToggle
        public async Task<IActionResult> BookingToggle(int? id) 
            {
            if (id == null || _context.GymClasses == null) { return NotFound(); }
            
            var userId = userManager.GetUserId(User);
            if (userId == null) { return BadRequest(); }


            //var currentClass = _context.GymClasses.Include(g => g.AttendingMembers)
            //                                        .FirstOrDefault(g => g.GymClassId == id);
            //var attendingClasses = currentClass?.AttendingMembers.FirstOrDefault(a => a.ApplicationUserId == userId);

            //Query direct the Join tabel [AppUsersGymClasses] since we have the composit Key (userId and GymClassId)
            var attendingClasses = await _context.AppUsersGymClasses.FindAsync(userId, id);

            if (attendingClasses == null)
            {
                var booking = new ApplicationUserGymClass
                {
                    ApplicationUserId = userId,
                    GymClassId = (int)id
                };

                _context.AppUsersGymClasses.Add(booking);
            }
            else
            {
                _context.AppUsersGymClasses.Remove(attendingClasses);
            }

            await _context.SaveChangesAsync();

           
            return RedirectToAction(nameof(Index));
        }
        // GET: GymClasses
        public async Task<IActionResult> Index()
        {
            //return _context.GymClasses != null ? 
            //            View(await _context.GymClasses.ToListAsync()) :
            //            Problem("Entity set 'ApplicationDbContext.GymClasses'  is null.");
            // List<GymClass> model = await uow.GymClassRepository.GetAsync();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) { return BadRequest(); }
            //var userId = userManager.GetUserId(User);

            var model = (await _context.GymClasses.Include(g => g.AttendingMembers)
                                                     .Select(g => new GymClassViewModel
                                                     {
                                                         GymClassId = g.GymClassId,
                                                         Name = g.Name,
                                                         Duration = g.Duration,
                                                         StartTime = g.StartTime,
                                                         Attending = g.AttendingMembers.Any(a => a.ApplicationUserId == userId)
                                                    })
                                                     .ToListAsync());
        

            return View(model);
        }
    

        // GET: GymClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GymClasses == null)
            {
                return NotFound();
            }

            var gymClass = await _context.GymClasses
                .Include(v => v.AttendingMembers)
                .FirstOrDefaultAsync(m => m.GymClassId == id);
            if (gymClass == null)
            {
                return NotFound();
            }

            return View(gymClass);
        }

        // GET: GymClasses/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        // GET: GymClasses/Create
        public IActionResult Create()
        {
            return Request.IsAjax() ? PartialView("CreatePartial") : View();
        }

        public IActionResult FetchForm()
        {
            return PartialView("CreatePartial");
        }

        // POST: GymClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GymClassId,Name,StartTime,Duration,Description")] GymClass gymClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gymClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gymClass);
        }

        // GET: GymClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GymClasses == null)
            {
                return NotFound();
            }

            var gymClass = await _context.GymClasses
                .Include(v => v.AttendingMembers)
                .FirstOrDefaultAsync(m => m.GymClassId == id);
            if (gymClass == null)
            {
                return NotFound();
            }
            return View(gymClass);
        }

        // POST: GymClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GymClassId,Name,StartTime,Duration,Description")] GymClass gymClass)
        {
            if (id != gymClass.GymClassId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gymClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GymClassExists(gymClass.GymClassId))
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
            return View(gymClass);
        }

        // GET: GymClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GymClasses == null)
            {
                return NotFound();
            }

            var gymClass = await _context.GymClasses
                .FirstOrDefaultAsync(m => m.GymClassId == id);
            if (gymClass == null)
            {
                return NotFound();
            }

            return View(gymClass);
        }

        // POST: GymClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GymClasses == null)
            {
                return Problem("Entity set 'ApplicationDbContext.GymClasses'  is null.");
            }
            var gymClass = await _context.GymClasses.FindAsync(id);
            if (gymClass != null)
            {
                _context.GymClasses.Remove(gymClass);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GymClassExists(int id)
        {
          return (_context.GymClasses?.Any(e => e.GymClassId == id)).GetValueOrDefault();
        }
    }
}

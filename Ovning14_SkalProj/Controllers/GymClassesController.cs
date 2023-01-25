using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ovning14_SkalProj.Data;
using Ovning14_SkalProj.Models;

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
            if (id == null || _context.GymClasses == null)
            {
                return NotFound();
            }
            var userId = userManager.GetUserId(User);
            if (userId == null) { return BadRequest(); }

            var currentClass = _context.GymClasses.Include(g => g.AttendingMembers)
                                                    .FirstOrDefault(g => g.GymClassId == id);
            
            var attendingClasses = currentClass?.AttendingMembers.FirstOrDefault(a => a.ApplicationUserId == userId);

            if (User.Identity.IsAuthenticated)
            {
                //if the member is included among the AttendingMembers do mothing else add 
                //ConnectExistingUserAndGymClassObjects();

                async void ConnectExistingUserAndGymClassObjects()
                {
                    var userA = userManager.GetUserId(User);
                    var classA = _context.GymClasses.Find(id);
                    classA.AttendingMembers.Add(userA);
                    _context.SaveChanges();
                }

                //UnAssignAnUserFromAClass();
                void UnAssignAnUserFromAClass()
                {
                    var memberwithclass = _context.GymClasses
                        .Include(c => c.AttendingMembers.Where(a => a.GymClassId == id))
                        .FirstOrDefault(c => c.ApplicationUserId == userId);
                    //AttendingMembers.GymClasses.RemoveAt(0);
                    _context.GymClasses.Remove(memberwithclass.userId[0]);
                    _context.ChangeTracker.DetectChanges();
                    var debugview = _context.ChangeTracker.DebugView.ShortView;
                    //_context.SaveChanges();
                }

                Console.WriteLine(userId); 
                var memberWithClasses = await _context.GymClasses.Include(a => a.AttendingMembers)
                    .FirstOrDefaultAsync(m => m.GymClassId == id);

            }
            else 
            {
                Console.WriteLine("User not login yet");
            }
            return RedirectToAction(nameof(Index));
        }
        // GET: GymClasses
        public async Task<IActionResult> Index()
        {
              return _context.GymClasses != null ? 
                          View(await _context.GymClasses.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.GymClasses'  is null.");
        }

        // GET: GymClasses/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: GymClasses/Create
        public IActionResult Create()
        {
            return View();
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

            var gymClass = await _context.GymClasses.FindAsync(id);
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

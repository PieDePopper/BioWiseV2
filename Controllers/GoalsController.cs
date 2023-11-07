using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BioWiseV2.Data;
using BioWiseV2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BioWiseV2.Controllers
{
    [Authorize]
    public class GoalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GoalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Goals
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Goal.Include(g => g.Consumer);
            return View(await applicationDbContext.ToListAsync());
        }


        // GET: Goals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Goal == null)
            {
                return NotFound();
            }

            var goal = await _context.Goal
                .Include(g => g.Consumer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (goal == null)
            {
                return NotFound();
            }

            return View(goal);
        }

        // GET: Goals/Create
        public IActionResult Create()
        {
            ViewData["ConsumerId"] = new SelectList(_context.Set<Consumer>(), "Id", "Id");
            return View();
        }

        // POST: Goals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Personalgoal,Completed,Impact")] Goal goal)
        {
            if (ModelState.IsValid)
            {
                // Get the username of the currently logged-in user
                var userName = User.Identity?.Name;

                if (userName == null)
                {
                    // Handle the case where the user is not logged in
                    return RedirectToAction("Login", "Account"); // Redirect to the login page or handle it as needed
                }

                // Find the Consumer with a matching username
                var consumer = await _context.Consumer.FirstOrDefaultAsync(c => c.Name == userName);

                if (consumer != null)
                {
                    // Set the ConsumerId of the new Goal to the found Consumer's ID
                    goal.ConsumerId = consumer.Id;

                    // Add and save the new Goal to the database
                    _context.Add(goal);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            // If there is an issue or the user is not logged in, return to the Create view
            ViewData["ConsumerId"] = new SelectList(_context.Set<Consumer>(), "Id", "Id", goal.ConsumerId);
            return View(goal);
        }

        // GET: Goals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Goal == null)
            {
                return NotFound();
            }

            var goal = await _context.Goal.FindAsync(id);
            if (goal == null)
            {
                return NotFound();
            }
            ViewData["ConsumerId"] = new SelectList(_context.Set<Consumer>(), "Id", "Id", goal.ConsumerId);
            return View(goal);
        }

        // POST: Goals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Personalgoal,Completed,Impact,ConsumerId")] Goal goal)
        {
            if (id != goal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(goal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GoalExists(goal.Id))
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
            ViewData["ConsumerId"] = new SelectList(_context.Set<Consumer>(), "Id", "Id", goal.ConsumerId);
            return View(goal);
        }

        // GET: Goals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Goal == null)
            {
                return NotFound();
            }

            var goal = await _context.Goal
                .Include(g => g.Consumer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (goal == null)
            {
                return NotFound();
            }

            return View(goal);
        }

        // POST: Goals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Goal == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Goal' is null.");
            }
            var goal = await _context.Goal.FindAsync(id);
            if (goal != null)
            {
                _context.Goal.Remove(goal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GoalExists(int id)
        {
            return (_context.Goal?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

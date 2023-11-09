using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BioWiseV2.Data;
using BioWiseV2.Models;

namespace BioWiseV2.Controllers
{
    public class TransportUsagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransportUsagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TransportUsages
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TransportUsage.Include(t => t.Consumer).Include(t => t.Weekly_report);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TransportUsages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TransportUsage == null)
            {
                return NotFound();
            }

            var transportUsage = await _context.TransportUsage
                .Include(t => t.Consumer)
                .Include(t => t.Weekly_report)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transportUsage == null)
            {
                return NotFound();
            }

            return View(transportUsage);
        }

        // GET: TransportUsages/Create
        public IActionResult Create()
        {
            ViewData["ConsumerId"] = new SelectList(_context.Set<Consumer>(), "Id", "Id");
            ViewData["Weekly_reportId"] = new SelectList(_context.Weekly_report, "Id", "Id");
            return View();
        }

        // POST: TransportUsages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Transport_type,Distance,Emmission_KM,Weekly_reportId")] TransportUsage transportUsage)
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
                    // Set the ConsumerId of the new TransportUsage to the found Consumer's ID
                    transportUsage.ConsumerId = consumer.Id;

                    // Add and save the new TransportUsage to the database
                    _context.Add(transportUsage);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            // If there is an issue or the user is not logged in, return to the Create view
            ViewData["ConsumerId"] = new SelectList(_context.Set<Consumer>(), "Id", "Id", transportUsage.ConsumerId);
            ViewData["Weekly_reportId"] = new SelectList(_context.Weekly_report, "Id", "Id", transportUsage.Weekly_reportId);
            return View(transportUsage);
        }

        // GET: TransportUsages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TransportUsage == null)
            {
                return NotFound();
            }

            var transportUsage = await _context.TransportUsage.FindAsync(id);
            if (transportUsage == null)
            {
                return NotFound();
            }
            ViewData["ConsumerId"] = new SelectList(_context.Set<Consumer>(), "Id", "Id", transportUsage.ConsumerId);
            ViewData["Weekly_reportId"] = new SelectList(_context.Weekly_report, "Id", "Id", transportUsage.Weekly_reportId);
            return View(transportUsage);
        }

        // POST: TransportUsages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Transport_type,Distance,Emmission_KM,ConsumerId,Weekly_reportId")] TransportUsage transportUsage)
        {
            if (id != transportUsage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transportUsage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransportUsageExists(transportUsage.Id))
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
            ViewData["ConsumerId"] = new SelectList(_context.Set<Consumer>(), "Id", "Id", transportUsage.ConsumerId);
            ViewData["Weekly_reportId"] = new SelectList(_context.Weekly_report, "Id", "Id", transportUsage.Weekly_reportId);
            return View(transportUsage);
        }

        // GET: TransportUsages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TransportUsage == null)
            {
                return NotFound();
            }

            var transportUsage = await _context.TransportUsage
                .Include(t => t.Consumer)
                .Include(t => t.Weekly_report)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transportUsage == null)
            {
                return NotFound();
            }

            return View(transportUsage);
        }

        // POST: TransportUsages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TransportUsage == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TransportUsage'  is null.");
            }
            var transportUsage = await _context.TransportUsage.FindAsync(id);
            if (transportUsage != null)
            {
                _context.TransportUsage.Remove(transportUsage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransportUsageExists(int id)
        {
          return (_context.TransportUsage?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

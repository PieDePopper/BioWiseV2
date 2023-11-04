using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BioWiseV2.Data;
using BioWiseV2.Models;
using BioWiseV2.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace BioWiseV2.Controllers
{
    [Authorize]
    public class Weekly_reportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Weekly_reportController(ApplicationDbContext context)
        {
            _context = context;

        }


        // GET: Weekly_report
        public async Task<IActionResult> Index()
        {

            //return _context.Weekly_report != null ? 
            //            View(await _context.Weekly_report.ToListAsync()) :
            //            Problem("Entity set 'ApplicationDbContext.Weekly_report'  is null.");

            WeeklyReportAndGoalViewModel vm = new WeeklyReportAndGoalViewModel();
            vm.WeeklyReport = await _context.Weekly_report.ToListAsync();
            vm.Goal = await _context.Goal.Include(g => g.Consumer).ToListAsync();
            return View(vm);
        }

        // GET: Weekly_report/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Weekly_report == null)
            {
                return NotFound();
            }

            var weekly_report = await _context.Weekly_report
                .FirstOrDefaultAsync(m => m.Id == id);
            if (weekly_report == null)
            {
                return NotFound();
            }

            return View(weekly_report);
        }

        // GET: Weekly_report/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Weekly_report/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Weeknr,Transport_emission,Total,Difference,TransportusageId")] Weekly_report weekly_report)
        {
            if (ModelState.IsValid)
            {
                _context.Add(weekly_report);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(weekly_report);
        }

        // GET: Weekly_report/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Weekly_report == null)
            {
                return NotFound();
            }

            var weekly_report = await _context.Weekly_report.FindAsync(id);
            if (weekly_report == null)
            {
                return NotFound();
            }
            return View(weekly_report);
        }

        // POST: Weekly_report/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Weeknr,Transport_emission,Total,Difference,TransportusageId")] Weekly_report weekly_report)
        {
            if (id != weekly_report.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(weekly_report);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Weekly_reportExists(weekly_report.Id))
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
            return View(weekly_report);
        }

        // GET: Weekly_report/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Weekly_report == null)
            {
                return NotFound();
            }

            var weekly_report = await _context.Weekly_report
                .FirstOrDefaultAsync(m => m.Id == id);
            if (weekly_report == null)
            {
                return NotFound();
            }

            return View(weekly_report);
        }

        // POST: Weekly_report/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Weekly_report == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Weekly_report'  is null.");
            }
            var weekly_report = await _context.Weekly_report.FindAsync(id);
            if (weekly_report != null)
            {
                _context.Weekly_report.Remove(weekly_report);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Weekly_reportExists(int id)
        {
          return (_context.Weekly_report?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

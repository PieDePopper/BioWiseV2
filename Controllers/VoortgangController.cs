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
    public class VoortgangController : Controller
    {

        private readonly ApplicationDbContext _context;

        public VoortgangController(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<IActionResult> Index()
        {
            WeeklyReportAndGoalViewModel vm = new WeeklyReportAndGoalViewModel();
            vm.Consumers = await _context.Consumer.ToListAsync();
            foreach (var consumer in vm.Consumers)
            {
                if (consumer != null && consumer.Name == User.Identity?.Name)
                {
                    vm.CurrentConsumerId = consumer.Id;
                }
            }
            
            vm.Goals = await _context.Goal.Include(g => g.Consumer).Where(g => g.ConsumerId == vm.CurrentConsumerId).ToListAsync();
            vm.TransportUsages = await _context.TransportUsage.Where(g => g.ConsumerId == vm.CurrentConsumerId).Include(t => t.Consumer).Include(t => t.Weekly_report).ToListAsync();
            vm.WeeklyReports = await _context.Weekly_report.ToListAsync();
            return View(vm);
        }

        
        
    }
}
using Microsoft.AspNetCore.Mvc;
using JCTMS.Data;
using JCTMS.Web.Models;
using Microsoft.EntityFrameworkCore;
using JCTMS.Core.Entities;

namespace JCTMS.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Gather Statistics
            var viewModel = new DashboardViewModel
            {
                ActiveCases = await _context.Cases.CountAsync(c => c.Status != Status.Ready),
                PendingHearings = await _context.Hearings.CountAsync(h => h.HearingDate >= DateTime.Today),
                TotalJudges = await _context.Judges.CountAsync(),
                RecentCaseCount = await _context.Cases.CountAsync(c => c.OpenDate > DateTime.Today.AddDays(-7))
            };

            return View(viewModel);
        }
    }
}

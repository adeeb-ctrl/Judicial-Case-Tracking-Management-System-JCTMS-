using Microsoft.AspNetCore.Mvc;
using JCTMS.Core.Entities;
using JCTMS.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace JCTMS.Web.Controllers
{
    public class HearingsController : Controller
    {
        private readonly IRepository<Hearing> _hearingRepository;
        private readonly IRepository<Case> _caseRepository;

        public HearingsController(IRepository<Hearing> hearingRepository, IRepository<Case> caseRepository)
        {
            _hearingRepository = hearingRepository;
            _caseRepository = caseRepository;
        }

        // GET: Hearings/Create?caseId=5
        public async Task<IActionResult> Create(int? caseId)
        {
            if (caseId == null)
            {
                return NotFound("CaseID is required to schedule a hearing.");
            }

            var @case = await _caseRepository.GetByIdAsync(caseId.Value);
            if (@case == null)
            {
                return NotFound("Case not found.");
            }

            ViewBag.CaseRef = @case.RefNumber;
            
            return View(new Hearing { CaseID = caseId.Value, HearingDate = DateTime.Now.AddDays(7) });
        }

        // POST: Hearings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CaseID,HearingDate,Remarks")] Hearing hearing)
        {
            if (ModelState.IsValid)
            {
                await _hearingRepository.AddAsync(hearing);
                return RedirectToAction("Details", "Cases", new { id = hearing.CaseID });
            }
            return View(hearing);
        }

        // GET: Hearings/Index (Optional: List all hearings or filter by case)
        public async Task<IActionResult> Index()
        {
            // For now, just list all upcoming hearings
            var hearings = await _hearingRepository.FindAsync(h => h.HearingDate >= DateTime.Today);
            return View(hearings);
        }
    }
}

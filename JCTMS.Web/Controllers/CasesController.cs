using Microsoft.AspNetCore.Mvc;
using JCTMS.Core.Entities;
using JCTMS.Core.Interfaces;
using JCTMS.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace JCTMS.Web.Controllers
{
    public class CasesController : Controller
    {
        private readonly IRepository<Case> _caseRepository;
        private readonly ICaseService _caseService;

        public CasesController(IRepository<Case> caseRepository, ICaseService caseService)
        {
            _caseRepository = caseRepository;
            _caseService = caseService;
        }

        // GET: Cases
        public async Task<IActionResult> Index()
        {
            var cases = await _caseRepository.GetAllAsync();
            return View(cases);
        }

        // GET: Cases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // We need to use FindAsync with an Include equivalent if the repo supported it, 
            // but for now we might need to rely on Lazy Loading or explicit loading if using generic repo.
            // However, generic repo as defined does not support Include nicely without extension.
            // For this simple lab, we will just fetch by ID. 
            // NOTE: To show Parties/Hearings, we might need a specific query or enable lazy loading.
            // Let's assume standard fetch for now and address eager loading if needed.
            
            var @case = await _caseRepository.GetByIdAsync(id.Value);
            
            if (@case == null)
            {
                return NotFound();
            }

            return View(@case);
        }

        // GET: Cases/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cases/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,CourtRoom")] Case @case)
        {
            if (ModelState.IsValid)
            {
                // Delegates RefNumber generation and logic to the Service
                await _caseService.CreateCaseAsync(@case);
                return RedirectToAction(nameof(Index));
            }
            return View(@case);
        }
    }
}

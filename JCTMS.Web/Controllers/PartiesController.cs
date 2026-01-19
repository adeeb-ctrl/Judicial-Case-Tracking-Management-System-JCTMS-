using Microsoft.AspNetCore.Mvc;
using JCTMS.Core.Entities;
using JCTMS.Data.Repositories;
using System.Threading.Tasks;

namespace JCTMS.Web.Controllers
{
    public class PartiesController : Controller
    {
        private readonly IRepository<Party> _partyRepository;
        private readonly IRepository<Case> _caseRepository;

        public PartiesController(IRepository<Party> partyRepository, IRepository<Case> caseRepository)
        {
            _partyRepository = partyRepository;
            _caseRepository = caseRepository;
        }

        // GET: Parties/Create?caseId=5
        public async Task<IActionResult> Create(int? caseId)
        {
             if (caseId == null)
            {
                return NotFound("CaseID is required.");
            }

            var @case = await _caseRepository.GetByIdAsync(caseId.Value);
            if (@case == null)
            {
                return NotFound("Case not found.");
            }

            ViewBag.CaseRef = @case.RefNumber;
            return View(new Party { CaseID = caseId.Value });
        }

        // POST: Parties/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CaseID,Name,Role,ContactInfo")] Party party)
        {
            if (ModelState.IsValid)
            {
                await _partyRepository.AddAsync(party);
                return RedirectToAction("Details", "Cases", new { id = party.CaseID });
            }
            return View(party);
        }

         // GET: Parties/Index
        public async Task<IActionResult> Index()
        {
            var parties = await _partyRepository.GetAllAsync();
            return View(parties);
        }
    }
}

using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JCTMS.Core.Entities;
using JCTMS.Core.Interfaces;
using JCTMS.Data;
using JCTMS.Data.Repositories;

namespace JCTMS.Services
{
    public class CaseService : ICaseService
    {
        private readonly AppDbContext _context;
        private readonly IRepository<Case> _caseRepository;

        public CaseService(AppDbContext context, IRepository<Case> caseRepository)
        {
            _context = context;
            _caseRepository = caseRepository;
        }

        public async Task<Case> CreateCaseAsync(Case newCase)
        {
            // 1. Generate Reference Number
            newCase.RefNumber = await GenerateRefNumberAsync();
            newCase.OpenDate = DateTime.Now;
            
            // Set initial status if not set
            if (newCase.Status == 0)
            {
                newCase.Status = Status.Reported;
            }

            // 2. Add to Repository
            await _caseRepository.AddAsync(newCase);
            return newCase;
        }

        public async Task<string> GenerateRefNumberAsync()
        {
            // Pattern: {Year}-45-{Sequence}
            // Logic adapted from reference repo "ErrandService"

            var sequenceName = "CaseRef";
            var sequence = await _context.Sequences.FindAsync(sequenceName);

            if (sequence == null)
            {
                sequence = new Sequence { SequenceName = sequenceName, CurrentValue = 0 };
                await _context.Sequences.AddAsync(sequence);
            }

            sequence.CurrentValue++;
            await _context.SaveChangesAsync();

            var year = DateTime.Now.Year;
            return $"{year}-45-{sequence.CurrentValue}";
        }
    }
}

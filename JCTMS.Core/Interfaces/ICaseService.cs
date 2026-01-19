using System.Threading.Tasks;
using JCTMS.Core.Entities;

namespace JCTMS.Core.Interfaces
{
    public interface ICaseService
    {
        Task<Case> CreateCaseAsync(Case newCase);
        Task<string> GenerateRefNumberAsync();
    }
}

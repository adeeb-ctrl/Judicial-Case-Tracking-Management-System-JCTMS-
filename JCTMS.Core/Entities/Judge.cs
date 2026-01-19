using System.Collections.Generic;

namespace JCTMS.Core.Entities
{
    public class Judge
    {
        public int JudgeID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? RoomNumber { get; set; }

        // Navigation Property
        public ICollection<Case> AssignedCases { get; set; } = new List<Case>();
    }
}

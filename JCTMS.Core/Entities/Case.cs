using System;
using System.Collections.Generic;

namespace JCTMS.Core.Entities
{
    public class Case
    {
        public int CaseID { get; set; }
        public string? RefNumber { get; set; } // Generated Pattern: YYYY-45-XXXX
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } // Logic: Observation
        public DateTime OpenDate { get; set; }
        public Status Status { get; set; }
        
        // Foreign Key to Judge
        public int? JudgeID { get; set; }
        public Judge? Judge { get; set; }
        
        public string? CourtRoom { get; set; }

        // Navigation Properties
        public ICollection<Party> Parties { get; set; } = new List<Party>();
        public ICollection<Hearing> Hearings { get; set; } = new List<Hearing>();
    }
}

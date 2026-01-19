using System;

namespace JCTMS.Core.Entities
{
    public class Hearing
    {
        public int HearingID { get; set; }
        
        public int CaseID { get; set; }
        public Case? Case { get; set; }

        public DateTime HearingDate { get; set; }
        public string? Remarks { get; set; }
    }
}

using System;

namespace JCTMS.Core.Entities
{
    public enum PartyRole
    {
        Plaintiff,
        Defendant
    }

    public class Party
    {
        public int PartyID { get; set; }
        
        public int CaseID { get; set; }
        public Case? Case { get; set; }

        public string Name { get; set; } = string.Empty;
        public PartyRole Role { get; set; }
        public string? ContactInfo { get; set; }
    }
}

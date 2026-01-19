using System.ComponentModel.DataAnnotations;

namespace JCTMS.Core.Entities
{
    public class Sequence
    {
        [Key]
        public string SequenceName { get; set; }
        public int CurrentValue { get; set; }
    }
}

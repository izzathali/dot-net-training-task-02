using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC_Drive.Model
{
    public class DayHire
    {
        [Key]
        public int HireId { get; set; }
        [Required]
        public Package Packages { get; set; }
        [Required]
        public TimeSpan StartTime { get; set; }
        [Required]
        public TimeSpan EndTime { get; set; }
        [Required]
        public int StartKm { get; set; }
        [Required]
        public int EndKm { get; set; }
        [Required]
        public int HireCharge { get; set; }
        public int WaitingCharge { get; set; }
        public int ExtraKmCharge { get; set; }
        [Required]
        public int TotalHireCharge { get; set; }
    }
}

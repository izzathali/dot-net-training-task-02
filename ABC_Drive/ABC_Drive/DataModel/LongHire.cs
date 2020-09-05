using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC_Drive.Model
{
    public class LongHire
    {
        [Key]
        public int HireId { get; set; }
        [Required]
        public int PackageId { get; set; }
        public virtual Package Packages { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public int StartKm { get; set; }
        [Required]
        public int EndKm { get; set; }
        public int? DriverId { get; set; }
        public virtual Driver Driver { get; set; }
        public int TotDriverCharge { get; set; }
        [Required]
        public int HireCharge { get; set; }
        public int OvernightStayCharge { get; set; }
        public int ExtraKmCharge { get; set; }
        [Required]
        public int TotalHireCharge { get; set; }
    }
}

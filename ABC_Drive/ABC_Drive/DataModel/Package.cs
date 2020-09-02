using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC_Drive.Model
{
    public class Package
    {
        [Key]
        public int PackageId { get; set; }
        [Required]
        public Vehicle Vehicle { get; set; }
        [Required]
        public string PackageName { get; set; }
        [Required]
        public int StandardRate { get; set; }
        [Required]
        public int MaxKmLimit { get; set; }
        [Required]
        public TimeSpan MaxNumOfHours { get; set; }
        [Required]
        public int ExtraRatePerKm { get; set; }
        [Required]
        public int ExtraRatePerHour { get; set; }

    }
}

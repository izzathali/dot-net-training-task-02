
using ABC_Drive.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC_Drive.Model
{
    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }
        [Required]
        public string VehicleNo { get; set; }
        [Required]
        public string VehicleName { get; set; }
        [Required]
        public int RatePerDay { get; set; }
        [Required]
        public int RatePerWeek { get; set; }
        [Required]
        public int RatePerMonth { get; set; }
<<<<<<< HEAD
        public int RatePerNightPark { get; set; }
        public ICollection<Rent> Rents { get; set; }
        public ICollection<Package> Packages { get; set; }
        public ICollection<LongHire> LongHires { get; set; }
=======

        public virtual ICollection<Rent> Rents { get; set; }
>>>>>>> 294df09dc5a725ee95fc4957f7035a231667bdaf
    }
}

using ABC_Drive.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC_Drive.Model
{
    public class Rent
    {
        [Key]
        public int RentId { get; set; }
        [Required]
<<<<<<< HEAD
        public int VehicleId { get; set; }
=======
        public int VehicleId  { get; set; }
>>>>>>> 294df09dc5a725ee95fc4957f7035a231667bdaf
        [Required]
        public DateTime RentedDate { get; set; }
        [Required]
        public DateTime ReturnedDate { get; set; }
        public int? DriverId { get; set; }
        public virtual Driver Driver { get; set; }
        public int TotDays { get; set; }
        public int TotDriverCost { get; set; }
        public int TotDaysAmnt { get; set; }
        public int TotWeeksAmnt { get; set; }
        public int TotMonthsAmnt { get; set; }
        public float TotalRent { get; set; }
    }
}

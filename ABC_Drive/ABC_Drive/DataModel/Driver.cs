using ABC_Drive.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC_Drive.Model
{
    public class Driver
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string DriverName { get; set; }
        public string LicenceType { get; set; }
        public int DriverCost { get; set; }
        public int RatePerOverNight { get; set; }
        public ICollection<Rent> Rents { get; set; }
        public ICollection<LongHire> LongHires { get; set; }
    }
}

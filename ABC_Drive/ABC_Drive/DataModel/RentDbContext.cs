
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC_Drive.Model
{
    class RentDbContext:DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<DayHire> DayHires { get; set; }
        public DbSet<LongHire> LongHires { get; set; }
    }
}

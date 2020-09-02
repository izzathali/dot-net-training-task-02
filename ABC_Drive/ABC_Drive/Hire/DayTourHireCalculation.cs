using ABC_Drive.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC_Drive.Hire
{
    class DayTourHireCalculation
    {
        RentDbContext db = new RentDbContext();

        private int ExtraHours = 0;
        private TimeSpan MaxNoOfHours = TimeSpan.Parse("0:0");
        private int MaxKmLimit = 0;
        private int ExtraKm = 0;
        private int ExtraRateKms = 0;
        private int PckStandardRate = 0;

        public int WaitingHours(TimeSpan StartTime, TimeSpan EndTime, string VehicleNo, string PackageName)
        {
            TimeSpan TotCustomerDuration = EndTime - StartTime;
            var GetMaxNoOfHours = (from Package in db.Packages
                                  where Package.Vehicle.VehicleNo == VehicleNo && Package.PackageName == PackageName
                                  select Package.MaxNumOfHours).FirstOrDefault();
            MaxNoOfHours = GetMaxNoOfHours;

            TimeSpan ExtraHoursCalcu = TimeSpan.Parse("0:0");

            if (TotCustomerDuration > MaxNoOfHours)
            {
                ExtraHoursCalcu = TotCustomerDuration - MaxNoOfHours;
            }
            ExtraHours = Convert.ToInt32(ExtraHoursCalcu.TotalHours);

            return ExtraHours;
        }
        public int WaitingCharge(int WaitingHours , string VehicleNo, string PackageName)
        {
            var GetRatePerHour = (from Package in db.Packages
                                 where Package.Vehicle.VehicleNo == VehicleNo && Package.PackageName == PackageName
                                  select Package.ExtraRatePerHour).FirstOrDefault();
            int ExtraRateHours = GetRatePerHour * WaitingHours;

            return ExtraRateHours;
        }
        public int CalculateExtraKm(int StartKm, int EndKm,string VehicleNo , string PackageName)
        {
            int TotCustomerKm = EndKm - StartKm;
            var GetMaxKmLimit = (from Package in db.Packages
                                where Package.Vehicle.VehicleNo == VehicleNo && Package.PackageName == PackageName
                                select Package.MaxKmLimit).FirstOrDefault();

            MaxKmLimit = GetMaxKmLimit;

            if (TotCustomerKm > MaxKmLimit)
            {
                ExtraKm = TotCustomerKm - MaxKmLimit;
            }
            return ExtraKm;
        }
        public int ExtraKmTotalRate(int ExtraKm, string VehicleNo, string PackageName)
        {
            var GetExtraPerKmRate = (from Package in db.Packages
                                     where Package.Vehicle.VehicleNo == VehicleNo && Package.PackageName == PackageName
                                     select Package.ExtraRatePerKm).FirstOrDefault();
            ExtraRateKms = GetExtraPerKmRate * ExtraKm;
            return ExtraRateKms;
        }
        public int PackageStandardRate(string VehicleNo, string PackageName)
        {
            var StandardRate = (from Package in db.Packages
                               where Package.Vehicle.VehicleNo == VehicleNo && Package.PackageName == PackageName
                                select Package.StandardRate).FirstOrDefault();
            PckStandardRate = StandardRate;

            return PckStandardRate;
        }
        public int TotalHireCharge(int PackageStandardRate, int ExtraHoursTotalRate, int ExtraKmTotalRate)
        {
            int TotalHireCharge = 0;
            TotalHireCharge = PackageStandardRate + ExtraHoursTotalRate + ExtraKmTotalRate;
            return TotalHireCharge;
        }
    }
}

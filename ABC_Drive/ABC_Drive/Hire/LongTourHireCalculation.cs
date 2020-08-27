using ABC_Drive.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC_Drive.Hire
{
    class LongTourHireCalculation
    {
        RentDbContext db = new RentDbContext();

        public int PckStandardRate = 0;
        public int ExtraRateKms = 0;
        public int TotDriverOverNightRate = 0;
        public int TotVehicleNightPark = 0;
        public int TotNightStayCharge = 0;
        public int MaxKmLimit = 0;
        public double Days = 0;

        public string VehicleNo ="" ;
        public string PackageName = "";
        public bool DriverCkedNo = true;
        public string DriverName = "" ;

        public DateTime StartDate = DateTime.Today;
        public DateTime EndDate = DateTime.Today;
        public int StartKm = 0;
        public int EndKm = 0;
        public int OverNigths = 0;
        public int ExtraKm = 0;

        public int TotalHireCharge = 0;

        public LongTourHireCalculation(string _cmbVehicleNo, string _cmbPackage, DateTime _StartDate,
            DateTime _EndDate, int _StartKm, int _EndKm, bool _DriverCked, string _cmbDriverName)
        {
            VehicleNo = _cmbVehicleNo;
            PackageName = _cmbPackage;
            StartDate = _StartDate;
            EndDate = _EndDate;
            StartKm = _StartKm;
            EndKm = _EndKm;
            DriverCkedNo = _DriverCked;
            DriverName = _cmbDriverName;
        }

        public void DurationCalculation()
        {
            //Customer Takes Time
            TimeSpan CountDays = EndDate - StartDate;
            Days = CountDays.TotalDays;

            OverNigths = Convert.ToInt32(Days);

            var DriverRatePerOverNight = db.Drivers.Where(x => x.DriverName == DriverName).Select(u => u.RatePerOverNight).FirstOrDefault();
            var VehicleNightParkRate = db.Vehicles.Where(x => x.VehicleNo == VehicleNo).Select(u => u.RatePerNightPark).FirstOrDefault();

            if (OverNigths > 0)
            {
                TotDriverOverNightRate = DriverRatePerOverNight * OverNigths;
                TotVehicleNightPark = VehicleNightParkRate * OverNigths;
            }
            TotNightStayCharge = TotDriverOverNightRate + TotVehicleNightPark;
        }
        public void KmCalculation()
        {
            //CustomerTakesKm
            int TotCustomerKm = EndKm - StartKm;
            var GetMaxKmLimit = (from Package in db.Packages
                                where Package.Vehicle.VehicleNo == VehicleNo && Package.PackageName == PackageName
                                select Package.MaxKmLimit).FirstOrDefault();
            MaxKmLimit = GetMaxKmLimit;

            if (TotCustomerKm > MaxKmLimit)
            {
                ExtraKm = TotCustomerKm - MaxKmLimit;
            }

            //Extra Km Rate 
            var GetExtraPerKmRate = (from Package in db.Packages
                                    where Package.Vehicle.VehicleNo == VehicleNo && Package.PackageName == PackageName
                                    select Package.ExtraRatePerKm).FirstOrDefault();
            ExtraRateKms = GetExtraPerKmRate * ExtraKm;
        }
        public void PackageStandardRate()
        {
            //Package Standard Rate
            var StandardRate = (from Package in db.Packages
                                where Package.Vehicle.VehicleNo == VehicleNo && Package.PackageName == PackageName
                                select Package.StandardRate).FirstOrDefault();
            PckStandardRate = StandardRate;
        }
        public void TotalHireCalculation()
        {
            //TotalHireCharge
            TotalHireCharge = PckStandardRate + TotNightStayCharge + ExtraRateKms;
        }
    }
}

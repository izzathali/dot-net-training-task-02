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

        private int PckStandardRate = 0;
        private int ExtraRateKms = 0;
        private int TotDriverOverNightRate = 0;
        private int TotVehicleNightPark = 0;
        private int TotNightStayCharge = 0;
        private int MaxKmLimit = 0;
        private double Days = 0;

        //private string VehicleNo ="" ;
        //private string PackageName = "";
        //private bool DriverCkedNo = true;
        //private string DriverName = "" ;

        //private DateTime StartDate = DateTime.Today;
        //private DateTime EndDate = DateTime.Today;
        //private int StartKm = 0;
        //private int EndKm = 0;
        private int OverNigths = 0;
        private int ExtraKm = 0;

        public int TotalHireCharge = 0;
        /*
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
        }*/
        public int StayOverNights( DateTime StartDate , DateTime EndDate)
        {
            TimeSpan CountDays = EndDate - StartDate;
            Days = CountDays.TotalDays;

            OverNigths = Convert.ToInt32(Days);

            return OverNigths;
        }
        public int TotalDriverOverNightRate(string _DriverName)
        {
            var DriverRatePerOverNight = db.Drivers.Where(x => x.DriverName == _DriverName).Select(u => u.RatePerOverNight).FirstOrDefault();
            if (OverNigths > 0)
            {
                TotDriverOverNightRate = DriverRatePerOverNight * OverNigths;
            }
            return TotDriverOverNightRate;

        }
        public int TotalVehicleNightPark(string _VehicleNo)
        {
            var VehicleNightParkRate = db.Vehicles.Where(x => x.VehicleNo == _VehicleNo).Select(u => u.RatePerNightPark).FirstOrDefault();

            if (OverNigths > 0)
            {
                TotVehicleNightPark = VehicleNightParkRate * OverNigths;
            }
            return TotVehicleNightPark;
        }
        public int TotalNightStayCharge(int TotDriverOverNightRate , int TotVehicleNightPark)
        {
            TotNightStayCharge = TotDriverOverNightRate + TotVehicleNightPark;
            return TotNightStayCharge;
        }

        public int CalExtraKm(int StartKm , int EndKm, string VehicleNo , string PackageName)
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
        public int ExtraKmCharge(int ExtraKm ,string VehicleNo, string PackageName)
        {
            var GetExtraPerKmRate = (from Package in db.Packages
                                    where Package.Vehicle.VehicleNo == VehicleNo && Package.PackageName == PackageName
                                    select Package.ExtraRatePerKm).FirstOrDefault();
            ExtraRateKms = GetExtraPerKmRate * ExtraKm;

            return ExtraRateKms;
        }
        public int PackageStandardRate(string VehicleNo,string PackageName)
        {
            //Package Standard Rate
            var StandardRate = (from Package in db.Packages
                                where Package.Vehicle.VehicleNo == VehicleNo && Package.PackageName == PackageName
                                select Package.StandardRate).FirstOrDefault();
            PckStandardRate = StandardRate;

            return PckStandardRate;
        }
        public int TotalHireCalculation(int PckStandardRate, int TotNightStayCharge , int ExtraRateKms)
        {
            //TotalHireCharge
            TotalHireCharge = PckStandardRate + TotNightStayCharge + ExtraRateKms;
            return TotalHireCharge;
        }
    }
}

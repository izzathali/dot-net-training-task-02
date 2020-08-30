using ABC_Drive.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC_Drive.Rent
{
    class RentCalculation
    {
        RentDbContext db = new RentDbContext();
        private int Days = 0;
        private int TotalDays = 0;
        private int TotalWeeks = 0;
        private int TotalMonths = 0;
        private int DriverRatePerDy = 0;
        //private int TotalDriverCharge = 0;


        public int CountDays(DateTime RentedDate , DateTime ReturnedDate)
        {
            TimeSpan CountDays = ReturnedDate - RentedDate;
            Days = Convert.ToInt32(CountDays.TotalDays);

            return Days;
        }
        public int RatePerDay(int VehicleID)
        {
            var RatePerDay = db.Vehicles.Where(x => x.VehicleId == VehicleID).Select(u => u.RatePerDay).FirstOrDefault();

            return RatePerDay;
        }
        public int RatePerWeek(int VehicleID)
        {
            var RatePerWeek = db.Vehicles.Where(x => x.VehicleId == VehicleID).Select(u => u.RatePerWeek).FirstOrDefault();

            return RatePerWeek;
        }
        public int RatePerMonth(int VehicleID)
        {
            var RatePerMonth = db.Vehicles.Where(x => x.VehicleId == VehicleID).Select(u => u.RatePerMonth).FirstOrDefault();

            return RatePerMonth;
        }
        public int DriverRatePerDay(string DriverName)
        {
            var DriverRatePerDay = db.Drivers.Where(x => x.DriverName == DriverName).Select(u => u.DriverCost).FirstOrDefault();
            DriverRatePerDy = DriverRatePerDay;
            return DriverRatePerDy;
        }
        public int TotDays(int Days_)
        {
            if (Days_ > 6)
            {
                if (Days_ > 30)
                {
                    TotalMonths = Convert.ToInt32(Days_ / 30);
                    int BalanceDays = (Days_ - (TotalMonths * 30));
                    TotalWeeks = Convert.ToInt32(BalanceDays / 7);
                    TotalDays = Days_ - (TotalWeeks * 7) - (TotalMonths * 30);
                }
                else
                {
                    TotalWeeks = Convert.ToInt32(Days_ / 7);
                    TotalDays = (Days_ - (TotalWeeks * 7));
                }
            }
            else
            {
                TotalDays = Days_;
            }
            return TotalDays;
        }
        public int TotWeeks(int Days_)
        {
            if (Days_ > 6)
            {
                if (Days_ > 30)
                {
                    TotalMonths = Convert.ToInt32(Days_ / 30);
                    int BalanceDays = (Days_ - (TotalMonths * 30));
                    TotalWeeks = Convert.ToInt32(BalanceDays / 7);
                    TotalDays = Days_ - (TotalWeeks * 7) - (TotalMonths * 30);
                }
                else
                {
                    TotalWeeks = Convert.ToInt32(Days_ / 7);
                    TotalDays = (Days_ - (TotalWeeks * 7));
                }
            }
            else
            {
                TotalDays = Days_;
            }
            return TotalWeeks;
        }
        public int TotMonths(int Days_)
        {
            if (Days_ > 6)
            {
                if (Days_ > 30)
                {
                    TotalMonths = Convert.ToInt32(Days_ / 30);
                    int BalanceDays = (Days_ - (TotalMonths * 30));
                    TotalWeeks = Convert.ToInt32(BalanceDays / 7);
                    TotalDays = Days_ - (TotalWeeks * 7) - (TotalMonths * 30);
                }
                else
                {
                    TotalWeeks = Convert.ToInt32(Days_ / 7);
                    TotalDays = (Days_ - (TotalWeeks * 7));
                }
            }
            else
            {
                TotalDays = Days_;
            }
            return TotalMonths;
        }
        public int TotDaysAmount(int Days, int RatePerDay)
        {
            int TotDaysAmnt = Days * RatePerDay;
            return TotDaysAmnt;
        }
        public int TotWeeksAmount(int Weeks , int RatePerWeek)
        {
            int TotWeeksAmnt = Weeks * RatePerWeek;
            return TotWeeksAmnt;
        }
        public int TotMonthsAmount(int Months , int RatePerMonth)
        {
            int TotMonthsAmnt = Months * RatePerMonth;
            return TotMonthsAmnt;
        }
        public int TotDriverCharge(int Days, int DriverRatePerDay)
        {
            int TotDriverCharge = Days * DriverRatePerDay;
            return TotDriverCharge;
        }
        public int TotalRent(int TotDaysAmount, int TotWeeksAmount, int TotMonthsAmount,int TotDriverCharge)
        {
            int TotRent = TotDaysAmount + TotWeeksAmount + TotMonthsAmount + TotDriverCharge;
            return TotRent;
        }
    }
}

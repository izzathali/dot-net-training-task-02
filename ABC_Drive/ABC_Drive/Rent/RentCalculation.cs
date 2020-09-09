using ABC_Drive.Model;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        private static readonly Random random = new Random();

        public int CountDays(DateTime RentedDate , DateTime ReturnedDate)
        {
            if (RentedDate > ReturnedDate)
            {
                Days = 0;
            }
            else
            {
                TimeSpan CountDays = ReturnedDate - RentedDate;
                Days = Convert.ToInt32(CountDays.TotalDays);
            }
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
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
        public static void ExportRents(DataTable dt)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "PDF (*.pdf)|*.pdf",
                    FileName = RandomString(5) + DateTime.Now.ToShortDateString().Replace("/", "")
                };
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string fileName = sfd.FileName;
                    PdfWriter writer = new PdfWriter(sfd.FileName);
                    var pdf = new PdfDocument(writer);
                    Document document = new Document(pdf, iText.Kernel.Geom.PageSize.A4);
                    Paragraph header = new Paragraph("Rent Records").SetTextAlignment(TextAlignment.CENTER).SetFontSize(15);
                    Paragraph dl = new Paragraph(".       .").SetTextAlignment(TextAlignment.CENTER).SetFontSize(8).SetFontColor(ColorConstants.WHITE);
                    LineSeparator ls = new LineSeparator(new SolidLine());
                    document.Add(header);
                    document.Add(ls);
                    document.Add(dl);
                    int FontSize = 8;
                    Table table = new Table(10, false);
                    table.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);
                    table.SetVerticalAlignment(VerticalAlignment.TOP);
                    table.AddHeaderCell("Vehicle No").SetFontSize(FontSize);
                    table.AddHeaderCell("Rented Date").SetFontSize(FontSize);
                    table.AddHeaderCell("Returned Date").SetFontSize(FontSize);
                    table.AddHeaderCell("Driver").SetFontSize(FontSize);
                    table.AddHeaderCell("Total Days").SetFontSize(FontSize);
                    table.AddHeaderCell("Driver Cost").SetFontSize(FontSize);
                    table.AddHeaderCell("Days Amount").SetFontSize(FontSize);
                    table.AddHeaderCell("Weeks Amount").SetFontSize(FontSize);
                    table.AddHeaderCell("Months Amount").SetFontSize(FontSize);
                    table.AddHeaderCell("Total Rent").SetFontSize(FontSize);

                    foreach (DataRow d in dt.Rows)
                    {
                        table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetFontSize(FontSize).Add(new Paragraph(d[1].ToString())));
                        table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetFontSize(FontSize).Add(new Paragraph(d[2].ToString())));
                        table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetFontSize(FontSize).Add(new Paragraph(d[3].ToString())));
                        table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetFontSize(FontSize).Add(new Paragraph(d[4].ToString())));
                        table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.CENTER).SetFontSize(FontSize).Add(new Paragraph(d[5].ToString())));
                        table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.RIGHT).SetFontSize(FontSize).Add(new Paragraph(d[6].ToString())));
                        table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.RIGHT).SetFontSize(FontSize).Add(new Paragraph(d[7].ToString())));
                        table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.RIGHT).SetFontSize(FontSize).Add(new Paragraph(d[8].ToString())));
                        table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.RIGHT).SetFontSize(FontSize).Add(new Paragraph(d[9].ToString())));
                        table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.RIGHT).SetFontSize(FontSize).Add(new Paragraph(d[10].ToString())));
                    }

                    document.Add(table);
                    document.Close();
                    StartProcess(fileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
        
        public static void StartProcess(string Path)
        {
            try
            {
                Process.Start(Path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

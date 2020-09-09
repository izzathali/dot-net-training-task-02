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
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        private static readonly Random random = new Random();
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
        public static void ExportLongHires(DataTable dt)
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
                    Paragraph header = new Paragraph("Long tour Records").SetTextAlignment(TextAlignment.CENTER).SetFontSize(15);
                    Paragraph dl = new Paragraph(".       .").SetTextAlignment(TextAlignment.CENTER).SetFontSize(8).SetFontColor(ColorConstants.WHITE);
                    LineSeparator ls = new LineSeparator(new SolidLine());
                    document.Add(header);
                    document.Add(ls);
                    document.Add(dl);
                    int FontSize = 8;
                    Table table = new Table(12, false);
                    table.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);
                    table.SetVerticalAlignment(VerticalAlignment.TOP);
                    table.AddHeaderCell("Vehicle No").SetFontSize(FontSize);
                    table.AddHeaderCell("Package").SetFontSize(FontSize);
                    table.AddHeaderCell("Start Date").SetFontSize(FontSize);
                    table.AddHeaderCell("End Date").SetFontSize(FontSize);
                    table.AddHeaderCell("Start Km").SetFontSize(FontSize);
                    table.AddHeaderCell("End Km").SetFontSize(FontSize);
                    table.AddHeaderCell("Driver").SetFontSize(FontSize);
                    table.AddHeaderCell("Driver Charge").SetFontSize(FontSize);
                    table.AddHeaderCell("Package Charge").SetFontSize(FontSize);
                    table.AddHeaderCell("Night Stay Charge").SetFontSize(FontSize);
                    table.AddHeaderCell("Extra Km Chrge").SetFontSize(FontSize);
                    table.AddHeaderCell("Total Hire").SetFontSize(FontSize);

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
                        table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.RIGHT).SetFontSize(FontSize).Add(new Paragraph(d[11].ToString())));
                        table.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.RIGHT).SetFontSize(FontSize).Add(new Paragraph(d[12].ToString())));
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

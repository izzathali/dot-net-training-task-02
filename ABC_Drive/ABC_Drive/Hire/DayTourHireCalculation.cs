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
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        private static readonly Random random = new Random();
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
        public static void ExportDayHires(DataTable dt)
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
                    int FontSize = 10;
                    Table table = new Table(10, false);
                    table.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);
                    table.SetVerticalAlignment(VerticalAlignment.TOP);
                    table.AddHeaderCell("Vehicle No").SetFontSize(FontSize);
                    table.AddHeaderCell("Package").SetFontSize(FontSize);
                    table.AddHeaderCell("Start Time").SetFontSize(FontSize);
                    table.AddHeaderCell("End Time").SetFontSize(FontSize);
                    table.AddHeaderCell("Start Km").SetFontSize(FontSize);
                    table.AddHeaderCell("End Km").SetFontSize(FontSize);
                    table.AddHeaderCell("Package Charge").SetFontSize(FontSize);
                    table.AddHeaderCell("Waiting Charge").SetFontSize(FontSize);
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

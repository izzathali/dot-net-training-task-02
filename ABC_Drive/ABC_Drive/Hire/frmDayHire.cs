using ABC_Drive.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABC_Drive.Hire
{
    public partial class frmDayHire : Form
    {
        RentDbContext db = new RentDbContext();
        Model.DayHire model = new DayHire();
        
        public frmDayHire()
        {
            InitializeComponent();
        }
        private void frmDayHire_Load(object sender, EventArgs e)
        {
            LoadcmbVehicle();
            PlaceHolder();
            //LoadcmbPackage();
            //DayTourHireCalculation();
        }
        private void LoadcmbVehicle()
        {
            var Vehi = from Vehicle in db.Vehicles select Vehicle;

            cmbVehicleNo.DataSource = Vehi.ToList();
            cmbVehicleNo.DisplayMember = "VehicleNo";
            cmbVehicleNo.ValueMember = "VehicleId";
            cmbVehicleNo.SelectedIndex = -1;
        }
        private void LoadcmbPackage()
        {
            if (cmbVehicleNo.SelectedIndex != -1)
            {
                var Pckg = from Package in db.Packages where Package.Vehicle.VehicleNo == cmbVehicleNo.Text select Package;
                //var Pckg = db.Packages.Where(x => x.Vehicle.VehicleNo == cmbVehicleNo.Text).Select(u => u.PackageName).FirstOrDefault();
                /*
                foreach (var item in Pckg)
                {
                    cmbPackageName.Text = item.ToString();
                }*/
                
                cmbPackageName.DataSource = Pckg.ToList();
                cmbPackageName.DisplayMember = "PackageName";
                cmbPackageName.ValueMember = "PackageId";
                cmbPackageName.SelectedIndex = -1;
            }

        }
        private void ClearText()
        {
            txtStartTime.Clear();
            txtEndTime.Clear();
            txtStartKm.Clear();
            txtEndKm.Clear();
            txtHireCharge.Clear();
            txtWaitingCharge.Clear();
            txtExtraKmCharge.Clear();
            lblWaitingHours.Text = "0";
            lblExtraKm.Text = "0";
            lblTotalHireCharge.Text = "0";
            PlaceHolder();
        }
        private void cmbVehicleNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadcmbPackage();
            ClearText();
        }

        private void btnAddVehicle_Click(object sender, EventArgs e)
        {
            Vehicle.frmVehicle frmV = new Vehicle.frmVehicle(this.LoadcmbVehicle);
            using (frmV)
            {
                if (frmV.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }
        private void DayTourHireCalculation()
        {
            int PckStandardRate = 0;
            int ExtraRateKms = 0;
            int ExtraRateHours = 0;
            int MaxKmLimit = 0;
            TimeSpan MaxNoOfHours = TimeSpan.Parse("0:0");

            //Inputs
            TimeSpan StartTime = (txtStartTime.Text == "" || txtStartTime.Text == "hh:mm") ? TimeSpan.Parse("0:0") : TimeSpan.Parse(txtStartTime.Text);
            TimeSpan EndTime = (txtEndTime.Text == ""|| txtEndTime.Text == "hh:mm" || txtStartTime.Text == "hh:mm") ? TimeSpan.Parse("0:0") : TimeSpan.Parse(txtEndTime.Text);
            int StartKm = (txtStartKm.Text == "") ? 0 : Convert.ToInt32(txtStartKm.Text);
            int EndKm = (txtEndKm.Text == "")? 0:Convert.ToInt32(txtEndKm.Text);
            int ExtraHours = 0;
            int ExtraKm = 0;

            //Assigning input from UI
            //StartTime = TimeSpan.Parse(txtStartTime.Text);
            //EndTime = TimeSpan.Parse(txtEndTime.Text);
            //StartKm = Convert.ToInt32(txtStartKm.Text);
            //EndKm = Convert.ToInt32(txtEndKm.Text);


            //Customer Takes Time
            TimeSpan TotCustomerDuration = EndTime - StartTime;
            var GetMaxNoOfHours = from Package in db.Packages
                                  where Package.Vehicle.VehicleNo == cmbVehicleNo.Text && Package.PackageName == cmbPackageName.Text
                                  select Package.MaxNumOfHours;
            MaxNoOfHours = GetMaxNoOfHours.FirstOrDefault();

            TimeSpan ExtraHoursCalcu = TimeSpan.Parse("0:0");
            
            if (TotCustomerDuration > MaxNoOfHours)
            {
                ExtraHoursCalcu = TotCustomerDuration - MaxNoOfHours;
            }
            ExtraHours = Convert.ToInt32(ExtraHoursCalcu.TotalHours);
            lblWaitingHours.Text = ExtraHours.ToString()+":hrs";

            //Extra Hours Rate
            var GetRatePerHour = from Package in db.Packages
                                  where Package.Vehicle.VehicleNo == cmbVehicleNo.Text && Package.PackageName == cmbPackageName.Text
                                  select Package.ExtraRatePerHour;
            ExtraRateHours = GetRatePerHour.FirstOrDefault() * ExtraHours;
            txtWaitingCharge.Text = ExtraRateHours.ToString();


            //CustomerTakesKm
            int TotCustomerKm = EndKm - StartKm;
            var GetMaxKmLimit = from Package in db.Packages
                                    where Package.Vehicle.VehicleNo == cmbVehicleNo.Text && Package.PackageName == cmbPackageName.Text
                                    select Package.MaxKmLimit;

            MaxKmLimit = GetMaxKmLimit.FirstOrDefault();

            if (TotCustomerKm > MaxKmLimit)
            {
                ExtraKm = TotCustomerKm - MaxKmLimit;
            }
            lblExtraKm.Text = ExtraKm.ToString()+":km";

            //Extra Km Rate 
            var GetExtraPerKmRate = from Package in db.Packages
                                    where Package.Vehicle.VehicleNo == cmbVehicleNo.Text && Package.PackageName == cmbPackageName.Text
                                    select Package.ExtraRatePerKm;
            ExtraRateKms = GetExtraPerKmRate.FirstOrDefault() * ExtraKm;
            txtExtraKmCharge.Text = ExtraRateKms.ToString();

            //Package Standard Rate
            var StandardRate = from Package in db.Packages
                               where Package.Vehicle.VehicleNo == cmbVehicleNo.Text && Package.PackageName == cmbPackageName.Text
                               select Package.StandardRate;
            PckStandardRate = StandardRate.FirstOrDefault();

            txtHireCharge.Text = PckStandardRate.ToString();

            //TotalHireCharge
            int TotalHireCharge = 0;
            TotalHireCharge = PckStandardRate + ExtraRateHours + ExtraRateKms;
            lblTotalHireCharge.Text = TotalHireCharge.ToString();

        }

        private void cmbPackageName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DayTourHireCalculation();
        }

        private void txtEndTime_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtStartKm_TextChanged(object sender, EventArgs e)
        {
            DayTourHireCalculation();
        }
        
        private void txtEndKm_TextChanged(object sender, EventArgs e)
        {
            DayTourHireCalculation();
        }
        private void PlaceHolder()
        {
            if (string.IsNullOrWhiteSpace(txtStartTime.Text))
            {
                txtStartTime.Text = "hh:mm";
            }
            if (string.IsNullOrWhiteSpace(txtEndTime.Text))
            {
                txtEndTime.Text = "hh:mm";
            }
        }

        private void txtStartTime_Enter(object sender, EventArgs e)
        {
            if (txtStartTime.Text == "hh:mm")
            {
                txtStartTime.Text = "";
            }
        }

        private void txtStartTime_Leave(object sender, EventArgs e)
        {
            if (txtStartTime.Text != "")
            {
                DayTourHireCalculation();
            }
            else if (string.IsNullOrWhiteSpace(txtStartTime.Text))
            {
                txtStartTime.Text = "0:0";
                txtEndTime.Text = "0:0";
                DayTourHireCalculation();
                txtStartTime.Text = "hh:mm";
                txtEndTime.Text = "hh:mm";
            }
            
        }

        private void txtEndTime_Enter(object sender, EventArgs e)
        {
            if (txtEndTime.Text == "hh:mm")
            {
                txtEndTime.Text = "";
            }
        }

        private void txtEndTime_Leave(object sender, EventArgs e)
        {
            
            if (txtEndTime.Text != "")
            {
                DayTourHireCalculation();
            }
            else if (string.IsNullOrWhiteSpace(txtEndTime.Text))
            {
                txtEndTime.Text = "0:0";
                DayTourHireCalculation();
                txtEndTime.Text = "hh:mm";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DayTourHireCalculation();
            if (cmbVehicleNo.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Vehicle No");
            }
            else if (cmbPackageName.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Package");
            }
            else if (txtStartTime.Text == String.Empty)
            {
                MessageBox.Show("Plese type correct Start time format");
            }
            else if (txtEndTime.Text == String.Empty)
            {
                MessageBox.Show("Plese type correct End time format");
            }
            else if (txtStartKm.Text == String.Empty)
            {
                MessageBox.Show("Please type Start km");
            }
            else if (txtEndKm.Text == String.Empty)
            {
                MessageBox.Show("Please type End km");
            }
            else
            {
                SaveDayHire();
                MessageBox.Show("Day hire record successfully saved.");
                ClearText();
            }
        }
        private void SaveDayHire()
        {
            Model.DayHire model = new Model.DayHire()
            {
                Packages = (Model.Package)cmbPackageName.SelectedItem,
                StartTime = TimeSpan.Parse(txtStartTime.Text),
                EndTime = TimeSpan.Parse(txtEndTime.Text),
                StartKm = Convert.ToInt32(txtStartKm.Text),
                EndKm = Convert.ToInt32(txtEndKm.Text),
                HireCharge = Convert.ToInt32(txtHireCharge.Text),
                WaitingCharge = Convert.ToInt32(txtWaitingCharge.Text),
                ExtraKmCharge = Convert.ToInt32(txtExtraKmCharge.Text),
                TotalHireCharge = Convert.ToInt32(lblTotalHireCharge.Text)
            };
            db.DayHires.Add(model);
            db.SaveChanges();
        }

        private void btnAddPackage_Click(object sender, EventArgs e)
        {
            Package.frmPackage frmP = new Package.frmPackage(this.LoadcmbPackage);
            using (frmP)
            {
                if (frmP.ShowDialog() == DialogResult.OK)
                {

                }
            }
            LoadcmbVehicle();
        }
    }
}

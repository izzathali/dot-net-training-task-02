using ABC_Drive.Driver;
using ABC_Drive.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABC_Drive.Hire
{
    public partial class frmLongHire : Form
    {
        RentDbContext db = new RentDbContext();
        Model.LongHire model = new LongHire();
        public frmLongHire()
        {
            InitializeComponent();
        }

        private void frmLongHire_Load(object sender, EventArgs e)
        {
            dtpStartDate.CustomFormat = "dd-MM-yyyy";
            dtpEndDate.CustomFormat = "dd-MM-yyyy";
            rbDriverNo.Checked = true;
            LoadcmbVehicle();
            LoadcmbDriver();
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

                cmbPackageName.DataSource = Pckg.ToList();
                cmbPackageName.DisplayMember = "PackageName";
                cmbPackageName.ValueMember = "PackageId";
                cmbPackageName.SelectedIndex = -1;
            }

        }
        private void LoadcmbDriver()
        {
            var Drv = from Driver in db.Drivers select Driver;

            cmbDriverName.DataSource = Drv.ToList();
            cmbDriverName.DisplayMember = "DriverName";
            cmbDriverName.ValueMember = "Id";
            cmbDriverName.SelectedIndex = -1;
        }
        private void ClearText()
        {
            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Today;
            txtStartKm.Clear();
            txtEndKm.Clear();
            txtHireCharge.Clear();
            txtNightStayCharge.Clear();
            txtExtraKmCharge.Clear();
            lblDriverOverNight.Text = "0";
            lblVehicleNightPark.Text = "0";
            lblOverNightStay.Text = "0";
            lblExtraKm.Text = "0";
            lblTotalHireCharge.Text = "0";
            rbDriverNo.Checked = true;
            cmbPackageName.Text = "";
            cmbDriverName.Text = "";
            
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
        private void LongTourHireCalculation()
        {
            string _cmbVehicleNo = cmbVehicleNo.Text;
            string _cmbPackage = cmbPackageName.Text;
            DateTime _StartDate = dtpStartDate.Value;
            DateTime _EndDate = dtpEndDate.Value;
            int _StartKm = (txtStartKm.Text == "") ? 0 : Convert.ToInt32(txtStartKm.Text);
            int _EndKm = (txtEndKm.Text == "") ? 0 : Convert.ToInt32(txtEndKm.Text);
            bool _DriverCked = rbDriverNo.Checked;
            string _cmbDriverName = cmbDriverName.Text;

            /*
            LongTourHireCalculation Cal = new LongTourHireCalculation(_cmbVehicleNo,_cmbPackage,_StartDate,_EndDate,_StartKm,
                _EndKm,_DriverCked,_cmbDriverName);*/
            LongTourHireCalculation Cal = new LongTourHireCalculation();

            int StayOverNights = Cal.StayOverNights(_StartDate,_EndDate);
            lblOverNightStay.Text = StayOverNights.ToString()+":nts";

            int TotDriverOvrNightRate = Cal.TotalDriverOverNightRate(_cmbDriverName);
            lblDriverOverNight.Text = TotDriverOvrNightRate.ToString();

            int TotVehiNightParkRate = Cal.TotalVehicleNightPark(_cmbVehicleNo);
            lblVehicleNightPark.Text = TotVehiNightParkRate.ToString();

            int TotalNightStayCharge = Cal.TotalNightStayCharge(TotDriverOvrNightRate,TotVehiNightParkRate);
            txtNightStayCharge.Text = TotalNightStayCharge.ToString();

            int CalExtraKm = Cal.CalExtraKm(_StartKm, _EndKm, _cmbVehicleNo,_cmbPackage);
            lblExtraKm.Text = CalExtraKm.ToString() + ":km";

            int ExtraKmCharge = Cal.ExtraKmCharge(CalExtraKm, _cmbVehicleNo, _cmbPackage);
            txtExtraKmCharge.Text = ExtraKmCharge.ToString();

            int PackageStandardRate = Cal.PackageStandardRate(_cmbVehicleNo, _cmbPackage);
            txtHireCharge.Text = PackageStandardRate.ToString();

            int TotalHireCharge = Cal.TotalHireCalculation(PackageStandardRate, TotalNightStayCharge, ExtraKmCharge);
            lblTotalHireCharge.Text = TotalHireCharge.ToString();
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            LongTourHireCalculation();
        }

        private void cmbPackageName_SelectedIndexChanged(object sender, EventArgs e)
        {
            LongTourHireCalculation();
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            LongTourHireCalculation();
        }

        private void btnAddDriver_Click(object sender, EventArgs e)
        {
            frmDriver frmD = new frmDriver();
            using (frmD)
            {
                if (frmD.ShowDialog() == DialogResult.OK)
                {

                }
            }
            LoadcmbDriver();
        }

        private void cmbDriverName_SelectedIndexChanged(object sender, EventArgs e)
        {
            LongTourHireCalculation();
        }

        private void txtEndKm_TextChanged(object sender, EventArgs e)
        {
            LongTourHireCalculation();
        }

        private void txtStartKm_TextChanged(object sender, EventArgs e)
        {
            LongTourHireCalculation();
        }

        private void rbDriverYes_Click(object sender, EventArgs e)
        {
            if (cmbVehicleNo.Text == String.Empty)
            {
                MessageBox.Show("Please select a Vehicle No first.");
                rbDriverNo.Checked = true;
            }
            else
            {
                cmbDriverName.Enabled = true;
                btnAddDriver.Enabled = true;
            }
        }

        private void rbDriverNo_Click(object sender, EventArgs e)
        {
            
        }

        private void rbDriverNo_CheckedChanged(object sender, EventArgs e)
        {
            cmbDriverName.Enabled = false;
            btnAddDriver.Enabled = false;
            cmbDriverName.SelectedIndex = -1;
        }
    }
}

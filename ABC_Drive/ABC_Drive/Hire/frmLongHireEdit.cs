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
    public partial class frmLongHireEdit : Form
    {
        RentDbContext db = new RentDbContext();
        public DataGridViewRow dgvr;
        private int HireID = 0;
        private readonly Action _dataUpdate;
        public frmLongHireEdit(Action dataUpdate)
        {
            InitializeComponent();
            _dataUpdate = dataUpdate;
        }

        private void frmLongHireEdit_Load(object sender, EventArgs e)
        {
            dtpStartDate.CustomFormat = "dd-MM-yyyy";
            dtpEndDate.CustomFormat = "dd-MM-yyyy";
            LoadcmbVehicle();
            LoadcmbPackage();
            LoadcmbDriver();
            LoadDataFromLongHire();
            LongTourHireCalculation();
        }
        private void LoadDataFromLongHire()
        {
            HireID = int.Parse(dgvr.Cells[0].Value.ToString());
            cmbVehicleNo.Text = dgvr.Cells[1].Value.ToString();
            cmbPackageName.Text = dgvr.Cells[2].Value.ToString();
            dtpStartDate.Value = DateTime.Parse(dgvr.Cells[3].Value.ToString());
            dtpEndDate.Value = DateTime.Parse(dgvr.Cells[4].Value.ToString());
            txtStartKm.Text = dgvr.Cells[5].Value.ToString();
            txtEndKm.Text = dgvr.Cells[6].Value.ToString();
            if (db.LongHires.Any(u => u.HireId == HireID && u.DriverId == null))
            {
                rbDriverNo.Checked = true;
            }
            else
            {
                cmbDriverName.Text = dgvr.Cells[7].Value.ToString();
                rbDriverYes.Checked = true;
            }
            lblDriverOverNight.Text = dgvr.Cells[8].Value.ToString();
            txtHireCharge.Text = dgvr.Cells[9].Value.ToString();
            txtNightStayCharge.Text = dgvr.Cells[10].Value.ToString();
            txtExtraKmCharge.Text = dgvr.Cells[11].Value.ToString();
            lblTotalHireCharge.Text = dgvr.Cells[12].Value.ToString();
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

            LongTourHireCalculation Cal = new LongTourHireCalculation();

            int StayOverNights = Cal.StayOverNights(_StartDate, _EndDate);
            lblOverNightStay.Text = StayOverNights.ToString() + ":nts";

            int TotDriverOvrNightRate = Cal.TotalDriverOverNightRate(_cmbDriverName);
            lblDriverOverNight.Text = TotDriverOvrNightRate.ToString();

            int TotVehiNightParkRate = Cal.TotalVehicleNightPark(_cmbVehicleNo);
            lblVehicleNightPark.Text = TotVehiNightParkRate.ToString();

            int TotalNightStayCharge = Cal.TotalNightStayCharge(TotDriverOvrNightRate, TotVehiNightParkRate);
            txtNightStayCharge.Text = TotalNightStayCharge.ToString();

            int CalExtraKm = Cal.CalExtraKm(_StartKm, _EndKm, _cmbVehicleNo, _cmbPackage);
            lblExtraKm.Text = CalExtraKm.ToString() + ":km";

            int ExtraKmCharge = Cal.ExtraKmCharge(CalExtraKm, _cmbVehicleNo, _cmbPackage);
            txtExtraKmCharge.Text = ExtraKmCharge.ToString();

            int PackageStandardRate = Cal.PackageStandardRate(_cmbVehicleNo, _cmbPackage);
            txtHireCharge.Text = PackageStandardRate.ToString();

            int TotalHireCharge = Cal.TotalHireCalculation(PackageStandardRate, TotalNightStayCharge, ExtraKmCharge);
            lblTotalHireCharge.Text = TotalHireCharge.ToString();
        }

        private void cmbVehicleNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadcmbPackage();
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            LongTourHireCalculation();
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            LongTourHireCalculation();
        }

        private void cmbPackageName_SelectedIndexChanged(object sender, EventArgs e)
        {
            LongTourHireCalculation();
        }

        private void cmbDriverName_SelectedIndexChanged(object sender, EventArgs e)
        {
            LongTourHireCalculation();
        }

        private void txtStartKm_TextChanged(object sender, EventArgs e)
        {
            LongTourHireCalculation();
        }

        private void txtEndKm_TextChanged(object sender, EventArgs e)
        {
            LongTourHireCalculation();
        }

        private void rbDriverYes_Click(object sender, EventArgs e)
        {
            cmbDriverName.Enabled = true;
            btnAddDriver.Enabled = true;
        }

        private void rbDriverNo_CheckedChanged(object sender, EventArgs e)
        {
            cmbDriverName.Enabled = false;
            btnAddDriver.Enabled = false;
            cmbDriverName.SelectedIndex = -1;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            LongTourHireCalculation();
            if (cmbVehicleNo.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Vehicle No");
            }
            else if (cmbPackageName.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Package");
            }
            else if (dtpStartDate.Value > dtpEndDate.Value)
            {
                MessageBox.Show("Plese check the date.");
            }
            else if (txtStartKm.Text == String.Empty)
            {
                MessageBox.Show("Please type Start km");
            }
            else if (txtEndKm.Text == String.Empty)
            {
                MessageBox.Show("Please type End km");
            }
            else if (rbDriverYes.Checked == true && cmbDriverName.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Driver.");
            }
            else
            {
                LongHireUpdate();
                MessageBox.Show("Long hire record successfully saved.");
                _dataUpdate();
                this.Close();
            }
        }
        private void LongHireUpdate()
        {
            var LongHire = db.LongHires.First(i => i.HireId == HireID);

            var Pckg = (from Pck in db.Packages
                        where Pck.PackageName == cmbPackageName.Text && Pck.Vehicle.VehicleNo == cmbVehicleNo.Text
                        select Pck).FirstOrDefault();

            LongHire.Packages = Pckg;
            LongHire.StartDate = dtpStartDate.Value;
            LongHire.EndDate = dtpEndDate.Value;
            LongHire.StartKm = Convert.ToInt32(txtStartKm.Text);
            LongHire.EndKm = Convert.ToInt32(txtEndKm.Text);
            if (rbDriverNo.Checked != true)
            {
                LongHire.Driver = (Model.Driver)cmbDriverName.SelectedItem;
            }
            else
            {
                LongHire.Driver = null;
            }
            LongHire.TotDriverCharge = Convert.ToInt32(lblDriverOverNight.Text);
            LongHire.HireCharge = Convert.ToInt32(txtHireCharge.Text);
            LongHire.OvernightStayCharge = Convert.ToInt32(txtNightStayCharge.Text);
            LongHire.ExtraKmCharge = Convert.ToInt32(txtExtraKmCharge.Text);
            LongHire.TotalHireCharge = Convert.ToInt32(lblTotalHireCharge.Text);

            db.SaveChanges();

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void btnAddDriver_Click(object sender, EventArgs e)
        {
            frmDriver frmD = new frmDriver(this.LoadcmbDriver);
            using (frmD)
            {
                if (frmD.ShowDialog() == DialogResult.OK)
                {

                }
            }
            LoadcmbDriver();
        }
    }
}

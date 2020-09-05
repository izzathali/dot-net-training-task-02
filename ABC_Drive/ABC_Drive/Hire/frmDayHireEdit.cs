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
    public partial class frmDayHireEdit : Form
    {
        RentDbContext db = new RentDbContext();
        public DataGridViewRow dgvr;
        public int HireID = 0;
        private readonly Action _dataUpdate;

        public frmDayHireEdit(Action dataUpdate)
        {
            InitializeComponent();
            _dataUpdate = dataUpdate;
        }

        private void frmDayHireEdit_Load(object sender, EventArgs e)
        {
            LoadcmbVehicle();
            LoadcmbPackage();
            LoadDataFromDayHire();
        }
        private void LoadDataFromDayHire()
        {
            HireID = int.Parse(dgvr.Cells[0].Value.ToString());
            cmbVehicleNo.Text = dgvr.Cells[1].Value.ToString();
            cmbPackageName.Text = dgvr.Cells[2].Value.ToString();
            TimeSpan StartTime = TimeSpan.Parse(dgvr.Cells[3].Value.ToString());
            txtStartTime.Text = StartTime.ToString();
            TimeSpan EndTime = TimeSpan.Parse(dgvr.Cells[4].Value.ToString());
            txtEndTime.Text = EndTime.ToString();
            txtStartKm.Text = dgvr.Cells[5].Value.ToString();
            txtEndKm.Text = dgvr.Cells[6].Value.ToString();
            txtHireCharge.Text = dgvr.Cells[7].Value.ToString();
            txtWaitingCharge.Text = dgvr.Cells[8].Value.ToString();
            txtExtraKmCharge.Text = dgvr.Cells[9].Value.ToString();
            lblTotalHireCharge.Text = dgvr.Cells[10].Value.ToString();

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
        private void cmbVehicleNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadcmbPackage();
        }

        private void cmbPackageName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DayTourHireCalculation();
        }
        private void DayTourHireCalculation()
        {
            int PackageStandardRate = 0;
            int ExtraKmsTotalRate = 0;
            int WaitingCharge = 0;

            TimeSpan StartTime = (txtStartTime.Text == "" || txtStartTime.Text == "hh:mm") ? TimeSpan.Parse("0:0") : TimeSpan.Parse(txtStartTime.Text);
            TimeSpan EndTime = (txtEndTime.Text == "" || txtEndTime.Text == "hh:mm" || txtStartTime.Text == "hh:mm") ? TimeSpan.Parse("0:0") : TimeSpan.Parse(txtEndTime.Text);
            int StartKm = (txtStartKm.Text == "") ? 0 : Convert.ToInt32(txtStartKm.Text);
            int EndKm = (txtEndKm.Text == "") ? 0 : Convert.ToInt32(txtEndKm.Text);
            int WaitingHours = 0;
            int ExtraKm = 0;
            string VehicleNo = cmbVehicleNo.Text;
            string Package = cmbPackageName.Text;

            DayTourHireCalculation Cal = new DayTourHireCalculation();
            WaitingHours = Cal.WaitingHours(StartTime, EndTime, VehicleNo, Package);
            WaitingCharge = Cal.WaitingCharge(WaitingHours, VehicleNo, Package);
            ExtraKm = Cal.CalculateExtraKm(StartKm, EndKm, VehicleNo, Package);
            ExtraKmsTotalRate = Cal.ExtraKmTotalRate(ExtraKm, VehicleNo, Package);
            PackageStandardRate = Cal.PackageStandardRate(VehicleNo, Package);
            int TotalHireCharge = Cal.TotalHireCharge(PackageStandardRate, WaitingCharge, ExtraKmsTotalRate);

            lblWaitingHours.Text = WaitingHours.ToString() + ":hrs";
            txtWaitingCharge.Text = WaitingCharge.ToString();
            lblExtraKm.Text = ExtraKm.ToString() + ":km";
            txtExtraKmCharge.Text = ExtraKmsTotalRate.ToString();
            txtHireCharge.Text = PackageStandardRate.ToString();
            lblTotalHireCharge.Text = TotalHireCharge.ToString();
        }

        private void txtStartTime_Leave(object sender, EventArgs e)
        {
            DayTourHireCalculation();
        }

        private void txtEndTime_Leave(object sender, EventArgs e)
        {
            DayTourHireCalculation();
        }

        private void txtStartKm_TextChanged(object sender, EventArgs e)
        {
            DayTourHireCalculation();
        }

        private void txtEndKm_TextChanged(object sender, EventArgs e)
        {
            DayTourHireCalculation();
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

        private void btnUpdate_Click(object sender, EventArgs e)
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
                UpdateDayHire();
                MessageBox.Show("Day tour record successfully Updated.");
                _dataUpdate();
                this.Close();
            }
        }
        private void UpdateDayHire()
        {
            var DayHire = db.DayHires.First(i => i.HireId == HireID);
            var PckId = db.Packages.Where(p => p.Vehicle.VehicleNo == cmbVehicleNo.Text && p.PackageName == cmbPackageName.Text)
                                    .Select(x => x.PackageId).FirstOrDefault();
            var Pckg = (from Pck in db.Packages
                       where Pck.PackageName == cmbPackageName.Text && Pck.Vehicle.VehicleNo == cmbVehicleNo.Text
                       select Pck).FirstOrDefault();

            DayHire.Packages = Pckg;
            DayHire.StartTime = TimeSpan.Parse(txtStartTime.Text);
            DayHire.EndTime = TimeSpan.Parse(txtEndTime.Text);
            DayHire.StartKm = Convert.ToInt32(txtStartKm.Text);
            DayHire.EndKm = Convert.ToInt32(txtEndKm.Text);
            DayHire.HireCharge = Convert.ToInt32(txtHireCharge.Text);
            DayHire.WaitingCharge = Convert.ToInt32(txtWaitingCharge.Text);
            DayHire.ExtraKmCharge = Convert.ToInt32(txtExtraKmCharge.Text);
            DayHire.TotalHireCharge = Convert.ToInt32(lblTotalHireCharge.Text);

            db.SaveChanges();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

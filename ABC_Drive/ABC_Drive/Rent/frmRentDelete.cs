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

namespace ABC_Drive.Rent
{
    public partial class frmRentDelete : Form
    {
        public DataGridViewRow dgvr;
        private int RentId;
        RentDbContext db = new RentDbContext();
        public frmRentDelete()
        {
            InitializeComponent();
        }

        private void frmRentDelete_Load(object sender, EventArgs e)
        {
            LoadDataFromRent();
            LoadVehicleData();
            dtpRentedDate.CustomFormat = "dd-MM-yyyy";
            dtpReturnedDate.CustomFormat = "dd-MM-yyyy";
            rbDriverNo.Checked = true;
            LoadCmbDriver();
        }
        private void LoadDataFromRent()
        {
            RentId = int.Parse(dgvr.Cells[0].Value.ToString());
            dtpRentedDate.Value = DateTime.Parse(dgvr.Cells[2].Value.ToString());
            dtpReturnedDate.Value = DateTime.Parse(dgvr.Cells[3].Value.ToString());
            cmbLoadDriver.Text = (dgvr.Cells[4].Value.ToString() == null) ? "" : dgvr.Cells[4].Value.ToString();
            lblTotDays.Text = dgvr.Cells[5].Value.ToString();
            lblTotDriverCost.Text = dgvr.Cells[6].Value.ToString();
            txtTotDaysAmnt.Text = dgvr.Cells[7].Value.ToString();
            txtTotWeeksAmnt.Text = dgvr.Cells[8].Value.ToString();
            txtTotMonthsAmnt.Text = dgvr.Cells[9].Value.ToString();
            lblTotRent.Text = dgvr.Cells[10].Value.ToString();
        }
        private void LoadVehicleData()
        {
            var VehId = (from Rent in db.Rents
                         where Rent.RentId == RentId
                         select Rent.VehicleId).FirstOrDefault();

            var VehNo = (from Vehicle in db.Vehicles
                         where Vehicle.VehicleId == VehId
                         select Vehicle.VehicleNo).FirstOrDefault();

            txtVehicleNo.Text = VehNo.ToString();
        }
        private void LoadCmbDriver()
        {
            var Drv = from Driver in db.Drivers select Driver;

            cmbLoadDriver.DataSource = Drv.ToList();
            cmbLoadDriver.DisplayMember = "DriverName";
            cmbLoadDriver.ValueMember = "Id";
            cmbLoadDriver.SelectedIndex = -1;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure to Delete this Record ?", "Rent Record", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                db.Rents.RemoveRange(db.Rents.Where(x => x.RentId == RentId));
                db.SaveChanges();
                MessageBox.Show("Rent Successfully Deleted");
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

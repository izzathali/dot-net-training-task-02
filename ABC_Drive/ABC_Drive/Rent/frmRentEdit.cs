using ABC_Drive.Model;
using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace ABC_Drive.Rent
{
    public partial class frmRentEdit : Form
    {
        public DataGridViewRow dgvr;
        private int RentId;
        RentDbContext db = new RentDbContext();
        public frmRentEdit()
        {
            InitializeComponent();
        }

        private void frmRentEdit_Load(object sender, EventArgs e)
        {
            LoadDataFromRent();
            LoadVehicleData();
            dtpRentedDate.CustomFormat = "dd-MM-yyyy";
            dtpReturnedDate.CustomFormat = "dd-MM-yyyy";
            rbDriverNo.Checked = true;
            LoadCmbDriver();
        }
        private void LoadCmbDriver()
        {
            var Drv = from Driver in db.Drivers select Driver;

            cmbLoadDriver.DataSource = Drv.ToList();
            cmbLoadDriver.DisplayMember = "DriverName";
            cmbLoadDriver.ValueMember = "Id";
            cmbLoadDriver.SelectedIndex = -1;
        }
        private void LoadDataFromRent()
        {
            RentId = int.Parse(dgvr.Cells[0].Value.ToString());
            dtpRentedDate.Value = DateTime.Parse(dgvr.Cells[2].Value.ToString());
            dtpReturnedDate.Value = DateTime.Parse(dgvr.Cells[3].Value.ToString());
            cmbLoadDriver.Text = (dgvr.Cells[4].Value.ToString() == null)?"": dgvr.Cells[4].Value.ToString();
            if (cmbLoadDriver.Text == "")
            {
                rbDriverNo.Checked = true;
            }
            else
            {
                rbDriverYes.Checked = true;
            }
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtVehicleNo.Text == "")
            {
                MessageBox.Show("Please type or select Vehicle No");
            }
            else
            {
                if (db.Vehicles.Any(p => p.VehicleNo == txtVehicleNo.Text))
                {
                    if (dtpRentedDate.Value > dtpReturnedDate.Value)
                    {
                        MessageBox.Show("Rented Date cannot be more than Returned date");
                    }
                    else
                    {
                        if (rbDriverYes.Checked == true && cmbLoadDriver.SelectedIndex == -1)
                        {
                            MessageBox.Show("Please select the Driver");
                        }
                        else
                        {
                            UpdateRent();
                            MessageBox.Show("Successfully Updated");
                            this.Close();
                        }
                        
                    }
                }
                else
                {
                    MessageBox.Show("There is no vehicle matching this vehicle no. Please check vehicle no");
                }
            }
        }
        private void UpdateRent()
        {
            var rent = db.Rents.First(i => i.RentId == RentId);

            string VehicleNo = txtVehicleNo.Text;
            var VehiId = (from Vehicle in db.Vehicles
                          where Vehicle.VehicleNo == VehicleNo
                          select Vehicle.VehicleId).FirstOrDefault();

            rent.VehicleId = VehiId;

            rent.RentedDate = dtpRentedDate.Value;

            rent.ReturnedDate = dtpReturnedDate.Value;

            if (rbDriverNo.Checked != true)
            {
                rent.Driver = (Model.Driver)cmbLoadDriver.SelectedItem;
            }

            rent.TotDays = Convert.ToInt32(lblTotDays.Text);
            rent.TotDriverCost = Convert.ToInt32(lblTotDriverCost.Text);
            rent.TotDaysAmnt = Convert.ToInt32(txtTotDaysAmnt.Text);
            rent.TotWeeksAmnt = Convert.ToInt32(txtTotWeeksAmnt.Text);
            rent.TotMonthsAmnt = Convert.ToInt32(txtTotMonthsAmnt.Text);
            rent.TotalRent = Convert.ToInt32(lblTotRent.Text);

            db.SaveChanges();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbDriverNo_CheckedChanged(object sender, EventArgs e)
        {
            cmbLoadDriver.Enabled = false;
            btnAddDriver.Enabled = false;
            cmbLoadDriver.SelectedIndex = -1;
        }

        private void rbDriverYes_Click(object sender, EventArgs e)
        {
            if (txtVehicleNo.Text == String.Empty)
            {
                MessageBox.Show("Please type a Vehicle No first.");
                rbDriverNo.Checked = true;
                txtVehicleNo.Focus();
            }
            else if (db.Vehicles.Any(p => p.VehicleNo != txtVehicleNo.Text))
            {
                MessageBox.Show("Please type Valid Vehicle No");
                rbDriverNo.Checked = true;
                txtVehicleNo.Focus();
            }
            else
            {
                cmbLoadDriver.Enabled = true;
                btnAddDriver.Enabled = true;
                //RentCalculation();
            }
        }
    }
}

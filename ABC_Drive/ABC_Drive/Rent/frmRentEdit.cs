using ABC_Drive.Driver;
using ABC_Drive.Model;
using ABC_Drive.Vehicle;
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
        private readonly Action _dataUpdate;
        private int VehicleID = 0;

        public frmRentEdit(Action dataUpdate)
        {
            InitializeComponent();
            _dataUpdate = dataUpdate;
        }

        private void frmRentEdit_Load(object sender, EventArgs e)
        {
            LoadCmbDriver();
            LoadDataFromRent();
            //LoadVehicleData();
            dtpRentedDate.CustomFormat = "dd-MM-yyyy";
            dtpReturnedDate.CustomFormat = "dd-MM-yyyy";
            //rbDriverNo.Checked = true;
            
            //LoadDriver();
            
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
            txtVehicleNo.Text = dgvr.Cells[1].Value.ToString();
            dtpRentedDate.Value = DateTime.Parse(dgvr.Cells[2].Value.ToString());
            dtpReturnedDate.Value = DateTime.Parse(dgvr.Cells[3].Value.ToString());
            
            if (db.Rents.Any(u => u.RentId == RentId && u.DriverId == null))
            {
                rbDriverNo.Checked = true;
            }
            else
            {
                cmbLoadDriver.Text = dgvr.Cells[4].Value.ToString();
                rbDriverYes.Checked = true;
            }
            lblTotDays.Text = dgvr.Cells[5].Value.ToString();
            lblTotDriverCost.Text = dgvr.Cells[6].Value.ToString();
            txtTotDaysAmnt.Text = dgvr.Cells[7].Value.ToString();
            txtTotWeeksAmnt.Text = dgvr.Cells[8].Value.ToString();
            txtTotMonthsAmnt.Text = dgvr.Cells[9].Value.ToString();
            lblTotRent.Text = dgvr.Cells[10].Value.ToString();
            
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
                            _dataUpdate();
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
        
        private void LoadVehicle()
        {
            var Vehi = (from Vehicle in db.Vehicles
                        where Vehicle.VehicleNo == txtVehicleNo.Text
                        select Vehicle.VehicleId).FirstOrDefault();
            VehicleID = Vehi;
        }
        private void RentCalculation()
        {
            if (db.Vehicles.Any(p => p.VehicleNo == txtVehicleNo.Text))
            {
                LoadVehicle();

                int VehiId = VehicleID;
                string VehiNo = txtVehicleNo.Text;
                DateTime RentedDate = dtpRentedDate.Value;
                DateTime ReturnedDate = dtpReturnedDate.Value;
                bool Driver = rbDriverYes.Checked;
                string DriverName = cmbLoadDriver.Text;

                Rent.RentCalculation Cal = new RentCalculation();
                int Days = Cal.CountDays(RentedDate, ReturnedDate);
                int RatePerDay = Cal.RatePerDay(VehicleID);
                int RatePerWeek = Cal.RatePerWeek(VehicleID);
                int RatePerMonth = Cal.RatePerMonth(VehicleID);
                int DriverRatePerDay = Cal.DriverRatePerDay(DriverName);
                int TotDays = Cal.TotDays(Days);
                int TotWeeks = Cal.TotWeeks(Days);
                int TotMonths = Cal.TotMonths(Days);
                int TotDaysAmnt = Cal.TotDaysAmount(TotDays, RatePerDay);
                int TotWeeksAmnt = Cal.TotWeeksAmount(TotWeeks, RatePerWeek);
                int TotMonthsAmnt = Cal.TotMonthsAmount(TotMonths, RatePerMonth);
                int TotDriverCharge = Cal.TotDriverCharge(Days, DriverRatePerDay);
                int TotRent = Cal.TotalRent(TotDaysAmnt, TotWeeksAmnt, TotMonthsAmnt, TotDriverCharge);

                lblTotDays.Text = TotDays.ToString();
                lblTotWeeks.Text = TotWeeks.ToString();
                lblTotMonths.Text = TotMonths.ToString();
                txtTotDaysAmnt.Text = TotDaysAmnt.ToString();
                txtTotWeeksAmnt.Text = TotWeeksAmnt.ToString();
                txtTotMonthsAmnt.Text = TotMonthsAmnt.ToString();
                lblTotDriverCost.Text = TotDriverCharge.ToString();
                lblTotRent.Text = TotRent.ToString();
            }
        }

        private void txtVehicleNo_TextChanged(object sender, EventArgs e)
        {
            if (txtVehicleNo.Text != "")
            {
                dgvVehicleList.Visible = dgvVehicleList.Rows.Count > 0;

                var result = (from Vehicle in db.Vehicles
                              where Vehicle.VehicleNo.Contains(txtVehicleNo.Text.ToString())
                              select new
                              {
                                  ID = Vehicle.VehicleId,
                                  VehicleNo = Vehicle.VehicleNo
                              }).ToList();
                dgvVehicleList.DataSource = result;
                dgvVehicleList.Columns[0].Visible = false;
            }
            else if (txtVehicleNo.Text == "")
            {
                dgvVehicleList.Visible = false;
            }
        }

        private void dtpRentedDate_ValueChanged(object sender, EventArgs e)
        {
            RentCalculation();
            
        }

        private void dtpReturnedDate_ValueChanged(object sender, EventArgs e)
        {
            RentCalculation();
        }

        private void btnAddDriver_Click(object sender, EventArgs e)
        {
            frmDriver frmD = new frmDriver(this.LoadCmbDriver);
            using (frmD)
            {
                if (frmD.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }

        private void cmbLoadDriver_SelectedIndexChanged(object sender, EventArgs e)
        {
            RentCalculation();
        }

        private void dgvVehicleList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow dgv = this.dgvVehicleList.CurrentRow;
                VehicleID = int.Parse(dgv.Cells[0].Value.ToString());
                this.txtVehicleNo.Text = dgv.Cells[1].Value.ToString();
                dgvVehicleList.Visible = false;
                RentCalculation();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbLoadDriver_Click(object sender, EventArgs e)
        {
            
        }

        private void btnAddVehicle_Click(object sender, EventArgs e)
        {
            frmVehicle frmV = new frmVehicle(this.LoadVehicle);
            using (frmV)
            {
                if (frmV.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }
    }
}

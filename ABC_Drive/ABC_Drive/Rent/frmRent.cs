using ABC_Drive.Driver;
using ABC_Drive.Model;
using ABC_Drive.Vehicle;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABC_Drive.Rent
{
    public partial class frmRent : Form
    {
        
        RentDbContext db = new RentDbContext();
        Model.Rent model = new Model.Rent();
        private int VehicleID = 0;
        //int VehicleID;
        
        public frmRent()
        {
            InitializeComponent();
        }

        private void frmRent_Load(object sender, EventArgs e)
        {
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
        private void RentCalculation()
        {
            
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
            int TotDaysAmnt = Cal.TotDaysAmount(TotDays,RatePerDay);
            int TotWeeksAmnt = Cal.TotWeeksAmount(TotWeeks,RatePerWeek);
            int TotMonthsAmnt = Cal.TotMonthsAmount(TotMonths, RatePerMonth);
            int TotDriverCharge = Cal.TotDriverCharge(Days,DriverRatePerDay);
            int TotRent = Cal.TotalRent(TotDaysAmnt,TotWeeksAmnt,TotMonthsAmnt,TotDriverCharge);

            lblTotDays.Text = TotDays.ToString();
            lblTotWeeks.Text = TotWeeks.ToString();
            lblTotMonths.Text = TotMonths.ToString();
            txtTotDaysAmnt.Text = TotDaysAmnt.ToString();
            txtTotWeeksAmnt.Text = TotWeeksAmnt.ToString();
            txtTotMonthsAmnt.Text = TotMonthsAmnt.ToString();
            lblTotDriverCost.Text = TotDriverCharge.ToString();
            lblTotRent.Text = TotRent.ToString();

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

        private void dtpRentedDate_ValueChanged(object sender, EventArgs e)
        {
            if (txtVehicleNo.Text == String.Empty)
            {
                MessageBox.Show("Please type a Vehicle No first.");
                txtVehicleNo.Focus();
            }
            else
            {
                RentCalculation();
            }
        }
        private void dtpReturnedDate_ValueChanged(object sender, EventArgs e)
        {
            if (txtVehicleNo.Text == String.Empty)
            {
                MessageBox.Show("Please type a Vehicle No first.");
                txtVehicleNo.Focus();
            }
            else
            {
                RentCalculation();
            }
        }

        private void rbDriverYes_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void rbDriverNo_CheckedChanged(object sender, EventArgs e)
        {
            cmbLoadDriver.Enabled = false;
            btnAddDriver.Enabled = false;
            cmbLoadDriver.SelectedIndex = -1;
        }
        private void LoadVehicle()
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

        private void txtVehicleNo_Leave(object sender, EventArgs e)
        {
            VehicleNoLoad();
        }
        
        private void VehicleNoLoad()
        {
            if (txtVehicleNo.Text != String.Empty)
            {
                if (db.Vehicles.Any(p => p.VehicleNo == txtVehicleNo.Text))
                {
                    var vehic = db.Vehicles.Where(x => x.VehicleNo == txtVehicleNo.Text).Select(u => u.VehicleId).FirstOrDefault();
                    VehicleID = vehic;
                    dgvVehicleList.Visible = false;
                }
                else
                {
                    VehicleID = 0;
                }
            }
        }
        private void ClearText()
        {
            dtpRentedDate.Value = DateTime.Today;
            dtpReturnedDate.Value = DateTime.Today;
            rbDriverNo.Checked = true;
            lblTotDays.Text = "0";
            lblTotWeeks.Text = "0";
            lblTotMonths.Text = "0";
            txtTotDaysAmnt.Text = String.Empty;
            txtTotWeeksAmnt.Text = String.Empty;
            txtTotMonthsAmnt.Text = String.Empty;
            lblTotDriverCost.Text = "0";
            lblTotRent.Text = "0";

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtVehicleNo.Text == String.Empty)
            {
                MessageBox.Show("Please Type Vehicle No.");
            }
            else if (dtpRentedDate.Value > dtpReturnedDate.Value)
            {
                MessageBox.Show("Please type valid date");
            }
            else if (rbDriverYes.Checked == true && cmbLoadDriver.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Driver");
            }
            else
            {
                if (db.Vehicles.Any(p => p.VehicleNo == txtVehicleNo.Text))
                {
                    RentCalculation();
                    SaveRent();
                    MessageBox.Show("Rent successfully saved");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid Vehicle No");
                }
                    
            }
        }
        private void SaveRent()
        {
            Model.Rent model = new Model.Rent()
            {
                VehicleId = VehicleID,
                RentedDate = dtpRentedDate.Value,
                ReturnedDate = dtpReturnedDate.Value,
                Driver = (Model.Driver)cmbLoadDriver.SelectedItem,
                TotDays = Convert.ToInt32(lblTotDays.Text),
                TotDriverCost = Convert.ToInt32(lblTotDriverCost.Text),
                TotDaysAmnt = Convert.ToInt32(txtTotDaysAmnt.Text),
                TotWeeksAmnt = Convert.ToInt32(txtTotWeeksAmnt.Text),
                TotMonthsAmnt = Convert.ToInt32(txtTotMonthsAmnt.Text),
                TotalRent = Convert.ToInt32(lblTotRent.Text)
            };
            db.Rents.Add(model);
            db.SaveChanges();
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

        private void rbDriverYes_Click(object sender, EventArgs e)
        {
            if (txtVehicleNo.Text == String.Empty)
            {
                MessageBox.Show("Please type a Vehicle No first.");
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

        private void cmbLoadDriver_SelectedIndexChanged(object sender, EventArgs e)
        {
             RentCalculation();
        }
    }
}

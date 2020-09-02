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

namespace ABC_Drive.Package
{
    public partial class frmPackage : Form
    {
        Model.Package model = new Model.Package();
        RentDbContext db = new RentDbContext();

        private readonly Action _dataUpdate;
        public frmPackage(Action dataUpdate)
        {
            InitializeComponent();
            _dataUpdate = dataUpdate;
        }

        private void frmPackage_Load(object sender, EventArgs e)
        {
            loadCmbVehicleNo();
        }
        private void loadCmbVehicleNo()
        {
            var Vehi = from Vehicle in db.Vehicles select Vehicle;

            cmbVehicleNo.DataSource = Vehi.ToList();
            cmbVehicleNo.DisplayMember = "VehicleNo";
            cmbVehicleNo.ValueMember = "VehicleId";
            cmbVehicleNo.SelectedIndex = -1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbVehicleNo.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Vechicle No");
            }
            else if (txtPackageName.Text == "")
            {
                MessageBox.Show("Please type package Name");
            }
            else if (txtStandardRate.Text == "")
            {
                MessageBox.Show("Please type Standard Rate");
            }
            else if (txtExtraRatePerKm.Text == "")
            {
                MessageBox.Show("Please type Extra Rate for per Km");
            }
            else if (txtExtraRatePerHour.Text =="")
            {
                MessageBox.Show("Please type Extra Rate for per Hour");
            }
            else if (txtMaxKmLimit.Text == "")
            {
                MessageBox.Show("Please type Maximum Km limit");
            }
            else if (txtMaxNoOfHours.Text == "")
            {
                MessageBox.Show("Please type Maximum number of Hours");
            }
            else
            {
                if (db.Packages.Any(p => p.Vehicle.VehicleNo == cmbVehicleNo.Text && p.PackageName == txtPackageName.Text))
                {
                    MessageBox.Show("This package already in the Package list.");
                }
                else
                {
                    //MessageBox.Show("You can add this package");
                    
                    Model.Package model = new Model.Package()
                    {
                        Vehicle = (Model.Vehicle)cmbVehicleNo.SelectedItem,
                        PackageName = txtPackageName.Text,
                        StandardRate = Convert.ToInt32(txtStandardRate.Text),
                        MaxKmLimit = Convert.ToInt32(txtMaxKmLimit.Text),
                        MaxNumOfHours = TimeSpan.Parse(txtMaxNoOfHours.Text),
                        ExtraRatePerHour = Convert.ToInt32(txtExtraRatePerHour.Text),
                        ExtraRatePerKm = Convert.ToInt32(txtExtraRatePerKm.Text)
                    };
                    db.Packages.Add(model);
                    db.SaveChanges();
                    MessageBox.Show("Package Successfully saved");
                    _dataUpdate();
                    this.Close();
                }
            }
        }
        private void btnAddVehicle_Click(object sender, EventArgs e)
        {
            Vehicle.frmVehicle frmV = new Vehicle.frmVehicle(this.loadCmbVehicleNo);
            using (frmV)
            {
                if (frmV.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }
    }
}

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

namespace ABC_Drive.Driver
{
    public partial class frmDriver : Form
    {
        Model.Driver model = new Model.Driver();
        RentDbContext db = new RentDbContext();

        private readonly Action _dataUpdate;
        public frmDriver(Action dataUpdate)
        {
            InitializeComponent();
            _dataUpdate = dataUpdate;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtDriverName.Text == String.Empty)
            {
                MessageBox.Show("Please type a Driver name");
            }
            else if (txtDriverCost.Text == String.Empty)
            {
                MessageBox.Show("Please type a Driver Cost per day");
            }
            else if (cmbLicence.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Licence type");
            }
            else
            {
                if (db.Drivers.Any(p => p.DriverName == txtDriverName.Text))
                {
                    MessageBox.Show("This Driver Name already Recorded. Please type Unique Driver Name...");
                }
                else
                {
                    Model.Driver model = new Model.Driver()
                    {
                        DriverName = txtDriverName.Text,
                        DriverCost = Convert.ToInt32(txtDriverCost.Text),
                        LicenceType = cmbLicence.SelectedText,
                        RatePerOverNight = Convert.ToInt32(txtRatePerOverNight.Text)
                    };
                    db.Drivers.Add(model);
                    db.SaveChanges();
                    MessageBox.Show("Driver Details Saccessfully Saved");
                    _dataUpdate();
                    this.Close();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDriver_Load(object sender, EventArgs e)
        {

        }
        private void LoadCmbLicenceType()
        {
            
        }
    }
}

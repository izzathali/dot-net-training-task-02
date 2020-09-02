using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ABC_Drive.Model;

namespace ABC_Drive.UserControlls
{
    public partial class longhire : UserControl
    {
        public longhire()
        {
            InitializeComponent();
        }

        private void longhire_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        
        private void LoadData()
        {
            try
            {
                BindingSource bs = new BindingSource();
                using (RentDbContext db = new RentDbContext())
                {
                    var LongHire = from Hire in db.LongHires
                                   select new
                                   {
                                       Package = Hire.Packages.PackageName,
                                       StartTime = Hire.StartDate,
                                       EndTime = Hire.EndDate,
                                       StartKm = Hire.StartKm,
                                       EndKm = Hire.EndKm,
                                       Driver = Hire.Driver.DriverName,
                                       TotalDriverCharge = Hire.TotDriverCharge,
                                       PackageCharge = Hire.HireCharge,
                                       OverNightStayCharge = Hire.OvernightStayCharge,
                                       ExtraKm = Hire.EndKm,
                                       TotalHireCharge = Hire.TotalHireCharge
                                   };
                    bs.DataSource = LongHire.ToList();
                    dgvLongTour.DataSource = bs;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void btnAddLongTour_Click(object sender, EventArgs e)
        {

        }

        private void btnEditLongTour_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteLongTour_Click(object sender, EventArgs e)
        {

        }
    }
}

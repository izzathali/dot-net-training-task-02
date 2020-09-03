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
    public partial class daytour : UserControl
    {
        public daytour()
        {
            InitializeComponent();
        }

        private void daytour_Load(object sender, EventArgs e)
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
                    var DayHire = from Hire in db.DayHires
                                  select new
                                  {
                                      Package = Hire.Packages.PackageName,
                                      StartTime = Hire.StartTime,
                                      EndTime = Hire.EndTime,
                                      StartKm = Hire.StartKm,
                                      EndKm = Hire.EndKm,
                                      PackageCharge = Hire.HireCharge,
                                      WaitingCharge = Hire.WaitingCharge,
                                      ExtraKm = Hire.EndKm,
                                      TotalHireCharge = Hire.TotalHireCharge
                                  };
                    bs.DataSource = DayHire.ToList();
                    dgvDayTour.DataSource = bs;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private void btnAddDayTour_Click(object sender, EventArgs e)
        {

        }

        private void btnEditDayTour_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteDayTour_Click(object sender, EventArgs e)
        {

        }
    }
}

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
using ABC_Drive.Rent;
using System.Data.Entity;

namespace ABC_Drive.UserControlls
{
    public partial class Rent : UserControl
    {
        public Rent()
        {
            InitializeComponent();
        }

        private void Rent_Load(object sender, EventArgs e)
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
                    var rent = from Rent in db.Rents
                               select new
                               {
                                   Vehicle = Rent.VehicleId,
                                   RentedDate = Rent.RentedDate,
                                   ReturnedDate = Rent.ReturnedDate,
                                   Driver = Rent.Driver.DriverName,
                                   TotalDays = Rent.TotDays,
                                   TotalDriver = Rent.TotDriverCost,
                                   TotalDaysAmount = Rent.TotDaysAmnt,
                                   TotalWeeksAmount = Rent.TotWeeksAmnt,
                                   TotalMonthsAmount = Rent.TotMonthsAmnt,
                                   TotalRent = Rent.TotalRent
                               };
                    bs.DataSource = rent.ToList();
                    dgvRent.DataSource = bs;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private void btnAddRent_Click(object sender, EventArgs e)
        {
            frmRent rent = new frmRent();
            using (rent)
            {
                if (rent.ShowDialog() == DialogResult.OK)
                {

                }
            }

        }

        private void btnEditRent_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteRent_Click(object sender, EventArgs e)
        {

        }
    }
}

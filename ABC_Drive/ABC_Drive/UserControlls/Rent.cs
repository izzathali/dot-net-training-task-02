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
                                   RentId = Rent.RentId,
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
                    dgvRent.Columns[0].Visible = false;
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
            try
            {
                frmRentEdit rentEdit = new frmRentEdit();
                using (rentEdit)
                {
                    if (dgvRent.CurrentRow.Index != -1 || dgvRent.CurrentRow.Index > 0)
                    {
                        int indx = dgvRent.CurrentRow.Index;
                        rentEdit.dgvr = dgvRent.Rows[indx];
                        if (rentEdit.ShowDialog() == DialogResult.OK)
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteRent_Click(object sender, EventArgs e)
        {
            try
            {
                frmRentDelete rentDelete = new frmRentDelete();
                using (rentDelete)
                {
                    if (dgvRent.CurrentRow.Index != -1 || dgvRent.CurrentRow.Index > 0)
                    {
                        int indx = dgvRent.CurrentRow.Index;
                        rentDelete.dgvr = dgvRent.Rows[indx];
                        if (rentDelete.ShowDialog() == DialogResult.OK)
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

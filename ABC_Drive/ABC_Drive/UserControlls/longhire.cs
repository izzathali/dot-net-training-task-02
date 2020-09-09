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
using ABC_Drive.Hire;

namespace ABC_Drive.UserControlls
{
    public partial class longhire : UserControl
    {
        RentDbContext db = new RentDbContext();
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
                                       HireId = Hire.HireId,
                                       Vehicle = Hire.Packages.Vehicle.VehicleNo,
                                       Package = Hire.Packages.PackageName,
                                       StartTime = Hire.StartDate,
                                       EndTime = Hire.EndDate,
                                       StartKm = Hire.StartKm,
                                       EndKm = Hire.EndKm,
                                       Driver = Hire.Driver.DriverName,
                                       TotalDriverCharge = Hire.TotDriverCharge,
                                       PackageCharge = Hire.HireCharge,
                                       OverNightStayCharge = Hire.OvernightStayCharge,
                                       ExtraKm = Hire.ExtraKmCharge,
                                       TotalHireCharge = Hire.TotalHireCharge
                                   };
                    bs.DataSource = LongHire.ToList();
                    dgvLongTour.DataSource = bs;
                    dgvLongTour.Columns[0].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void btnAddLongTour_Click(object sender, EventArgs e)
        {
            frmLongHire frmL = new frmLongHire(this.LoadData);
            using (frmL)
            {
                if (frmL.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }

        private void btnEditLongTour_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvLongTour.RowCount > 0)
                {
                    frmLongHireEdit frmL = new frmLongHireEdit(this.LoadData);
                    using (frmL)
                    {
                        if (dgvLongTour.CurrentRow.Index != -1 || dgvLongTour.CurrentRow.Index > 0)
                        {
                            int indx = dgvLongTour.CurrentRow.Index;
                            frmL.dgvr = dgvLongTour.Rows[indx];
                            if (frmL.ShowDialog() == DialogResult.OK)
                            {

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteLongTour_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvLongTour.RowCount > 0)
                {

                    if (dgvLongTour.CurrentRow.Index != -1 || dgvLongTour.CurrentRow.Index > 0)
                    {
                        int indx = dgvLongTour.CurrentRow.Index;
                        if (MessageBox.Show("Are You Sure to Delete this Record ?", "Long tour Record", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            db.LongHires.RemoveRange(db.LongHires.Where(x => x.HireId == indx));
                            db.SaveChanges();
                            MessageBox.Show("Long tour record Successfully Deleted");
                            LoadData();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvLongTour.RowCount > 0)
                {
                    frmLongHireEdit frmL = new frmLongHireEdit(this.LoadData);
                    using (frmL)
                    {
                        if (dgvLongTour.CurrentRow.Index != -1 || dgvLongTour.CurrentRow.Index > 0)
                        {
                            int indx = dgvLongTour.CurrentRow.Index;
                            frmL.dgvr = dgvLongTour.Rows[indx];
                            if (frmL.ShowDialog() == DialogResult.OK)
                            {

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvLongTour.RowCount > 0)
                {

                    if (dgvLongTour.CurrentRow.Index != -1 || dgvLongTour.CurrentRow.Index > 0)
                    {
                        int indx = dgvLongTour.CurrentRow.Index;
                        if (MessageBox.Show("Are You Sure to Delete this Record ?", "Long tour Record", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            db.LongHires.RemoveRange(db.LongHires.Where(x => x.HireId == indx));
                            db.SaveChanges();
                            MessageBox.Show("Long tour record Successfully Deleted");
                            LoadData();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                using (RentDbContext db = new RentDbContext())
                {
                    var LongHire = (from Hire in db.LongHires
                                   select new
                                   {
                                       HireId = Hire.HireId,
                                       Vehicle = Hire.Packages.Vehicle.VehicleNo,
                                       Package = Hire.Packages.PackageName,
                                       StartDate = Hire.StartDate,
                                       EndDate = Hire.EndDate,
                                       StartKm = Hire.StartKm,
                                       EndKm = Hire.EndKm,
                                       Driver = Hire.Driver.DriverName,
                                       TotalDriverCharge = Hire.TotDriverCharge,
                                       PackageCharge = Hire.HireCharge,
                                       OverNightStayCharge = Hire.OvernightStayCharge,
                                       ExtraKmCharge = Hire.ExtraKmCharge,
                                       TotalHireCharge = Hire.TotalHireCharge
                                   }).ToList();
                    DataTable LongHires = LongTourHireCalculation.ToDataTable(LongHire);
                    LongTourHireCalculation.ExportLongHires(LongHires);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

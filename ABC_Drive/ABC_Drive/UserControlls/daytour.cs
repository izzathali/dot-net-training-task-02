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
    public partial class daytour : UserControl
    {
        RentDbContext db = new RentDbContext();
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
                                      HireId = Hire.HireId,
                                      Vehicle = Hire.Packages.Vehicle.VehicleNo,
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
                    dgvDayTour.Columns[0].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private void btnAddDayTour_Click(object sender, EventArgs e)
        {
            frmDayHire frmD = new frmDayHire(this.LoadData);
            using (frmD)
            {
                if (frmD.ShowDialog() == DialogResult.OK)
                {

                }
            }
        }
        
        private void btnEditDayTour_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDayTour.RowCount > 0)
                {
                    frmDayHireEdit Edit = new frmDayHireEdit(this.LoadData);
                    using (Edit)
                    {
                        if (dgvDayTour.CurrentRow.Index != -1 || dgvDayTour.CurrentRow.Index > 0)
                        {
                            int indx = dgvDayTour.CurrentRow.Index;
                            Edit.dgvr = dgvDayTour.Rows[indx];
                            if (Edit.ShowDialog() == DialogResult.OK)
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
        private void btnDeleteDayTour_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDayTour.RowCount > 0)
                {
                    
                    if (dgvDayTour.CurrentRow.Index != -1 || dgvDayTour.CurrentRow.Index > 0)
                    {
                        int indx = dgvDayTour.CurrentRow.Index;
                        if (MessageBox.Show("Are You Sure to Delete this Record ?", "Day tour Record", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            db.DayHires.RemoveRange(db.DayHires.Where(x => x.HireId== indx));
                            db.SaveChanges();
                            MessageBox.Show("Day tour record Successfully Deleted");
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
                if (dgvDayTour.RowCount > 0)
                {
                    frmDayHireEdit Edit = new frmDayHireEdit(this.LoadData);
                    using (Edit)
                    {
                        if (dgvDayTour.CurrentRow.Index != -1 || dgvDayTour.CurrentRow.Index > 0)
                        {
                            int indx = dgvDayTour.CurrentRow.Index;
                            Edit.dgvr = dgvDayTour.Rows[indx];
                            if (Edit.ShowDialog() == DialogResult.OK)
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
                if (dgvDayTour.RowCount > 0)
                {

                    if (dgvDayTour.CurrentRow.Index != -1 || dgvDayTour.CurrentRow.Index > 0)
                    {
                        int indx = dgvDayTour.CurrentRow.Index;
                        if (MessageBox.Show("Are You Sure to Delete this Record ?", "Day tour Record", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            db.DayHires.RemoveRange(db.DayHires.Where(x => x.HireId == indx));
                            db.SaveChanges();
                            MessageBox.Show("Day tour record Successfully Deleted");
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
                DayTourHireCalculation cal = new DayTourHireCalculation();
                using (RentDbContext db = new RentDbContext())
                {
                    var DayHire = (from Hire in db.DayHires
                                  select new
                                  {
                                      HireId = Hire.HireId,
                                      Vehicle = Hire.Packages.Vehicle.VehicleNo,
                                      Package = Hire.Packages.PackageName,
                                      StartTime = Hire.StartTime,
                                      EndTime = Hire.EndTime,
                                      StartKm = Hire.StartKm,
                                      EndKm = Hire.EndKm,
                                      PackageCharge = Hire.HireCharge,
                                      WaitingCharge = Hire.WaitingCharge,
                                      ExtraKmCharge = Hire.ExtraKmCharge,
                                      TotalHireCharge = Hire.TotalHireCharge
                                  }).ToList();
                    DataTable DayHires = DayTourHireCalculation.ToDataTable(DayHire);
                    DayTourHireCalculation.ExportDayHires(DayHires);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

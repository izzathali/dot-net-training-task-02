using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABC_Drive.Dashboard
{
    public partial class frmDashboard : Form
    {
        private static bool _running = false;
        public static bool Running
        {
            get
            {
                return _running;
            }
        }
        public frmDashboard()
        {
            InitializeComponent();
            _running = true;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            rent1.Visible = false;
            daytour1.Visible = false;
            longhire1.Visible = false;
        }

        private void btnRent_Click(object sender, EventArgs e)
        {
            daytour1.Visible = false;
            longhire1.Visible = false;
            rent1.Visible = true;
            rent1.BringToFront();
        }

        private void btnDayTour_Click(object sender, EventArgs e)
        {
            rent1.Visible = false;
            longhire1.Visible = false;
            daytour1.Visible = true;
            daytour1.BringToFront();
        }

        private void btnLongTour_Click(object sender, EventArgs e)
        {
            rent1.Visible = false;
            daytour1.Visible = false;
            longhire1.Visible = true;
            longhire1.BringToFront();
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {

        }

        private void frmDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}

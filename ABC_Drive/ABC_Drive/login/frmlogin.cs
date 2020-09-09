using ABC_Drive.Dashboard;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABC_Drive.login
{
    public partial class frmlogin : Form
    {
        public frmlogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string Username = txtUsername.Text;
            string Password = txtPassword.Text;

            if (Username == String.Empty)
            {
                MessageBox.Show("Please type Username");
            }
            else if (Password == String.Empty)
            {
                MessageBox.Show("Please type Password");
            }
            else
            {
                Login log = new Login();
                bool Check = log.CheckLogin(Username, Password);
                if (Check == true)
                {
                    //MessageBox.Show("can Login");
                    frmDashboard D = new frmDashboard();
                    D.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid Username & Password");
                }
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmlogin_Load(object sender, EventArgs e)
        {
           
        }
    }
}

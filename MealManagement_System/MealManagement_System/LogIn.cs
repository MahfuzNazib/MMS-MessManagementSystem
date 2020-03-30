using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MealManagement_System
{
    public partial class LogIn : Form
    {
        public LogIn()
        {
            InitializeComponent();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from Login_Info where Username='"+txtUsername.Text+"' and [Password]='"+txtPassword.Text+"'";
                DataTable dt = DBConnection.GetDataTable(query);
                if(dt.Rows.Count==1)
                {
                    lblError.Visible = false;
                    Home h = new Home();
                    h.Show();
                    this.Hide();
                }
                else
                {
                    lblError.Visible = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register r = new Register();
            r.Show();
            this.Hide();
        }

        private void LogIn_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                txtPassword.Focus();
            }
        }
    }
}

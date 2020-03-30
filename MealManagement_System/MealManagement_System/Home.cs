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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            MemberList ml = new MemberList();
            ml.Show();
        }

        private void MealEntry()
        {
            MealEntry me = new MealEntry();
            me.TopLevel = false;
            me.AutoScroll = true;
            me.FormBorderStyle = FormBorderStyle.None;
            me.Dock = DockStyle.Fill;

            this.pnlDisplay.Controls.Clear();
            this.pnlDisplay.Controls.Add(me);
            me.Show();
        }
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            MealEntry();
        }

        private void Payment()
        {
            Payment me = new Payment();
            me.TopLevel = false;
            me.AutoScroll = true;
            me.FormBorderStyle = FormBorderStyle.None;
            me.Dock = DockStyle.Fill;

            this.pnlDisplay.Controls.Clear();
            this.pnlDisplay.Controls.Add(me);
            me.Show();
        }
        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            Payment();
        }

        private void BazarList()
        {
            BazarList me = new BazarList();
            me.TopLevel = false;
            me.AutoScroll = true;
            me.FormBorderStyle = FormBorderStyle.None;
            me.Dock = DockStyle.Fill;

            this.pnlDisplay.Controls.Clear();
            this.pnlDisplay.Controls.Add(me);
            me.Show();
        }
        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            BazarList();
        }

        private void OthersCost()
        {
            Others_Cost me = new Others_Cost();
            me.TopLevel = false;
            me.AutoScroll = true;
            me.FormBorderStyle = FormBorderStyle.None;
            me.Dock = DockStyle.Fill;

            this.pnlDisplay.Controls.Clear();
            this.pnlDisplay.Controls.Add(me);
            me.Show();
        }
        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            OthersCost();
        }

        private void RateCost()
        {
            Rate_Cost me = new Rate_Cost();
            me.TopLevel = false;
            me.AutoScroll = true;
            me.FormBorderStyle = FormBorderStyle.None;
            me.Dock = DockStyle.Fill;

            this.pnlDisplay.Controls.Clear();
            this.pnlDisplay.Controls.Add(me);
            me.Show();
        }
        private void bunifuFlatButton8_Click(object sender, EventArgs e)
        {
            RateCost();
        }

        private void Home_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==(char)Keys.R)
            {
                RateCost();
            }
            else if(e.KeyChar==(char)Keys.P)
            {
                Payment();
            }
            else if (e.KeyChar == (char)Keys.B)
            {
                BazarList();
            }
            else if (e.KeyChar == (char)Keys.O)
            {
                OthersCost();
            }
            else if (e.KeyChar == (char)Keys.M)
            {
                MealEntry();
            }
            else if(e.KeyChar==(char)Keys.D)
            {
                LoadDashbord();
            }
            else if(e.KeyChar==(char)Keys.L)
            {
                Logout();
            }
        }

        private void Logout()
        {
            if(MessageBox.Show("Are You Sure To Logout?","Confirmation",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                LogIn l = new LogIn();
                this.Hide();
                l.Show();
            }
        }

        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            Logout();
            
        }

        private void LoadDashbord()
        {
            Dashbord me = new Dashbord();
            me.TopLevel = false;
            me.AutoScroll = true;
            me.FormBorderStyle = FormBorderStyle.None;
            me.Dock = DockStyle.Fill;

            this.pnlDisplay.Controls.Clear();
            this.pnlDisplay.Controls.Add(me);
            me.Show();
        }
        private void Home_Load(object sender, EventArgs e)
        {
            LoadDashbord();
        }

        private void btnDashbord_Click(object sender, EventArgs e)
        {
            LoadDashbord();
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            
        }

        private void btnNotices_Click(object sender, EventArgs e)
        {
            Notices me = new Notices();
            me.TopLevel = false;
            me.AutoScroll = true;
            me.FormBorderStyle = FormBorderStyle.None;
            me.Dock = DockStyle.Fill;

            this.pnlDisplay.Controls.Clear();
            this.pnlDisplay.Controls.Add(me);
            me.Show();
        }

        private void bunifuFlatButton5_Click_1(object sender, EventArgs e)
        {
            About me = new About();
            me.TopLevel = false;
            me.AutoScroll = true;
            me.FormBorderStyle = FormBorderStyle.None;
            me.Dock = DockStyle.Fill;

            this.pnlDisplay.Controls.Clear();
            this.pnlDisplay.Controls.Add(me);
            me.Show();
        }

        
    }
}

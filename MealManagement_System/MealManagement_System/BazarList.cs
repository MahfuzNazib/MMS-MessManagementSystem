using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MealManagement_System
{
    public partial class BazarList : Form
    {
        public BazarList()
        {
            InitializeComponent();
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            //BackColor = Color.Red;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "insert into BazarCost([Date],Name,Amount) "
                + "values ('" + dtpDate.Text + "','" + cmboName.Text + "'," + txtAmount.Text + ")";
                DBConnection.ExecuteQuery(query);
                MessageBox.Show("Bazar Cost Added successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadBazarCost();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            TodaysTotal();
            TotalBazar();
            Init();
        }

        private void FillMemberName()
        {
            SqlConnection con = new SqlConnection("Data Source=NAZIBMAHFUZ;Initial Catalog=MealManagement_System;Integrated Security=True");
            string query = "select * from MemberList";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader myreader;
            try
            {

                con.Open();
                myreader = cmd.ExecuteReader();
                while (myreader.Read())
                {
                    string name = myreader.GetString(0);
                    cmboName.Items.Add(name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadBazarCost()
        {
            //string query = "select * from BazarCost ";
            string query = "SELECT * FROM BazarCost ORDER BY SlNo DESC";
            DataTable dt = DBConnection.GetDataTable(query);
            dgvBazarCost.DataSource = dt;
            dgvBazarCost.Refresh();
        }

        public void Init()
        {
            cmboName.Text = txtAmount.Text = "";
        }
        private void TotalBazar()
        {
            try
            {
                string query = "select SUM(Amount) as TB from BazarCost";// where [Date] between '"+dtpStartingDate.Text+"' and '"+dtpDate.Text+"'";
                DataTable dt = DBConnection.GetDataTable(query);

                if (dt.Rows.Count == 1)
                {
                    txtTB.Text = dt.Rows[0]["TB"].ToString();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
   
        }

        private void TodaysTotal()
        {
            try
            {
                string query = "select sum(Amount) as TT from BazarCost where [Date]='"+dtpDate.Text+"'";
                DataTable dt = DBConnection.GetDataTable(query);

                if (dt.Rows.Count == 1)
                {
                    txtTT.Text = dt.Rows[0]["TT"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void BazarList_Load(object sender, EventArgs e)
        {
            FillMemberName();
            LoadBazarCost();
            TotalBazar();
            TodaysTotal();
            cmboName.Focus();
            SetStartingDate();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
            try
            {
                string query = "update BazarCost set "
                    +"Name='"+cmboName.Text+"',Amount="+txtAmount.Text+" where [Date]='"+dtpDate.Text+"' and SlNo="+lblSlNo.Text+"";
                DBConnection.ExecuteQuery(query);
                MessageBox.Show("Bazar Cost successfully Updated", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadBazarCost();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            TodaysTotal();
            TotalBazar();
            Init();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you seur to Delete todays Bazar Cost?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string query = "delete from BazarCost where [Date]='" + dtpDate.Text + "' and SlNo='"+lblSlNo.Text+"'";
                    DBConnection.ExecuteQuery(query);
                    MessageBox.Show("Bazar Cost successfully Deleted", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadBazarCost();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            TodaysTotal();
            TotalBazar();
            Init();
        }

        private void dgvBazarCost_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnUpdate.Visible = true;
            btnDelete.Visible = true;
            btnSubmit.Visible = false;
            //------------------------------

            if(e.RowIndex >= 0)
            {
                string id = dgvBazarCost.Rows[e.RowIndex].Cells[0].Value.ToString();
                try
                {
                    string query = "Select * from BazarCost where [Date]='" + dtpDate.Text + "' and SlNo="+id;
                    DataTable dt = DBConnection.GetDataTable(query);

                    if(dt.Rows.Count==1)
                    {
                        cmboName.Text = dt.Rows[0]["Name"].ToString();
                        txtAmount.Text = dt.Rows[0]["Amount"].ToString();
                        lblSlNo.Text = dt.Rows[0]["SlNo"].ToString();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void dateLock_Click(object sender, EventArgs e)
        {
            if (dateLock.Value == true)
            {
                dtpDate.Enabled = false;
            }
            else
                dtpDate.Enabled = true;
        }

        private void txtSearch_OnTextChange(object sender, EventArgs e)
        {
            if (cmboSearch.SelectedItem == "Name")
            {
                string query = "select * from BazarCost where Name='" + txtSearch.text + "'";
                DataTable dt = DBConnection.GetDataTable(query);
                dgvBazarCost.DataSource = dt;
                dgvBazarCost.Refresh();
            }
            else if (cmboSearch.SelectedItem == "Date")
            {
                string query = "select * from BazarCost where [Date]='" + txtSearch.text + "'";
                DataTable dt = DBConnection.GetDataTable(query);
                dgvBazarCost.DataSource = dt;
                dgvBazarCost.Refresh();
            }
            else
            {
                MessageBox.Show("Please Select a Option First", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadBazarCost();
        }

        private void cmboName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                txtAmount.Focus();
            }
        }

        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                btnSubmit.PerformClick();
            }
        }

        private void cmboName_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnUpdate.Visible = btnDelete.Visible = false;
            btnSubmit.Visible = true;
        }

        private void SetStartingDate()
        {
            
            dtpStartingDate.Text =  dtpDate.Value.Month.ToString() + "-" + dtpDate.Value.Year.ToString()+"-"+"01";
           
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            SetStartingDate();
            DateTime dt;
            dt = Convert.ToDateTime(dtpDate.Text);
        }

        private void txtTT_TextChanged(object sender, EventArgs e)
        {

        }

        private void BazarCost()
        {
            try
            {
                string query = "select *  from BazarCost";
                DataTable dt = DBConnection.GetDataTable(query);
                dgvBazarCost.DataSource = dt;
                dgvBazarCost.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Backup()
        {
            if (dgvBazarCost.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Application xcelApp = new Microsoft.Office.Interop.Excel.Application();
                xcelApp.Application.Workbooks.Add(Type.Missing);
                for (int i = 1; i < dgvBazarCost.Columns.Count + 1; i++)
                {
                    xcelApp.Cells[1, i] = dgvBazarCost.Columns[i - 1].HeaderText;
                }
                for (int i = 0; i < dgvBazarCost.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvBazarCost.Columns.Count; j++)
                    {
                        xcelApp.Cells[i + 2, j + 1] = dgvBazarCost.Rows[i].Cells[j].Value.ToString();
                        //xcelApp.Cells[i + 2, j + 1] = bkMealList.Rows[i].Cells[j].Value.ToString();
                    }
                }
                xcelApp.Columns.AutoFit();
                xcelApp.Visible = true;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            BazarCost();
            Backup();
        }
    }
}

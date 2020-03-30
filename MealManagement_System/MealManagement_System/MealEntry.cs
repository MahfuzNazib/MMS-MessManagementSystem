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
using iTextSharp.text; //for ItextSharp
using iTextSharp.text.pdf; //for ItextSharp ->> PDF
using System.IO; //for Input and Output

namespace MealManagement_System
{
    public partial class MealEntry : Form
    {
        public MealEntry()
        {
            InitializeComponent();
        }

        private void LoadMealList()
        {
            //string query = "select * from MealList";
            string query = "SELECT * FROM MealList ORDER BY SlNo DESC";
            DataTable dt = DBConnection.GetDataTable(query);
            dgvMealList.DataSource = dt;
            dgvMealList.Refresh();
        }

        private void Init()
        {
            cmboName.Text = txtLunch.Text = txtDinner.Text = txtTotal.Text = "";
        }

        private void ButtonControl()
        {
            btnSubmit.Visible = false;
            btnUpdate.Visible = true;
            btnDelete.Visible = true;
        }
        private void ButtonControl2()
        {
            btnSubmit.Visible = true;
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
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

        private void TotalLunchByDay()
        {
            string query = "select count(Lunch) as TL from MealList where [Date]='"+dtpEntryDate.Text+"'";
            DataTable dt = DBConnection.GetDataTable(query);
            
            if(dt.Rows.Count==1)
            {
                txtTL.Text = dt.Rows[0]["TL"].ToString();
            }
        }

        private void CountDailyTotalLunch_Dinner()
        {
            try
            {
                string query = "select SUM(Lunch) as Lunch,SUM(Dinner) as Dinner from MealList where [Date]='"+dtpEntryDate.Text+"'";
                DataTable dt = DBConnection.GetDataTable(query);
                if(dt.Rows.Count!=1)
                {
                    txtTL.Text = txtTD.Text = "0";
                }
                else
                {
                    txtTL.Text = dt.Rows[0]["Lunch"].ToString();
                    txtTD.Text = dt.Rows[0]["Dinner"].ToString();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ConvertToDate()
        {
            DateTime dt;
            dt = Convert.ToDateTime(dtpEntryDate.Text);
        }

        private void setStartingDate()
        {
            dtpStartingDate.Text = dtpEntryDate.Value.Year.ToString() + "-" + dtpEntryDate.Value.Month.ToString() + "-" + "01";
        }
        private void MealEntry_Load(object sender, EventArgs e)
        {
            LoadMealList();
            FillMemberName();
            DateLock();
            cmboName.Focus();
            CountDailyTotalLunch_Dinner();
            setStartingDate();
        }

        private void dgvMealList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ButtonControl();
            if(e.RowIndex>=0)
            {
                string id = dgvMealList.Rows[e.RowIndex].Cells[0].Value.ToString();

                try
                {
                    string query = "select * from MealList where SlNo="+id+" and [Date]='"+dtpEntryDate.Text+"'";
                    DataTable dt = DBConnection.GetDataTable(query);

                    if (dt.Rows.Count == 1)
                    {
                        lblSlNo.Text = dt.Rows[0]["SlNo"].ToString();
                        cmboName.Text = dt.Rows[0]["Name"].ToString();
                        txtLunch.Text = dt.Rows[0]["Lunch"].ToString();
                        txtDinner.Text = dt.Rows[0]["Dinner"].ToString();
                        txtTotal.Text = dt.Rows[0]["Total"].ToString();
                    }
                    else
                        MessageBox.Show("Member Meal Info doesn't exits", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void MealCountPerDay()
        {
            if (txtLunch.Text != "" && txtDinner.Text != "")
            {
                try
                {
                    double lunch, dinner, total;
                    lunch = Convert.ToDouble(txtLunch.Text);
                    dinner = Convert.ToDouble(txtDinner.Text);
                    total = lunch + dinner;
                    txtTotal.Text = total.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void txtDinner_TextChanged(object sender, EventArgs e)
        {
            MealCountPerDay();
            
        }

        private void Submit()
        {
            try
            {
                string query = "insert into MealList([Date],Name,Lunch,Dinner,Total)"
                + " values ('" + dtpEntryDate.Text + "','" + cmboName.Text + "'," + txtLunch.Text + "," + txtDinner.Text + "," + txtTotal.Text + ")";
                DBConnection.ExecuteQuery(query);
                MessageBox.Show(cmboName.Text + " " + txtTotal.Text + " Meal added Done", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadMealList();
                Init();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            
        }

        private void txtLunch_TextChanged(object sender, EventArgs e)
        {
            MealCountPerDay();
            
        }

        private void cmboName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ButtonControl2();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string query = "update MealList set Lunch="+txtLunch.Text+",Dinner="+txtDinner.Text+",Total="+txtTotal.Text+" "
                +"where [Date]='"+dtpEntryDate.Text+"' and SlNo="+lblSlNo.Text+"";
            DBConnection.ExecuteQuery(query);
            MessageBox.Show(cmboName.Text + " Successfully Updated", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadMealList();
            CountDailyTotalLunch_Dinner();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure to delete this meal Data?","Confirmation",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                string query = "delete from MealList where Name=" + lblSlNo.Text + " and [Date]='" + dtpEntryDate.Text + "'";
                DBConnection.ExecuteQuery(query);
                MessageBox.Show("Successfully Deleted", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadMealList();
                CountDailyTotalLunch_Dinner();
            }
        }

        private void bunifuTextbox1_OnTextChange(object sender, EventArgs e)
        {
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (cmboSearch.SelectedItem == "Name")
            {
                string query = "Select * from MealList where Name='" + txtSearch.text + "'";
                DataTable dt = DBConnection.GetDataTable(query);
                dgvMealList.DataSource = dt;
                dgvMealList.Refresh();
            }
            else if (cmboSearch.SelectedItem == "Date")
            {
                string query = "Select * from MealList where [Date]='" + txtSearch.text + "'";
                DataTable dt = DBConnection.GetDataTable(query);
                dgvMealList.DataSource = dt;
                dgvMealList.Refresh();
            }
            else
                MessageBox.Show("Please Choose option first", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void swtDateLock_OnValueChange(object sender, EventArgs e)
        {
            
        }
        private void DateLock()
        {
            if (swtDateLock1.Value == true)
            {
                dtpEntryDate.Enabled = false;
            }
            else
                dtpEntryDate.Enabled = true;
        }

        

        private void swtDateLock1_OnValueChange(object sender, EventArgs e)
        {
            DateLock();
        }

        private void cmboName_MouseDown(object sender, MouseEventArgs e)
        {
           
        }

        private void cmboName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                txtLunch.Focus();
            }
        }

        private void txtDinner_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSubmit.PerformClick();
            }
        }

        private void txtLunch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDinner.Focus();
            }
        }

        private void btnSubmit_Click_1(object sender, EventArgs e)
        {
            Submit();
            CountDailyTotalLunch_Dinner();
            cmboName.Focus();

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            LoadMealList();
        }

        private void dtpEntryDate_ValueChanged(object sender, EventArgs e)
        {
            ConvertToDate();
            setStartingDate();
        }

        public void exportPdf(DataGridView dgv, string filename)
        {
            BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.EMBEDDED);
            PdfPTable pdftable = new PdfPTable(dgv.Columns.Count);
            pdftable.DefaultCell.Padding = 3;
            pdftable.WidthPercentage = 100;
            pdftable.HorizontalAlignment = Element.ALIGN_CENTER;
            pdftable.DefaultCell.BorderWidth = 1;

            iTextSharp.text.Font text = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);
            //adding header

            foreach (DataGridViewColumn column in dgv.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, text));
                cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                pdftable.AddCell(cell);
            }

            //add data row

            foreach (DataGridViewRow row in dgv.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    pdftable.AddCell(new Phrase(cell.Value.ToString(), text));
                }
            }

            var savefiledialoge = new SaveFileDialog();
            savefiledialoge.FileName = filename;
            savefiledialoge.DefaultExt = ".pdf";
            if (savefiledialoge.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(savefiledialoge.FileName, FileMode.Create))
                {
                    Document pdfdoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    PdfWriter.GetInstance(pdfdoc, stream);
                    pdfdoc.Open();
                    pdfdoc.Add(pdftable);
                    pdfdoc.Close();
                    stream.Close();
                }
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            exportPdf(dgvMealList, "Monthly Meal Report");
            
        }

        private void MealList()
        {
            try
            {
                string query = "select *  from MealList";
                DataTable dt = DBConnection.GetDataTable(query);
                dgvMealList.DataSource = dt;
                dgvMealList.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Backup()
        {
            if (dgvMealList.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Application xcelApp = new Microsoft.Office.Interop.Excel.Application();
                xcelApp.Application.Workbooks.Add(Type.Missing);
                for (int i = 1; i < dgvMealList.Columns.Count + 1; i++)
                {
                    xcelApp.Cells[1, i] = dgvMealList.Columns[i - 1].HeaderText;
                }
                for (int i = 0; i < dgvMealList.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvMealList.Columns.Count; j++)
                    {
                        xcelApp.Cells[i + 2, j + 1] = dgvMealList.Rows[i].Cells[j].Value.ToString();
                        //xcelApp.Cells[i + 2, j + 1] = bkMealList.Rows[i].Cells[j].Value.ToString();
                    }
                }
                xcelApp.Columns.AutoFit();
                xcelApp.Visible = true;
            }
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MealList();
            Backup();
        }

    }
}

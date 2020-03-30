using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text; //for ItextSharp
using iTextSharp.text.pdf; //for ItextSharp ->> PDF
using System.IO; //for Input and Output

namespace MealManagement_System
{
    public partial class MemberList : Form
    {
        public MemberList()
        {
            InitializeComponent();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            btnConfirm.Visible = true;
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
            btnAdd.Visible = false;
            Init();
        }

        private void ButtonControl()
        {
            btnConfirm.Visible = false;
            btnUpdate.Visible = true;
            btnDelete.Visible = true;
            btnAdd.Visible = true;
        }

        private void Init()
        {
            txtId.Text = txtName.Text = txtInstitution.Text = txtMail.Text = txtPhone.Text = "";
        }

        private void InsertTblRateCost()
        {
            try
            {
                string query = "insert into Rate_Cost(Name,Total_Meal,Cost,Adding_Tk,Balance,[Message],[Status])"
                     + " values ('" + txtName.Text + "',0,0,0,0,'null','null')";
                DBConnection.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void InsertMemberListInfo()
        {
            try
            {
                if (txtName.Text == "" && txtPhone.Text == "")
                {
                    MessageBox.Show("FillUp all the required Field", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                InsertTblRateCost();
                string query = "Insert into MemberList(Member_Name,Institution,ContactNo,Email) "
                + "values('" + txtName.Text + "','" + txtInstitution.Text + "','" + txtPhone.Text + "','" + txtMail.Text + "')";
                DBConnection.ExecuteQuery(query);

                MessageBox.Show("Member : " + txtName.Text + " added Successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Text = txtInstitution.Text = txtPhone.Text = txtMail.Text = "";
                

                LoadAllMember();
                TotalMember();
                
                Init();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
           
            
        }

        public void TotalMember()
        {
            string query = "select COUNT(MemberId) as TotalMember from MemberList";
            DataTable dt=DBConnection.GetDataTable(query);

            txtTM.Text = dt.Rows[0]["TotalMember"].ToString();
           
        }

        private void MemberList_Load(object sender, EventArgs e)
        {
            
            LoadAllMember();
            TotalMember();
            ButtonControl();
        }

        private void LoadAllMember()
        {
            try
            {
                string query = "Select * from MemberList";
                DataTable dt = DBConnection.GetDataTable(query);
                dgvMemberList.DataSource = dt;
                dgvMemberList.Refresh();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void dgvMemberList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex>=0)
            {
                string id = dgvMemberList.Rows[e.RowIndex].Cells[4].Value.ToString();
                try
                {
                    string query = "select * from MemberList where MemberId=" + id;
                    DataTable dt = DBConnection.GetDataTable(query);

                    if (dt.Rows.Count == 1)
                    {
                        txtId.Text = dt.Rows[0]["MemberId"].ToString();
                        txtName.Text = dt.Rows[0]["Member_Name"].ToString();
                        txtInstitution.Text = dt.Rows[0]["Institution"].ToString();
                        txtPhone.Text = dt.Rows[0]["ContactNo"].ToString();
                        txtMail.Text = dt.Rows[0]["Email"].ToString();
                    }

                    else
                        MessageBox.Show("Invalid Member", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            ButtonControl();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "update MemberList set Member_Name='"+txtName.Text+"',Institution='"+txtInstitution.Text+"',"
                    +"ContactNo='"+txtPhone.Text+"',Email='"+txtMail.Text+"' where MemberId="+txtId.Text+"";
                DBConnection.ExecuteQuery(query);
                MessageBox.Show("Update Successfully Done :)", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAllMember();
                TotalMember();
                Init();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deleteFromRateCost()
        {
            try
            {
                string query = "delete from Rate_Cost where Name='"+txtName.Text+"'";
                DBConnection.ExecuteQuery(query);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                
                if(MessageBox.Show("Are you sure to delete this Member ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    string query = "delete from MemberList where MemberId=" + txtId.Text + "";
                    DBConnection.ExecuteQuery(query);
                    deleteFromRateCost();
                    MessageBox.Show("Successfullay Deleted", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAllMember();
                    TotalMember();
                    Init();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           try
           {
               if(cmboSearch.Text=="ID")
               {
                   string query = "select * from MemberList where MemberId=" + txtSearch.text + "";
                   DataTable dt = DBConnection.GetDataTable(query);
                   if (dt.Rows.Count == 1)
                   {
                       dgvMemberList.DataSource = dt;
                       dgvMemberList.Refresh();
                   }
                   else
                   {
                       MessageBox.Show("Member ID doesn't exists", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   }
               }

               else if(cmboSearch.Text=="Name")
               {
                   string query = "select * from MemberList where Name='"+txtSearch.text+"'";
                   DataTable dt = DBConnection.GetDataTable(query);
                   if (dt.Rows.Count == 1)
                   {
                       dgvMemberList.DataSource = dt;
                       dgvMemberList.Refresh();
                   }
                   else
                   {
                       MessageBox.Show("Member Name doesn't exists", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   }
               }

               else if (cmboSearch.Text == "Institution")
               {
                   string query = "select * from MemberList where Institution='"+txtSearch.text+"'";
                   DataTable dt = DBConnection.GetDataTable(query);
                   
                   dgvMemberList.DataSource = dt;
                   dgvMemberList.Refresh();
                   
                   if(dt.Rows.Count==0)
                   {
                       MessageBox.Show("Institution Name doesn't exists", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   }
               }
               else if(cmboSearch.Text=="Phone")
               {
                   string query = "select * from MemberList where ContactNo='"+txtSearch.text+"'";
                   DataTable dt = DBConnection.GetDataTable(query);
                   if (dt.Rows.Count == 1)
                   {
                       dgvMemberList.DataSource = dt;
                       dgvMemberList.Refresh();
                   }
                   else
                   {
                       MessageBox.Show("Contact No. doesn't exists", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   }
               }

           }
            catch(Exception ex)
           {
               MessageBox.Show(ex.Message);
           }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            LoadAllMember();
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

        private void bunifuThinButton21_Click_1(object sender, EventArgs e)
        {
            exportPdf(dgvMemberList, "All Member List");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InsertMemberListInfo();
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if(dgvMemberList.Rows.Count>0)
            {
                Microsoft.Office.Interop.Excel.Application xcelApp = new Microsoft.Office.Interop.Excel.Application();
                xcelApp.Application.Workbooks.Add(Type.Missing);
                for(int i=1;i<dgvMemberList.Columns.Count+1;i++)
                {
                    xcelApp.Cells[1, i] = dgvMemberList.Columns[i - 1].HeaderText;
                }
                for(int i=0;i<dgvMemberList.Rows.Count;i++)
                {
                    for(int j=0;j<dgvMemberList.Columns.Count;j++)
                    {
                        xcelApp.Cells[i + 2, j + 1] = dgvMemberList.Rows[i].Cells[j].Value.ToString();
                        //xcelApp.Cells[i + 2, j + 1] = bkMealList.Rows[i].Cells[j].Value.ToString();
                    }
                }
                xcelApp.Columns.AutoFit();
                xcelApp.Visible = true;
            }
        }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop;


namespace MealManagement_System
{
    public partial class Notices : Form
    {
        public Notices()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.PowerPoint.Application pptApp = new Microsoft.Office.Interop.PowerPoint.Application();
            Microsoft.Office.Core.MsoTriState ofalse = Microsoft.Office.Core.MsoTriState.msoFalse;
            Microsoft.Office.Core.MsoTriState otrue = Microsoft.Office.Core.MsoTriState.msoTrue;
            pptApp.Visible = otrue;
            pptApp.Activate();
            Microsoft.Office.Interop.PowerPoint.Presentations ps = pptApp.Presentations;
            Microsoft.Office.Interop.PowerPoint.Presentation p = ps.Open(@"F:\Mess Managment\PersonalCostV.1.1.00V.pptx", ofalse, ofalse, otrue);
            System.Diagnostics.Debug.Print(p.Windows.Count.ToString());
            MessageBox.Show(pptApp.ActiveWindow.Caption);
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.PowerPoint.Application pptApp = new Microsoft.Office.Interop.PowerPoint.Application();
            Microsoft.Office.Core.MsoTriState ofalse = Microsoft.Office.Core.MsoTriState.msoFalse;
            Microsoft.Office.Core.MsoTriState otrue = Microsoft.Office.Core.MsoTriState.msoTrue;
            pptApp.Visible = otrue;
            pptApp.Activate();
            Microsoft.Office.Interop.PowerPoint.Presentations ps = pptApp.Presentations;
            Microsoft.Office.Interop.PowerPoint.Presentation p = ps.Open(@"F:\Mess Managment\Mess Notice\PPTFormatNotice\Notice.pptx", ofalse, ofalse, otrue);
            System.Diagnostics.Debug.Print(p.Windows.Count.ToString());
            //MessageBox.Show(pptApp.ActiveWindow.Caption);
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.PowerPoint.Application pptApp = new Microsoft.Office.Interop.PowerPoint.Application();
            Microsoft.Office.Core.MsoTriState ofalse = Microsoft.Office.Core.MsoTriState.msoFalse;
            Microsoft.Office.Core.MsoTriState otrue = Microsoft.Office.Core.MsoTriState.msoTrue;
            pptApp.Visible = otrue;
            pptApp.Activate();
            Microsoft.Office.Interop.PowerPoint.Presentations ps = pptApp.Presentations;
            Microsoft.Office.Interop.PowerPoint.Presentation p = ps.Open(@"F:\Mess Managment\Mess Notice\PPTFormatNotice\Notice_Upload_Content_V.1.0.pptx", ofalse, ofalse, otrue);
            System.Diagnostics.Debug.Print(p.Windows.Count.ToString());
        }

        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.PowerPoint.Application pptApp = new Microsoft.Office.Interop.PowerPoint.Application();
            Microsoft.Office.Core.MsoTriState ofalse = Microsoft.Office.Core.MsoTriState.msoFalse;
            Microsoft.Office.Core.MsoTriState otrue = Microsoft.Office.Core.MsoTriState.msoTrue;
            pptApp.Visible = otrue;
            pptApp.Activate();
            Microsoft.Office.Interop.PowerPoint.Presentations ps = pptApp.Presentations;
            Microsoft.Office.Interop.PowerPoint.Presentation p = ps.Open(@"F:\Mess Managment\Mess Notice\PPTFormatNotice\PersonalCostV.1.1.00V.pptx", ofalse, ofalse, otrue);
            System.Diagnostics.Debug.Print(p.Windows.Count.ToString());
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.PowerPoint.Application pptApp = new Microsoft.Office.Interop.PowerPoint.Application();
            Microsoft.Office.Core.MsoTriState ofalse = Microsoft.Office.Core.MsoTriState.msoFalse;
            Microsoft.Office.Core.MsoTriState otrue = Microsoft.Office.Core.MsoTriState.msoTrue;
            pptApp.Visible = otrue;
            pptApp.Activate();
            Microsoft.Office.Interop.PowerPoint.Presentations ps = pptApp.Presentations;
            Microsoft.Office.Interop.PowerPoint.Presentation p = ps.Open(@"F:\Mess Managment\Mess Notice\PPTFormatNotice\PenaltyMeal.pptx", ofalse, ofalse, otrue);
            System.Diagnostics.Debug.Print(p.Windows.Count.ToString());
        }

        private void MonthClosed()
        {
            Microsoft.Office.Interop.PowerPoint.Application pptApp = new Microsoft.Office.Interop.PowerPoint.Application();
            Microsoft.Office.Core.MsoTriState ofalse = Microsoft.Office.Core.MsoTriState.msoFalse;
            Microsoft.Office.Core.MsoTriState otrue = Microsoft.Office.Core.MsoTriState.msoTrue;
            pptApp.Visible = otrue;
            pptApp.Activate();
            Microsoft.Office.Interop.PowerPoint.Presentations ps = pptApp.Presentations;
            Microsoft.Office.Interop.PowerPoint.Presentation p = ps.Open(@"F:\Mess Managment\Mess Notice\PPTFormatNotice\MonthClosed.pptx", ofalse, ofalse, otrue);
            System.Diagnostics.Debug.Print(p.Windows.Count.ToString());
        }
        private void btnMonthClosed_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("ARE YOU SURE TO CLOSED THIS MONTH?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)==DialogResult.Yes)
            {
                if (MessageBox.Show("This will erase Total Months Data", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        string query = "delete from MealList";
                        string queryB = "delete from BazarCost";
                        string queryP = "delete from Payment";
                        DBConnection.ExecuteQuery(query);
                        DBConnection.ExecuteQuery(queryB);
                        DBConnection.ExecuteQuery(queryP);
                        MonthClosed();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}

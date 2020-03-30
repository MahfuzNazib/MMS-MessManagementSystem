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
    public partial class Dashbord : Form
    {
        public Dashbord()
        {
            InitializeComponent();
        }

        private void LoadPaymentChart()
        {
           string query = "select Name,SUM(Add_Amount)  from Payment Group by Name";
           SqlDataReader dr;
           dr = DBConnection.getReader(query);
           try
           {
                while(dr.Read())
                {
                    this.chrtPayment.Series["Series1"].Points.AddXY(dr.GetString(0), dr.GetDecimal(1));
                }
           } 
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadMealChart()
        {
            string query = "select Name,SUM(Total) from MealList group by Name";
            SqlDataReader dr;
            dr = DBConnection.getReader(query);
            try
            {
                while (dr.Read())
                {
                    this.chrtMealCount.Series["Series1"].Points.AddXY(dr.GetString(0), dr.GetDouble(1));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void LoadBazarChrt()
        {
            string query = "select Name, SUM(Amount) from BazarCost group by Name";
            SqlDataReader dr;
            dr = DBConnection.getReader(query);
            try
            {
                while (dr.Read())
                {
                    this.chrtBazar.Series["Series1"].Points.AddXY(dr.GetString(0), dr.GetDecimal(1));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadRatechrt()
        {
            string query = "select Name,SUM(HousingFee+GasWater+InternetBill+CurrentBill+BowaBill+OthersCost) from MonthlyFee group by Name";
            SqlDataReader dr;
            dr = DBConnection.getReader(query);
            try
            {
                while (dr.Read())
                {
                    this.chrtBazar.Series["Series1"].Points.AddXY(dr.GetString(0), dr.GetDecimal(1));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Dashbord_Load(object sender, EventArgs e)
        {
            LoadPaymentChart();
            LoadMealChart();
            LoadBazarChrt();
            LoadRatechrt();
        }
    }
}

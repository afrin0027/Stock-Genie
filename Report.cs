using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_Management_System
{
    public partial class Report : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HP\Desktop\Inventory_Management_System\Inventory_Management_System\Database1.mdf;Integrated Security=True");
        public Report()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from Product", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            SqlDataAdapter da2 = new SqlDataAdapter("Select * from Product WHERE Quantity<5", con);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            dataGridView1.DataSource = dt2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da3 = new SqlDataAdapter("Select * from StockTransaction", con);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);
            dataGridView1.DataSource = dt3;
        }
    }

}

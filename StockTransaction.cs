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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Inventory_Management_System
{
    public partial class StockTransaction : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HP\Desktop\Inventory_Management_System\Inventory_Management_System\Database1.mdf;Integrated Security = True");



        public StockTransaction()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ProductName = comboBox1.Text;
            int AvailableQuantity = int.Parse(textBox1.Text);
            int TransactionQuantity = int.Parse(textBox2.Text);
            string TransactionType = comboBox2.Text;
            DateTime DateofTransaction = dateTimePicker1.Value;

            int newStock = AvailableQuantity;

            if (TransactionType == "IN")
            {
                newStock = AvailableQuantity + TransactionQuantity;
            }
            else if (TransactionType == "OUT")
            {
                if (AvailableQuantity >= TransactionQuantity)
                {
                    newStock = AvailableQuantity - TransactionQuantity;
                }
                else
                {
                    MessageBox.Show("Not enough stock available!");
                    return;
                }
            }

            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE Product SET Quantity = @qnty WHERE ProductName = @pname", con);
            cmd.Parameters.AddWithValue("@qnty", newStock);
            cmd.Parameters.AddWithValue("@pname", ProductName);
            cmd.ExecuteNonQuery();
            con.Close();

            con.Open();
            SqlCommand cmd1 = new SqlCommand(
                "INSERT INTO StockTransaction(ProductName, AvailableQuantity, TransactionQuantity, TransactionType, DateofTransaction) " +
                "VALUES(@pname, @currQty, @transQty, @type, @date)", con);

            cmd1.Parameters.AddWithValue("@pname", ProductName);
            cmd1.Parameters.AddWithValue("@currQty", newStock);
            cmd1.Parameters.AddWithValue("@transQty", TransactionQuantity);
            cmd1.Parameters.AddWithValue("@type", TransactionType);
            cmd1.Parameters.AddWithValue("@date", DateofTransaction.ToString("yyyy-MM-dd")); 

            cmd1.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Transaction Saved Successfully!");

            display();
            cleartext();
        }

        private void display()
        {
            con.Open();
            SqlCommand CMD = con.CreateCommand();
            CMD.CommandType = CommandType.Text;
            CMD.CommandText = "SELECT * FROM StockTransaction";
            SqlDataAdapter da = new SqlDataAdapter(CMD);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cleartext();
        }
        private void cleartext()
        {
            comboBox1.Text = "";
            textBox2.Text = "";
            textBox1.Text = "";
            comboBox2.Text = "";
            dateTimePicker1.Text = "";
            comboBox1.Focus();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            display();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                comboBox1.Text = row.Cells[0].Value.ToString();
                textBox1.Text = row.Cells[1].Value.ToString();
                textBox2.Text = row.Cells[2].Value.ToString();
                comboBox2.Text = row.Cells[3].Value.ToString();
                dateTimePicker1.Text = row.Cells[4].Value.ToString();

            }
            comboBox1.Enabled = false;
        }

        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand CMD = con.CreateCommand();
            CMD.CommandType = CommandType.Text;
            CMD.CommandText = "Select Quantity from Product where ProductName='" + comboBox1.SelectedItem.ToString() + "'";
            SqlDataReader reader = CMD.ExecuteReader();

            while (reader.Read())
            {
                textBox1.Text = reader["Quantity"].ToString();
            }

            con.Close();
        }

        private void StockTransaction_Load(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand CMD = new SqlCommand("SELECT ProductName FROM Product", con);
            SqlDataReader dr = CMD.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["ProductName"].ToString());
            }
            con.Close();

            display();
        }
    }
}


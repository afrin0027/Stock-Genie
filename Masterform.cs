using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_Management_System
{
    public partial class Masterform : Form
    {
        public Masterform()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Products products = new Products();
            products.MdiParent = this;
            products.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Suppliers suppliers = new Suppliers();
            suppliers.MdiParent = this;
            suppliers.Show();
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            StockTransaction st = new StockTransaction();
            st.MdiParent = this;
            st.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Report st = new Report();
            st.MdiParent = this;
            st.Show();
        }
    }
}

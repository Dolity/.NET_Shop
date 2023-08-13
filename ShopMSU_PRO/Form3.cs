using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopMSU_PRO
{
    public partial class Form3 : Form
    {
        APD65_63011212004Entities2 context = new APD65_63011212004Entities2();
        public Form3()
        {
            InitializeComponent();
           



        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'aPD65_63011212004DataSet6.ShopOrderItem' table. You can move, or remove it, as needed.
            this.shopOrderItemTableAdapter1.Fill(this.aPD65_63011212004DataSet6.ShopOrderItem);
            // TODO: This line of code loads data into the 'aPD65_63011212004DataSet5.ShopOrder' table. You can move, or remove it, as needed.
            this.shopOrderTableAdapter1.Fill(this.aPD65_63011212004DataSet5.ShopOrder);
            //// TODO: This line of code loads data into the 'aPD65_63011212004DataSet4.ShopOrderItem' table. You can move, or remove it, as needed.
            //this.shopOrderItemTableAdapter.Fill(this.aPD65_63011212004DataSet4.ShopOrderItem);
            //// TODO: This line of code loads data into the 'aPD65_63011212004DataSet31.ShopOrder' table. You can move, or remove it, as needed.
            //this.shopOrderTableAdapter.Fill(this.aPD65_63011212004DataSet31.ShopOrder);
            //// TODO: This line of code loads data into the 'aPD65_63011212004DataSet1.ShopEmployee' table. You can move, or remove it, as needed.
            //this.shopEmployeeTableAdapter.Fill(this.aPD65_63011212004DataSet1.ShopEmployee);
            //// TODO: This line of code loads data into the 'aPD65_63011212004DataSet.ShopProduct' table. You can move, or remove it, as needed.
            //this.shopProductTableAdapter.Fill(this.aPD65_63011212004DataSet.ShopProduct);

            ShopProduct product = new ShopProduct();

            shopProductBindingSource.DataSource = context.ShopProducts.ToList();
            shopEmployeeBindingSource.DataSource = context.ShopEmployees.ToList();
            shopCustomerBindingSource.DataSource = context.ShopCustomers.ToList();
            shopOrderBindingSource.DataSource = context.ShopOrders.ToList();
            shopOrderItemBindingSource.DataSource = context.ShopOrders.ToList();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private Image LoadImage(string url)
        {
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            myHttpWebRequest.UserAgent = "Chrome/105.0.0.0";
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            Stream stramResponse = myHttpWebResponse.GetResponseStream();
            Bitmap bmp = new Bitmap(stramResponse);
            stramResponse.Dispose();
            return bmp;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            string id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            var result = context.ShopProducts.Where(i => i.product_Serial.ToString() == id).First();
            pictureBox1.Image = LoadImage(result.product_IMG);

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            var Form2 = new Form2();

            Form2.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            var Form2 = new Form2();

            Form2.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            var Form2 = new Form2();

            Form2.Show();
            this.Hide();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            string keepfind = textBox22.Text;

            var findDB = context.ShopProducts.Where(t => t.product_Serial.ToString().Contains(keepfind)
                || t.product_Name.Contains(keepfind)
                || t.product_Detail.Contains(keepfind)
                || t.product_Type.Contains(keepfind))
                .ToList();

            if (findDB.Count > 0)
            {
                MessageBox.Show("find!!");
                dataGridView1.DataSource = findDB;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShopEmployee employee = new ShopEmployee();

            employee.employee_Fname = textBox2.Text;
            employee.employee_Lname = textBox6.Text;
            employee.employee_Address = textBox8.Text;
            employee.employee_TEL = int.Parse(textBox9.Text);
            employee.employee_Username = textBox10.Text;
            employee.employee_Password = textBox11.Text;
            employee.employee_Status = int.Parse(textBox12.Text);

            context.ShopEmployees.Add(employee);
            int change = context.SaveChanges();
            MessageBox.Show("Change: " + change + " records");

            shopEmployeeBindingSource.DataSource = context.ShopEmployees.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            shopEmployeeBindingSource.EndEdit();
            int change = context.SaveChanges();
            MessageBox.Show("Change: " + change + " records");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            shopEmployeeBindingSource.AddNew();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox1.Text);

            var result = context.ShopEmployees.Where(i => i.employee_ID == id).First();
            context.ShopEmployees.Remove(result);
            int change = context.SaveChanges();
            MessageBox.Show("Change: " + change + " records");

            shopEmployeeBindingSource.DataSource = context.ShopEmployees.ToList();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ShopCustomer customer = new ShopCustomer();

            customer.customer_Fname = textBox18.Text;
            customer.customer_Lname = textBox17.Text;
            customer.customer_Address = textBox16.Text;
            customer.customer_TEL = int.Parse(textBox15.Text);

            context.ShopCustomers.Add(customer);
            int change = context.SaveChanges();
            MessageBox.Show("Change: " + change + " records");

            shopCustomerBindingSource.DataSource = context.ShopCustomers.ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            shopCustomerBindingSource.EndEdit();
            int change = context.SaveChanges();
            MessageBox.Show("Change: " + change + " records");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox19.Text);

            var result = context.ShopCustomers.Where(i => i.customer_ID == id).First();
            context.ShopCustomers.Remove(result);
            int change = context.SaveChanges();
            MessageBox.Show("Change: " + change + " records");

            shopCustomerBindingSource.DataSource = context.ShopCustomers.ToList();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            shopCustomerBindingSource.AddNew();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            string keepfind = textBox20.Text;

            var findDB = context.ShopCustomers.Where(t => t.customer_ID.ToString().Contains(keepfind)
                || t.customer_Fname.Contains(keepfind)
                || t.customer_Lname.Contains(keepfind)
                || t.customer_Address.Contains(keepfind)
                || t.customer_TEL.ToString().Contains(keepfind))
                .ToList();

            if (findDB.Count > 0)
            {
                MessageBox.Show("find!");
                dataGridView3.DataSource = findDB;

            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            string keepfind = textBox21.Text;

            var findDB = context.ShopEmployees.Where(t => t.employee_ID.ToString().Contains(keepfind)
                || t.employee_Fname.Contains(keepfind)
                || t.employee_Lname.Contains(keepfind)
                || t.employee_Address.Contains(keepfind)
                || t.employee_TEL.ToString().Contains(keepfind))
                .ToList();

            if (findDB.Count > 0)
            {
                MessageBox.Show("find!");
                dataGridView2.DataSource = findDB;

            }
        }

        private void textBox19_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            string keepfind = comboBox2.Text;

            Console.WriteLine("ResultFIND!= "+keepfind);

            var findDB = context.ShopProducts.Where(t => t.product_Serial.ToString().Contains(keepfind)
                || t.product_Name.Contains(keepfind)
                || t.product_Detail.Contains(keepfind)
                || t.product_Type.Contains(keepfind)
                || t.product_Serial.ToString().Contains(keepfind)
                || t.product_Price.ToString().Contains(keepfind))
                .ToList();

            if (findDB.Count > 0)
            {
                MessageBox.Show("find!!");
                dataGridView1.DataSource = findDB;

            }
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            string keepfind = textBox13.Text;

            var findDB = context.ShopOrders.Where(t => t.order_ID.ToString().Contains(keepfind)
                || t.customer_Serial.ToString().Contains(keepfind)
                //|| t.customer_Fname.Contains(keepfind)
                || t.ShopCustomer.customer_Lname.Contains(keepfind)
                || t.ShopCustomer.customer_TEL.ToString().Contains(keepfind))
                .ToList();

            Console.WriteLine(findDB);
            if (findDB.Count > 0)
            {
                MessageBox.Show("find!");
                dataGridView4.DataSource = findDB;

            }

            //var findDB = from c in context.ShopOrders
            //             where c.ShopCustomer.customer_Fname.Contains(keepfind)
            //             || c.ShopCustomer.customer_TEL == k1
            //             select c;
            //dataGridView4.DataSource = findDB.ToList();

            //Console.WriteLine("result= "+ findDB.First().customer_ID);

        }

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = int.Parse(dataGridView4.SelectedRows[0]
                .Cells[0].Value.ToString());

            var result = context.ShopOrderItems.Where(t => t.order_ID == id);
            Console.WriteLine(id);

            dataGridView5.DataSource = result.ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ShopMSU_PRO.Properties;

namespace ShopMSU_PRO
{
    public partial class Form2 : Form
    {

        APD65_63011212004Entities2 context = new APD65_63011212004Entities2();


        public Form2()
        {
            InitializeComponent();

        }



        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try{ 
                ShopEmployee employee = new ShopEmployee();

                string u1, p1, role1;

                var selAdmin = context.ShopEmployees.Where(i => i.employee_Username == textBox1.Text).First();


                Console.WriteLine();
                string[] items = new string[]
                {
                    u1 = selAdmin.employee_Username.ToString(),
                    p1 = selAdmin.employee_Password.ToString(),
 
                    role1 = selAdmin.employee_Status.ToString(),

                };

                var Form1 = new Form1();
                var Form3 = new Form3();
                var Form4 = new Form4();

                Console.WriteLine("U: "+u1);
                Console.WriteLine("P: " + p1);
                Console.WriteLine("Role: " + role1);


            
            
                    //string user1 = (read["employee_Username"].ToString());

                    if (textBox1.Text == u1 && textBox2.Text == p1)
                    {
                        Console.WriteLine("Pass: 1" );
                        if (role1 == "1")
                        {
                            MessageBox.Show("Login Success: Admin");
                            Form3.Show();
                            this.Visible = false;
                        }

                    }
                    if (textBox1.Text == u1 && textBox2.Text == p1)
                    {
                        Console.WriteLine("Pass: 2");
                        if (role1 == "2")
                        {
                            MessageBox.Show("Login Success: Shop");
                            Form4.Show();
                            this.Visible = false;
                        }

                    }
                    if (textBox1.Text == u1 && textBox2.Text == p1)
                    {
                        Console.WriteLine("Pass: 3");
                        if (role1 == "3")
                        {
                            MessageBox.Show("Login Success: Store");
                            Form1.Show();
                            this.Visible = false;
                        }

                    }
                    else
                    {
                        MessageBox.Show("Wrong Username/Password!");
                    }
            }
            //System.InvalidOperationException
            catch (System.InvalidOperationException)
            {
                MessageBox.Show("Wrong Username/Password!");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //string u2, p2;

            //var selShop = context.ShopEmployees.Where(i => i.employee_Status == 2).FirstOrDefault();
            //string[] items = new string[]
            //{

            //    u2 = selShop.employee_Username.ToString(),
            //    p2 = selShop.employee_Password.ToString(),

            //};

            //var Form4 = new Form4();
            //try
            //{
            //    if (textBox4.Text == u2 && textBox3.Text == p2)
            //    {
            //        MessageBox.Show("Login Success: Shop");
            //        Form4.Show();
            //        this.Visible = false;

            //    }
            //    else
            //    {
            //        MessageBox.Show("Wrong Shop Username/Password");
            //    }
            //}
            //catch (System.Configuration.SettingsPropertyNotFoundException ex)
            //{
            //    MessageBox.Show("ErrorShop!!");
            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string  u3, p3;


            //var selStore = context.ShopEmployees.Where(i => i.employee_Status == 3).FirstOrDefault();
            //string[] items = new string[]
            //{
            //    u3 = selStore.employee_Username.ToString(),
            //    p3 = selStore.employee_Password.ToString()
            //};

            //var Form1 = new Form1();
            //try
            //{
            //    if (textBox6.Text == u3 && textBox5.Text == p3)
            //    {
            //        MessageBox.Show("Login Success: Store");
            //        Form1.Show();
            //        this.Visible = false;

            //    }
            //    else
            //    {
            //        MessageBox.Show("Wrong Store Username/Password");
            //    }
            //}
            //catch (System.Configuration.SettingsPropertyNotFoundException ex)
            //{
            //    MessageBox.Show("ErrorStore!!!");
            //}
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

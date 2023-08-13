using AForge.Video.DirectShow;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Validation;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using ZXing.QrCode;

namespace ShopMSU_PRO
{
    public partial class Form4 : Form
    {
        FilterInfoCollection webcams;
        VideoCaptureDevice videoin;

        APD65_63011212004Entities2 context = new APD65_63011212004Entities2();

        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'aPD65_63011212004DataSet8.ShopOrderItem' table. You can move, or remove it, as needed.
            this.shopOrderItemTableAdapter.Fill(this.aPD65_63011212004DataSet8.ShopOrderItem);
            // TODO: This line of code loads data into the 'aPD65_63011212004DataSet7.ShopOrder' table. You can move, or remove it, as needed.
            this.shopOrderTableAdapter.Fill(this.aPD65_63011212004DataSet7.ShopOrder);
            // TODO: This line of code loads data into the 'aPD65_63011212004DataSet2.ShopCustomer' table. You can move, or remove it, as needed.
            this.shopCustomerTableAdapter.Fill(this.aPD65_63011212004DataSet2.ShopCustomer);

            webcams = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo webcam in webcams)
            {
                comboBox1.Items.Add(webcam.Name);
            }

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            var Form2 = new Form2();

            Form2.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (videoin != null && videoin.IsRunning)
            {
                timer1.Stop();
                videoin.Stop();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            videoin = new VideoCaptureDevice(webcams[comboBox1.SelectedIndex].MonikerString);
            videoSourcePlayer1.VideoSource = videoin;
            videoSourcePlayer1.Start();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string id, title, detail, img, category, price, pall;
            ShopProduct product = new ShopProduct();

            

            var capture = videoSourcePlayer1.GetCurrentVideoFrame();
            if (capture != null)
            {
                BarcodeReader reader = new BarcodeReader();
                var result = reader.Decode(capture);
                
                if (result != null)
                {
                    string chgRe = result.ToString();
                    var selpro = context.ShopProducts.Where(i => i.product_Serial.ToString() == chgRe.ToString()).FirstOrDefault();


                    string[] items = new string[]
                    {
                    selpro.product_Serial.ToString(),
                    selpro.product_Name.ToString(),
                    selpro.product_Type.ToString(),
                    selpro.product_All.ToString(),
                    (selpro.product_Price * selpro.product_All).ToString(),
                    selpro.product_ID.ToString()
                    };

                    listView1.Items.Add(new ListViewItem(items));
                    double sum = calTotal(listView1.Items);
                    label2.Text = sum.ToString();
                }
            }
        }

        private double calTotal(ListView.ListViewItemCollection items)
        {
            double sum = 0;
            foreach (ListViewItem item in items)
            {
                sum += double.Parse(item.SubItems[4].Text);
            }
            return sum;
        }

        private double calDis(ListView.ListViewItemCollection items)
        {
            double Discount = 0, keepTotal = 0, sale = 0;


            

            foreach (ListViewItem item in items)
            {




                if (listView1.Items.Cast<ListViewItem>().Any((i) => i.SubItems[0].Text == 52538.ToString())
                    && listView1.Items.Cast<ListViewItem>().Any((i) => i.SubItems[0].Text == 52135.ToString()))
                {
                    keepTotal = int.Parse(label2.Text);
                    Discount = (float)(keepTotal * 0.1);
                    sale = double.Parse(label2.Text) - Discount;
                    //Console.WriteLine("Sale: " + sale);
                    MessageBox.Show("Dis!"+Discount);
                    MessageBox.Show("sale!" + sale);
                    break;

                }

              
            }
            return sale;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoin != null && videoin.IsRunning)
            {
                timer1.Stop();
                videoSourcePlayer1.Stop();
            }
        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

            string keepfind = textBox20.Text;

            var findDB = context.ShopCustomers.Where(t =>t.customer_ID.ToString().Contains(keepfind) 
                ||t.customer_Fname.Contains(keepfind) 
                || t.customer_Lname.Contains(keepfind)
                || t.customer_Address.Contains(keepfind)
                || t.customer_TEL.ToString().Contains(keepfind))
                .ToList();

            if (findDB.Count > 0)
            {
                dataGridView2.DataSource = findDB;
               
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            ShopCustomer customer = new ShopCustomer();

            customer.customer_Fname = textBox2.Text;
            customer.customer_Lname = textBox6.Text;
            customer.customer_Address = textBox8.Text;
            customer.customer_TEL = int.Parse(textBox1.Text);

            context.ShopCustomers.Add(customer);
            int change = context.SaveChanges();
            MessageBox.Show("Change: " + change + " records");

            shopCustomerBindingSource.DataSource = context.ShopCustomers.ToList();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox3.Text);  

            var result = context.ShopCustomers.Where(i => i.customer_ID == id).First();
            context.ShopCustomers.Remove(result);
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

        private void button9_Click(object sender, EventArgs e)
        {
            shopCustomerBindingSource.AddNew();
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void button8_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                if (item.Selected)
                {
                    listView1.Items.Remove(item);
                    double sum = calTotal(listView1.Items);
                    label2.Text = sum.ToString();
                }
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            ShopProduct product = new ShopProduct();
            double Discount = 0, keepTotal = 0, sale = 0;
            int itemS1 = 0, itemS2 = 0, amountSale = 0;
            int itemDiscount = 0;
            int keepMoney1 = 0, keepMoney2 = 0;
            string say = "";

            ShopOrderItem orderItem = new ShopOrderItem();

            foreach (ListViewItem item in listView1.Items)
            {

                int b = Convert.ToInt32(textBox4.Text);

                if (item.Selected)
                {
                    var kID = listView1.Items[item.Index].SubItems[0].Text;
                    Console.WriteLine("keep: " + kID);
                    var selpro = context.ShopProducts.Where(i => i.product_Serial.ToString() == kID).FirstOrDefault();
                    int a = Convert.ToInt32(selpro.product_Price);
                    Console.WriteLine("price items : " + a + " " + "priceDB: " + selpro.product_Price);

                    item.SubItems[3].Text = textBox4.Text;
                    item.SubItems[4].Text = Convert.ToString(a * b);

                    if (listView1.Items.Cast<ListViewItem>().Any((i) => i.SubItems[0].Text == 52538.ToString())
                        && listView1.Items.Cast<ListViewItem>().Any((i) => i.SubItems[0].Text == 52135.ToString()))
                    {
                        keepTotal = int.Parse(label2.Text);
                        Discount = (float)(keepTotal * 0.1);
                        sale = double.Parse(label2.Text) - Discount;
                        //Console.WriteLine("Sale: " + sale);
                        MessageBox.Show("Dis!" + Discount);
                        MessageBox.Show("sale!" + sale);
                        //double Kdis = Discount;
                        //double Ksale = sale;
                        break;

                    }
                }

                //orderItem.orderItem_Amount = int.Parse(item.SubItems[3].Text);
                //orderItem.orderItem_Price = int.Parse(item.SubItems[4].Text);

                //var selProductSale = from s in context.ShopSales select s;

                //foreach (var kSale in selProductSale)
                //{
                //    if (int.Parse(item.SubItems[0].Text) == kSale.product_ID1)
                //    {
                //        itemS1 = int.Parse(item.SubItems[3].Text);
                //        keepMoney1 = int.Parse(item.SubItems[4].Text);

                //    }
                //    else if (int.Parse(item.SubItems[0].Text) == kSale.product_ID2)
                //    {
                //        itemS2 = int.Parse(item.SubItems[3].Text);
                //        keepMoney2 = int.Parse(item.SubItems[4].Text);
                //    }

                //}

                //if (itemS1 > 0 && itemS2 > 0)
                //{
                //    if (itemS1 == itemS2)
                //    {
                //        amountSale = itemS1;
                //        itemDiscount = (int)((keepMoney1 + keepMoney2) * (decimal)0.1);
                //    }
                //    else
                //    {
                //        if (itemS1 == itemS2)
                //        {
                //            amountSale = itemS2;
                //            decimal tt = (int)((keepMoney1 / itemS2) * (decimal)0.1);
                //            itemDiscount = (int)((tt + keepMoney2) * (decimal)0.1);
                //        }
                //        else
                //        {
                //            amountSale = itemS1;
                //            decimal tt = (int)(keepMoney2 / itemS2) * amountSale;
                //            itemDiscount = (int)((tt + keepMoney2) * (decimal)0.1);
                //        }
                //    }

                //    keepTotal = int.Parse(label2.Text);
                //    //Discount = (float)(keepTotal * 0.1);

                //    sale = double.Parse(label2.Text) - itemDiscount;

                //    label14.Text = itemDiscount.ToString();
                //    label17.Text = sale.ToString();

                //    say = "Promotion is 10% " + amountSale + "package";
                //    Console.WriteLine(say);
                //    MessageBox.Show(say);

                //}
            }

            double sum = calTotal(listView1.Items);
            
            //if (sum > 0)
            //{
            //    double sale = calDis(listView1.Items);
            //    Console.WriteLine("Sale: " + sale);
            //}
            
            label2.Text = sum.ToString();
            label14.Text = Discount.ToString();
            label17.Text = sale.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            double sum = calTotal(listView1.Items);
            label2.Text = sum.ToString();

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            int checkDisC1 = 52538, checkDisC2 = 52538;
            int keepTotal;
            float keepDis;

            int keepID = int.Parse(textBox5.Text);
            Console.WriteLine("ID: "+ keepID);

            try
            {
                var selpro = context.ShopProducts.Where(i => i.product_Serial.ToString() == keepID.ToString()).FirstOrDefault();

                string[] items = new string[]
                {
                    selpro.product_Serial.ToString(),
                    selpro.product_Name.ToString(),
                    selpro.product_Type.ToString(),
                    selpro.product_All.ToString(),
                    (selpro.product_Price * selpro.product_All).ToString(),
                    selpro.product_ID.ToString()

                };

                listView1.Items.Add(new ListViewItem(items));

                double sum = calTotal(listView1.Items);

                label2.Text = sum.ToString();
            }
            catch(NullReferenceException ex)
            {
                var selpro = context.ShopProducts.Where(i => i.product_Serial.ToString() == keepID.ToString()).FirstOrDefault();

                string[] items = new string[]
                {
                    selpro.product_Serial.ToString(),
                    selpro.product_Name.ToString(),
                    selpro.product_Type.ToString(),
                    selpro.product_All.ToString(),
                    (selpro.product_Price * selpro.product_All).ToString(),
                    selpro.product_ID.ToString()

                };

                listView1.Items.Add(new ListViewItem(items));

                double sum = calTotal(listView1.Items);

                label2.Text = sum.ToString();
            }
               



        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {


            try
            {
                ShopOrderItem orderItem = new ShopOrderItem();
                ShopOrder order = new ShopOrder();
                ShopCustomer customer = new ShopCustomer();
                ShopProduct product = new ShopProduct();
                ShopEmployee employee = new ShopEmployee();

                var result = context.ShopCustomers.Where(c => c.customer_ID.ToString() == textBox7.Text).First();

                int itemS1 = 0, itemS2 = 0, amountSale = 0;
                int itemDiscount = 0;
                int keepMoney1 = 0, keepMoney2 = 0;
                string say = "";
                double Discount = 0, keepTotal = 0, sale = 0;

                orderItem.customer_Serial = customer.customer_ID;

                double sum = calTotal(listView1.Items);
                label2.Text = sum.ToString();

                //order.customer_ID = customer.customer_ID;


                order.customer_Serial = int.Parse(textBox7.Text);
                order.order_DATE = DateTime.Now;
                order.order_Total = decimal.Parse(label2.Text);

                context.ShopOrders.Add(order);
                context.ShopOrderItems.Add(orderItem);
                int change = context.SaveChanges();
                if (change > 0) MessageBox.Show("ชำระเงินเรียบร้อย");

                foreach (ListViewItem item in listView1.Items)
                {

                    //orderItem.order_ID = int.Parse(textBox7.Text);
                    //orderItem.product_ID = product.product_ID;
                    orderItem.orderItem_Name = item.SubItems[1].Text;
                    orderItem.orderItem_Amount = int.Parse(item.SubItems[3].Text);
                    orderItem.orderItem_Price = int.Parse(item.SubItems[4].Text);
                    orderItem.orderItem_Total = decimal.Parse(label2.Text);
                    orderItem.orderItem_Serial = int.Parse(item.SubItems[0].Text);
                    orderItem.order_ID = order.order_ID;
                    orderItem.customer_Serial = int.Parse(textBox7.Text);

                    //customer.customer_Serial = int.Parse(textBox7.Text);

                    //Console.WriteLine("ID: " + orderItem.sale_ID);

                    context.ShopOrderItems.Add(orderItem);
                    //context.ShopCustomers.Add(customer);

                    string keepID = item.SubItems[0].Text;
                    var selpro = context.ShopProducts.Where(i => i.product_Serial.ToString() == keepID).FirstOrDefault();
                    selpro.product_All = selpro.product_All - int.Parse(item.SubItems[3].Text);
                    Console.WriteLine(keepID);
                    Console.WriteLine(selpro.product_All);

                    


                    listView1.Items.Clear();
                    //shopProductBindingSource.DataSource = context.ShopProducts;

                    var selProductSale = from s in context.ShopSales select s;
                    



                    foreach (var kSale in selProductSale)
                    {
                        if (int.Parse(item.SubItems[0].Text) == kSale.product_ID1)
                        {
                            itemS1 = int.Parse(item.SubItems[3].Text);
                            keepMoney1 = int.Parse(item.SubItems[4].Text);

                        }
                        else if (int.Parse(item.SubItems[0].Text) == kSale.product_ID2)
                        {
                            itemS2 = int.Parse(item.SubItems[3].Text);
                            keepMoney2 = int.Parse(item.SubItems[4].Text);
                        }

                    }
                    if (itemS1 > 0 && itemS2 > 0)
                    {
                        if (itemS1 == itemS2)
                        {
                            amountSale = itemS1;
                            itemDiscount = (int)((keepMoney1 + keepMoney2) * (decimal)0.1);
                        }
                        else
                        {
                            //if (itemS1 > itemS2)
                            //{
                            //    amountSale = itemS2;
                            //    decimal tt = (int)((keepMoney1 / itemS2) * (decimal)0.1);
                            //    itemDiscount = (int)((tt + keepMoney2) * (decimal)0.1);
                            //}
                            //else
                            //{
                            //    amountSale = itemS1;
                            //    decimal tt = (int)(keepMoney2 / itemS2) * amountSale;
                            //    itemDiscount = (int)((tt + keepMoney2) * (decimal)0.1);
                            //}
                            context.ShopOrders.Add(order);

                        }

                        keepTotal = int.Parse(label2.Text);

                        sale = double.Parse(label2.Text) - itemDiscount;

                        label14.Text = itemDiscount.ToString();
                        label17.Text = sale.ToString();



                        say = "Promotion is 10% " + amountSale + "package";
                        Console.WriteLine(say);
                        MessageBox.Show(say);

                        order.order_Total = decimal.Parse(label17.Text);

                        context.ShopOrders.Add(order);
                        MessageBox.Show("save Order Sale Packge");
                    }
                    context.SaveChanges();



                }


                MessageBox.Show("Save Completed");
            }
            catch(System.Data.Entity.Validation.DbEntityValidationException  ex)
            {

            }

            //private void button13_Click(object sender, EventArgs e)
            //{
            //    int amount = calculateAmount(listView1.Items);
            //    double price = calculate(listView1.Items);
            //    var customer = context.Fcustomer.Where(c => c.Cid == textBox2.Text).First();

            //    ComSale Sale = new ComSale();
            //    Sale.cid = customer.Cid;
            //    Sale.eid = eid;
            //    Sale.amount = amount;
            //    Sale.totalPrice = decimal.Parse(price.ToString());
            //    Sale.dateOrder = DateTime.Now;
            //    context.ComSale.Add(Sale);
            //    int change = context.SaveChanges();
            //    if (change > 0) MessageBox.Show("ชำระเงินเรียบร้อย");

            //    if (change > 0)
            //    {
            //        foreach (ListViewItem item in listView1.Items)
            //        {
            //            INEX nEX = new INEX();
            //            nEX.sid = Sale.Sid;
            //            nEX.pid = item.SubItems[0].Text;
            //            nEX.dateOrder = DateTime.Now;
            //            nEX.money = decimal.Parse(item.SubItems[5].Text);
            //            nEX.quantity = int.Parse(item.SubItems[4].Text);

            //            context.INEX.Add(nEX);

            //            string pid = item.SubItems[0].Text;
            //            var product = context.ComProduct.Where(c => c.Pid == pid).First();
            //            product.amount = product.amount - int.Parse(item.SubItems[4].Text);

            //            context.SaveChanges();
            //        }
            //        textBox1.Text = "";
            //        textBox2.Text = "";
            //        textBox3.Text = "";
            //        textBox5.Text = "";
            //        pictureBox2.Image = null;
            //        label3.Text = "...";
            //        listView1.Items.Clear();
            //        comProductBindingSource.DataSource = context.ComProduct.ToList();
            //    }
            //}
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            string keepfind = textBox13.Text;

            var findDB = context.ShopOrderItems.Where(t => t.orderItem_ID.ToString().Contains(keepfind)
                || t.orderItem_ID.ToString().Contains(keepfind)
                || t.order_ID.ToString().Contains(keepfind)
                || t.orderItem_Name.Contains(keepfind))
                .ToList();

            var findDB2 = context.ShopCustomers.Where(t => t.customer_Serial.ToString().Contains(keepfind)
                || t.customer_Fname.ToString().Contains(keepfind)
                || t.customer_Lname.ToString().Contains(keepfind)
                || t.customer_TEL.ToString().Contains(keepfind))

                .ToList();

            if (findDB.Count > 0)
            {
                MessageBox.Show("find!!");
                dataGridView5.DataSource = findDB;

            }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            string keepfind = comboBox2.Text;
            
            Console.WriteLine("ResultFIND!= " + keepfind);

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

        private void button12_Click(object sender, EventArgs e)
        {
            listView2.Items.Clear();
            textBox15.Text = "";
            textBox17.Text = "";
            textBox18.Text = "";
            textBox19.Text = "";

            String id = textBox9.Text;

            ShopProduct shopproduct = new ShopProduct();

            string data_url = "https://www.jib.co.th/web/product/readProduct/" + id;

            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument data_doc = web.Load(data_url);

            HtmlNode titleNode = data_doc.DocumentNode.
                SelectSingleNode("//meta[@property=\"og:title\"]");

            HtmlNode description = data_doc.DocumentNode.
            SelectSingleNode("//meta[@property = \"og:description\"]");

            var typeNode = data_doc.DocumentNode.
            SelectNodes(" //div[@class = \"step_nav\"] //a ");

            HtmlNode imageNode = data_doc.DocumentNode.
            SelectSingleNode("//meta[@property = \"og:image\"]");

            HtmlNode priceNode = data_doc.DocumentNode.
            SelectSingleNode("//div[@class = " +
            " \"col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center price_block\" ]");

            String price = priceNode.InnerText;
            price = new string(price.Where(c => char.IsDigit(c)).ToArray());

            Console.WriteLine("PRICE: " + price);

            //String price = priceNode.InnerText;
            //price = new string(price.Where(ch => char.IsDigit(ch)).ToArray());
            //textBox7.Text = price;


            string[] data = new string[] {
                 id,
                 titleNode.Attributes["content"].Value,
            };

            listView2.Items.Add(new ListViewItem(data));

        }

        private void button11_Click(object sender, EventArgs e)
        {
            String id = textBox22.Text;

            ShopProduct shopproduct = new ShopProduct();

            string data_url = "https://www.jib.co.th/web/product/readProduct/" + id;

            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument data_doc = web.Load(data_url);

            HtmlNode titleNode = data_doc.DocumentNode.
                SelectSingleNode("//meta[@property=\"og:title\"]");

            HtmlNode description = data_doc.DocumentNode.
            SelectSingleNode("//meta[@property = \"og:description\"]");

            var typeNode = data_doc.DocumentNode.
            SelectNodes(" //div[@class = \"step_nav\"] //a ");

            HtmlNode imageNode = data_doc.DocumentNode.
            SelectSingleNode("//meta[@property = \"og:image\"]");

            HtmlNode priceNode = data_doc.DocumentNode.
            SelectSingleNode("//div[@class = " +
            " \"col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center price_block\" ]");
            String price = priceNode.InnerText;
            price = new string(price.Where(c => char.IsDigit(c)).ToArray());

            shopproduct.product_ID = int.Parse(id);
            shopproduct.product_Name = titleNode.Attributes["content"].Value;
            shopproduct.product_Detail = description.Attributes["content"].Value;
            shopproduct.product_Price = decimal.Parse(price);
            shopproduct.product_Type = typeNode[2].InnerText;
            shopproduct.product_IMG = imageNode.Attributes["content"].Value;
            shopproduct.product_All = 100;

            context.ShopProducts.Add(shopproduct);
            int save = context.SaveChanges();
            if (save == 1)
            {
                MessageBox.Show("บันทึกสำเร็จ");
            }

            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException exception)
            {
                Console.WriteLine(exception);
                MessageBox.Show("" + exception);
            }

            MessageBox.Show("บันทึกสำเร็จ");
            shopProductBindingSource.DataSource = context.ShopProducts.ToList();

        }

        private void listView2_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                textBox19.Text = listView2.SelectedItems[0].Text;
                int kERR = int.Parse(listView2.SelectedItems[0].Text);
                string url = "https://www.jib.co.th/web/product/readProduct/" + textBox19.Text;
                HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = web.Load(url);

                HtmlNode titleNode = doc.DocumentNode.SelectSingleNode("//meta[@property=\"og:title\"]");
                textBox18.Text = titleNode.Attributes["content"].Value;

                HtmlNode descriptionNode = doc.DocumentNode.SelectSingleNode("//meta[@property=\"og:description\"]");
                textBox17.Text = descriptionNode.Attributes["content"].Value;



                if (kERR == 5583)
                {
                    //pictureBox1 = null;
                    Console.WriteLine("Pic is NULL!");
                }

                HtmlNode imageNode = doc.DocumentNode.SelectSingleNode("//meta[@property=\"og:image\"]");
                pictureBox5.Image = LoadImage(imageNode.GetAttributeValue("content", ""));

                HtmlNode priceNode = doc.DocumentNode.SelectSingleNode("//div[@class=" + "\"col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center price_block\"]");
                String price = priceNode.InnerText;
                price = new string(price.Where(ch => char.IsDigit(ch)).ToArray());
                textBox15.Text = price;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                //pictureBox1 = null;
            }
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

        private void numericUpDown1_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged_2(object sender, EventArgs e)
        {
            var conv = Convert.ToInt32(numericUpDown1.Value);
            var day = DateTime.Now.Day - conv;
            var date = DateTime.Today.Subtract(TimeSpan.FromDays(conv));
            var result = from order in context.ShopOrders where order.order_DATE >= date select order;
            dataGridView4.DataSource = result.ToList();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            var result = from order in context.ShopOrders
                         where order.order_DATE.Value.Year == dateTimePicker1.Value.Year &&
                         order.order_DATE.Value.Month == dateTimePicker1.Value.Month &&
                         order.order_DATE.Value.Day == dateTimePicker1.Value.Day
                         select order;
            dataGridView4.DataSource = result.ToList();
            decimal total = 0;
            //foreach (var item in result)
            //{
            //    total += item.order_Total;
            //}
            //label23.Text = total.ToString();

        }
    }
}

using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopMSU_PRO
{
    public partial class Form1 : Form
    {

        APD65_63011212004Entities2 context = new APD65_63011212004Entities2();
        public Form1()
        {
            InitializeComponent();


        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {



        }

        private Image LoadImage(string url)
        {
            try
            {
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                myHttpWebRequest.UserAgent = "Chrome/105.0.0.0";
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                Stream stramResponse = myHttpWebResponse.GetResponseStream();
                Bitmap bmp = new Bitmap(stramResponse);
                stramResponse.Dispose();
                return bmp;
            }
            catch(Exception ex) { return null; }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String url = textBox1.Text;
            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load(url);

            HtmlNode[] pageNode = doc.DocumentNode.SelectNodes("//div[@class=\"col-md-6 col-sm-6 pad-0\"] //span").ToArray();
            textBox2.Text = pageNode[1].InnerText;

            HtmlNode[] typeNode = doc.DocumentNode.SelectNodes("//div[@class=\"list-group-item sub2\"] //span").ToArray();
            textBox6.Text = typeNode[1].InnerText;

            //HtmlNode[] maxpageNode = doc.DocumentNode.SelectNodes("//div[@class=\"col-md-6 col-sm-6 pad-0\"] //span").ToArray();
            //String kmax = maxpageNode[2].InnerText;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            int c = 0;
            int page = 0;
            int newIpage = 0;
            int appendpage1 = 0;
            int appendpage2 = 0;
            int chgpage;
            int calpage;
            int totalmenu;
            string countItem;


            while (true)
            {

                String url = textBox1.Text + "/" + page;
                Console.WriteLine(url);
                HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = web.Load(url);

                //HtmlNode[] pageNode = doc.DocumentNode.SelectNodes("//div[@class=\"col-md-6 col-sm-6 pad-0\"] //span").ToArray();
                //var cpage = pageNode[1].InnerText;

                HtmlNode[] totalNode = doc.DocumentNode.SelectNodes("//div[@class=\"col-md-6 col-sm-6 pad-0\"] //span").ToArray();
                var tt = totalNode[0].InnerText;
                totalmenu = int.Parse(tt);


                var listviewNode = doc.DocumentNode.SelectNodes("//div[@class=\"cart_modal buy_promo\"]");

                if (c == totalmenu)
                {
                    break;
                }

                foreach (HtmlNode node in listviewNode)
                {

                    string[] items = new string[]
                    {
                                node.Attributes["data-id"].Value,
                                node.Attributes["data-name"].Value

                    };

                    listView1.Items.Add(new ListViewItem(items));
                    c++;
                }
                Console.WriteLine("LIST: " + listView1.Items);

                Console.WriteLine("total: " + totalmenu); //รวมสินค้าทั้งหมด

                //chgpage = int.Parse(cpage);
                //calpage = chgpage - 1; //เลขหน้า

                //newIpage = int.Parse(calpage.ToString() + appendpage1.ToString() + appendpage2.ToString());
                //Console.WriteLine("TESTpage :" + newIpage);


                page += 100;
                //Console.WriteLine("Page: " + page);
                Console.WriteLine("count: " + c);
                //countItem = c.ToString;

                textBox8.Text = c.ToString();

            }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                textBox3.Text = listView1.SelectedItems[0].Text;
                int kERR = int.Parse(listView1.SelectedItems[0].Text);
                string url = "https://www.jib.co.th/web/product/readProduct/" + textBox3.Text;
                HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = web.Load(url);

                HtmlNode titleNode = doc.DocumentNode.SelectSingleNode("//meta[@property=\"og:title\"]");
                textBox4.Text = titleNode.Attributes["content"].Value;

                HtmlNode descriptionNode = doc.DocumentNode.SelectSingleNode("//meta[@property=\"og:description\"]");
                textBox5.Text = descriptionNode.Attributes["content"].Value;



                if (kERR == 5583)
                {
                    //pictureBox1 = null;
                    Console.WriteLine("Pic is NULL!");
                }

                HtmlNode imageNode = doc.DocumentNode.SelectSingleNode("//meta[@property=\"og:image\"]");
                pictureBox1.Image = LoadImage(imageNode.GetAttributeValue("content", ""));

                HtmlNode priceNode = doc.DocumentNode.SelectSingleNode("//div[@class=" + "\"col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center price_block\"]");
                String price = priceNode.InnerText;
                price = new string(price.Where(ch => char.IsDigit(ch)).ToArray());
                textBox7.Text = price;
            }
            catch (System.Net.WebException ex)
            {

                Console.WriteLine(ex.Message);
                //pictureBox1 = null;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int c = 0;

            try
            {

                foreach (ListViewItem items in listView1.Items)
                {
                    Thread.Sleep(0);
                    ShopProduct product = new ShopProduct();

                    string id = items.SubItems[0].Text;
                    string name = items.SubItems[1].Text;

                    string url = "https://www.jib.co.th/web/product/readProduct/";
                    HtmlWeb web = new HtmlWeb();
                    HtmlAgilityPack.HtmlDocument doc = web.Load(url);


                    HtmlNode[] typeNode = doc.DocumentNode.SelectNodes("//div[@class=\"step_nav\"] //a").ToArray();
                    textBox6.Text = typeNode[2].InnerText;

                    //HtmlNode[] typeNode = doc.DocumentNode.SelectNodes("//div[@class=\"list-group-item sub2\"] //span").ToArray();
                    //textBox6.Text = typeNode[1].InnerText;

                    var descriptionNode = doc.DocumentNode.SelectSingleNode("//meta[@property=\"og:description\"]");
                    textBox5.Text = descriptionNode.Attributes["content"].Value;

                    int productAll = 50;

                    int productRemain = 50;

                    var priceNode = doc.DocumentNode.SelectSingleNode("//div[@class=" + "\"col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center price_block\"]");
                    String price = priceNode.InnerText;
                    price = new string(price.Where(ch => char.IsDigit(ch)).ToArray());
                    textBox7.Text = price;

                    //productDiscount 



                    var imageNode = doc.DocumentNode.SelectSingleNode("//meta[@property=\"og:image\"]");
                    pictureBox1.Image = LoadImage(imageNode.GetAttributeValue("content", ""));

                    product.product_Serial = int.Parse(id);

                    if (product.product_Serial == 5583)
                    {
                        product.product_IMG = null;
                        Console.WriteLine("Pic is NULL!!");
                    }

                    product.product_Name = name;
                    product.product_Type = typeNode[2].InnerText;
                    product.product_Detail = descriptionNode.Attributes["content"].Value;
                    product.product_All = productAll;
                    product.product_Remain = productRemain;
                    product.product_Price = decimal.Parse(price);
                    product.product_IMG = imageNode.GetAttributeValue("content", "");

                    //if (id != null )
                    //{
                    //    if (int.Parse(id) ==  product.product_Serial)
                    //    {
                    //        Console.WriteLine("Same Products!!!!!!!!!!");
                    //        continue;
                    //    }
                    //}
                    context.ShopProducts.Add(product);
                    context.SaveChanges();
                    c++;
                    Console.WriteLine(c + " ER: " + product.product_Serial);
                }



                MessageBox.Show("Save Products!");
                //listView1.Items.Clear();

                //Console.WriteLine("dbDetail: " + product);
                //Console.WriteLine("dbname: " + id);
            }
            catch (System.Net.WebException ex)
            {
                Console.WriteLine(ex.Message);


            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            shopProductBindingSource.DataSource = context.ShopProducts.ToList();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                var result = context.ShopProducts.Where(i => i.product_Serial.ToString() == id).First();
                pictureBox4.Image = LoadImage(result.product_IMG);
            }
            catch(System.NullReferenceException ex)
            {
                string id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                var result = context.ShopProducts.Where(i => i.product_Serial.ToString() == id).First();
                pictureBox4.Image = LoadImage(result.product_IMG);
            }

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            var Form2 = new Form2();

            Form2.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            shopProductBindingSource.AddNew();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            string keepfind = textBox21.Text;

            var findDB = context.ShopProducts.Where(t => t.product_Serial.ToString().Contains(keepfind)
                || t.product_Name.Contains(keepfind)
                || t.product_Detail.Contains(keepfind))
                .ToList();

            if (findDB.Count > 0)
            {
                MessageBox.Show("find!!");
                dataGridView1.DataSource = findDB;

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ShopProduct product = new ShopProduct();

            product.product_Serial = int.Parse(textBox10.Text);
            product.product_Type = textBox13.Text;
            product.product_Name = textBox12.Text;
            product.product_Detail = textBox11.Text;
            product.product_IMG = pictureBox4.Text;
            product.product_Price = int.Parse(textBox9.Text);

            context.ShopProducts.Add(product);
            int change = context.SaveChanges();
            MessageBox.Show("Change: " + change + " records");

            shopProductBindingSource.DataSource = context.ShopProducts.ToList();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void button5_Click(object sender, EventArgs e)
        {
            shopProductBindingSource.EndEdit();
            int change = context.SaveChanges();
            MessageBox.Show("Change: " + change + " records");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox10.Text);

            var result = context.ShopProducts.Where(i => i.product_Serial == id).First();
            context.ShopProducts.Remove(result);
            int change = context.SaveChanges();
            MessageBox.Show("Change: " + change + " records");

            shopProductBindingSource.DataSource = context.ShopProducts.ToList();
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
                dataGridView2.DataSource = findDB;

            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                listView2.Items.Clear();
                textBox15.Text = "";
                textBox17.Text = "";
                textBox18.Text = "";
                textBox19.Text = "";

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
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Fill text!");
            }
 


        }


        private void button8_Click(object sender, EventArgs e)
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

            shopproduct.product_ID =  int.Parse(id);
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

        private void tabControl1_Click(object sender, EventArgs e)
        {

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
            catch (System.Net.WebException ex)
            {

                Console.WriteLine(ex.Message);
                //pictureBox1 = null;
            }
        }
    }
}

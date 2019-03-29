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

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //combobox1 de yer alan stunların altında yer alan bilgileri listview1 de 
            //id no adını ve birim fiyatını ekleyen program
            string[] dizi = comboBox1.SelectedItem.ToString().Split(' ');
            int id = Convert.ToInt16(dizi[0]);
            string str = @"Data Source=(LocalDB)\v11.0;AttachDbFilename='F:\Gorsel prog\WindowsFormsApplication3\WindowsFormsApplication3\ticari.mdf';Integrated Security=True";
            SqlConnection con = new SqlConnection(str);
            con.Open();
            SqlCommand cmd2 = new SqlCommand("select * from stok where cat_id=@x", con);
            cmd2.Parameters.AddWithValue("@x", id);
            listView1.Items.Clear();
            SqlDataReader dr = cmd2.ExecuteReader();
            while (dr.Read())
            {
                ListViewItem item = listView1.Items.Add(dr["stok_id"].ToString());
                item.SubItems.Add(dr["stok_name"].ToString());
                item.SubItems.Add(dr["brfiyat"].ToString());
               
            }
            con.Close();           
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //Form Load oldugunda category tablosundaki stunları combobox1 e ekleyen program
            string str = @"Data Source=(LocalDB)\v11.0;AttachDbFilename='F:\Gorsel prog\WindowsFormsApplication3\WindowsFormsApplication3\ticari.mdf';Integrated Security=True";
            SqlConnection con = new SqlConnection(str);
            con.Open();
            SqlCommand cmd1 = new SqlCommand("select * from category", con);
            comboBox1.Items.Clear();
            SqlDataReader dr = cmd1.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString() + " " + dr["cat_name"].ToString());
            }
            con.Close();
            comboBox1.SelectedIndex = 0;         
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //buton clik oldugunda comboboxda secilen kategorideki ürünlerin fiyatını % 10 arttıran program
            string[] dizi = comboBox1.SelectedItem.ToString().Split(' ');
            int id = Convert.ToInt16(dizi[0]);
            string str = @"Data Source=(LocalDB)\v11.0;AttachDbFilename='F:\Gorsel prog\WindowsFormsApplication3\WindowsFormsApplication3\ticari.mdf';Integrated Security=True";
            SqlConnection con = new SqlConnection(str);
            con.Open();
            SqlCommand cmd2 = new SqlCommand("update stok set brfiyat= convert(int,1.1*brfiyat) where  cat_id=@x", con);
            cmd2.Parameters.AddWithValue("@x", id);
            listView1.Items.Clear();
            int adet = cmd2.ExecuteNonQuery();
            MessageBox.Show(adet.ToString() +" kadar kayıt güncellendi");
            con.Close();
        }
    }
}

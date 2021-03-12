using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace öğrenciOtomasyonu
{
    public partial class Form1 : Form
    {
        SqlConnection baglantı = new SqlConnection("Data Source=DESKTOP-UPRUOBV;Initial Catalog=öğrenci_otomasyon;Integrated Security=True");

        int ogrenciNo;
        int selectedDersKodu;
        int id;
        //private string dersKodu;

        public Form1()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //ders_kayıt();
            bolum_goster();
            ders_goster();
            listeleme();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label6_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void kaydet_click(object sender, EventArgs e)
        {
            //   bolum_goster();
            //  ders_goster();
            ogrencı_bılgı();
            not_kayıt();
            listeleme();

        }

        enum Dersler
        {
            Biyoloji = 1,
            Kimya = 2,
            Matematik = 3
        }

        private void sil_click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txbOgrenciNo.Text))
                ogrenciNo = Convert.ToInt32(txbOgrenciNo.Text);

            if (baglantı.State == ConnectionState.Closed)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = baglantı;

                cmd.Connection.Open();

                cmd.CommandText = "delete from ogrencıbılgı where ogrencıNo='" + ogrenciNo + "'";
                cmd.ExecuteNonQuery();
                baglantı.Close();

                MessageBox.Show("Kaydınız Başarılı Bir Şekilde Silindi.");
                listeleme();
            }
        }

        private void güncelle_click(object sender, EventArgs e)
        {
            ogr_güncelle();
            dersgüncelle();
            notgüncelle();
        }

        private void ara_click(object sender, EventArgs e)
        {

        }

        void ogrencı_bılgı()
        {
            if (baglantı.State == ConnectionState.Closed)
            {
                int bolumId = Convert.ToInt32(comboBox1.SelectedValue);
                //int dersKodu = Convert.ToInt32(cmbDersAd.SelectedIndex);
                //DataRow selectedRow = ((DataRowView)cmbDersAd.SelectedItem).Row;
                //dersKodu = Convert.ToInt32(selectedRow["dersKodu"]);

                baglantı.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = baglantı;
                //cmd.CommandText = "INSERT INTO ogrencıbılgı(ogrencıNo,ad,soyad,telefon,bolumId,dersKodu) VALUES(@ogrencıNo, @ad,@soyad,@telefon,@bolumid,@derskodu)";
                cmd.CommandText = "insert into ogrencıbılgı(ogrencıNo,ad,soyad,telefon,bolumId) values ('" + txbOgrenciNo.Text + "','" + ogrencı_ad.Text + "','" + ogrencı_soyad.Text + "','" + ogrencı_tel.Text + "'," + bolumId + ")";
                //cmd.Parameters.AddWithValue("@ogrenciNo", txbOgrenciNo.Text);
                //cmd.Parameters.AddWithValue("@ad", ogrencı_ad.Text);
                //cmd.Parameters.AddWithValue("@soyad", ogrencı_soyad.Text);
                //cmd.Parameters.AddWithValue("@telefon", ogrencı_tel.Text);
                //cmd.Parameters.AddWithValue("@bolumId", bolumId);
                //cmd.Parameters.AddWithValue("@dersKodu", dersKodu);
                cmd.ExecuteNonQuery();
                baglantı.Close();
            }
        }

        //void ders_kayıt()
        //{
        //    if (baglantı.State == ConnectionState.Closed)
        //    {
        //int bolumId = Convert.ToInt32(comboBox1.SelectedValue);
        //int dersKodu = Convert.ToInt32(ComboBox2.SelectedValue);
        //        baglantı.Open();
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = baglantı;
        //        cmd.CommandText = "insert into dersler(dersADı,bolumId) values ("") ";
        //    }

        //}

        void not_kayıt()
        {
            if (baglantı.State == ConnectionState.Closed)
            {
                int dersKodu = Convert.ToInt32(cmbDersAd.SelectedValue);


                baglantı.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = baglantı;
                cmd.CommandText = "insert into notlar (vıze,fınal,but,dersKodu,ogrencıNo) values ('" + vıze.Text + "','" + fınal.Text + "','" + but.Text + "'," + dersKodu + "," + Convert.ToInt32(txbOgrenciNo.Text) + ")";
                cmd.ExecuteNonQuery();
                baglantı.Close();

            }

        }

        void bolum_goster()
        {
            if (baglantı.State == ConnectionState.Closed)
            {
                baglantı.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = baglantı;
                cmd.CommandText = "select * from bolumler";
                SqlDataAdapter adpr = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpr.Fill(ds, "bolumler");
                baglantı.Close();
                comboBox1.ValueMember = "bolumId";
                comboBox1.DisplayMember = "bolumAdı";
                comboBox1.DataSource = ds.Tables[0];

            }
        }

        void ders_goster()
        {
            if (baglantı.State == ConnectionState.Closed)
            {
                baglantı.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = baglantı;
                cmd.CommandText = "select * from dersler";
                SqlDataAdapter adpr = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpr.Fill(ds, "dersler");
                baglantı.Close();

                cmbDersAd.ValueMember = "dersKodu";
                cmbDersAd.DisplayMember = "dersADı";
                cmbDersAd.DataSource = ds.Tables[0];

            }
        }

        void listeleme()
        {
            if (baglantı.State == ConnectionState.Closed)
            {
                baglantı.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = baglantı;
                cmd.CommandText = "select ogrencıbılgı.ogrencıNo,ad,soyad,telefon,bolumAdı,dersADı,vıze,fınal,but from ogrencıbılgı inner join notlar on ogrencıbılgı.ogrencıNo = notlar.ogrencıNo inner join dersler on ogrencıbılgı.bolumId = dersler.bolumId inner join bolumler on ogrencıbılgı.bolumId = bolumler.bolumId";
                SqlDataAdapter adpr = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpr.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];

                baglantı.Close();
            }
        }

        void ogr_güncelle()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = baglantı;

            if (baglantı.State == ConnectionState.Closed)
                baglantı.Open();

            cmd.CommandText = @"update ogrencıbılgı set ad='" + ogrencı_ad.Text + "', " +
                "soyad='" + ogrencı_soyad.Text + "'," +
                "telefon='" + ogrencı_tel.Text + "' where ogrencıNo";
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            baglantı.Close();
        }
        void dersgüncelle()
        {


            if (baglantı.State == ConnectionState.Closed)
            {
                baglantı.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = baglantı;
                cmd.CommandText = "update dersler set dersADı='" + cmbDersAd.SelectedText + "' where dersKodu ";
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                baglantı.Close();
            }
        }
        void notgüncelle()
        {
            if (baglantı.State == ConnectionState.Closed)
            {
                baglantı.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = baglantı;
                cmd.CommandText = "update notlar set vıze='" + vıze.Text + "','" + fınal.Text + "','" + but.Text + "' where notId";
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                baglantı.Close();
            }
        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbDersAd_SelectedIndexChanged(object sender, EventArgs e)
        {



        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txbOgrenciNo.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            ogrencı_ad.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            ogrencı_soyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            ogrencı_tel.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            cmbDersAd.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            vıze.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            fınal.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            but.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
        }

        private void ogrencı_ad_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

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

namespace dosyaKontrol
{
    public partial class Form3 : Form
    {
        string aktarimKlasor = @"C:\Users\dolfi\OneDrive\Masaüstü\Aktarim";
        private string connectionString = ("server=.;Initial Catalog=proje;Integrated Security=SSPI");

        public Form3()
        {
            InitializeComponent();
        }
        public void GetirLoglar()
        {
            string connectionString = "Server=ENOCHIAN\\MSSQL;Database=proje;Trusted_Connection=True;TrustServerCertificate=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Loglar", connection);
                DataTable tablo = new DataTable();
                da.Fill(tablo);
                logKaydi.DataSource = tablo;
                logKaydi.ReadOnly = true;
                logKaydi.AllowUserToResizeRows = false;
                logKaydi.AllowUserToAddRows = false;
                connection.Close();             
            }
        }
        private void tabloHizala()
        {
            foreach (DataGridViewColumn column in logKaydi.Columns)
            {
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                logKaydi.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
           /* 
            int toplamGenislikLog = 0;
            foreach (DataGridViewColumn column in logKaydi.Columns)
            {
                toplamGenislikLog += column.Width;
            }
           
            logKaydi.Width = toplamGenislikLog + 20;*/
        }

        private void getirrr()
        {
            logKaydi.ReadOnly = true;
            logKaydi.AllowUserToAddRows = false;
            logKaydi.AllowUserToResizeRows = false; logKaydi.ReadOnly = true;
            logKaydi.AllowUserToResizeColumns = false; logKaydi.RowHeadersVisible = false;
            logKaydi.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            logKaydi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void Form3_Load(object sender, EventArgs e)
        {            
            GetirLoglar();
            getirrr();
            tabloHizala();
            isimGuncelle();
        }
        private void isimGuncelle()
        {     
                if (logKaydi.Columns.Contains("dosyaAdi"))
                {
                    logKaydi.Columns["dosyaAdi"].HeaderText = "Dosya Adı";              }
            if (logKaydi.Columns.Contains("HataMesaji"))
            {
                logKaydi.Columns["HataMesaji"].HeaderText = "Hata Mesajı";
            }
            if (logKaydi.Columns.Contains("HataKodu"))
            {
                logKaydi.Columns["HataKodu"].HeaderText = "Hata Kodu";
            }
        }

        private void Temizle_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=ENOCHIAN\\MSSQL;Database=proje;Trusted_Connection=True;TrustServerCertificate=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string deleteQuery = "DELETE FROM Loglar";
                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"{rowsAffected} adet log kaydı başarıyla silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Silinecek log kaydı bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Loglar temizlenirken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void logKaydi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

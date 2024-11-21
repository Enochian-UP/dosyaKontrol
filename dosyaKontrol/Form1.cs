using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using ExcelDataReader;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Runtime.InteropServices;
using Zebra;
using System.Drawing.Printing;
using System.Globalization;
using System.IO.Ports;

namespace dosyaKontrol
{
    public partial class Form1 : Form
    {
        private DataTable dataTable;
        SqlConnection baglanti;
        SqlDataAdapter da;
        string aktarimKlasor = @"C:\Users\dolfi\OneDrive\Masaüstü\Aktarim";
        string aktarilanKlasor = @"C:\Users\dolfi\OneDrive\Masaüstü\Aktarilan";
        private string connectionString = ("server=.;Initial Catalog=proje;Integrated Security=SSPI");
        private Form2 form2;
        private Form3 form3;
        private SerialPort sp;
        public Form1()
        {
            InitializeComponent();
         // this.IsMdiContainer = true;       Kodu etkinleştirince form siyahlaşıyor
            aktarimTimer = new Timer();
            aktarimTimer.Interval = 10000;
            aktarimTimer.Tick += new EventHandler(AktarimYap);
            form2 = new Form2();
            LoadDataFromExcelFiles(aktarilanKlasor);
            sp = new SerialPort();                      // Barkod okuyucu için gereli port ayarları
            sp.PortName = "COM3";
            sp.BaudRate = 9600;
            sp.DataBits = 8;
            sp.Parity = Parity.None;
            sp.StopBits = StopBits.One;           
            sp.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);          
        }
        private void Form1_Load_1(object sender, EventArgs e)
        {

            DirectoryInfo aktarimKlasoruOlustur = new DirectoryInfo("C:\\Users\\dolfi\\OneDrive\\Masaüstü");
            aktarimKlasoruOlustur.CreateSubdirectory("Aktarim");
            aktarimKlasoruOlustur.CreateSubdirectory("Aktarilan");
            this.BackColor = System.Drawing.SystemColors.Control;
            Getir();
            aktarimTimer.Start();       // 10 saniyede bir aktarımdan aktarılana doğru aktarım işlemi başlıyor
            LoglariGoster();
            sqlVerileriYukle();
            UpdateColumnHeaders();
            sutunOrtala();
            scannerBilgi.Focus();
            scannerBilgi.BackColor = System.Drawing.Color.White;
            dataTable = new DataTable();
            dataTable.Columns.Add("PartiNum", typeof(string));
            dataTable.Columns.Add("SupplierNum", typeof(string));
            dataTable.Columns.Add("Tarih", typeof(DateTime));

           
        }
        public void LoadDataFromExcelFiles(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                MessageBox.Show("Belirtilen dizin mevcut değil.");
                return;
            }

            DataTable dataTable = new DataTable(); //veriler datatable saklanılıp bir excel tablosu oluşturuyor
            dataTable.Columns.Add("PartiNum");
            dataTable.Columns.Add("dosyaAdi");
            dataTable.Columns.Add("Tarih");

            foreach (var file in Directory.GetFiles(directoryPath, "*.xlsx"))
            {
                try
                {
                    using (var workbook = new XLWorkbook(file))
                    {
                        var worksheet = workbook.Worksheet(1);
                        var rows = worksheet.RangeUsed().RowsUsed();

                        foreach (var row in rows.Skip(1)) // başlık sütunlarını atla
                        {
                            var partiNum = row.Cell(1).GetValue<string>();
                            var dosyaAdi = Path.GetFileName(file);

                            DateTime tarih;
                            var tarihCellValue = row.Cell(3).GetValue<string>();

                            if (DateTime.TryParse(tarihCellValue, out tarih))
                            {
                            }
                            else
                            {
                                tarih = DateTime.MinValue;
                            }

                            dataTable.Rows.Add(partiNum, dosyaAdi, tarih);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Dosya '{file}' okunurken bir hata oluştu: {ex.Message}");
                }
            }
        }
        void Getir()
        {
            baglanti = new SqlConnection(@"Data Source=ENOCHIAN\MSSQL;Initial Catalog=proje;Integrated Security=True;TrustServerCertificate=True");
            baglanti.Open();
            da = new SqlDataAdapter("SELECT partiNum,SupplierNum,Tarih,dosyaAdi FROM teknikMalzeme order by id desc;", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);                 // datagrid ayarları 
            sqlTablosu.DataSource = tablo;
            sqlTablosu.ReadOnly = true;
            sqlTablosu.AllowUserToResizeRows = false;
            sqlTablosu.AllowUserToAddRows = false;
            sqlTablosu.RowHeadersVisible = false;
            sqlTablosu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            sqlTablosu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            baglanti.Close();
            excelTablosu.ReadOnly = true;
            excelTablosu.AllowUserToResizeRows = false;
            excelTablosu.AllowUserToAddRows = false;
            excelTablosu.RowHeadersVisible = false;
            excelTablosu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            excelTablosu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            yeniDataGrid.RowHeadersVisible = false;
            yeniDataGrid.ReadOnly = true;
            yeniDataGrid.AllowUserToResizeRows = false;
            yeniDataGrid.AllowUserToAddRows = false;
            yeniDataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            yeniDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (!excelTablosu.Columns.Contains("CheckBoxColumn"))
            {
                DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn
                {
                    Name = "CheckBoxColumn",
                    HeaderText = "Seç",
                };
                excelTablosu.Columns.Add(checkBoxColumn);
            }
        }
        public void UpdateColumnHeaders()
        {
            if (sqlTablosu.Columns.Contains("partiNum"))   // datagrid sütun başlıklarını düzeltme işlemi
            {
                sqlTablosu.Columns["partiNum"].HeaderText = "Parti Numarası";
            }
            if (sqlTablosu.Columns.Contains("supplierNum"))
            {
                sqlTablosu.Columns["supplierNum"].HeaderText = "Müşteri Numarası";
            }
            if (sqlTablosu.Columns.Contains("dosyaAdi"))
            {
                sqlTablosu.Columns["dosyaAdi"].HeaderText = "Dosya Adı";
            }
            if (sqlTablosu.Columns.Contains("tarih"))
            {
                sqlTablosu.Columns["tarih"].HeaderText = "Tarih";
            }
            /*if (excelTablosu.Columns.Contains("partiNum"))     excelTablosu isimli datagriddin sütunlarını excel'e referans olarak aldığı için değiştirmek sorun teşkil ediyor
            {
                excelTablosu.Columns["partiNum"].HeaderText = "Parti Numarası";
            }
            if (excelTablosu.Columns.Contains("supplierNum"))
            {
                excelTablosu.Columns["supplierNum"].HeaderText = "Müşteri Numarası";
            }
            if (excelTablosu.Columns.Contains("tarih"))
            {
                excelTablosu.Columns["tarih"].HeaderText = "Tarih";
            }*/
            if (yeniDataGrid.Columns.Contains("partiNum"))
            {
                yeniDataGrid.Columns["partiNum"].HeaderText = "Parti Numarası";
            }
            if (yeniDataGrid.Columns.Contains("dosyaAdi"))
            {
                yeniDataGrid.Columns["dosyaAdi"].HeaderText = "Dosya Adı";
            }
            if (yeniDataGrid.Columns.Contains("tarih"))
            {
                yeniDataGrid.Columns["tarih"].HeaderText = "Tarih";
            }
        }
        private DataTable ReadExcelFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                LogError("Dosya mevcut değil", filePath);
                return null;
            }
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read)) // Manuel yolla eklerken seçilen excel dosyalarını okuma işlemi burda gerçekleşiyor.
            {
                try
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                        });

                        if (result.Tables.Count == 0)
                        {
                            LogError("Excel dosyasında hiç veri bulunamadı", filePath);
                            return null;
                        }

                        DataTable table = result.Tables[0];

                        if (!table.Columns.Contains("partiNum"))
                        {
                            throw new ArgumentException("Dosyada 'partiNum' sütunu yer almıyor!");
                        }

                        return table;
                    }
                }
                catch (ArgumentException ex)
                {
                    LogError(ex.Message, filePath);
                    return null;
                }
                catch (Exception ex)
                {
                    LogError("Bir hata oluştu: " + ex.Message, filePath);
                    return null;
                }
            }
        }
        private bool CheckPrimaryKeyConflicts(DataTable dataTable, string primaryKeyColumn)
        {
            string connectionString = "Server=ENOCHIAN\\MSSQL;Database=proje;Trusted_Connection=True;TrustServerCertificate=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();          // bu metod sqlde kayıtlı olan partiNumları arar ve çakışır ise manuel yolla eklerken hata veriyor.
                var cakismalar = new StringBuilder();
                bool cakismaBuldu = false;
                var cakismaDetaylari = new Dictionary<string, List<int>>();

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    DataRow row = dataTable.Rows[i];
                    if (row[primaryKeyColumn] == DBNull.Value)
                        continue;

                    string value = row[primaryKeyColumn].ToString();
                    string query = $"SELECT COUNT(*) FROM TeknikMalzeme WHERE {primaryKeyColumn} = @Value";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Value", value);
                        int count = (int)command.ExecuteScalar();

                        if (count > 0)
                        {
                            cakismaBuldu = true;
                            if (!cakismaDetaylari.ContainsKey(value))
                            {
                                cakismaDetaylari[value] = new List<int>();
                            }
                            cakismaDetaylari[value].Add(i + 1);
                        }
                    }
                }
                connection.Close();
                if (cakismaBuldu)
                {
                    foreach (var cakisma in cakismaDetaylari)
                    {
                        string key = cakisma.Key;
                        string rows = string.Join(",", cakisma.Value);
                        cakismalar.AppendLine($"{rows}.Satırdaki Parti numarası zaten mevcut! " + $"(Parti Numarası: {string.Join(", ", cakisma.Key)})");
                    }
                }
                return cakismaBuldu;
            }
        }
        private void verileriEkle(DataTable dataTable, string dosyaAdi)
        {
            string connectionString = "Server=ENOCHIAN\\MSSQL;Database=proje;Trusted_Connection=True;TrustServerCertificate=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open(); // bu metod manuel yolla excel eklerken verileri veritabanına ekliyor.

                foreach (DataRow row in dataTable.Rows)
                {
                    if (row["partiNum"] == DBNull.Value)
                        continue;
                    string query = "INSERT INTO TeknikMalzeme (partiNum, supplierNum, tarih, dosyaAdi) VALUES (@partiNum, @supplierNum, @tarih, @dosyaAdi)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@partiNum", row["partiNum"].ToString().Trim());
                        command.Parameters.AddWithValue("@supplierNum", row["supplierNum"].ToString().Trim());
                        command.Parameters.AddWithValue("@tarih", row["tarih"].ToString().Trim());
                        command.Parameters.AddWithValue("@dosyaAdi", dosyaAdi ?? "Belirtilmemiş"); 

                        try
                        {
                            command.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show($"Veri eklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                Getir(); 
                connection.Close();
            }
        }
        private void btnLoadExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog tara = new OpenFileDialog
            {
                Filter = "Excel Files (*.xls;*.xlsx)|*.xls;*.xlsx|All Files (*.*)|*.*",
                FilterIndex = 1
            };

            if (tara.ShowDialog() == DialogResult.OK)           // gerekli koşullar sağlanıyorsa (çakışma yoksa ve dosya partiNum'a sahipse excel taşınıyor ve sql'e ekleniyor.
            {
                string filePath = tara.FileName;
                txtFileName.Text = filePath;
                string filename = Path.GetFileName(filePath);
              
                DataTable excelData = ReadExcelFile(filePath);
                if (excelData != null)
                {
                    if (!excelData.Columns.Contains("partiNum"))
                    {
                        MessageBox.Show("Dosyada parti numarası yer almıyor!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    bool hataVarmi = CheckPrimaryKeyConflicts(excelData, "partiNum");

                    if (hataVarmi)
                    {
                        MessageBox.Show("Dosya zaten eklenmiş parti numaralara sahip!");

                    }
                    else
                    {
                        verileriEkle(excelData,filename);
                 
                            string HedefKlasor = @"C:\Users\dolfi\OneDrive\Masaüstü\Aktarilan";
                            string aktarilanKlasoru = Path.Combine(HedefKlasor, filename);
                            File.Move(filePath, aktarilanKlasoru);                         
                    }
                }
            }
        }
        private void btnSil_Click_1(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in sqlTablosu.SelectedRows)
            {
                string partiNum = row.Cells["partiNum"].Value.ToString(); // seçilen veriyi hem datagridden hem de sql den siliyor fakat excel satırından silmiyor.

                string connectionString = "Server=ENOCHIAN\\MSSQL;Database=proje;Trusted_Connection=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string deleteQuery = "DELETE FROM teknikMalzeme WHERE partiNum= @partiNum";

                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@partiNum", partiNum);
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                sqlTablosu.Rows.Remove(row);
            }
        }
        private void sutunOrtala()
        {                                   // datagriddeki verilerin görsel olarak ortalanması
            int toplamGenislikSql = 0;
            foreach (DataGridViewColumn column in sqlTablosu.Columns)
            {
                toplamGenislikSql += column.Width;
            }
            foreach (DataGridViewColumn column in excelTablosu.Columns)
            {
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                excelTablosu.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            foreach (DataGridViewColumn column in sqlTablosu.Columns)
            {
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                sqlTablosu.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            foreach (DataGridViewColumn column in yeniDataGrid.Columns)
            {
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                yeniDataGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
        private void veriTabaninaEkle(string ExcelDosyaYolu)
        {
            DataTable dataTable = ReadExcelFile(ExcelDosyaYolu);

            if (dataTable == null)          // aktarım yaptığı excellerin verilerini sql'e girmesi burada gerçekleşiyor.
            {
                MessageBox.Show($"Excel dosyası okunmadı: {Path.GetFileName(ExcelDosyaYolu)}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CheckPrimaryKeyConflicts(dataTable, "partiNum");
            string dosyaAdi = Path.GetFileName(ExcelDosyaYolu);
            string connectionString = "Server=ENOCHIAN\\MSSQL;Database=proje;Trusted_Connection=True;TrustServerCertificate=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlConnectionString = "Server=ENOCHIAN\\MSSQL;Database=proje;Trusted_Connection=True;TrustServerCertificate=True";
                using (SqlConnection sqlConnection = new SqlConnection(sqlConnectionString))

                    foreach (DataRow row in dataTable.Rows)
                    {
                        if (row["partiNum"] == DBNull.Value)
                        {
                            LogError(ExcelDosyaYolu, "Dosyada parti numarası zaten yer alıyor!");
                            continue;
                        }

                         string query = "INSERT INTO TeknikMalzeme (partiNum, supplierNum, tarih, dosyaAdi) VALUES (@partiNum, @supplierNum, @tarih, @dosyaAdi)";


                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@partiNum", row["partiNum"].ToString().Trim());
                            command.Parameters.AddWithValue("@supplierNum", row["supplierNum"].ToString().Trim());
                            command.Parameters.AddWithValue("@tarih", row["tarih"].ToString().Trim());
                            command.Parameters.AddWithValue("@dosyaAdi", dosyaAdi ?? "Belirtilmemiş"); 
                            try
                            {
                                command.ExecuteNonQuery();
                            }

                            catch (SqlException ex)
                            {
                                MessageBox.Show($"Veri eklenirken hata oluştu {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                connection.Close();
            }
        }
        private void LogError(string hataMesaji, string dosya)
        {
            string dosyaAdi = Path.GetFileName(dosya);  // aktarım yaparken bir hata oluşursa sql de bir hata mesajı oluşuyor  
            string connectionString = "Server=ENOCHIAN\\MSSQL;Database=proje;Trusted_Connection=True;TrustServerCertificate=True";
            {
                using (SqlConnection bg = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Loglar(Tarih,DosyaAdi,HataMesaji) VALUES (@Tarih,@DosyaAdi,@HataMesaji)";
                    using (SqlCommand command = new SqlCommand(query, bg))
                    {
                        command.Parameters.AddWithValue("@Tarih", DateTime.Now);
                        command.Parameters.AddWithValue("@DosyaAdi", dosyaAdi ?? "Belirtilmemiş");
                        command.Parameters.AddWithValue("@HataMesaji", hataMesaji);
                        bg.Open();
                        command.ExecuteNonQuery();
                        bg.Close();
                        // UpdateColumnHeaders();
                    }
                }
            }
        }     
        private void AktarimYap(object sender, EventArgs e)
        {
       
            var excelDosyalar = Directory.GetFiles(aktarimKlasor, "*.xlsx");
            var aktarilanDosyalar = Directory.GetFiles(aktarilanKlasor, "*.xlsx");

            foreach (var dosya in excelDosyalar)            // aktarım klasöründen aktarılan klasörüne 10 saniyede bir excelleri taşıyor
            {
                string dosyaAdi = Path.GetFileName(dosya);
                DataTable dataTable = ReadExcelFile(dosya);
                string hedefYol = Path.Combine(aktarilanKlasor, dosyaAdi);

                if (dataTable == null)
                {
                    try
                    {
                        File.Delete(dosya);
                    }
                    catch (Exception ex)
                    {
                        LogError("Dosya silinirken hata oluştu: " + ex.Message, dosyaAdi);
                    }
                    continue;
                }
                if (dataTable.Columns == null || !dataTable.Columns.Contains("partiNum"))   // eğer excelde bir hata varsa veya çakışırsa excel silinip ardında bir log mesajı bırakıyor.
                {
                    LogError("Dosyada parti numarası yer almıyor! Dosya Taşınmadı", dosyaAdi);
                    try
                    {
                        File.Delete(dosya);
                    }
                    catch (Exception ex)
                    {
                        LogError("Dosya silinirken hata oluştu: " + ex.Message, dosya);
                    }
                    continue;
                }
                if (CheckPrimaryKeyConflicts(dataTable, "partiNum"))
                {
                    LogError("Dosya zaten eklenmiş parti numaralarına sahip!", dosyaAdi);
                    try
                    {
                        File.Delete(dosya);
                    }
                    catch (Exception ex)
                    {
                        LogError("Dosya silinirken hata oluştu: " + ex.Message, dosya);
                    }
                    continue;
                }
                try
                {
                    File.Move(dosya, hedefYol); // başarılıysa taşınıyor ve sql'e ekleniyor veriler.
                    veriTabaninaEkle(hedefYol);
                }
                catch (Exception ex)
                {
                    LogError("Dosya taşınırken hata oluştu: " + ex.Message, dosyaAdi);
                }
                Getir();
                LoglariGoster();
            }
        }
        private void btnEkle_Click(object sender, EventArgs e)
        {
            string partiNum = textBox1.Text;        // yeni excel oluştururken verileri kullanıcıdan aldığı kısım
            string supplierNum = textBox2.Text;
            DateTime tarih = dateTimePicker1.Value;

            if (string.IsNullOrWhiteSpace(partiNum) || string.IsNullOrWhiteSpace(supplierNum))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (DataRow row in dataTable.Rows)
            {
                if (row["PartiNum"].ToString() == partiNum)
                {
                    MessageBox.Show("Bu Parti Numarası zaten mevcut.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            // DataTable'e yeni satır ekleyin
            DataRow newRow = dataTable.NewRow();
            newRow["PartiNum"] = partiNum;
            newRow["SupplierNum"] = supplierNum;
            newRow["Tarih"] = tarih;
            dataTable.Rows.Add(newRow);
            sqlEkle(partiNum, supplierNum, tarih);  // formu kapatıp açınca eski eklemek istediği veriler olmasına karşın veriler kaybolmasın diye sql de tuttum.
            sqlVerileriYukle();
            textBox1.Clear();           // excel oluşturuldan sonra textboxlar temizlenir
            textBox2.Clear();
            dateTimePicker1.Value = DateTime.Now;
        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (excelTablosu.Rows.Count == 0)
            {
                MessageBox.Show("Veri girmediniz!");
                return;
            }
            bool isAnyChecked = false;          // herhangi bir checkbox işaretli değilse veriler eklenmez
            foreach (DataGridViewRow row in excelTablosu.Rows)
            {
                if (row.Cells.Cast<DataGridViewCell>().FirstOrDefault(c => c.OwningColumn.HeaderText == "Seç") is DataGridViewCheckBoxCell checkBoxCell)
                {
                    bool isChecked = checkBoxCell.Value != null && (bool)checkBoxCell.Value;
                    if (isChecked)
                    {
                        isAnyChecked = true;
                        break;
                    }
                }
            }
            if (!isAnyChecked)
            {
                MessageBox.Show("Hiçbir satır seçilmedi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string exportFolder = @"C:\Users\dolfi\OneDrive\Masaüstü\Aktarim";
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");    // bu tarih formatıyla excelde veriler kaydediliyor
            string excelFileName = $"{timestamp}.xlsx";         // dosyaların kaydedildiği isim
            string exportPath = Path.Combine(exportFolder, excelFileName);
            try
            {
                using (var workbook = new XLWorkbook())     // excel tablosu görsel ayarlar
                {
                    var worksheet = workbook.Worksheets.Add("Sheet1");
                    int headerColumnIndex = 1;
                    for (int i = 0; i < excelTablosu.Columns.Count; i++)
                    {
                        if (excelTablosu.Columns[i].HeaderText == "Seç")
                            continue;

                        var headerCell = worksheet.Cell(1, headerColumnIndex);
                        headerCell.Value = excelTablosu.Columns[i].HeaderText;
                        headerCell.Style.Fill.BackgroundColor = XLColor.White;
                        headerCell.Style.Font.FontColor = XLColor.Black;
                        headerCell.Style.Font.Bold = true;
                        headerCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        headerCell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        headerCell.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                        headerColumnIndex++;
                    }
                    int rowIndex = 2;
                    var rowsToDelete = new List<DataGridViewRow>();
                    foreach (DataGridViewRow row in excelTablosu.Rows)
                    {
                        if (row.Cells.Cast<DataGridViewCell>().FirstOrDefault(c => c.OwningColumn.HeaderText == "Seç") is DataGridViewCheckBoxCell checkBoxCell)
                        {
                            bool isChecked = checkBoxCell.Value != null && (bool)checkBoxCell.Value;
                            if (isChecked)
                            {
                                var partiNumarasi = row.Cells["PartiNum"].Value?.ToString();
                                var rowColor = (rowIndex % 2 == 0) ? XLColor.LightGreen : XLColor.White;
                                int dataColumnIndex = 1;

                                for (int j = 0; j < excelTablosu.Columns.Count; j++)
                                {
                                    if (excelTablosu.Columns[j].HeaderText == "Seç")
                                        continue;

                                    var cellValue = row.Cells[j].Value?.ToString() ?? string.Empty;

                                    if (dataColumnIndex <= 16384)
                                    {
                                        var cell = worksheet.Cell(rowIndex, dataColumnIndex);
                                        cell.Value = cellValue;
                                        cell.Style.Fill.BackgroundColor = rowColor;
                                        cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                        cell.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                                    }
                                    dataColumnIndex++;
                                }
                                sqlSil(partiNumarasi); // excel oluşturulduktan sonra excelTablosu datagridinden silinir
                                rowIndex++;
                                LogKaydet(partiNumarasi, excelFileName, DateTime.Now);  
                                rowsToDelete.Add(row);
                            }
                        }
                    }
                    foreach (var row in rowsToDelete)
                    {
                        excelTablosu.Rows.Remove(row);
                    }
                    var range = worksheet.Range(worksheet.Cell(1, 1), worksheet.Cell(rowIndex - 1, headerColumnIndex - 1));
                    range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;            // excel görsel ayarlar
                    range.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                    range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    range.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    worksheet.Columns().AdjustToContents();
                    workbook.SaveAs(exportPath);
                    MessageBox.Show($"Veriler başarıyla excel dosyasına kaydedildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Dosya kaydedilirken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void sqlEkle(string partiNum, string supplierNum, DateTime tarih)
        {
            string connectionString = "Server=ENOCHIAN\\MSSQL;Database=proje;Trusted_Connection=True;TrustServerCertificate=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();      // yeni excel oluşturken ekle butonu sadece datagrid değil sql'ede ekliyor verilerin silinmemesi için
                string kontrolQuery = "SELECT COUNT(*) FROM exceller WHERE PartiNum = @PartiNum";
                using (SqlCommand kontrolCommand = new SqlCommand(kontrolQuery, connection))
                {
                    kontrolCommand.Parameters.AddWithValue("@PartiNum", partiNum);
                    int count = (int)kontrolCommand.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("Bu Parti Numarası zaten mevcut.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                string query = "INSERT INTO exceller (PartiNum, SupplierNum, Tarih) VALUES (@PartiNum, @SupplierNum, @Tarih)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PartiNum", partiNum);
                    command.Parameters.AddWithValue("@SupplierNum", supplierNum);
                    command.Parameters.AddWithValue("@Tarih", tarih);
                    command.ExecuteNonQuery();
                }
            }
        }
        private void sqlSil(string partiNum)
        {
            string connectionString = "Server=ENOCHIAN\\MSSQL;Database=proje;Trusted_Connection=True;TrustServerCertificate=True";
            using (SqlConnection connection = new SqlConnection(connectionString)) // aynı şekilde silindiği zaman datagridden ve sqlden de siliniyor
            {
                string query = "DELETE FROM exceller WHERE PartiNum = @PartiNum";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PartiNum", partiNum);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        private void sqlVerileriYukle()
        {
            string connectionString = "Server=ENOCHIAN\\MSSQL;Database=proje;Trusted_Connection=True;TrustServerCertificate=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT PartiNum, SupplierNum, Tarih FROM exceller ORDER BY Id desc";
                using (SqlCommand command = new SqlCommand(query, connection))  // başlangıçta datagride verileri yüklemek için 
                {
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    excelTablosu.DataSource = dataTable;
                }
            }
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;   // textbox'a sadece sayısal veriler girilebilir
            }
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) // textbox'a sadece sayısal veriler girilebilir
            {
                e.Handled = true;
            }
        }
        private void FormAc_Click(object sender, EventArgs e)
        {
            Form2 form2 = Application.OpenForms.OfType<Form2>().FirstOrDefault();
            if (form2 == null)
            {
                form2 = new Form2();        // form2 yi açma komutları
                form2.Show();
                form2.listele();
            }
            else
            {
                form2.BringToFront();
            }
        }
        private void btnLogGoster_Click(object sender, EventArgs e)
        {
            if (form3 == null || form3.IsDisposed)
            {
                form3 = new Form3();      // form3 ü açma komutları
            }
            form3.GetirLoglar();
            form3.Show();
            form3.BringToFront();
        }
        private void excelTablosu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == excelTablosu.Columns["CheckBoxColumn"].Index)  // tıklandığında checkbox değeri değişir.
            {
                if (excelTablosu.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewCheckBoxCell checkBoxCell)
                {
                    bool currentValue = (bool)(checkBoxCell.Value ?? false);
                    checkBoxCell.Value = !currentValue;
                }
            }
        }
        private void btnExcelSil_Click(object sender, EventArgs e)
        {
           /* foreach (DataGridViewRow row in excelTablosu.SelectedRows)
            {
                if (!row.IsNewRow)
                {
                    excelTablosu.Rows.Remove(row);
                }
            }*/

            foreach (DataGridViewRow row in excelTablosu.SelectedRows)
            {
                string partiNum = row.Cells["partiNum"].Value.ToString();

                string connectionString = "Server=ENOCHIAN\\MSSQL;Database=proje;Trusted_Connection=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open(); // seçilen satır aynı zamanda sqlden de siliniyor.
                    string deleteQuery = "DELETE FROM exceller WHERE partiNum= @partiNum";

                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@partiNum", partiNum);
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                sqlTablosu.Rows.Remove(row);
            }
        }
        private void LogKaydet(string partiNum, string dosyaAdi, DateTime tarih)
        {
            string connectionString = "Server=ENOCHIAN\\MSSQL;Database=proje;Trusted_Connection=True;TrustServerCertificate=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO ExcelLog (PartiNum, DosyaAdi, Tarih) VALUES (@PartiNum, @DosyaAdi, @Tarih)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PartiNum", partiNum);  // Log mesajlarının tutulduğu sql
                    command.Parameters.AddWithValue("@DosyaAdi", dosyaAdi);
                    command.Parameters.AddWithValue("@Tarih", tarih);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        private void LoglariGoster()
        {
            string connectionString = "Server=ENOCHIAN\\MSSQL;Database=proje;Trusted_Connection=True;TrustServerCertificate=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT PartiNum, DosyaAdi, Tarih FROM ExcelLog ORDER BY Tarih DESC";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);   // sqlden çektiği log mesajları datagridde burda gösteriliyor.
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    yeniDataGrid.DataSource = dataTable;
                }
            }
        }
        public class RawPrinterHelper
        {
            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public class DOCINFOA
            {
                [MarshalAs(UnmanagedType.LPStr)] public string pDocName;
                [MarshalAs(UnmanagedType.LPStr)] public string pOutputFile;
                [MarshalAs(UnmanagedType.LPStr)] public string pDataType;
            }       // yazıcı için gerekli komutlar

            [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

            [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool ClosePrinter(IntPtr hPrinter);

            [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

            [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool EndDocPrinter(IntPtr hPrinter);

            [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool StartPagePrinter(IntPtr hPrinter);

            [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool EndPagePrinter(IntPtr hPrinter);

            [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

            // SendBytesToPrinter()
            // Fonksiyona bir yazıcı adı ve yönetilmeyen bir byte dizisi
            // verildiğinde, fonksiyon bu byte'ları yazdırma kuyruğuna gönderir.
            // Başarı durumunda true, hata durumunda false döner.
            public static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, Int32 dwCount)
            {
                Int32 dwError = 0, dwWritten = 0; // yazma işlemleri byte a dönüştürülüp yazılıyor
                IntPtr hPrinter = new IntPtr(0);
                DOCINFOA di = new DOCINFOA();
                bool bSuccess = false; 

                di.pDocName = "My C#.NET RAW Document";
                di.pDataType = "RAW";

                if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
                {

                    if (StartDocPrinter(hPrinter, 1, di))
                    {
                        if (StartPagePrinter(hPrinter))
                        {
                            bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                            EndPagePrinter(hPrinter);
                        }
                        EndDocPrinter(hPrinter);
                    }
                    ClosePrinter(hPrinter);
                }

                if (bSuccess == false)
                {
                    dwError = Marshal.GetLastWin32Error();
                }
                return bSuccess;
            }
            public static bool SendStringToPrinter(string szPrinterName, string szString)
            {
                IntPtr pBytes;
                Int32 dwCount;
                dwCount = szString.Length;  // yazdırılmak istenen metnin byte cinsinden uzunluğu 
                pBytes = Marshal.StringToCoTaskMemAnsi(szString);
                SendBytesToPrinter(szPrinterName, pBytes, dwCount);
                Marshal.FreeCoTaskMem(pBytes);
                return true;
            }
        }
        private void btn_yazdir_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in sqlTablosu.SelectedRows)
            {
                DateTime tarih = Convert.ToDateTime(row.Cells["Tarih"].Value);

                CultureInfo cultureInfo = CultureInfo.CurrentCulture;
                int weekOfYearCount = cultureInfo.Calendar.GetWeekOfYear(tarih, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                string yearAndWeek = $"W{weekOfYearCount:D2}"; //Yılın iki basamaklı haftası

                string partiNum = row.Cells["partiNum"].Value.ToString();
                string supplierNum = row.Cells["supplierNum"].Value.ToString();
                string tarihE = tarih.ToString("dd.MM.yyyy"); // barkkoda gösterilen tarih (zpl de alt alta hizalamak için ikisini ayırdım.)
                string saatE = tarih.ToString("HH:mm:ss");  //barkodda gösterilen saat

                                                        // referans olarak alınan zpl kodları (BARKOD)
                string barkod = "^XA" +         
                                    "^MUM^JZN^JMA^MMT^PR5 " +
                                    $"^FO28,5^BY0.4^BCR,7,N,N,N^FD{partiNum}^FS" +
                                    $"^FO12,55^BXN,10,200,200^FD{supplierNum}^FS " +
                                    $"^FO22,10^A0R2,4^FD{partiNum}^FS " +
                                    $"^FO13,12^A0R2,4^FD{tarihE}^FS " +
                                     $"^FO9,12^A0R2,4^FD{saatE}^FS "+
                                     $"^FO5,12^A0R2,4^FD{yearAndWeek}^FS "+
                                    $"^FO6,55^A0R2,4^FD{supplierNum}^FS" +

                                    "^XZ";
                                             // referans olarak alınan zpl kodları (QR)
                string qr = "^XA" +
                    "^MUM^JZN^JMA^MMT^PR5 " +

                    $"^FO10,12^BXN,4,200,200^FD{partiNum}  {supplierNum}  {tarihE} {saatE} {yearAndWeek}^FS" +
                    $"^FO19,40^A0R2,3^FD{partiNum}^FS" +
                    $"^FO16,40^A0R2,3^FD{supplierNum}^FS" +
                    $"^FO13,40^A0R2,2^FD{tarihE}^FS" +
                    $"^FO11,40^A0R2,2^FD{saatE}^FS" +
                    $"^FO9,40^A0R2,2^FD{yearAndWeek}^FS" +
                    "^XZ";

                if (rdBtnBarkod.Checked)
                {                 
                    RawPrinterHelper.SendStringToPrinter("ZDesigner ZT411-203dpi ZPL", barkod);
                    // RawPrinterHelper.SendStringToPrinter("BSTESTU", barkod); ağa bağlanılan eski yazıcı

                }
                else if (rdBtnQr.Checked)
                {
                    RawPrinterHelper.SendStringToPrinter("ZDesigner ZT411-203dpi ZPL", qr);
                  //  RawPrinterHelper.SendStringToPrinter("BSTESTU", qr);  Ağa bağlanılan eski yazıcı

                }
                else
                {
                    MessageBox.Show("Lütfen bir barkod türü seçin!");
                }
            }
        }
        private void btnYenile_Click(object sender, EventArgs e)
        {
            Getir();
            aktarimTimer.Start();   // herhangi bir datagridde silinme veya ekleme işlemi yapıldığında otomatik yenileniyor fakat yinede manuel ekledim.
            LoglariGoster();
            sqlVerileriYukle();
            UpdateColumnHeaders();
            sutunOrtala();
        }
        private void btn_datagridTemizle_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=ENOCHIAN\\MSSQL;Database=proje;Trusted_Connection=True;TrustServerCertificate=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {                       // logların tutulduğu datagrid'i temizleme butonu aynı zamanda sqlden de siler
                    connection.Open();
                    string deleteQuery = "DELETE FROM excelLog";
                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"{rowsAffected} adet log kaydı başarıyla silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoglariGoster();
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
        private void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string OkunanVeri = sp.ReadExisting().Trim();
                this.Invoke(new MethodInvoker(delegate
                {
                    scannerBilgi.Text = OkunanVeri; // Okunan veri burada gösteriliyor

                    int qrPartiNumKismi = OkunanVeri.IndexOf(' '); // Boşluğa kadar olan kısmı al
                    string partiNum = (qrPartiNumKismi > 0) ? OkunanVeri.Substring(0, qrPartiNumKismi) : OkunanVeri;
                    string tarih = partiNumVarmi(partiNum); // SQL'den kontrol et

                    if (!string.IsNullOrEmpty(tarih))
                    {
                        scannerBilgi.BackColor = System.Drawing.Color.Red;
                        MessageBox.Show("Parti numarası zaten mevcut! (" + partiNum + ")\n" + tarih + "'inde eklenmiş."); // Uyarı ver                       
                        sp.DataReceived += sp_DataReceived;
                        scannerBilgi.Clear();
                        scannerBilgi.BackColor = System.Drawing.Color.White;
                    }
                    else
                    {
                        scannerBilgi.BackColor = System.Drawing.Color.LightGreen;
                    }
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri okunurken hata!" + ex.Message);
            }
        }
        public string partiNumVarmi(string partiNum)
        {
            string tarih = null;   // sql den partiNum ve tarihleri çekerek karşılaştırma yapıyor
            string connectionString = "Server=ENOCHIAN\\MSSQL;Database=proje;Trusted_Connection=True;TrustServerCertificate=True";
            string query = $"SELECT tarih FROM teknikMalzeme WHERE partiNum = @partiNum";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand komut = new SqlCommand(query, connection))
                {
                    komut.Parameters.AddWithValue("@partiNum", partiNum);

                    try
                    {
                        connection.Open();
                        var result = komut.ExecuteScalar();
                        if (result != null)
                        {
                            tarih = result.ToString();
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show("Veri Tabanı Hatası" + ex.Message);
                    }
                }
            }
            return tarih;
        }
        private void scannerBilgi_TextChanged(object sender, EventArgs e)
        {
            string barkod = scannerBilgi.Text; // okunan verinin nerede gösterileceği
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sp != null && sp.IsOpen)
            {
                sp.DataReceived -= sp_DataReceived;
                sp.Close();  // Açılan portun geri kapanması
                sp.Dispose();
            }

        }
        private void btn_port_Click(object sender, EventArgs e)
        {
            sp.Open(); // Okuyucu takılmadığı zaman formLoad fonksiyonunda olduğundan form açılmıyordu o yüzden butona aldım
        }
    }
}



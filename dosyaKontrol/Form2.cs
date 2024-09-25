using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dosyaKontrol
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        void getirr()
        {
            excelListele.ReadOnly = true;
            excelListele.AllowUserToAddRows = false;
            excelListele.AllowUserToResizeRows = false; excelListele.ReadOnly = true;
            excelListele.AllowUserToResizeColumns = false; excelListele.RowHeadersVisible = false;
            excelListele.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            excelListele.CellFormatting += excelListele_CellFormatting;
            excelListele.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        public void listele()
        {
            string exportFolderPath = @"C:\Users\dolfi\OneDrive\Masaüstü\Aktarilan";

            excelListele.Columns.Clear();
            excelListele.Rows.Clear();
            excelListele.Columns.Add("FileName", "Dosya Adı");
            excelListele.Columns.Add("CreationDate", "Oluşturulma Tarihi");        

            if (Directory.Exists(exportFolderPath))
            {
                var exportDosyalar = Directory.GetFiles(exportFolderPath, "*.xlsx");

                foreach (var file in exportDosyalar)
                {
                    string fileName = Path.GetFileName(file);
                    DateTime creationDate = File.GetCreationTime(file);
                    int rowIndex = excelListele.Rows.Add(fileName, creationDate);                  
                }
            }
            excelListele.Sort(excelListele.Columns["CreationDate"], System.ComponentModel.ListSortDirection.Descending);

        }
        private void excelListele_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        private void tabloHizala()
        {
            foreach (DataGridViewColumn column in excelListele.Columns)
            {
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                excelListele.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            getirr();
            tabloHizala();
        }
        private void excelListele_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (excelListele.Columns[e.ColumnIndex].Name == "CheckBox")
            {
                bool isChecked = (e.Value is bool) && (bool)e.Value;
                if (isChecked)
                {
                   foreach (DataGridViewCell cell in excelListele.Rows[e.RowIndex].Cells)
                   {
                       cell.Style.BackColor = Color.LightGreen;
                       cell.Style.ForeColor = Color.Black;
                   }
               }
               else
               {
                   foreach (DataGridViewCell cell in excelListele.Rows[e.RowIndex].Cells)
                   {
                       cell.Style.BackColor = Color.White;
                       cell.Style.ForeColor = Color.Black;
                   }
               }
             }
        }
        private void excelListele_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;

            if (rowIndex >= 0)
            {
                string fileName = excelListele.Rows[rowIndex].Cells["FileName"].Value.ToString();
                string exportFolderPath = @"C:\Users\dolfi\OneDrive\Masaüstü\Export";
                string aktarilanFolderPath = @"C:\Users\dolfi\OneDrive\Masaüstü\Aktarilan";
                string fullPathExport = Path.Combine(exportFolderPath, fileName);
                string fullPathAktarilan = Path.Combine(aktarilanFolderPath, fileName);

                if (File.Exists(fullPathExport))
                {
                    Process.Start(fullPathExport);
                }
                else if (File.Exists(fullPathAktarilan))
                {
                    Process.Start(fullPathAktarilan);
                }
                else
                {
                    MessageBox.Show("Dosya bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

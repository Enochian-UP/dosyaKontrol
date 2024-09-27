namespace dosyaKontrol
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnLoadExcel = new System.Windows.Forms.Button();
            this.btnSil = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.sqlTablosu = new System.Windows.Forms.DataGridView();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnKaydet = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.btnEkle = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.FormAc = new System.Windows.Forms.Button();
            this.btnLogGoster = new System.Windows.Forms.Button();
            this.excelTablosu = new System.Windows.Forms.DataGridView();
            this.btnExcelSil = new System.Windows.Forms.Button();
            this.yeniDataGrid = new System.Windows.Forms.DataGridView();
            this.aktarimTimer = new System.Windows.Forms.Timer(this.components);
            this.btn_yazdir = new System.Windows.Forms.Button();
            this.rdBtnBarkod = new System.Windows.Forms.RadioButton();
            this.rdBtnQr = new System.Windows.Forms.RadioButton();
            this.btnYenile = new System.Windows.Forms.Button();
            this.btn_datagridTemizle = new System.Windows.Forms.Button();
            this.scannerBilgi = new System.Windows.Forms.TextBox();
            this.lbl_Scan = new System.Windows.Forms.Label();
            this.lbl_YeniVeri = new System.Windows.Forms.Label();
            this.btn_port = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.sqlTablosu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.excelTablosu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yeniDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoadExcel
            // 
            this.btnLoadExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnLoadExcel.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.btnLoadExcel.Location = new System.Drawing.Point(12, 209);
            this.btnLoadExcel.Name = "btnLoadExcel";
            this.btnLoadExcel.Size = new System.Drawing.Size(134, 38);
            this.btnLoadExcel.TabIndex = 0;
            this.btnLoadExcel.Text = "Seç ve Yükle";
            this.btnLoadExcel.UseVisualStyleBackColor = true;
            this.btnLoadExcel.Click += new System.EventHandler(this.btnLoadExcel_Click);
            // 
            // btnSil
            // 
            this.btnSil.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnSil.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnSil.ForeColor = System.Drawing.Color.Red;
            this.btnSil.Location = new System.Drawing.Point(163, 563);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(108, 38);
            this.btnSil.TabIndex = 1;
            this.btnSil.Text = "Sil";
            this.btnSil.UseVisualStyleBackColor = false;
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(72, 139);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Dosya İsmi";
            // 
            // sqlTablosu
            // 
            this.sqlTablosu.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightGreen;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.sqlTablosu.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.sqlTablosu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sqlTablosu.Location = new System.Drawing.Point(334, 36);
            this.sqlTablosu.Name = "sqlTablosu";
            this.sqlTablosu.Size = new System.Drawing.Size(498, 254);
            this.sqlTablosu.TabIndex = 3;
            // 
            // txtFileName
            // 
            this.txtFileName.Enabled = false;
            this.txtFileName.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFileName.Location = new System.Drawing.Point(37, 167);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.ReadOnly = true;
            this.txtFileName.Size = new System.Drawing.Size(202, 26);
            this.txtFileName.TabIndex = 4;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1, -2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(270, 124);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // btnKaydet
            // 
            this.btnKaydet.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnKaydet.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnKaydet.Location = new System.Drawing.Point(37, 628);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(234, 37);
            this.btnKaydet.TabIndex = 10;
            this.btnKaydet.Text = "Verilerle Excel Oluştur";
            this.btnKaydet.UseVisualStyleBackColor = true;
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(23, 436);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 24);
            this.label2.TabIndex = 11;
            this.label2.Text = "Parti Numarası:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(11, 469);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(178, 24);
            this.label3.TabIndex = 12;
            this.label3.Text = "Müşteri Numarası:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(68, 503);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 24);
            this.label4.TabIndex = 13;
            this.label4.Text = "Tarih:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(199, 436);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 14;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(199, 469);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 15;
            this.textBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(199, 507);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(100, 20);
            this.dateTimePicker1.TabIndex = 18;
            // 
            // btnEkle
            // 
            this.btnEkle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnEkle.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.btnEkle.Location = new System.Drawing.Point(24, 563);
            this.btnEkle.Name = "btnEkle";
            this.btnEkle.Size = new System.Drawing.Size(108, 38);
            this.btnEkle.TabIndex = 19;
            this.btnEkle.Text = "Ekle";
            this.btnEkle.UseVisualStyleBackColor = true;
            this.btnEkle.Click += new System.EventHandler(this.btnEkle_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Uighur", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(470, -2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(210, 38);
            this.label5.TabIndex = 24;
            this.label5.Text = "Kayıtlı Parti Numaraları";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.Location = new System.Drawing.Point(960, 397);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(186, 24);
            this.label7.TabIndex = 26;
            this.label7.Text = "Son Kaydedilenler";
            // 
            // FormAc
            // 
            this.FormAc.Location = new System.Drawing.Point(1011, 24);
            this.FormAc.Name = "FormAc";
            this.FormAc.Size = new System.Drawing.Size(124, 36);
            this.FormAc.TabIndex = 28;
            this.FormAc.Text = "Excel Dosyalarını Göster";
            this.FormAc.UseVisualStyleBackColor = true;
            this.FormAc.Click += new System.EventHandler(this.FormAc_Click);
            // 
            // btnLogGoster
            // 
            this.btnLogGoster.Location = new System.Drawing.Point(1141, 24);
            this.btnLogGoster.Name = "btnLogGoster";
            this.btnLogGoster.Size = new System.Drawing.Size(124, 36);
            this.btnLogGoster.TabIndex = 29;
            this.btnLogGoster.Text = "Log Listesini Göster";
            this.btnLogGoster.UseVisualStyleBackColor = true;
            this.btnLogGoster.Click += new System.EventHandler(this.btnLogGoster_Click);
            // 
            // excelTablosu
            // 
            this.excelTablosu.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.excelTablosu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.excelTablosu.Location = new System.Drawing.Point(345, 424);
            this.excelTablosu.Name = "excelTablosu";
            this.excelTablosu.Size = new System.Drawing.Size(296, 254);
            this.excelTablosu.TabIndex = 22;
            this.excelTablosu.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.excelTablosu_CellContentClick);
            // 
            // btnExcelSil
            // 
            this.btnExcelSil.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnExcelSil.ForeColor = System.Drawing.Color.Red;
            this.btnExcelSil.Location = new System.Drawing.Point(165, 209);
            this.btnExcelSil.Name = "btnExcelSil";
            this.btnExcelSil.Size = new System.Drawing.Size(134, 38);
            this.btnExcelSil.TabIndex = 35;
            this.btnExcelSil.Text = "Sil";
            this.btnExcelSil.UseVisualStyleBackColor = true;
            this.btnExcelSil.Click += new System.EventHandler(this.btnExcelSil_Click);
            // 
            // yeniDataGrid
            // 
            this.yeniDataGrid.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.yeniDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.yeniDataGrid.DefaultCellStyle = dataGridViewCellStyle4;
            this.yeniDataGrid.Location = new System.Drawing.Point(887, 436);
            this.yeniDataGrid.Name = "yeniDataGrid";
            this.yeniDataGrid.Size = new System.Drawing.Size(378, 192);
            this.yeniDataGrid.TabIndex = 36;
            // 
            // btn_yazdir
            // 
            this.btn_yazdir.Location = new System.Drawing.Point(522, 329);
            this.btn_yazdir.Name = "btn_yazdir";
            this.btn_yazdir.Size = new System.Drawing.Size(100, 45);
            this.btn_yazdir.TabIndex = 38;
            this.btn_yazdir.Text = "Yazdır";
            this.btn_yazdir.UseVisualStyleBackColor = true;
            this.btn_yazdir.Click += new System.EventHandler(this.btn_yazdir_Click);
            // 
            // rdBtnBarkod
            // 
            this.rdBtnBarkod.AutoSize = true;
            this.rdBtnBarkod.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.rdBtnBarkod.Location = new System.Drawing.Point(396, 296);
            this.rdBtnBarkod.Name = "rdBtnBarkod";
            this.rdBtnBarkod.Size = new System.Drawing.Size(158, 28);
            this.rdBtnBarkod.TabIndex = 39;
            this.rdBtnBarkod.TabStop = true;
            this.rdBtnBarkod.Text = "Şekil 1 (barkod)";
            this.rdBtnBarkod.UseVisualStyleBackColor = true;
            // 
            // rdBtnQr
            // 
            this.rdBtnQr.AutoSize = true;
            this.rdBtnQr.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.rdBtnQr.Location = new System.Drawing.Point(594, 296);
            this.rdBtnQr.Name = "rdBtnQr";
            this.rdBtnQr.Size = new System.Drawing.Size(128, 28);
            this.rdBtnQr.TabIndex = 40;
            this.rdBtnQr.TabStop = true;
            this.rdBtnQr.Text = "Şekil 2 (QR)";
            this.rdBtnQr.UseVisualStyleBackColor = true;
            // 
            // btnYenile
            // 
            this.btnYenile.Location = new System.Drawing.Point(887, 654);
            this.btnYenile.Name = "btnYenile";
            this.btnYenile.Size = new System.Drawing.Size(73, 24);
            this.btnYenile.TabIndex = 41;
            this.btnYenile.Text = "Yenile";
            this.btnYenile.UseVisualStyleBackColor = true;
            this.btnYenile.Click += new System.EventHandler(this.btnYenile_Click);
            // 
            // btn_datagridTemizle
            // 
            this.btn_datagridTemizle.Location = new System.Drawing.Point(1180, 655);
            this.btn_datagridTemizle.Name = "btn_datagridTemizle";
            this.btn_datagridTemizle.Size = new System.Drawing.Size(75, 23);
            this.btn_datagridTemizle.TabIndex = 42;
            this.btn_datagridTemizle.Text = "Temizle";
            this.btn_datagridTemizle.UseVisualStyleBackColor = true;
            this.btn_datagridTemizle.Click += new System.EventHandler(this.btn_datagridTemizle_Click);
            // 
            // scannerBilgi
            // 
            this.scannerBilgi.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.scannerBilgi.Location = new System.Drawing.Point(887, 139);
            this.scannerBilgi.Multiline = true;
            this.scannerBilgi.Name = "scannerBilgi";
            this.scannerBilgi.ReadOnly = true;
            this.scannerBilgi.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.scannerBilgi.Size = new System.Drawing.Size(368, 235);
            this.scannerBilgi.TabIndex = 43;
            this.scannerBilgi.TextChanged += new System.EventHandler(this.scannerBilgi_TextChanged);
            // 
            // lbl_Scan
            // 
            this.lbl_Scan.AutoSize = true;
            this.lbl_Scan.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbl_Scan.Location = new System.Drawing.Point(1006, 97);
            this.lbl_Scan.Name = "lbl_Scan";
            this.lbl_Scan.Size = new System.Drawing.Size(151, 25);
            this.lbl_Scan.TabIndex = 44;
            this.lbl_Scan.Text = "Okunan Değer";
            // 
            // lbl_YeniVeri
            // 
            this.lbl_YeniVeri.AutoSize = true;
            this.lbl_YeniVeri.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbl_YeniVeri.Location = new System.Drawing.Point(420, 396);
            this.lbl_YeniVeri.Name = "lbl_YeniVeri";
            this.lbl_YeniVeri.Size = new System.Drawing.Size(134, 25);
            this.lbl_YeniVeri.TabIndex = 45;
            this.lbl_YeniVeri.Text = "Yeni Veri Gir";
            // 
            // btn_port
            // 
            this.btn_port.Location = new System.Drawing.Point(887, 95);
            this.btn_port.Name = "btn_port";
            this.btn_port.Size = new System.Drawing.Size(106, 34);
            this.btn_port.TabIndex = 46;
            this.btn_port.Text = "Okutmaya Başla";
            this.btn_port.UseVisualStyleBackColor = true;
            this.btn_port.Click += new System.EventHandler(this.btn_port_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(1292, 690);
            this.Controls.Add(this.btn_port);
            this.Controls.Add(this.lbl_YeniVeri);
            this.Controls.Add(this.lbl_Scan);
            this.Controls.Add(this.scannerBilgi);
            this.Controls.Add(this.btn_datagridTemizle);
            this.Controls.Add(this.btnYenile);
            this.Controls.Add(this.rdBtnQr);
            this.Controls.Add(this.rdBtnBarkod);
            this.Controls.Add(this.btn_yazdir);
            this.Controls.Add(this.yeniDataGrid);
            this.Controls.Add(this.btnExcelSil);
            this.Controls.Add(this.btnLogGoster);
            this.Controls.Add(this.FormAc);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.excelTablosu);
            this.Controls.Add(this.btnEkle);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnKaydet);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.sqlTablosu);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSil);
            this.Controls.Add(this.btnLoadExcel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.sqlTablosu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.excelTablosu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yeniDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoadExcel;
        private System.Windows.Forms.Button btnSil;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView sqlTablosu;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnKaydet;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button btnEkle;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button FormAc;
        private System.Windows.Forms.Button btnLogGoster;
        private System.Windows.Forms.DataGridView excelTablosu;
        private System.Windows.Forms.Button btnExcelSil;
        private System.Windows.Forms.DataGridView yeniDataGrid;
        private System.Windows.Forms.Timer aktarimTimer;
        private System.Windows.Forms.Button btn_yazdir;
        private System.Windows.Forms.RadioButton rdBtnBarkod;
        private System.Windows.Forms.RadioButton rdBtnQr;
        private System.Windows.Forms.Button btnYenile;
        private System.Windows.Forms.Button btn_datagridTemizle;
        private System.Windows.Forms.TextBox scannerBilgi;
        private System.Windows.Forms.Label lbl_Scan;
        private System.Windows.Forms.Label lbl_YeniVeri;
        private System.Windows.Forms.Button btn_port;
    }
}


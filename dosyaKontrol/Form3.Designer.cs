namespace dosyaKontrol
{
    partial class Form3
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
            this.logKaydi = new System.Windows.Forms.DataGridView();
            this.Temizle = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.logKaydi)).BeginInit();
            this.SuspendLayout();
            // 
            // logKaydi
            // 
            this.logKaydi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.logKaydi.Location = new System.Drawing.Point(21, 39);
            this.logKaydi.Name = "logKaydi";
            this.logKaydi.Size = new System.Drawing.Size(822, 292);
            this.logKaydi.TabIndex = 0;
            this.logKaydi.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.logKaydi_CellContentClick);
            // 
            // Temizle
            // 
            this.Temizle.Location = new System.Drawing.Point(21, 346);
            this.Temizle.Name = "Temizle";
            this.Temizle.Size = new System.Drawing.Size(149, 23);
            this.Temizle.TabIndex = 1;
            this.Temizle.Text = "Logları Temizle";
            this.Temizle.UseVisualStyleBackColor = true;
            this.Temizle.Click += new System.EventHandler(this.Temizle_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(865, 390);
            this.Controls.Add(this.Temizle);
            this.Controls.Add(this.logKaydi);
            this.Name = "Form3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form3";
            this.Load += new System.EventHandler(this.Form3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logKaydi)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView logKaydi;
        private System.Windows.Forms.Button Temizle;
    }
}
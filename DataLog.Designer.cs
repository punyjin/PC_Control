namespace NS
{
    partial class DataLog
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
            this.datalogTextBox = new System.Windows.Forms.RichTextBox();
            this.btn_install = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // datalogTextBox
            // 
            this.datalogTextBox.Location = new System.Drawing.Point(12, 222);
            this.datalogTextBox.Name = "datalogTextBox";
            this.datalogTextBox.Size = new System.Drawing.Size(560, 149);
            this.datalogTextBox.TabIndex = 0;
            this.datalogTextBox.Text = "";
            // 
            // btn_install
            // 
            this.btn_install.Enabled = false;
            this.btn_install.Location = new System.Drawing.Point(497, 377);
            this.btn_install.Name = "btn_install";
            this.btn_install.Size = new System.Drawing.Size(75, 23);
            this.btn_install.TabIndex = 1;
            this.btn_install.Text = "Install";
            this.btn_install.UseVisualStyleBackColor = true;
            this.btn_install.Click += new System.EventHandler(this.btn_install_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(12, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(243, 145);
            this.label1.TabIndex = 2;
            this.label1.Text = "1.netsh int ip reset\r\n2.netsh winsock reset\r\n3.ipconfig /release\r\n4.ipconfig /ren" +
    "ew\r\n5.ipconfig /flushdns";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(458, 50);
            this.label2.TabIndex = 3;
            this.label2.Text = "เปิด CMD ด้วย Run Admin เมื่อหน้าต่าง CMD ขึ้นมา \r\nให้พิมพ์คำสั่งตามนี้";
            // 
            // DataLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 407);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_install);
            this.Controls.Add(this.datalogTextBox);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(600, 446);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 446);
            this.Name = "DataLog";
            this.Text = "DataLog";
            this.Load += new System.EventHandler(this.DataLog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox datalogTextBox;
        private System.Windows.Forms.Button btn_install;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
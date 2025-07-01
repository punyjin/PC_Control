namespace NS
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
            this.btn_start = new System.Windows.Forms.Button();
            this.btn_stop = new System.Windows.Forms.Button();
            this.statusTimer = new System.Windows.Forms.Timer(this.components);
            this.lbl_status = new System.Windows.Forms.Label();
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btn_network = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(107, 90);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(89, 38);
            this.btn_start.TabIndex = 0;
            this.btn_start.Text = "Start";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // btn_stop
            // 
            this.btn_stop.Location = new System.Drawing.Point(12, 90);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(89, 38);
            this.btn_stop.TabIndex = 1;
            this.btn_stop.Text = "Stop";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // statusTimer
            // 
            this.statusTimer.Tick += new System.EventHandler(this.statusTimer_Tick);
            // 
            // lbl_status
            // 
            this.lbl_status.AutoSize = true;
            this.lbl_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lbl_status.Location = new System.Drawing.Point(24, 26);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(163, 25);
            this.lbl_status.TabIndex = 2;
            this.lbl_status.Text = "Software Status";
            this.lbl_status.Click += new System.EventHandler(this.label1_Click);
            // 
            // trayIcon
            // 
            this.trayIcon.Text = "notifyIcon1";
            this.trayIcon.Visible = true;
            // 
            // trayMenu
            // 
            this.trayMenu.Name = "trayMenu";
            this.trayMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // btn_network
            // 
            this.btn_network.Location = new System.Drawing.Point(59, 134);
            this.btn_network.Name = "btn_network";
            this.btn_network.Size = new System.Drawing.Size(102, 39);
            this.btn_network.TabIndex = 4;
            this.btn_network.Text = "Network Speed";
            this.btn_network.UseVisualStyleBackColor = true;
            this.btn_network.Click += new System.EventHandler(this.btn_network_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(213, 185);
            this.Controls.Add(this.btn_network);
            this.Controls.Add(this.lbl_status);
            this.Controls.Add(this.btn_stop);
            this.Controls.Add(this.btn_start);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(229, 224);
            this.MinimumSize = new System.Drawing.Size(229, 224);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Control Panel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.Timer statusTimer;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.ContextMenuStrip trayMenu;
        private System.Windows.Forms.Button btn_network;
    }
}


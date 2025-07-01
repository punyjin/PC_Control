using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            trayMenu = new ContextMenuStrip();
            trayMenu.Items.Add("เปิดหน้าต่าง", null, ShowWindow);
            trayMenu.Items.Add("ออกจากโปรแกรม", null, ExitApp);

            trayIcon = new NotifyIcon
            {
                Icon = SystemIcons.Application,
                ContextMenuStrip = trayMenu,
                Text = "NetSupport Client Controller",
                Visible = true
            };

            trayIcon.DoubleClick += ShowWindow;

            // ย่อโปรแกรมไปที่ Tray เมื่อเริ่ม
            this.Hide();
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false; 
            trayIcon.BalloonTipTitle = "NetSupport Client";
            trayIcon.BalloonTipText = "โปรแกรมกำลังทำงานอยู่เบื้องหลัง";
            trayIcon.ShowBalloonTip(1000);
            statusTimer.Start();
            UpdateStatus();
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            string exePath = @"C:\Program Files (x86)\NetSupport\NetSupport School\client32.exe";

            try
            {
                if (IsProcessRunning("client32"))
                {
                    MessageBox.Show("โปรแกรมกำลังทำงานอยู่แล้ว", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (File.Exists(exePath))
                {
                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = exePath,
                        UseShellExecute = true,
                        Verb = "runas" // รันแบบ Admin
                    };

                    Process.Start(psi);
                    this.Hide();
                    this.ShowInTaskbar = false;

                    trayIcon.BalloonTipTitle = "NetSupport Client";
                    trayIcon.BalloonTipText = "โปรแกรมกำลังทำงานอยู่เบื้องหลัง";
                    trayIcon.ShowBalloonTip(1000);

                }
                else
                {
                    MessageBox.Show("ไม่พบไฟล์โปรแกรมที่ต้องการรัน", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาดในการรันโปรแกรม:\n{ex.Message}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btn_stop_Click(object sender, EventArgs e)
        {
            try
            {
                var processes = Process.GetProcessesByName("client32");
                if (processes.Length == 0)
                {
                    MessageBox.Show("ไม่พบโปรแกรมที่กำลังทำงาน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (var proc in processes)
                {
                    proc.Kill();
                }

                MessageBox.Show("โปรแกรมถูกปิดเรียบร้อยแล้ว", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาดในการปิดโปรแกรม:\n{ex.Message}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        bool IsProcessRunning(string processName)
        {
            return Process.GetProcessesByName(processName).Length > 0;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void ShowWindow(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void ExitApp(object sender, EventArgs e)
        {
            trayIcon.Visible = false;
            Application.Exit();
        }

        private void UpdateStatus()
        {
            bool isRunning = IsProcessRunning("client32");
            lbl_status.Text = isRunning ? "สถานะ: ทำงานอยู่" : "สถานะ: ปิดอยู่";
            lbl_status.ForeColor = isRunning ? Color.Green : Color.Red;

            // Enable/Disable ปุ่ม
            btn_start.Enabled = !isRunning;
            btn_stop.Enabled = isRunning;
        }

        private void statusTimer_Tick(object sender, EventArgs e)
        {
            UpdateStatus();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
                this.ShowInTaskbar = false;
                trayIcon.BalloonTipTitle = "ยังทำงานอยู่";
                trayIcon.BalloonTipText = "โปรแกรมยังคงทำงานอยู่ใน System Tray";
                trayIcon.ShowBalloonTip(2000);
            }
            else
            {
                base.OnFormClosing(e);
            }
        }

        private async void btn_network_Click(object sender, EventArgs e)
        {
            string downloadUrl = "https://1111-releases.cloudflareclient.com/win/latest";
            string filePath = Path.Combine(Path.GetTempPath(), "Cloudflare_WARP_latest.msi"); // ตั้งชื่อไฟล์ตายตัว

            try
            {
                btn_network.Enabled = false;
                btn_network.Text = "กำลังดาวน์โหลด...";

                using (WebClient client = new WebClient())
                {
                    await client.DownloadFileTaskAsync(new Uri(downloadUrl), filePath);
                }

                btn_network.Text = "กำลังติดตั้ง...";

                // เรียกติดตั้งไฟล์ MSI แบบขอสิทธิ์ Admin
                ProcessStartInfo psi = new ProcessStartInfo()
                {
                    FileName = "msiexec",
                    Arguments = $"/i \"{filePath}\" /quiet /norestart",
                    UseShellExecute = true,
                    Verb = "runas" // ขอสิทธิ์ Administrator
                };

                Process installProcess = Process.Start(psi);
                installProcess.WaitForExit(); // รอจนติดตั้งเสร็จ

                MessageBox.Show("ติดตั้ง WARP เสร็จเรียบร้อยแล้ว", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btn_network.Text = "ติดตั้ง WARP";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด:\n{ex.Message}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btn_network.Text = "ติดตั้ง WARP";
            }
            finally
            {
                btn_network.Enabled = true;
            }
        }
    }
}

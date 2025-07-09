using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NS
{
    public partial class DataLog : Form
    {
        public DataLog()
        {
            InitializeComponent();
        }

        private void DataLog_Load(object sender, EventArgs e)
        {
            
        }
        private bool IsRunAsAdministrator()
        {
            return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
        }
        private void InstallBoBlox()
        {
            if (!IsRunAsAdministrator())
            {
                MessageBox.Show("โปรแกรมต้องรันด้วยสิทธิ์ Administrator!", "INSTALLER", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ProcessStartInfo processStartInfo = new ProcessStartInfo("cmd.exe")
            {
                Verb = "runas",
                UseShellExecute = true // ต้องเป็น true เพื่อให้ CMD ปรากฏขึ้น
            };

            try
            {
                Process.Start(processStartInfo);
                MessageBox.Show("กรุณารันคำสั่งต่อไปนี้ใน CMD:\nnetsh int ip reset\nnetsh winsock reset\nipconfig /release\nipconfig /renew\nipconfig /flushdns", "INSTALLER", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}", "INSTALLER", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void RunCommandAsAdministrator(string command)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo("cmd.exe")
            {
                Arguments = "/C " + command,
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                Verb = "runas"
            };

            try
            {
                using (Process process = Process.Start(processStartInfo))
                {
                    process.WaitForExit();
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    // บันทึกคำสั่งที่รันด้วย
                    datalogTextBox.AppendText($"Command: {command}\n");
                    if (!string.IsNullOrEmpty(output))
                    {
                        datalogTextBox.AppendText("Output: " + output + "\n");
                    }
                    if (!string.IsNullOrEmpty(error))
                    {
                        datalogTextBox.AppendText("Error: " + error + "\n");
                    }
                    if (process.ExitCode != 0)
                    {
                        datalogTextBox.AppendText($"Exit Code: {process.ExitCode}\n");
                    }
                }
            }
            catch (Exception ex)
            {
                datalogTextBox.AppendText($"Exception in command '{command}': {ex.Message}\n");
            }
        }

        private void btn_install_Click(object sender, EventArgs e)
        {
            InstallBoBlox();
        }
    }
}

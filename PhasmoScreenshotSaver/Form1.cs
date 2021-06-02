using System.Configuration;
using System.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using System.Runtime.Caching;

namespace PhasmoScreenshotSaver
{
    public partial class Form1 : Form
    {
        Properties.Settings Settings = Properties.Settings.Default;
        public Form1()
        {
            InitializeComponent();
        }

        private void folderBrowserDialog_HelpRequest(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            if (Directory.Exists(Settings.InstallDir))
            {
                textBox1.Text = Settings.InstallDir;
            }
            else
            {
                string subkey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Steam App 739630";

                RegistryKey localKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                textBox1.Text = localKey.OpenSubKey(subkey).GetValue("InstallLocation").ToString();

            }
            if (Directory.Exists(Settings.SaveDir))
            {
                textBox2.Text = Settings.SaveDir;
            }
            else
            {
                textBox2.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            }



        }
       
        private void button3_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = textBox1.Text;
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = textBox2.Text;
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox2.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            //if the form is minimized  
            //hide it from the task bar  
            //and show the system tray icon (represented by the NotifyIcon control)  
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(50000);
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Settings.InstallDir = textBox1.Text;
            Settings.SaveDir = textBox2.Text;
            Settings.Save();

            var watcher = new SimpleBlockAndDelayExample(Settings.InstallDir, this);

            Hide();
            notifyIcon1.Visible = true;
            notifyIcon1.ShowBalloonTip(50000);

        }

        delegate void SetTextCallback(string text);

        public void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.textBox3.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox3.Text += text;
            }
        }

        public void CopyFile (string filePath)
        {
            if (!CheckPaths(true))
            {
                SetText("Error opening save directory\r\n");                
            }
            else
            {
                DateTime now = DateTime.Now;
                string dateTime = now.ToString("yyyy-MM-dd_HHmmss");
                try
                {
                    // Will not overwrite if the destination file already exists.
                    File.Copy(filePath, $"{Settings.SaveDir}\\PhasmophobiaScreenshot_{dateTime}.png");
                    SetText($"Copied new file to {Settings.SaveDir}\\PhasmophobiaScreenshot_{dateTime}.png\r\n");
                }

                // Catch exception if the file was already copied.
                catch (IOException copyError)
                {
                    Console.WriteLine(copyError.Message);
                }
                
                
            }
        }

        private Boolean CheckPaths(bool onlySaveDir)
        {
            bool valid = false;
            if (Directory.Exists(Settings.SaveDir))
            {
                valid = true;
                if (onlySaveDir)
                {
                    return true;
                }
            }
            if (Directory.Exists(Settings.InstallDir) && valid)
            {
                return true;
            }
            return false;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/waggz81/PhasmoScreenshotSaver");
        }
    }
}

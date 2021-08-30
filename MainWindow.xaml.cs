using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Ini;
using Newtonsoft.Json;

namespace RAGEcopycat
{

    public partial class MainWindow : Window
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        const UInt32 WM_CLOSE = 0x0010;
        string maindirr = AppDomain.CurrentDomain.BaseDirectory;

        public static int server_process_id = -1;

        public MainWindow()
        {
            InitializeComponent();
            txtName1.Text = Convert.ToString(Properties.Settings.Default["path1"]);
            txtName2.Text = Convert.ToString(Properties.Settings.Default["path2"]);
            txtName3.Text = Convert.ToString(Properties.Settings.Default["path3"]);
            txtName4.Text = Convert.ToString(Properties.Settings.Default["path4"]);
            txtName5.Text = Convert.ToString(Properties.Settings.Default["path5"]);
            txtName6.Text = Convert.ToString(Properties.Settings.Default["path6"]);
            txtName7.Text = Convert.ToString(Properties.Settings.Default["path7"]);
            txtName8.Text = Convert.ToString(Properties.Settings.Default["path8"]);

            chkbCef.IsChecked = Convert.ToBoolean(Properties.Settings.Default["bool4"]);
            chkbClientside.IsChecked = Convert.ToBoolean(Properties.Settings.Default["bool3"]);
            chkbPublish.IsChecked = Convert.ToBoolean(Properties.Settings.Default["bool1"]);
            chkbServer.IsChecked = Convert.ToBoolean(Properties.Settings.Default["bool5"]);
            chkbServerside.IsChecked = Convert.ToBoolean(Properties.Settings.Default["bool2"]);



        }

        private void TxtName1_TextChanged(object sender, TextChangedEventArgs e)
        {
            Properties.Settings.Default["path1"] = txtName1.Text;
            Properties.Settings.Default.Save();
        }

        private void TxtName2_TextChanged(object sender, TextChangedEventArgs e)
        {
            Properties.Settings.Default["path2"] = txtName2.Text;
            Properties.Settings.Default.Save();
        }

        private void TxtName3_TextChanged(object sender, TextChangedEventArgs e)
        {
            Properties.Settings.Default["path3"] = txtName3.Text;
            Properties.Settings.Default.Save();
        }

        private void TxtName4_TextChanged(object sender, TextChangedEventArgs e)
        {
            Properties.Settings.Default["path4"] = txtName4.Text;
            Properties.Settings.Default.Save();
        }

        private void TxtName5_TextChanged(object sender, TextChangedEventArgs e)
        {
            Properties.Settings.Default["path5"] = txtName5.Text;
            Properties.Settings.Default.Save();
        }

        private void TxtName6_TextChanged(object sender, TextChangedEventArgs e)
        {
            Properties.Settings.Default["path6"] = txtName6.Text;
            Properties.Settings.Default.Save();
        }

        private void TxtName7_TextChanged(object sender, TextChangedEventArgs e)
        {
            Properties.Settings.Default["path7"] = txtName7.Text;
            Properties.Settings.Default.Save();
        }

        private void TxtName8_TextChanged(object sender, TextChangedEventArgs e)
        {
            Properties.Settings.Default["path8"] = txtName8.Text;
            Properties.Settings.Default.Save();
        }


        private void BtnPublish_Click(object sender, RoutedEventArgs e)
        {
            if (txtName1.Text == "")
            {
                MessageBox.Show("Path for dotnet publish is empty", "Execution failed", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            else if (!System.IO.Directory.Exists($@"{txtName1.Text}"))
            {
                MessageBox.Show("Folder for dotnet publish does not exist", "Execution failed", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                //startInfo.Verb = "runas";
                startInfo.WorkingDirectory = txtName1.Text;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/C dotnet publish";
                process.StartInfo = startInfo;
                process.Start();
        }

        private void BtnServerside_Click(object sender, RoutedEventArgs e)
        {
            if (txtName2.Text == "")
            {
                MessageBox.Show("Serverside input path is empty", "Execution failed", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            else if (!System.IO.Directory.Exists($@"{txtName2.Text}"))
            {
                MessageBox.Show("Serverside input folder does not exist", "Execution failed", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            else if (txtName6.Text == "")
            {
                MessageBox.Show("Serverside output path is empty", "Execution failed", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            else if (!System.IO.Directory.Exists($@"{txtName6.Text}"))
            {
                MessageBox.Show("Serverside output folder does not exist", "Execution failed", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                //startInfo.Verb = "runas";
                //startInfo.WorkingDirectory = txtName2.Text;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = $@"/C xcopy /s /e /q /y ""{txtName2.Text}"" ""{ txtName6.Text}""";
                process.StartInfo = startInfo;
                process.Start();
        }

        private void BtnClientside_Click(object sender, RoutedEventArgs e)
        {
            if (txtName3.Text == "")
            {
                MessageBox.Show("Clientside input path is empty", "Execution failed", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            else if (txtName7.Text == "")
            {
                MessageBox.Show("Clientside output path is empty", "Execution failed", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            else if (!System.IO.Directory.Exists($@"{txtName7.Text}"))
            {
                MessageBox.Show("Clientside output folder does not exist", "Execution failed", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                //startInfo.Verb = "runas";
                //startInfo.WorkingDirectory = txtName2.Text;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = $@"/C xcopy /s /q /y ""{txtName3.Text}"" ""{ txtName7.Text}"" ";
                process.StartInfo = startInfo;
                process.Start();
        }

        private void BtnCEF_Click(object sender, RoutedEventArgs e)
        {
            if (txtName4.Text == "")
            {
                MessageBox.Show("CEF input path is empty", "Execution failed", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            else if (!System.IO.Directory.Exists($@"{txtName4.Text}"))
            {
                MessageBox.Show("CEF input folder does not exist", "Execution failed", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            else if (txtName8.Text == "")
            {
                MessageBox.Show("CEF output path is empty", "Execution failed", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            else if (!System.IO.Directory.Exists($@"{txtName8.Text}"))
            {
                MessageBox.Show("CEF output folder does not exist", "Execution failed", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                //startInfo.Verb = "runas";
                //startInfo.WorkingDirectory = txtName2.Text;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = $@"/C xcopy /s /e /q /y ""{txtName4.Text}"" ""{ txtName8.Text}"" ";
                process.StartInfo = startInfo;
                process.Start();
        }

        private void BtnServer_Click(object sender, RoutedEventArgs e)
        {
            if (txtName5.Text == "")
            {
                MessageBox.Show("Server path is empty", "Execution failed", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            else if (!System.IO.Directory.Exists($@"{txtName5.Text}"))
            {
                MessageBox.Show("Server folder does not exist", "Execution failed", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            Process[] processes = Process.GetProcesses();
            foreach (var proc in processes)
            {
                if (proc.MainWindowTitle == $@"{txtName5.Text}\server.exe")
                {
                    proc.Kill();
                }
            }
            System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                //startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                //startInfo.Verb = "runas";
                startInfo.WorkingDirectory = txtName5.Text;
                startInfo.FileName = $@"{txtName5.Text}\server.exe";
                startInfo.Arguments = $@"/K";
                process.StartInfo = startInfo;
                process.Start();
        }

        private void BtnRun_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter w = new StreamWriter(@"runcombo.bat");
            if (Convert.ToBoolean(Properties.Settings.Default["bool1"]))
            {
                if (txtName1.Text == "")
                {
                    MessageBox.Show("Path for dotnet publish is empty", "Execution failed", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }
                else if (!System.IO.Directory.Exists($@"{txtName1.Text}"))
                {
                    MessageBox.Show("Folder for dotnet publish does not exist", "Execution failed", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }
                w.WriteLine($@"cd /d {txtName1.Text}");
                w.WriteLine($@"dotnet publish");

            }
            if (Convert.ToBoolean(Properties.Settings.Default["bool2"]))
            {
                if (txtName2.Text == "")
                {
                    MessageBox.Show("Serverside input path is empty", "Execution failed", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }
                else if (!System.IO.Directory.Exists($@"{txtName2.Text}") && !Convert.ToBoolean(Properties.Settings.Default["bool1"]))
                {
                    MessageBox.Show("Serverside input folder does not exist", "Execution failed", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }
                else if (txtName6.Text == "")
                {
                    MessageBox.Show("Serverside output path is empty", "Execution failed", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }
                else if (!System.IO.Directory.Exists($@"{txtName6.Text}"))
                {
                    MessageBox.Show("Serverside output folder does not exist", "Execution failed", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }
                w.WriteLine($@"xcopy /s /e /q /y ""{txtName2.Text}"" ""{ txtName6.Text}""");
            }
            if (Convert.ToBoolean(Properties.Settings.Default["bool3"]))
            {
                if (txtName3.Text == "")
                {
                    MessageBox.Show("Clientside input path is empty", "Execution failed", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }
                else if (txtName7.Text == "")
                {
                    MessageBox.Show("Clientside output path is empty", "Execution failed", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }
                else if (!System.IO.Directory.Exists($@"{txtName7.Text}"))
                {
                    MessageBox.Show("Clientside output folder does not exist", "Execution failed", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }
                w.WriteLine($@"xcopy /s /q /y ""{txtName3.Text}"" ""{ txtName7.Text}"" ");
            }
            if (Convert.ToBoolean(Properties.Settings.Default["bool4"]))
            {
                if (txtName4.Text == "")
                {
                    MessageBox.Show("CEF input path is empty", "Execution failed", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }
                else if (!System.IO.Directory.Exists($@"{txtName4.Text}"))
                {
                    MessageBox.Show("CEF input folder does not exist", "Execution failed", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }
                if (txtName8.Text == "")
                {
                    MessageBox.Show("CEF output path is empty", "Execution failed", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }
                else if (!System.IO.Directory.Exists($@"{txtName8.Text}"))
                {
                    MessageBox.Show("CEF output folder does not exist", "Execution failed", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }
                w.WriteLine($@"xcopy /s /e /q /y ""{txtName4.Text}"" ""{ txtName8.Text}"" ");
            }
            if (Convert.ToBoolean(Properties.Settings.Default["bool5"]))
            {
                if (txtName5.Text == "")
                {
                    MessageBox.Show("Server path is empty", "Execution failed", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }
                else if (!System.IO.Directory.Exists($@"{txtName5.Text}"))
                {
                    MessageBox.Show("Server folder does not exist", "Execution failed", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }
                Process[] processes = Process.GetProcesses();
                    foreach (var proc in processes)
                    {
                        if (proc.MainWindowTitle == $@"{txtName5.Text}\server.exe")
                        {
                            proc.Kill();
                        }
                    }
                    w.WriteLine($@"cd /d {txtName5.Text}");
                    w.WriteLine($@"start /B server.exe");
            }
            w.Close();
            System.Diagnostics.Process.Start($@"{maindirr}\runcombo.bat");

        }

        private void ChkbPublish_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default["bool1"] = true;
            Properties.Settings.Default.Save();
        }

        private void ChkbPublish_Unchecked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default["bool1"] = false;
            Properties.Settings.Default.Save();
        }

        private void ChkbServerside_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default["bool2"] = true;
            Properties.Settings.Default.Save();
        }

        private void ChkbServerside_Unchecked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default["bool2"] = false;
            Properties.Settings.Default.Save();
        }

        private void ChkbClientside_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default["bool3"] = true;
            Properties.Settings.Default.Save();
        }

        private void ChkbClientside_Unchecked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default["bool3"] = false;
            Properties.Settings.Default.Save();
        }

        private void ChkbCef_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default["bool4"] = true;
            Properties.Settings.Default.Save();
        }

        private void ChkbCef_Unchecked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default["bool4"] = false;
            Properties.Settings.Default.Save();
        }

        private void ChkbServer_Checked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default["bool5"] = true;
            Properties.Settings.Default.Save();
        }

        private void ChkbServer_Unchecked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default["bool5"] = false;
            Properties.Settings.Default.Save();
        }
    }
}

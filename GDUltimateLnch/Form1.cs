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

namespace GDUltimateLnch
{
    public partial class GDUL : Form
    {
        public static string version = "d0.0.2";
        public static bool update;
        public GDUL()
        {            
            Directory.CreateDirectory(@"C:\GDUL");
            Directory.CreateDirectory(@"C:\GDUL\Logs");
            Directory.CreateDirectory(@"C:\GDUL\Temp");
            InitializeComponent();
            ShouldUpdate();
        }

        public static void ShouldUpdate()
        {       
            if (!File.Exists(@"C:\GDUL\Temp\ver.txt")) return;
            try
            {
                string serverVersion = File.ReadAllText(@"C:\GDUL\Temp\ver.txt");
                if(serverVersion[5] > version[5])
                {
                    MessageBox.Show($"Вышло обновление! У вас всегда есть возможность обновиться через GitHub.", "Обновление", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    update = true;
                }
                else
                {
                    File.Delete(@"C:\GDUL\Temp\ver.txt");
                    update = false;
                }
            }
            catch(Exception ex)
            {
                File.WriteAllText(@"C:\GDUL\Logs\log.txt", "[" + DateTime.UtcNow + "] Вызвано исключение: " + ex.Message);
                MessageBox.Show($"Что-то пошло не так.\nИнформация об исключении, которую стоит передать Creepy0964, занесена в логи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

	    private void button1_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(richTextBox1.Text);
            startInfo.WorkingDirectory = @"C:\GDUL\Game";
            try
            {
                Process.Start(startInfo);             
            }
            catch(Exception ex)
            {
                File.WriteAllText(@"C:\GDUL\Logs\log.txt", "[" + DateTime.UtcNow + "] Вызвано исключение: " + ex.Message);
                MessageBox.Show($"Что-то пошло не так.\nИнформация об исключении, которую стоит передать Creepy0964, занесена в логи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Было приятно провести с Вами время. До скорых встреч!", "Вы уже уходите? :(", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            Process.GetCurrentProcess().Kill();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void GDUL_Load(object sender, EventArgs e)
        {
            if(update == false)
            {
                button5.Enabled = false;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("GDUL - лаунчер для Geometry Dash на C#, позволяющий запускать чистую GD, GD с модами и даже приваты с модами или же без! Создано EGA Team.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/Creepy0964/GDUL/releases");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/Creepy0964/GDUL");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(@"C:\GDUL\Game");
            }
            catch(Exception ex)
            {
                File.WriteAllText(@"C:\GDUL\Logs\log.txt", "[" + DateTime.UtcNow + "] Вызвано исключение: " + ex.Message);
                MessageBox.Show($"Что-то пошло не так.\nИнформация об исключении, которую стоит передать Creepy0964, занесена в логи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(@"C:\GDUL\Game\mods");
            }
            catch (Exception ex)
            {
                File.WriteAllText(@"C:\GDUL\Logs\log.txt", "[" + DateTime.UtcNow + "] Вызвано исключение: " + ex.Message);
                MessageBox.Show($"Что-то пошло не так.\nИнформация об исключении, которую стоит передать Creepy0964, занесена в логи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
    }
}

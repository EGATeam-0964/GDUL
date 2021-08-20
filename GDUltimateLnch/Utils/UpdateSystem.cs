using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GDUltimateLnch.Utils
{
    public static class UpdateSystem
    {
        private const string ServerVersionEndpoint = @"";
        private const string GithubReleasesUrl = @"";
        public static readonly Version CurrentVersion = new Version("0.0.2");

        /// <summary>
        /// Get the latest version on the server.
        /// </summary>
        public static Version GetServerVersion()
        {
            try
            {
                using (var client = new HttpClient())
                    return new Version(client.GetAsync(ServerVersionEndpoint).Result.Content.ReadAsStringAsync().Result);
            }
            catch { }
            return null;
        }

        /// <summary>
        /// Check version.
        /// </summary>
        /// <returns>A <see cref="bool"/>, which is true if upfate is required.</returns>
        
        public static void Ping()
        {
            try
            {
                Ping myPing = new Ping();
                String host = "google.com";
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                if (reply.Status == IPStatus.Success)
                {
                    return;
                }
            }
            catch(System.Net.NetworkInformation.PingException ex)
            {
                MessageBox.Show($"Невозможно проверить наличие обновлений. Вы подключены к интернету?",
                            "Обновление",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1,
                            MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        public static bool CheckVersion(bool notifyUser = true)
        {
            Ping();
            Version server = GetServerVersion();
            if (server == null)
            {
                if (notifyUser)
                    new Task(() =>
                    {
                        MessageBox.Show($"Ошибка сервера. Проверка обновлений невозможна.",
                            "Обновление",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1,
                            MessageBoxOptions.DefaultDesktopOnly);
                    }).Start();
                return false;
            }
            Version client = CurrentVersion;

            if (server > client)
            {
                if (notifyUser)
                {
                    new Task(() =>
                    {
                        var result = MessageBox.Show($"Вышло обновление! У вас всегда есть возможность обновиться через GitHub. Желаете перейти на страницу релизов проекта?",
                            "Обновление",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1,
                            MessageBoxOptions.DefaultDesktopOnly);
                        if (result == DialogResult.Yes)
                            Process.Start(GithubReleasesUrl);
                    }).Start();
                }
                return true;
            }
            return false;
        }
    }
}

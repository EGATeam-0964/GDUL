using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GDUltimateLnch.Utils
{
    /// <summary>
    /// Class to log crashes and send them to the remote.
    /// </summary>
    public static class CrashReporter
    {
        private const string ReportServerEndpoint = @"";

        /// <summary>
        /// Report an exception and log it to a file.
        /// </summary>
        /// <param name="exception">An exception.</param>
        /// <param name="shouldNotice">Shows whether a user should see the message.</param>
        /// <returns>Log file path.</returns>
        public static string ReportException(Exception exception, bool shouldNotice = true)
        {
            try
            {
                using (var client = new HttpClient())
                    client.PostAsync(ReportServerEndpoint, new StringContent(exception.ToString(), Encoding.UTF8, "text/text"));
            }
            catch { }
            string logFilePath = Path.GetTempFileName();
            File.AppendAllText(logFilePath, exception.ToString());
            if (shouldNotice)
                new Task(() => MessageBox.Show($"Что-то пошло не так.\nИнформация об исключении, которую стоит передать Creepy0964, занесена в логи. Путь к лог-файлу:\n\n{logFilePath}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly)).Start();
            return logFilePath;
        }
    }
}

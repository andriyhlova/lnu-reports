using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace SRS.Services.Interfaces.ReportGeneration
{
    public class TexReportBuilderService : ITexReportBuilderService
    {
        public string Build(string htmlText)
        {
            var fileUniqueId = Guid.NewGuid();
            var file = Path.Combine(ConfigurationManager.AppSettings["HtmlFilePath"], $"{fileUniqueId}.html");
            File.WriteAllText(file, htmlText);
            var result = new StringBuilder();
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = Path.Combine(ConfigurationManager.AppSettings["PandocPath"], "pandoc.exe"),
                    Arguments = $"--from html {file} --to latex -s --wrap=preserve",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    StandardOutputEncoding = Encoding.GetEncoding(866)
                }
            };
            proc.Start();

            var i = 0;
            while (!proc.StandardOutput.EndOfStream)
            {
                var line = proc.StandardOutput.ReadLine();
                result.AppendLine(line);
                result.AppendLine("\n");
                i++;
                if (i == 8)
                {
                    result.AppendLine(@"\usepackage[ukrainian]{babel}");
                    result.Append("\n");
                }
            }

            File.Delete(file);
            return result.ToString();
        }
    }
}

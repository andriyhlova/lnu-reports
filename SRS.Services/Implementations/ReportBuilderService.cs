using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace SRS.Services.Interfaces
{
    public class ReportBuilderService<TModel> : IReportBuilderService<TModel>
    {
        private readonly IHtmlCompiler _htmlCompiler;

        public ReportBuilderService(IHtmlCompiler htmlCompiler)
        {
            _htmlCompiler = htmlCompiler;
        }

        public string BuildHtml(string templateName, TModel model)
        {
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ReportTemplates");
            var templateText = File.ReadAllText(Path.Combine(path, templateName + ".html"));
            return _htmlCompiler.Compile(templateText, model);
        }

        public string BuildTex(string templateName, TModel model)
        {
            var htmlText = BuildHtml(templateName, model);
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

            return result.ToString();
        }
    }
}

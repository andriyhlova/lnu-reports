using System.IO;
using System.Reflection;

namespace SRS.Services.Interfaces.ReportGeneration
{
    public class HtmlReportBuilderService<TModel> : IHtmlReportBuilderService<TModel>
    {
        private readonly IHtmlCompiler _htmlCompiler;

        public HtmlReportBuilderService(IHtmlCompiler htmlCompiler)
        {
            _htmlCompiler = htmlCompiler;
        }

        public string Build(string templateName, TModel model)
        {
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase), "ReportTemplates").Replace("file:\\", string.Empty);
            var templateText = File.ReadAllText(Path.Combine(path, templateName + ".html"));
            return _htmlCompiler.Compile(templateText, model);
        }
    }
}

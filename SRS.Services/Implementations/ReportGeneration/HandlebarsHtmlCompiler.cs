using HandlebarsDotNet;
using SRS.Services.Interfaces.ReportGeneration;

namespace SRS.Services.Implementations.ReportGeneration
{
    public class HandlebarsHtmlCompiler : IHtmlCompiler
    {
        static HandlebarsHtmlCompiler()
        {
            Handlebars.RegisterHelper("inc", (writer, _, arguments) => writer.WriteSafeString((int)arguments[0] + 1));
        }

        public string Compile<TModel>(string templateText, TModel model)
        {
            var template = Handlebars.Compile(templateText);
            return template(model);
        }
    }
}

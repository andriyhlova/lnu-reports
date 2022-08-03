using HandlebarsDotNet;
using SRS.Services.Interfaces;

namespace SRS.Services.Implementations
{
    public class HandlebarsHtmlCompiler : IHtmlCompiler
    {
        public string Compile<TModel>(string templateText, TModel model)
        {
            var template = Handlebars.Compile(templateText);
            return template(model);
        }
    }
}

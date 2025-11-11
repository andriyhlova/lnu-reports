using HandlebarsDotNet;
using SRS.Services.Interfaces.ReportGeneration;
using System.Collections;

namespace SRS.Services.Implementations.ReportGeneration
{
    public class HandlebarsHtmlCompiler : IHtmlCompiler
    {
        static HandlebarsHtmlCompiler()
        {
            Handlebars.RegisterHelper("inc", (writer, _, arguments) => writer.WriteSafeString((int)arguments[0] + 1));

            Handlebars.RegisterHelper("anyNotEmptyString", (writer, options, context, arguments) =>
            {
                bool anyIsNotNullOrWhiteSpace = false;

                foreach (var arg in arguments)
                {
                    if (arg != null)
                    {
                        if (arg is string s)
                        {
                            if (!string.IsNullOrWhiteSpace(s))
                            {
                                anyIsNotNullOrWhiteSpace = true;
                                break;
                            }
                        }
                        else
                        {
                            anyIsNotNullOrWhiteSpace = true;
                            break;
                        }
                    }
                }

                if (anyIsNotNullOrWhiteSpace)
                {
                    options.Template(writer, context);
                }
                else
                {
                    options.Inverse(writer, context);
                }
            });

            Handlebars.RegisterHelper("anyNotEmptyList", (writer, options, context, arguments) =>
            {
                bool anyIsNotEmpty = false;
                foreach (var arg in arguments)
                {
                    if (arg is IEnumerable enumerable)
                    {
                        var enumerator = enumerable.GetEnumerator();
                        if (enumerator.MoveNext())
                        {
                            anyIsNotEmpty = true;
                        }
                    }
                }

                if (anyIsNotEmpty)
                {
                    options.Template(writer, context);
                }
                else
                {
                    options.Inverse(writer, context);
                }
            });
        }

        public string Compile<TModel>(string templateText, TModel model)
        {
            var template = Handlebars.Compile(templateText);
            return template(model);
        }
    }
}

using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using HtmlToOpenXml;

namespace SRS.Services.Interfaces.ReportGeneration
{
    public class WordReportBuilderService : IWordReportBuilderService
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S3220:Method calls should not resolve ambiguously to overloads with \"params\"", Justification = "Needed for word report generation")]
        public byte[] Build(string htmlText)
        {
            using (var stream = new MemoryStream())
            {
                using (var package = WordprocessingDocument.Create(stream, WordprocessingDocumentType.Document))
                {
                    var mainPart = package.MainDocumentPart;
                    if (mainPart == null)
                    {
                        mainPart = package.AddMainDocumentPart();
                        var document = new Document(new Body());
                        document.Save(mainPart);
                    }

                    var converter = new HtmlConverter(mainPart);
                    converter.ParseHtml(htmlText);

                    mainPart.Document.Save();
                }

                return stream.ToArray();
            }
        }
    }
}

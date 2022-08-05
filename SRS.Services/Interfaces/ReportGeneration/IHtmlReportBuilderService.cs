namespace SRS.Services.Interfaces.ReportGeneration
{
    public interface IHtmlReportBuilderService<in TModel>
    {
        string Build(string templateName, TModel model);
    }
}

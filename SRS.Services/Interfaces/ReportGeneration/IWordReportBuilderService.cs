namespace SRS.Services.Interfaces.ReportGeneration
{
    public interface IWordReportBuilderService
    {
        byte[] Build(string htmlText);
    }
}

namespace SRS.Services.Interfaces.ReportGeneration
{
    public interface IHtmlCompiler
    {
        string Compile<TModel>(string templateText, TModel model);
    }
}

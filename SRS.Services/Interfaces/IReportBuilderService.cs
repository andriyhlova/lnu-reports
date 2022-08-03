using System.Threading.Tasks;

namespace SRS.Services.Interfaces
{
    public interface IReportBuilderService<in TModel>
    {
        string BuildHtml(string templateName, TModel model);

        string BuildTex(string templateName, TModel model);
    }
}

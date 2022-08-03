using System.Threading.Tasks;

namespace SRS.Services.Interfaces
{
    public interface IHtmlCompiler
    {
        string Compile<TModel>(string templateText, TModel model);
    }
}

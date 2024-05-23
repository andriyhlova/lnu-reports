using System.Linq;

namespace SRS.Web.Services
{
    public class FieldInfo
    {
        public int Type { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            var values = new string[] { Type.ToString(), Name }.Where(x => !string.IsNullOrWhiteSpace(x));
            return string.Join(";", values);
        }
    }
}
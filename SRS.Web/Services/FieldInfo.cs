using System.Linq;

namespace SRS.Web.Services
{
    public class FieldInfo
    {
        public int Type { get; set; }

        public string Name { get; set; }

        public bool IsRequired { get; set; }

        public override string ToString()
        {
            if (IsRequired)
            {
                return $"{Type};{Name};1";
            }

            var values = new string[] { Type.ToString(), Name }.Where(x => !string.IsNullOrWhiteSpace(x));
            return string.Join(";", values);
        }
    }
}
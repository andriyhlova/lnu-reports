namespace SRS.Services.Implementations.Bibliography
{
    public abstract class BaseBibliographyService
    {
        protected string GetBibliographyPart(string prefix, string info)
        {
            return string.IsNullOrWhiteSpace(info) ? string.Empty : prefix + info;
        }

        protected string GetPartWithDot(string part)
        {
            if (string.IsNullOrWhiteSpace(part))
            {
                return part;
            }

            return part + (!part.EndsWith(".") ? "." : string.Empty);
        }
    }
}

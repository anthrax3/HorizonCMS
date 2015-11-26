
using System.Globalization;
using System.Text;
namespace Horizon.Web.Helpers
{
    public static class UrlHelper
    {
        public static string SanitizeUrl(string url)
        {
            string result = string.Empty;

            result = RemoveDiacritics(url.Replace(" ", "-").ToLower());

            return result;
        }

        static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
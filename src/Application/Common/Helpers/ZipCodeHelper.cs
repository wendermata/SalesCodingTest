using System.Text.RegularExpressions;

namespace Application.Common.Helpers
{
    public static class ZipCodeHelper
    {
        public static bool IsValid(string cep)
        {
            string pattern = @"^\d{5}-\d{3}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(cep);
        }
    }
}

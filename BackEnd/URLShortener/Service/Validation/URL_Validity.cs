using System;
using System.Text.RegularExpressions;

public static class URL_Validity
{
    private const string UrlPattern = @"^(https?|ftps?):\/\/(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?\.)+[a-zA-Z]{2,}(?::(?:0|[1-9]\d{0,3}|[1-5]\d{4}|6[0-4]\d{3}|65[0-4]\d{2}|655[0-2]\d|6553[0-5]))?(?:\/(?:[-a-zA-Z0-9@%_\+.~#?&=]+\/?)*)?$";

    public static bool CheckValidityWithRegex(string url)
    {
        try
        {
            var urlRegex = new Regex(UrlPattern, RegexOptions.IgnoreCase);
            return urlRegex.IsMatch(url);
        }
        catch (ArgumentException)
        {
            
            return false;
        }
    }
}

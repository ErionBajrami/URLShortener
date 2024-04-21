namespace URLShortener.Service.Validation { 
class UrlToValidate
{
    static void Main()
    {
        Console.WriteLine("Enter the URL to validate:");
        string url = Console.ReadLine();

        bool isValid = URL_Validity.CheckValidityWithRegex(url);

        if (isValid)
        {
            Console.WriteLine("URL is valid.");
        }
        else
        {
            Console.WriteLine("URL is not valid.");
        }
    }
}
}

namespace NewsLetter.Application.Extensions;
public static class CommonExtensions
{
    public static string ConvertTitleToUrl(this string str)
    {
        Dictionary<string, string> characters = new()
        {
            { "ü", "u" },
            { "ş", "s" },
            { "ı", "i" },
            { "ö", "o" },
            { "ç", "c" },
            { "ğ", "g" },
            { "#", "sharp" },
        };

        var url = str.ToLower();
        foreach (var character in characters)
        {
            url = url.Replace(character.Key, character.Value);
        }

        var urls = url.Split(" ");
        url = string.Join("-", urls);

        return url;
    }
}

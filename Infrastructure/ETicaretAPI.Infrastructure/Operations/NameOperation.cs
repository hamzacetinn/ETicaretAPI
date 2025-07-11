﻿namespace ETicaretAPI.Infrastructure.Operations
{
    public static class NameOperation
    {
        public static string CharacterRegulatory(string name)
        => name.Replace("/", "")
            .Replace("!", "")
            .Replace("'", "")
            .Replace("^", "")
            .Replace("+", "")
            .Replace("%", "")
            .Replace("&", "")
            .Replace("(", "")
            .Replace(")", "")
            .Replace("=", "")
            .Replace("?", "")
            .Replace("_", "")
            .Replace("", "")
            .Replace("@", "")
            .Replace("€", "")
            .Replace("æ", "")
            .Replace("ß", "")
            .Replace("Æ", "")
            .Replace("¨", "")
            .Replace("´", "")
            .Replace(",", "")
            .Replace(";", "")
            .Replace(".", "-")
            .Replace(":", "")
            .Replace("<", "")
            .Replace(">", "")
            .Replace("|", "")
            .Replace("*", "")
            .Replace("{", "")
            .Replace("}", "")
            .Replace("[", "")
            .Replace("]", "")
            .Replace("$", "")
            .Replace("#", "")
            .Replace("~", "")
            .Replace("Ö", "O")
            .Replace("ö", "o")
            .Replace("Ü", "U")
            .Replace("ü", "u")
            .Replace("Ğ", "G")
            .Replace("ğ", "g")
            .Replace("Ç", "C")
            .Replace("ç", "c")
            .Replace("İ", "I")
            .Replace("ı", "i")
            .Replace("Ş", "S")
            .Replace("ş", "s")
            .Replace("Â", "A")
            .Replace("â", "a")
            .Replace("Î", "I")
            .Replace("î", "i")
            .Replace("Û", "U")
            .Replace("û", "u");

    }
}

using System;

public static class GerarCodigo
{
    
    public static string GenerateCode(int length)
    {
        const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return GenerateRandomString(allowedChars, length);
    }

    
    public static string GenerateNumericCode(int length)
    {
        const string allowedChars = "0123456789";
        return GenerateRandomString(allowedChars, length);
    }

    private static string GenerateRandomString(string allowedChars, int length)
    {
        char[] code = new char[length];
        Random random = new Random();

        for (int i = 0; i < length; i++)
        {
            code[i] = allowedChars[random.Next(allowedChars.Length)];
        }

        return new string(code);
    }
}

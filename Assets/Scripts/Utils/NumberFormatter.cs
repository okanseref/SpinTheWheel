using System;

public static class NumberFormatter
{
    private static readonly string[] Suffixes = { "", "K", "M", "B" };
    private const int Scale = 1000;
    
    public static string FormatCompact(int number)
    {
        if (number == 0) return "0";
        
        int magnitude = (int)Math.Log(Math.Abs(number), Scale);
        magnitude = Math.Min(magnitude, Suffixes.Length - 1);
        
        double scaledNumber = number / Math.Pow(Scale, magnitude);
        
        // Handle rounding cases like 999999 → 1000K → 1M
        if (Math.Abs(scaledNumber) >= Scale && magnitude < Suffixes.Length - 1)
        {
            scaledNumber /= Scale;
            magnitude++;
        }
        
        // Format without decimal places for whole numbers
        return scaledNumber % 1 == 0 
            ? $"{scaledNumber:0}{Suffixes[magnitude]}" 
            : $"{scaledNumber:0.##}{Suffixes[magnitude]}";
    }
}
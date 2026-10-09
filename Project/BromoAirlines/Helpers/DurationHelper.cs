using System.Text.RegularExpressions;

namespace BromoAirlines.Helpers;

/// <summary>Helper durasi: menit <-> "XX jam YY menit", sesuai aturan L/M/N.</summary>
public static class DurationHelper
{
    public static string ToText(int totalMenit)
    {
        if (totalMenit <= 0) return "0 jam 0 menit";
        return $"{totalMenit / 60} jam {totalMenit % 60} menit";
    }

    public static string ToDelayText(int totalMenit) => $"Delay (selama ±{ToText(totalMenit)})";

    public static bool TryParse(string text, out int totalMenit)
    {
        totalMenit = 0;
        if (string.IsNullOrWhiteSpace(text)) return false;
        var m = Regex.Match(text.Trim(), @"^(\d+)\s*jam\s*(\d+)\s*menit$");
        if (!m.Success) return false;
        if (!int.TryParse(m.Groups[1].Value, out var jam)) return false;
        if (!int.TryParse(m.Groups[2].Value, out var menit)) return false;
        if (menit is < 0 or > 59) return false;
        totalMenit = jam * 60 + menit;
        return totalMenit > 0;
    }

    public static bool IsValid(int totalMenit) => totalMenit > 0;
}

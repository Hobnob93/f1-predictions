namespace F1Predictions.Core.Extensions;

public static class IntegerExtensions
{
    public static string AsPosition(this int i)
    {
        return $"{i}{GetPostfix(i)}";
    }

    private static string GetPostfix(int i) => i switch
    {
        1 => "st",
        2 => "nd",
        3 => "rd",
        _ => "th"
    };
}
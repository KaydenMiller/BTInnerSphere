using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace KaydenMiller.BattleTech.Core;

public struct GalacticCoordinates
{
    [JsonPropertyName("x")] 
    public float X { get; set; } = 0f;
    [JsonPropertyName("y")]
    public float Y { get; set; } = 0f;
    [JsonPropertyName("areKnown")] 
    public bool AreKnown { get; set; } = true;

    private GalacticCoordinates(float x, float y, bool areKnown)
    {
        X = x;
        Y = y;
        AreKnown = areKnown;
    }

    public static GalacticCoordinates Create(float x, float y, bool areKnown = true)
    {
        return new GalacticCoordinates(x, y, areKnown);
    }

    public static GalacticCoordinates Parse(string coords)
    {
        var regex = new Regex(@"(-?\d+\.?\d*).*?:.*?(-?\d+\.?\d*)");

        if (string.IsNullOrWhiteSpace(coords.Trim()))
        {
            return new GalacticCoordinates(0f, 0f, false);
        }

        var groups = regex.Match(coords.Trim()).Groups;
        var x = float.Parse(groups[1].Value);
        var y = float.Parse(groups[2].Value);
        return new GalacticCoordinates(x, y, true);
    }

    public override string ToString()
    {
        return $"{X:F3}:{Y:F3}";
    }
}
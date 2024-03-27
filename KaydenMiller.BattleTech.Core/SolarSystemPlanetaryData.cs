using System.Globalization;
using System.Text.Json.Serialization;

namespace KaydenMiller.BattleTech.Core;

public struct SolarSystemPlanetaryData
{
    [JsonPropertyName("planets")]
    public readonly uint Planets = 0;
    [JsonPropertyName("artificialStructures")]
    public readonly uint ArtificialStructures = 0;

    public SolarSystemPlanetaryData(uint planets, uint artificialStructures)
    {
        Planets = planets;
        ArtificialStructures = artificialStructures;
    }
    
    public static SolarSystemPlanetaryData Parse(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            // no data provided
            return new SolarSystemPlanetaryData(0, 0);
        }

        if (uint.TryParse(input, out var planets))
        {
            return new SolarSystemPlanetaryData(planets, 0);
        }
        
        
    }
}
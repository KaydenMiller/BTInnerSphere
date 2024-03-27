using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace KaydenMiller.BattleTech.Core;

public struct SolarSystemPlanetaryData
{
    [JsonPropertyName("planets")] 
    public uint Planets { get; set; } = 0;

    [JsonPropertyName("artificialStructures")]
    public uint ArtificialStructures { get; set; } = 0;

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

        if (!input.Contains(','))
            return new SolarSystemPlanetaryData(0, 0);

        var values = input.Split(',');

        var planetaryDictionary = new Dictionary<string, string>();
        foreach (var planetaryBodies in values)
        {
            Match match = Regex.Match(planetaryBodies.Trim(), """^(\d+)\s*([a-zA-Z ]*)""");
            var value = match.Groups[1].Value;
            var key = match.Groups[2].Value;
            planetaryDictionary.TryAdd(key.Trim(), value.Trim());
        }

        var knownPlanets = uint.Parse(planetaryDictionary.GetValueOrDefault("planets") ?? "0");
        var structures = uint.Parse(planetaryDictionary.GetValueOrDefault("artificial structure") ?? "0");

        return new SolarSystemPlanetaryData(knownPlanets, structures);
    }
}
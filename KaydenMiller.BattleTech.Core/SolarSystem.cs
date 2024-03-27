using System.Text.Json.Serialization;

namespace KaydenMiller.BattleTech.Core;

public class SolarSystem
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("coordinates")]
    public required GalacticCoordinates Coordinates { get; set; }

    [JsonPropertyName("planets")]
    public uint? PlanetCount { get; set; }
    
    [JsonPropertyName("spectralClass")]
    public SpectralClass? SpectralClass { get; set; }

    [JsonPropertyName("rechargeStations")]
    public IEnumerable<RechargeStationType>? RechargeStations { get; set; }
    
    [JsonPropertyName("wikiUrl")]
    public string? WikiUrl { get; set; }
    
    [JsonPropertyName("metadata")]
    public Dictionary<string, string>? Metadata { get; set; }
}
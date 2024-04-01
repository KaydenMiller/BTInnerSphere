using System.Text.Json.Serialization;

namespace KaydenMiller.BattleTech.Core;

public class SolarSystem
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("coordinates")]
    public GalacticCoordinates Coordinates { get; set; }

    [JsonPropertyName("planetaryData")]
    public SolarSystemPlanetaryData? PlanetaryData { get; set; }

    [JsonPropertyName("systemRechargeTime")]
    public SystemRechargeTime? RechargeTime { get; set; }
    
    [JsonPropertyName("spectralClassifications")]
    public List<SpectralClassification>? SpectralClassifications { get; set; }

    [JsonPropertyName("rechargeStations")]
    public List<RechargeStationType>? RechargeStations { get; set; }

    [JsonPropertyName("politicalAffiliations")]
    public List<PoliticalAffiliation>? PoliticalAffiliations { get; set; }
    
    [JsonPropertyName("wikiUrl")]
    public string? WikiUrl { get; set; }
    
    [JsonPropertyName("metadata")]
    public Dictionary<string, string?>? Metadata { get; set; }
}
using System.Text.Json.Serialization;

namespace KaydenMiller.BattleTech.Core;

public class PoliticalAffiliationFaction
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("percentOccupation")]
    public required int PercentOfOccupation { get; set; }
    [JsonPropertyName("percentOfOccupationKnown")]
    public required bool PercentOfOccupationKnown { get; set; }
}
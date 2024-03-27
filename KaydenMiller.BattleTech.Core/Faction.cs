using System.Text.Json.Serialization;

namespace KaydenMiller.BattleTech.Core;

public class Faction
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("foundingYear")]
    public required DateOnly FoundingYear { get; set; }

    [JsonPropertyName("army")]
    public required Army Army { get; set; }

    [JsonPropertyName("navy")]
    public required Navy Navy { get; set; }

    [JsonPropertyName("intelligence")]
    public required MilitaryIntelligence MilitaryIntelligence { get; set; }
}

public class Army
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("abbr")]
    public required string Abbreviation { get; set; }
}

public class Navy
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("abbr")]
    public required string Abbreviation { get; set; }
}

public class MilitaryIntelligence 
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("abbr")]
    public required string Abbreviation { get; set; }
}
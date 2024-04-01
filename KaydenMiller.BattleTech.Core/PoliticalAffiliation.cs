using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace KaydenMiller.BattleTech.Core;

public class PoliticalAffiliation
{
    [JsonPropertyName("year")]
    public required int Year { get; set; }

    [JsonPropertyName("approxEndYear")]
    public int? ApproximateEndYear { get; set; }
    
    [JsonPropertyName("factions")]
    public required List<string> Factions { get; set; }
    [JsonPropertyName("factionWikiUrl")]
    public required string? FactionWikiUrl { get; set; }

    [JsonPropertyName("isApproximate")]
    public bool Approximate { get; set; } = false;
    [JsonPropertyName("includePreviousYears")]
    public bool IncludesPreviousYears { get; set; }

    public static PoliticalAffiliation Parse(string input, string? factionWikiUrl = null)
    {
        var dateSplitter = Regex.Match(input, """^(.*?)\s-\s(.*)$""");
        var dateSection = dateSplitter.Groups[1].Value;
        var dateSectionMatches = Regex.Match(dateSection, """^(ca\.|pre-)?\s*([0-9-–]+)$""");
        var approximateData = dateSectionMatches.Groups[1].Value.Trim(); 
        var yearData = Regex.Match(dateSectionMatches.Groups[2].Value.Trim(), """^(\d+)-?–?(\d+)?$""");
        
        var factionSection = dateSplitter.Groups[2].Value;
        var factionMatches = Regex.Match(factionSection, """^(.*?)(\/.*|$)""");

        List<string> factions = [];
        
        
        var firstFaction = factionMatches.Groups[1].Value.Replace('/', ' ').Trim();
        var firstFactionName = Regex.Match(firstFaction, """^([a-zA-Z0-9 \(\)]+)""").Groups[1].Value;
        factions.Add(firstFactionName.Trim());

        if (string.IsNullOrWhiteSpace(factionMatches.Groups[2].Value) is false)
        {
            // There is another faction
            var secondFaction = factionMatches.Groups[2].Value.Replace('/', ' ').Trim();
            var secondFactionName = Regex.Match(secondFaction, """^([a-zA-Z0-9 \(\)]+)""").Groups[1].Value;
            factions.Add(secondFactionName.Trim());
        }
        
        
        var isApproximate = approximateData.Equals("ca.");
        var includePreviousYears = approximateData.Equals("pre-");
        var startYear = int.Parse(yearData.Groups[1].Value);
        int? endYear = null;
        if (isApproximate)
        {
            endYear = int.Parse(yearData.Groups[2].Value);
        }

        return new PoliticalAffiliation
        {
            Year = startYear,
            ApproximateEndYear = endYear,
            Factions = factions,
            Approximate = isApproximate,
            IncludesPreviousYears = includePreviousYears,
            FactionWikiUrl = null,
        };
    }
}
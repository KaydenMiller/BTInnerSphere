using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace KaydenMiller.BattleTech.Core;

public class PoliticalAffiliation
{
    [JsonPropertyName("dateOfAffiliation")]
    public required DateOnly DateOfAffiliation { get; set; }

    [JsonPropertyName("approxEndDateOfAffiliation")]
    public DateOnly? ApproximateEndDateOfAffiliation { get; set; }
    
    [JsonPropertyName("factions")]
    public required List<PoliticalAffiliationFaction> Factions { get; set; }
    [JsonPropertyName("factionWikiUrl")]
    public required string? FactionWikiUrl { get; set; }

    [JsonPropertyName("isApproximate")]
    public bool Approximate { get; set; } = false;
    [JsonPropertyName("includePreviousYears")]
    public bool IncludesPreviousYears { get; set; }

    public static PoliticalAffiliation Parse(string input, string? factionWikiUrl = null)
    {
        var dateSplitter = Regex.Match(input, """^(.*\d{4}?)s?\s-?\s?(.*)$""");
        var dateSection = dateSplitter.Groups[1].Value;
        
        DateOnly startDate;
        DateOnly? endDate = null;
        var includePreviousYears = false;
        var isApproximate = false;
        if (Regex.IsMatch(dateSection, """^\d{2}\s\w+,\s\d{4}$"""))
        {
            // It is a gregorian date
            startDate = DateOnly.ParseExact(dateSection, "dd MMMM, yyyy");
        }
        else if (Regex.IsMatch(dateSection, """^mid\-?\s*(\d{4})"""))
        {
            var startYear = int.Parse(Regex.Match(dateSection, """^mid\-?\s*(\d{4})""").Groups[1].Value);
            startDate = DateOnly.FromDateTime(new DateTime(startYear, 6, 1));
        }
        else if (Regex.IsMatch(dateSection, """^(Oct)\s*\d{4}"""))
        {
            startDate = DateOnly.ParseExact(dateSection, "MMM yyyy");
        }
        else if (Regex.IsMatch(dateSection, """^late\s*(\d{4})"""))
        {
            var startYear = int.Parse(Regex.Match(dateSection, """^late\s*(\d{4})""").Groups[1].Value);
            startDate = DateOnly.FromDateTime(new DateTime(startYear, 9, 1)); 
        }
        else
        {
            // It is just a year
            var dateSectionMatches = Regex.Match(dateSection, """^(ca\.|pre-)?\s*([0-9-– ]+)$""");
            var approximateData = dateSectionMatches.Groups[1].Value.Trim(); 
            var yearData = Regex.Match(dateSectionMatches.Groups[2].Value.Trim(), """^(\d+)\s?-?–?\s?(\d+)?$""");
            
            isApproximate = approximateData.Equals("ca.");
            includePreviousYears = approximateData.Equals("pre-");
            var startYear = int.Parse(yearData.Groups[1].Value);
            if (isApproximate && !string.IsNullOrWhiteSpace(yearData.Groups[2].Value))
            {
                var endYear = int.Parse(yearData.Groups[2].Value);
                endDate = DateOnly.FromDateTime(new DateTime(endYear, 1, 1));
            }

            startDate = DateOnly.FromDateTime(new DateTime(startYear, 1, 1));
        }

        List<string> factionMatches = [];
        var factionSection = dateSplitter.Groups[2];
        if (factionSection.Value.Contains(','))
        {
            var groupedFactionSplits = factionSection.Value.Split(',');

            foreach (var groupedFaction in groupedFactionSplits)
            {
                if (groupedFaction.Contains('/'))
                {
                    var factionSplit = groupedFaction.Split('/');
                    if (factionSplit.Any(f => f.Contains('%')))
                    {
                        // has percent ownership
                        var percentFaction = factionSplit.Single(f => f.Contains('%'));
                        var percent = int.Parse(Regex.Match(percentFaction, """(\d{2})%""").Groups[1].Value);

                        var updatedFactions = factionSplit
                           .Where(f => !f.Contains('%'))
                           .Select(f =>
                            {
                                f += $" - {percent}%";
                                return f;
                            })
                           .Append(percentFaction)
                           .ToList();
                        factionMatches.AddRange(updatedFactions);
                    }
                    else
                    {
                        factionMatches.AddRange(factionSplit);
                    }
                }
                else
                {
                    factionMatches.Add(groupedFaction);
                }
            }

        }
        else if (factionSection.Value.Contains('/'))
        {
            var groupedFactionSplits = factionSection.Value.Split('/');
            factionMatches = groupedFactionSplits.ToList();
        }
        else
        {
            factionMatches = [factionSection.Value];
        }
        
        
        List<PoliticalAffiliationFaction> factions = [];
        var validFactionMatches = factionMatches.ToArray().Select(g => g.Trim());
        foreach (var faction in validFactionMatches)
        {
            if (string.IsNullOrWhiteSpace(faction))
            {
                // no faction in capture group
                continue;
            }

            if (faction.Contains('%'))
            {
                var factionMatch = Regex.Match(faction, """^([a-zA-Z0-9 \(\)]+).*?(\d{1,3})\s?%""");
                var paFaction = new PoliticalAffiliationFaction()
                {
                    Name = factionMatch.Groups[1].Value.Trim(),
                    PercentOfOccupation = int.Parse(factionMatch.Groups[2].Value.Trim())
                };
                factions.Add(paFaction);   
            }
            else
            {
                var factionName = Regex.Match(faction, """^([a-zA-Z0-9 \(\)]+)""").Groups[1].Value;
                var paFaction = new PoliticalAffiliationFaction()
                {
                    Name = factionName.Trim(),
                    PercentOfOccupation = 100
                };
                factions.Add(paFaction);  
            }
        }
        


        return new PoliticalAffiliation
        {
            DateOfAffiliation = startDate,
            ApproximateEndDateOfAffiliation = endDate,
            Factions = factions,
            Approximate = isApproximate,
            IncludesPreviousYears = includePreviousYears,
            FactionWikiUrl = null,
        };
    }
}
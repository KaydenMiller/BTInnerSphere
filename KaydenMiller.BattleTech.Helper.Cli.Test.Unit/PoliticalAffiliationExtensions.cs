using KaydenMiller.BattleTech.Core;

namespace KaydenMiller.BattleTech.Helper.Cli.Test.Unit;

public static class PoliticalAffiliationExtensions
{
    internal static int CountFactions(this List<PoliticalAffiliation> politicalAffiliations, string factionName)
    {
        return politicalAffiliations.Count(a => a.Factions.Contains(factionName));
    }
}
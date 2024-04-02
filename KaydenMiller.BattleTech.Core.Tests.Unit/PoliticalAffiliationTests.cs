using FluentAssertions;

namespace KaydenMiller.BattleTech.Core.Tests.Unit;

public class PoliticalAffiliationTests
{
    [Theory]
    [InlineData("2571 - No record", 2571, "No record")]
    [InlineData("2750 - Outworlds Alliance", 2750, "Outworlds Alliance")]
    [InlineData("2864 - Independent world", 2864, "Independent world")]
    public void Should_ParseFromString_WhenValidInputs(string input, int year, string faction)
    {
        var actual = PoliticalAffiliation.Parse(input);
        var expected = new PoliticalAffiliation()
        {
            Factions = [new PoliticalAffiliationFaction() { Name = faction, PercentOfOccupation = 100}],
            DateOfAffiliation = new DateOnly(year, 1, 1),
            FactionWikiUrl = null
        };
        actual.Should().BeEquivalentTo(expected);
    }
    
    [Theory]
    [InlineData("2571 - No record[1]", 2571, "No record")]
    [InlineData("2750 - Outworlds Alliance[2][4]", 2750, "Outworlds Alliance")]
    [InlineData("2864 - Independent world[3]", 2864, "Independent world")]
    public void Should_ParseFromString_WhenWithReferences(string input, int year, string faction)
    {
        var actual = PoliticalAffiliation.Parse(input);
        var expected = new PoliticalAffiliation()
        {
            Factions = [new PoliticalAffiliationFaction() { Name = faction, PercentOfOccupation = 100}],
            DateOfAffiliation = new DateOnly(year, 1, 1),
            FactionWikiUrl = null
        };
        actual.Should().BeEquivalentTo(expected);
    }
}
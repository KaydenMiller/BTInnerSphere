using FluentAssertions;

namespace KaydenMiller.BattleTech.Core.Tests.Unit;

public class SolarSystemPlanetaryDataTests
{
    [Fact]
    public void Should_ParseFromString_WhereContainsJustNumber()
    {
        // Arrange
        const string input = "5";

        // Act
        var actual = SolarSystemPlanetaryData.Parse(input);

        // Assert
        var expected = new SolarSystemPlanetaryData(5, 0);
        expected.Should().BeEquivalentTo(actual);
    }
}
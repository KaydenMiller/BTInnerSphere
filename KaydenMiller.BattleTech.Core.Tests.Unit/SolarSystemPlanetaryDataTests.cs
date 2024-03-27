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
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Should_ParseFromString_WhereContainsArtificialStructure()
    {
        // Arrange
        const string input = "10 planets, 1 artificial structure";

        // Act
        var actual = SolarSystemPlanetaryData.Parse(input);

        // Assert
        var expected = new SolarSystemPlanetaryData(10, 1);
        actual.Should().BeEquivalentTo(expected);
    }
    
    [Fact]
    public void Should_ParseFromString_WhereContainsArtificialStructureAndHasReference()
    {
        // Arrange
        const string input = "1 planets, 10 artificial structure[1]";

        // Act
        var actual = SolarSystemPlanetaryData.Parse(input);

        // Assert
        var expected = new SolarSystemPlanetaryData(1, 10);
        actual.Should().BeEquivalentTo(expected);
    }
}
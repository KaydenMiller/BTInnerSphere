using FluentAssertions;

namespace KaydenMiller.BattleTech.Core.Tests.Unit;

public class GalacticCoordinatesTests
{
    [Fact]
    public void Should_ParseFromString_WithDecimals()
    {
        // Arrange
        const string toParse = "266.497 : -110.836";
        
        // Act
        var actual = GalacticCoordinates.Parse(toParse);

        // Assert
        var expected = GalacticCoordinates.Create(266.497f, -110.836f);

        actual.Should().BeEquivalentTo(expected);
    }
    
    [Fact]
    public void Should_ParseFromString_WithoutDecimals()
    {
        // Arrange
        const string toParse = "266 : -110";
        
        // Act
        var actual = GalacticCoordinates.Parse(toParse);

        // Assert
        var expected = GalacticCoordinates.Create(266f, -110f);

        actual.Should().BeEquivalentTo(expected);
    }
    
    [Fact]
    public void Should_ParseFromString_WithExtraCharacters()
    {
        // Arrange
        const string toParse = "ffflllewi266 : -110[e]";
        
        // Act
        var actual = GalacticCoordinates.Parse(toParse);

        // Assert
        var expected = GalacticCoordinates.Create(266f, -110f);

        actual.Should().BeEquivalentTo(expected);
    }
    
    [Fact]
    public void Should_ParseFromString_WithLeadingAndTrailingSpaces()
    {
        // Arrange
        const string toParse = "    266 : -110    ";
        
        // Act
        var actual = GalacticCoordinates.Parse(toParse);

        // Assert
        var expected = GalacticCoordinates.Create(266f, -110f);

        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Should_ParseFromString_WithNBSPInText()
    {
        // Arrange
        const string toParse = "266.497&nbsp;: -110.836";
        
        // Act
        var actual = GalacticCoordinates.Parse(toParse);

        // Assert
        var expected = GalacticCoordinates.Create(266.497f, -110.836f);

        actual.Should().BeEquivalentTo(expected);    }
}
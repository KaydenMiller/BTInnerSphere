using FluentAssertions;

namespace KaydenMiller.BattleTech.Core.Tests.Unit;

public class SpectralClassificationTests
{
    [Theory]
    [InlineData("A1VI", SpectralClass.A, 1, "VI")]
    [InlineData("O9I", SpectralClass.O, 9, "I")]
    [InlineData("O9II", SpectralClass.O, 9, "II")]
    [InlineData("O9III", SpectralClass.O, 9, "III")]
    [InlineData("O9IV", SpectralClass.O, 9, "IV")]
    [InlineData("O9V", SpectralClass.O, 9, "V")]
    [InlineData("O9VI", SpectralClass.O, 9, "VI")]
    [InlineData("O9VII", SpectralClass.O, 9, "VII")]
    [InlineData("O9VIII", SpectralClass.O, 9, "VIII")]
    [InlineData("O9Ia", SpectralClass.O, 9, "Ia")]
    [InlineData("O9Ib", SpectralClass.O, 9, "Ib")]
    public void Should_ParseFromString_WhenStandardInput(string input, SpectralClass spectralClass, uint temp, string lumin)
    {
        var actual = SpectralClassification.Parse(input);
        actual.SpectralClass.Should().Be(spectralClass);
        actual.SpectralTemperature.Should().Be(temp);
        actual.LuminosityClass.Should().Be(lumin);
    }

    [Fact]
    public void Should_MarkAsUnknown_WhenInputDoesntMatchRegex()
    {
        var actual = SpectralClassification.Parse("A5Iab");
        actual.SpectralClass.Should().Be(SpectralClass.Unknown);
        actual.SpectralTemperature.Should().Be(0);
        actual.LuminosityClass.Should().Be("UNKNOWN"); 
    }
    
    [Fact]
    public void Should_ParseFromString_WhenInputIsMultiple()
    {
        const string input = "A1VI, K0V";
    }
}
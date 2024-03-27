using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace KaydenMiller.BattleTech.Core;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SpectralClass
{
    O,      // Hot stars
    B,
    A,
    F,
    G,
    K,
    M,      // Cold stars
    Unknown
}

/// <summary>
/// This is spectral classification based on the MK Classification chart
/// https://en.wikipedia.org/wiki/Stellar_classification#Yerkes_spectral_classification
/// </summary>
public class SpectralClassification
{
    [JsonPropertyName("spectralClass")]
    public SpectralClass SpectralClass { get; init; }
    [JsonPropertyName("temperatureClass")]
    public uint SpectralTemperature { get; init; }
    [JsonPropertyName("luminosityClass")]
    public string LuminosityClass { get; init; }
    
    public SpectralClassification(SpectralClass @class, uint spectralTemperature, string luminosityClass)
    {
        SpectralClass = @class;
        SpectralTemperature = spectralTemperature;
        LuminosityClass = luminosityClass;
    }

    public static SpectralClassification Parse(string input)
    {
        var normalizedInput = input.Trim();
        var isValidRegex = new Regex("""^([OBAFGKM])(\d)(Ia|Ib|IV|VIII|VII|VI|III|II|I|V)$""");

        if (isValidRegex.IsMatch(normalizedInput) is false)
        {
            // not a match, invalid mark as unknown
            return SpectralClassification.Unknown();
        }

        var match = isValidRegex.Match(normalizedInput);

        var spectral = Enum.Parse<SpectralClass>(match.Groups[1].Value);
        var temp = uint.Parse(match.Groups[2].Value);
        var lumin = match.Groups[3].Value;

        return new SpectralClassification(spectral, temp, lumin);
    }

    public static SpectralClassification Unknown()
    {
        return new SpectralClassification(SpectralClass.Unknown, 0, "UNKNOWN");
    }
}
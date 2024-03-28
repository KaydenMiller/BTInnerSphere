using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace KaydenMiller.BattleTech.Core;

/// <summary>
/// This is spectral classification based on the MK Classification chart
/// https://en.wikipedia.org/wiki/Stellar_classification#Yerkes_spectral_classification
/// </summary>
public class SpectralClassification
{
    [JsonPropertyName("spectralClass")]
    public SpectralClass SpectralClass { get; set; }
    [JsonPropertyName("temperatureClass")]
    public uint SpectralTemperature { get; set; }
    [JsonPropertyName("luminosityClass")]
    public string LuminosityClass { get; set; }

    /// <summary>
    /// Needed for json serialization
    /// </summary>
    public SpectralClassification()
    {
    }
    
    public SpectralClassification(SpectralClass @class, uint spectralTemperature, string luminosityClass)
    {
        SpectralClass = @class;
        SpectralTemperature = spectralTemperature;
        LuminosityClass = luminosityClass;
    }

    public static SpectralClassification Parse(string input)
    {
        var normalizedInput = input.Trim();
        var isValidRegex = new Regex("""^([OBAFGKM])(\d)?(Ia|Ib|IV|VIII|VII|VI|III|II|I|V)?$""");

        if (isValidRegex.IsMatch(normalizedInput) is false)
        {
            // not a match, invalid mark as unknown
            return SpectralClassification.Unknown();
        }

        var match = isValidRegex.Match(normalizedInput);
        var tempString = string.IsNullOrWhiteSpace(match.Groups[2].Value) is false ? match.Groups[2].Value : "0";

        var spectral = Enum.Parse<SpectralClass>(match.Groups[1].Value);
        var temp = uint.Parse(tempString);
        var lumin = match.Groups[3].Value;

        return new SpectralClassification(spectral, temp, lumin);
    }

    public static SpectralClassification Unknown()
    {
        return new SpectralClassification(SpectralClass.Unknown, 0, "UNKNOWN");
    }

    public static string GetColor(SpectralClassification? classification)
    {
        return classification?.SpectralClass switch
        {
            SpectralClass.O => "#92B5FF", // blue
            SpectralClass.B => "#A2C0FF", // deep bluish white
            SpectralClass.A => "#D5E0FF", // bluish white
            SpectralClass.F => "#F9F5FF", // white
            SpectralClass.G => "#FFEDE3", // yellowish white
            SpectralClass.K => "#FFDAB5", // pale yellowish white
            SpectralClass.M => "#FFB56C", // light orangish red
            _ => "#F9F5FF"
        };
    }
}
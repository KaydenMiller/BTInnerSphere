using System.Text.Json.Serialization;

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
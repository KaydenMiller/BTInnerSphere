using System.Text.Json.Serialization;

namespace KaydenMiller.BattleTech.Core;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RechargeStationType
{
    Zenith,
    Nadir,
    None,
    Unknown
}
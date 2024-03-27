using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace KaydenMiller.BattleTech.Core;

public class SystemRechargeTime
{
    [JsonPropertyName("timeLength")]
    public TimeSpan LengthOfTime { get; set; }

    public static SystemRechargeTime Parse(string input)
    {
        var values = Regex.Match(input, """(\d+)\s*(hours)""");
        var valueOfTime = int.Parse(values.Groups[1].Value);
        var unitOfTime = values.Groups[2].Value;

        return unitOfTime switch
        {
            "hours" => new() { LengthOfTime = TimeSpan.FromHours(valueOfTime) },
            _ => new() { LengthOfTime = TimeSpan.FromHours(valueOfTime) }
        };
    }
}
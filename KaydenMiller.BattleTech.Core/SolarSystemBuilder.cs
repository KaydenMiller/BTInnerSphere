namespace KaydenMiller.BattleTech.Core;

public class SolarSystemBuilder
{
    private readonly SolarSystem _solarSystem;

    public Uri WikiUrl { get; private set; }

    public SolarSystemBuilder(string name, string wikiUrl)
    {
        _solarSystem = new SolarSystem()
        {
            Name = name,
            WikiUrl = wikiUrl 
        };

        WikiUrl = new Uri(wikiUrl);
    }

    public SolarSystemBuilder WithPoliticalAffiliation(PoliticalAffiliation politicalAffiliation)
    {
        _solarSystem.PoliticalAffiliations ??= [];
        _solarSystem.PoliticalAffiliations.Add(politicalAffiliation);
        return this;
    }

    public SolarSystemBuilder WithPoliticalAffiliations(IEnumerable<PoliticalAffiliation> politicalAffiliations)
    {
        _solarSystem.PoliticalAffiliations ??= [];
        _solarSystem.PoliticalAffiliations.AddRange(politicalAffiliations);
        return this;
    }

    public SolarSystemBuilder AddMetadata(string key, string value)
    {
        _solarSystem.Metadata ??= [];
        _solarSystem.Metadata.TryAdd(key, value);
        return this;
    }
}
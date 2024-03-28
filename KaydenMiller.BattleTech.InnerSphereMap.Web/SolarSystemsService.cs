using System.Net.Http.Json;
using KaydenMiller.BattleTech.Core;

namespace KaydenMiller.BattleTech.InnerSphereMap.Web;

public class SolarSystemsService
{
    private readonly Lazy<Task<List<SolarSystem>>> _solarSystems;

    public SolarSystemsService(HttpClient http)
    {
        _solarSystems = new Lazy<Task<List<SolarSystem>>>(() => http.GetFromJsonAsync<List<SolarSystem>>("assets/solar-systems.json")!);
    }

    public async Task<List<SolarSystem>> GetSolarSystems()
    {
        return await _solarSystems.Value;
    }
}
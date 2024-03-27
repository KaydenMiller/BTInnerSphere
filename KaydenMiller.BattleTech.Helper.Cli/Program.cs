// This tool is used to extract information from the www.sarna.net wiki

using System.Text.Json;
using KaydenMiller.BattleTech.Core;
using KaydenMiller.BattleTech.Helper.Cli;
using Microsoft.Playwright;

using var playwright = await Playwright.CreateAsync();
var chrome = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions()
{
    Headless = false
});

var page = await chrome.NewPageAsync();

// var s = await page.SearchSolarSystem(new KaydenMiller.BattleTech.Helper.Cli.System()
// {
//     Name = "Monument to Man",
//     SystemHref = "/wiki/Monument%20to%20Man"
// });

List<KaydenMiller.BattleTech.Helper.Cli.System> systems;
if (File.Exists("possible-systems.json"))
{
    var file = File.ReadAllText("possible-systems.json");
    systems = JsonSerializer.Deserialize<List<KaydenMiller.BattleTech.Helper.Cli.System>>(file) ?? [];
}
else
{
    systems = await page.FindPossibleSystems();
    File.WriteAllText("possible-systems.json", JsonSerializer.Serialize(systems));
}

var solarSystems = new List<SolarSystem>();
if (File.Exists("solar-systems.json"))
{
    Console.WriteLine("UPDATING SOLAR SYSTEMS");
    var file = File.ReadAllText("solar-systems.json");
    solarSystems = JsonSerializer.Deserialize<List<SolarSystem>>(file) ?? [];

    List<SolarSystem> systemDetailsList = [];
    foreach (var solarSystem in solarSystems)
    {
        solarSystem.Metadata?.TryAdd("SystemName", solarSystem.Name);
        solarSystem.Metadata?.TryAdd("SystemWikiUrl", solarSystem.WikiUrl ?? "");
        var systemDetails = PlanetSearch.ParseFromDictionary(solarSystem.Metadata ?? []);
        systemDetailsList.Add(systemDetails);
    }
    
    var json = JsonSerializer.Serialize(systemDetailsList, new JsonSerializerOptions()
    {
        WriteIndented = true
    });
    File.WriteAllText("solar-systems-update.json", json);
    Console.WriteLine("DONE");
}
else
{
    try
    {
        foreach (var system in systems)
        {
            var systemDetailsDictionary = await page.SearchSolarSystem(system);
            var systemDetails = PlanetSearch.ParseFromDictionary(systemDetailsDictionary);
            solarSystems.Add(systemDetails);
        }
    }
    finally
    {
        var json = JsonSerializer.Serialize(solarSystems);
        File.WriteAllText("solar-systems.json", json);
    }
}
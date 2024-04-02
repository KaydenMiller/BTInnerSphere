// This tool is used to extract information from the www.sarna.net wiki

using System.Collections.Concurrent;
using System.Text.Json;
using Flurl;
using KaydenMiller.BattleTech.Core;
using KaydenMiller.BattleTech.Helper.Cli;
using Microsoft.Playwright;

using var playwright = await Playwright.CreateAsync();
var chrome = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions()
{
    Headless = false
});

var page = await chrome.NewPageAsync();

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

var htmlPages = new ConcurrentDictionary<string, string>();
if (File.Exists("systems-html.json"))
{
    var text = File.ReadAllText("systems-html.json");
    htmlPages = JsonSerializer.Deserialize<ConcurrentDictionary<string, string>>(text);
}
else
{
    await Parallel.ForEachAsync(systems, async (system, token) =>
    {
        if (token.IsCancellationRequested) return;
        Console.WriteLine($"Pulling page for {system.Name}");
        var url = Constants.SARNA_WIKI.AppendPathSegment(system.SystemHref).ToUri();
        var htmlPage = await RemoteProcessAutomation.GetSolarSystemHtmlPage(url);
        htmlPages.TryAdd(system.SystemHref, htmlPage);
    });
    Console.WriteLine($"Pages found so far: {htmlPages.Count}");
    var htmlJson = JsonSerializer.Serialize(htmlPages);
    File.WriteAllText("systems-html.json", htmlJson);
}

Console.WriteLine($"Loaded Pages: {htmlPages!.Count}");

var parsedSystems = new ConcurrentStack<SolarSystem>();
var countOfErrors = 0;
Parallel.ForEach(htmlPages, (htmlPage, _) =>
{
    try
    {
        // Console.WriteLine($"Parsing Page {htmlPage.Key}");
        var systemHtml = htmlPage.Value;
        var systemName = RemoteProcessAutomation.FindPrimaryHeading(systemHtml);
        var infoBoxes = RemoteProcessAutomation.FindInfoBoxes(systemHtml);

        var politicalAffiliations = BattleTechHtmlParser.FindPoliticalAffiliations(systemHtml);

        Dictionary<string, string?> systemDetails = [];
        if (infoBoxes.Count >= 1)
        {
            systemDetails = BattleTechHtmlParser.ParseInfoBox(infoBoxes[0]);
        }

        var system = RemoteProcessAutomation.ParseFromDictionary(systemDetails, politicalAffiliations);
        parsedSystems.Push(system);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"ERROR: parsing page {htmlPage.Key}");
        Interlocked.Increment(ref countOfErrors);
    }
});
Console.WriteLine($"Systems are parsed, with {countOfErrors} errors");

var systemJson = JsonSerializer.Serialize(parsedSystems);
File.WriteAllText("solar-systems-2.json", systemJson);

// Systems are parsed, with 119 errors 
// Systems are parsed, with 42 errors - This was because of the hack for the xpath parser in the political data
// Systems are parsed, with 40 errors - Places with no infotables at all
// Systems are parsed, with 22 errors - Handle 'pre-' date tag
// Systems are parsed, with 27 errors - 
// 
Console.WriteLine("Done!");
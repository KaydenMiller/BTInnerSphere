using System.Text.RegularExpressions;
using Flurl;
using KaydenMiller.BattleTech.Core;
using Microsoft.Playwright;

namespace KaydenMiller.BattleTech.Helper.Cli;

public static class PlanetSearch
{
    public static async Task<List<System>> FindPossibleSystems(this IPage page)
    {
        Console.WriteLine("Find All Planets");
        var url = Constants.SARNA_WIKI
           .AppendPathSegment("wiki")
           .AppendPathSegment("Category:Systems")
           .ToString();

        var startUrl = url.AppendQueryParam("from", "A");

        await page.GotoAsync(startUrl, new PageGotoOptions()
        {
            WaitUntil = WaitUntilState.DOMContentLoaded
        });

        var systems = new List<System>();

        var isLastSystem = false;
        System? previousLastSystem = null;
        do
        {
            var systemLocators = await page.Locator("""//*[@id="mw-pages"]/div/div/div/ul/li""").AllAsync();
            var systemsHtml = systemLocators.Select(sh => sh.InnerHTMLAsync());

            foreach (var systemHtmlTask in systemsHtml)
            {
                var html = await systemHtmlTask;
                var system = new System()
                {
                    Name = Regex.Match(html, """title="(.*?)"\W?""").Groups[1].Value,
                    SystemHref = Regex.Match(html, """href="(.*?)"\W?""").Groups[1].Value
                };
                systems.Add(system);
            }

            var lastSystem = systems.Last();
            if (previousLastSystem?.Name == lastSystem.Name)
            {
                isLastSystem = true;
            }
            else
            {
                previousLastSystem = lastSystem;
                await page.GotoAsync(url.AppendQueryParam("pagefrom", lastSystem.Name), new PageGotoOptions()
                {
                    WaitUntil = WaitUntilState.DOMContentLoaded
                });
            }
        } while (!isLastSystem);
        
        // TODO: somehow we are getting 3,301 (after distinct) systems out of a total of 3,304
        return systems.DistinctBy(s => s.SystemHref).ToList();
    }

    public static async Task<Dictionary<string, string>> SearchSolarSystem(this IPage page, System system)
    {
        Console.WriteLine($"Open Page for System '{system.Name}'");
        var useSystemNameFromInput = false;
        var wikiUrl = Constants.SARNA_WIKI.AppendPathSegment(system.SystemHref);
        await page.GotoAsync(wikiUrl, new PageGotoOptions()
        {
            WaitUntil = WaitUntilState.DOMContentLoaded
        });

        var locator = page
           .Locator("""//table[@class="infobox"]""");

        var locatorElementTasks = (await locator.AllAsync())
           .Select(e => e.InnerHTMLAsync())
           .ToList();

        if (locatorElementTasks.Any() is false)
        {
            useSystemNameFromInput = true;
        }

        Console.WriteLine("Collect tables for system");
        var systemTable = "";

        var elements = await Task.WhenAll(locatorElementTasks);

        if (elements.Any(e => e.Contains("System Information")))
        {
            systemTable = elements.Single(e => e.Contains("System Information"));
        }
        else
        {
            // no system information was found likely just the name of the system
            useSystemNameFromInput = true;
        }

        Console.WriteLine("Collect System Headers");
        var systemHeadersRegex = new Regex("""<tr>\W*<th.*?>(.*?)</th>\W*</tr>""");
        var systemHeaders = new List<string>();
        var systemHeaderMatches = systemHeadersRegex.Matches(systemTable);
        foreach (Match systemHeaderMatch in systemHeaderMatches)
        {
            var key = systemHeaderMatch.Groups[1].Value;
            systemHeaders.Add(key);
        }


        Console.WriteLine("Collect System Information");
        var systemInformationRegex = new Regex("""<tr>\W*<th.*?>(.*?)</th>\W*<td.*?>(.*?)(<sup.*?)?</td>\W*</tr>""");
        var systemInformationTableHtml = Regex.Match(systemTable, "System Information</th>(.*)").Groups[1].Value;
        var systemInformation = new Dictionary<string, string>();
        var systemInformationMatches = systemInformationRegex.Matches(systemInformationTableHtml);
        foreach (Match systemInfoMatch in systemInformationMatches)
        {
            var key = systemInfoMatch.Groups[1].Value;
            var value = systemInfoMatch.Groups[2].Value;

            systemInformation.TryAdd(key, value);
        }

        var solarSystemName = !useSystemNameFromInput ? systemHeaders[0].Trim() : system.Name;
        systemInformation.TryAdd("SystemName", solarSystemName);
        systemInformation.TryAdd("SystemWikiUrl", wikiUrl);

        return systemInformation;
    }

    public static SolarSystem ParseFromDictionary(Dictionary<string, string> systemInformation)
    {
        List<SpectralClassification> spectralClassifications = [];
        try
        {
            var value = systemInformation.GetValue("Spectral class");

            if (value.Contains(','))
            {
                // multiple stars and classifications
                var stars = value.Split(",");
                var starClassifications = stars
                    .Select(s => s.Trim())
                    .Select(SpectralClassification.Parse);
                spectralClassifications.AddRange(starClassifications);
            }
            else
            {
                spectralClassifications.Add(SpectralClassification.Parse(value.Trim()));
            }
        }
        catch
        {
            spectralClassifications.Add(SpectralClassification.Unknown());
        }

        List<RechargeStationType> rechargeStations = [];
        try
        {
            var rechargeStationInputs = systemInformation.GetValueOrDefault("Recharge station(s)")?.Split(", ") ?? [];
            foreach (var station in rechargeStationInputs)
            {
                var stationType = Enum.Parse<RechargeStationType>(station);
                rechargeStations.Add(stationType);
            }
        }
        catch
        {
            rechargeStations.Add(RechargeStationType.Unknown);
        }

        GalacticCoordinates coordinates;
        try
        {
            coordinates = GalacticCoordinates.Parse(systemInformation.GetValue("X:Y Coordinates"));
        }
        catch
        {
            coordinates = GalacticCoordinates.Create(0f, 0f, false);
        }

        return new SolarSystem()
        {
            Name = systemInformation.GetValueOrDefault("SystemName") ?? "UNKNOWN",
            Coordinates = coordinates,
            PlanetaryData = SolarSystemPlanetaryData.Parse(systemInformation.GetValueOrDefault("Planet(s)") ?? ""),
            SpectralClassifications = spectralClassifications,
            RechargeStations = rechargeStations,
            WikiUrl = systemInformation.GetValueOrDefault("SystemWikiUrl"),
            Metadata = systemInformation
        }; 
    }
}
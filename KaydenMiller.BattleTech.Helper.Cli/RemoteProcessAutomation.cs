using System.Text.RegularExpressions;
using Flurl;
using Flurl.Http;
using HtmlAgilityPack;
using KaydenMiller.BattleTech.Core;
using Microsoft.Playwright;

namespace KaydenMiller.BattleTech.Helper.Cli;

public static class RemoteProcessAutomation
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
    
    public static async Task<string> GetSolarSystemHtmlPage(Uri solarSystemUrl)
    {
        var page = await solarSystemUrl.GetStringAsync();
        return page;
    }
    
    private static HtmlDocument GetHtmlDocument(string html)
    {
        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(html);
        return htmlDocument;
    }

    public static List<Infobox> FindInfoBoxes(string htmlPage)
    {
        var document = GetHtmlDocument(htmlPage).DocumentNode;
        var nodes = document.SelectNodes("""//table[contains(@class, "infobox")]""");

        if (nodes is null || nodes.Count == 0)
        {
            return [];
        }
        
        var infoBoxElementsHtml = nodes
           .Select(e => e.InnerHtml)
           .ToList();
        var infoBoxElementsText = nodes
           .Select(e => e.InnerText)
           .ToList();


        List<Infobox> infoboxes = [];
        for (var boxNumber = 0; boxNumber < infoBoxElementsHtml.Count; boxNumber++)
        {
            infoboxes.Add(new Infobox(infoBoxElementsHtml[boxNumber], infoBoxElementsText[boxNumber]));
        }
        return infoboxes;
    }
    
    public static string FindPrimaryHeading(string htmlPage)
    {
        var document = GetHtmlDocument(htmlPage);
        var locator = document.DocumentNode.SelectSingleNode("//h1[@id=\"firstHeading\"]");
        var heading = locator.InnerText.Trim();
        return heading;
    }
    
    public static SolarSystem ParseFromDictionary(
        Dictionary<string, string?> systemInformation,
        IEnumerable<PoliticalAffiliation> politicalAffiliations)
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

        SystemRechargeTime? rechargeTime;
        try
        {
            rechargeTime = SystemRechargeTime.Parse(systemInformation.GetValue("Recharge time"));
        }
        catch
        {
            rechargeTime = null;
        }

        return new SolarSystem()
        {
            Name = systemInformation.GetValueOrDefault("SystemName") ?? "UNKNOWN",
            Coordinates = coordinates,
            RechargeTime = rechargeTime,
            PlanetaryData = SolarSystemPlanetaryData.Parse(systemInformation.GetValueOrDefault("Planet(s)") ?? ""),
            SpectralClassifications = spectralClassifications,
            RechargeStations = rechargeStations,
            WikiUrl = systemInformation.GetValueOrDefault("SystemWikiUrl"),
            PoliticalAffiliations = politicalAffiliations.ToList(),
            Metadata = systemInformation 
        }; 
    } 
}
using System.Net;
using System.Text.RegularExpressions;
using Flurl.Util;
using HtmlAgilityPack;
using KaydenMiller.BattleTech.Core;

namespace KaydenMiller.BattleTech.Helper.Cli;

public static class BattleTechHtmlParser
{
    private static HtmlDocument GetHtmlDocument(string html)
    {
        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(html);
        return htmlDocument;
    }
    
    public static Dictionary<string, string?> ParseInfoBox(Infobox infobox)
    {
        var document = GetHtmlDocument(infobox.HtmlValue).DocumentNode;
        
        var systemInformation = new Dictionary<string, string?>();
        
        var systemName = document.SelectNodes("//tr[1]/th")?.First()?.InnerText;
        systemInformation.TryAdd("SystemName", WebUtility.HtmlDecode(systemName));
        var infoYear = document.SelectSingleNode("""//tr[2]/td//div[contains(@class,"system-nav-current")]""")?.InnerText;
        systemInformation.TryAdd("InformationYear", WebUtility.HtmlDecode(infoYear));
        var details = ParseInfoboxSection(infobox.HtmlValue);
        systemInformation.Merge(details);

        return systemInformation;
    }

    public static Dictionary<string, string?> ParseInfoboxSection(string html)
    {
        var xpath =
            """//tr[th[contains(@class,"infobox-header")]]/following-sibling::tr[preceding::tr[th[contains(@class,"infobox-header")]]]/self::tr[th[not(contains(@class, "infobox-header"))]]""";
        var document = GetHtmlDocument(html).DocumentNode;

        var nodes = document.SelectNodes(xpath);
        
        if (nodes is null || nodes.Count == 0)
        {
            return [];
        }

        var data = nodes.Select(n =>
            {
                var key = WebUtility.HtmlDecode(n.FirstChild.InnerText);
                var value = WebUtility.HtmlDecode(n.LastChild.InnerText);

                // Remove NBSP's that exist and replace with spaces
                key = key.Replace('\u00a0', ' ');
                value = value.Replace('\u00a0', ' ');
                
                return new KeyValuePair<string, string?>(key, value);
            })
           .ToDictionary();
        return data;
    }
    
    public static List<PoliticalAffiliation> FindPoliticalAffiliations(string htmlPage)
    {
        var document = GetHtmlDocument(htmlPage).DocumentNode;
        var nodes = document.SelectNodes("""//h2[span[contains(text(),"Political Affiliation")]]/following-sibling::div[@class="div-col"]/ul/li[not(i)]""");

        if (nodes is null || nodes.Count == 0)
        {
            return [];
        }

        var locatorInnerTextElements = nodes 
           .Select(e => WebUtility.HtmlDecode(e.InnerText).Trim())
           .Where(e => Regex.IsMatch(e, """^\d+|^ca\.|^pre-""")) // TODO: this is a hack to prevent the xpath above from becoming basically a million lines long
           .Select(e => PoliticalAffiliation.Parse(e))
           .ToList();
        return locatorInnerTextElements;
    }
}
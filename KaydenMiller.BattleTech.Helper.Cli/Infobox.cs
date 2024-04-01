namespace KaydenMiller.BattleTech.Helper.Cli;

public struct Infobox
{
    public readonly string HtmlValue;
    public readonly string TextValue;

    public Infobox(string html, string text)
    {
        HtmlValue = html;
        TextValue = text;
    }
}
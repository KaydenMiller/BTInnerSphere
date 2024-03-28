using Excubo.Blazor.Canvas.Contexts;

namespace KaydenMiller.BattleTech.InnerSphereMap.Web;

public static class BattleTechMapHelperExtensions
{
    public static async Task CreatePlanet(this Context2D ctx, int x, int y, string name, string color)
    {
        // await ctx.WriteText(x, y-15, 15, name);
        await ctx.CreateCircle(x, y, 5, color);
    }
    
    public static async Task CreatePlanet(this Batch2D batch, int x, int y, string name, string color)
    {
        // await batch.WriteText(x, y-15, 15, name);
        await batch.CreateCircle(x, y, 5, color);
    }
}
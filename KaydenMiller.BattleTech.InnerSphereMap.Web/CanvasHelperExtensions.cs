using Excubo.Blazor.Canvas;
using Excubo.Blazor.Canvas.Contexts;

namespace KaydenMiller.BattleTech.InnerSphereMap.Web;

public static class CanvasHelperExtensions
{
    public static async Task WriteText(this Context2D ctx, int x, int y, int fontSize, string text)
    {
        await ctx.FillStyleAsync("black");
        await ctx.FontAsync($"{fontSize}px solid");
        await ctx.FillTextAsync(text, x, y);
    }
    
    public static async Task WriteText(this Batch2D batch, int x, int y, int fontSize, string text)
    {
        await batch.FontAsync($"{fontSize}px solid");
        await batch.FillTextAsync(text, x, y);
    }

    public static async Task CreateCircle(this Context2D ctx, int x, int y, int radius, string color)
    {
        await ctx.BeginPathAsync();
        await ctx.EllipseAsync(x, y, radius, radius, Math.PI * 2, 0, Math.PI * 2);
        await ctx.StrokeAsync();
        await ctx.FillStyleAsync(color);
        await ctx.FillAsync(FillRule.EvenOdd);
    }

    public static async Task CreateCircle(this Batch2D batch, int x, int y, int radius, string color)
    {
        await batch.BeginPathAsync();
        await batch.EllipseAsync(x, y, radius, radius, Math.PI * 2, 0, Math.PI * 2);
        await batch.StrokeAsync();
        await batch.FillStyleAsync(color);
        await batch.FillAsync(FillRule.EvenOdd);
    }
}
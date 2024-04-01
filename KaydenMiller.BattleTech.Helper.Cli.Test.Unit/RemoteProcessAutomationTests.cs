using FluentAssertions;

namespace KaydenMiller.BattleTech.Helper.Cli.Test.Unit;

public class RemoteProcessAutomationTests
{
    [Fact]
    public void Should_NotFindAnyInfoBoxes_WhenThereAreNoInfoBoxes()
    {
        var page = File.ReadAllText("./pages/Luyten_68-28.html");

        var actual = RemoteProcessAutomation.FindInfoBoxes(page);

        actual.Count.Should().Be(0); 
    }
}
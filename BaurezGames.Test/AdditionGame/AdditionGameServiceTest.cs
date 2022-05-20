using BaurezGames.Shared.AdditionGame;

namespace BaurezGames.Test.AdditionGame;

public class AdditionGameServiceTest
{
    [Fact]
    public void NewGame_SouldReturn1()
    {
        var service = new AdditionGameService();
        var gameId = service.NewGame(1, 1, 1);
        Assert.Equal("1", gameId);
    }

    [Fact]
    public void NewGame_SouldReturn1Plus1()
    {
        var service = new AdditionGameService();
        var gameId = service.NewGame(1, 1, 2);
        Assert.Equal("1 + 1", gameId);
    }

    [Theory]
    [InlineData(1,1,1,"1")]
    [InlineData(1,1,2,"1 + 1")]
    [InlineData(1,1,3, "1 + 1 + 1")]
    public void NewGame_ShouldReturn(int minValue, int maxValue, int numberOfElements, string expected)
    {
        var service = new AdditionGameService();
        var gameValue = service.NewGame(minValue, maxValue, numberOfElements);
        Assert.Equal(expected, gameValue);
    }

}
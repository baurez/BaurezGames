using BaurezGames.Shared.MoreOrLessGame;

namespace BaurezGames.Test.MoreOrLessGame;

public class MoreOrLessGameServiceTest
{
    [Fact]
    public void NewGame_SouldReturnGuidString()
    {
        var service = new MoreOrLessGameService("c:/Temp/");
        var gameId = service.NewGame(0, 0);

        var guid = Guid.NewGuid().ToString("N");
        Assert.Equal(guid.Length, gameId.Length);
    }

    [Fact]
    public void SubmitResponse_SouldReturnNull_When_responseIsNull()
    {
        var service = new MoreOrLessGameService("c:/Temp/");
        var gameId = service.NewGame(0, 0);
        var request = service.SubmitResponse(null);

        Assert.Null(request);
    }

    [Fact]
    public void SubmitResponse_SouldReturnNull_When_responseGameIdIsNull()
    {
        var service = new MoreOrLessGameService("c:/Temp/");
        var gameId = service.NewGame(0, 0);
        var request = service.SubmitResponse(new MoreOrLessGameResponse(){GameId = ""});

        Assert.Null(request);
    }

    [Fact]
    public void SubmitResponse_SouldReturnNull_When_responseGameIdIsFantasy()
    {
        var service = new MoreOrLessGameService("c:/Temp/");
        var gameId = service.NewGame(0, 0);
        var request = service.SubmitResponse(new MoreOrLessGameResponse() { GameId = "123456" });

        Assert.Null(request);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(100)]
    [InlineData(1000)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void SubmitResponse_SouldReturnResponseWithScoreAsZero(int submitedValue)
    {
        var service = new MoreOrLessGameService("c:/Temp/");
        var gameId = service.NewGame(submitedValue, submitedValue);
        var request = service.SubmitResponse(new MoreOrLessGameResponse() { GameId = gameId, Value = submitedValue });

        Assert.Equal(submitedValue, request!.SubmitedResponse);
        Assert.Equal(0, request!.Score);
    }

    [Theory]
    [InlineData(int.MinValue, int.MaxValue, int.MaxValue, -1)]
    [InlineData(int.MinValue, int.MaxValue, int.MinValue, 1)]
    public void SubmitResponse_SouldReturnResponseWithScoreAsExpected(int minValue, int maxValue, int submitedValue, int expectedScore)
    {
        var service = new MoreOrLessGameService("c:/Temp/");
        var gameId = service.NewGame(minValue, maxValue);
        var request = service.SubmitResponse(new MoreOrLessGameResponse() { GameId = gameId, Value = submitedValue });

        Assert.Equal(submitedValue, request!.SubmitedResponse);
        Assert.Equal(expectedScore, request!.Score);
    }




    //[Fact]
    //public void NewGame_SouldReturn1Plus1()
    //{
    //    var service = new AdditionGameService();
    //    var gameId = service.NewGame(1, 1, 2);
    //    Assert.Equal("1 + 1", gameId);
    //}

    //[Theory]
    //[InlineData(1,1,1,"1")]
    //[InlineData(1,1,2,"1 + 1")]
    //[InlineData(1,1,3, "1 + 1 + 1")]
    //public void NewGame_ShouldReturn(int minValue, int maxValue, int numberOfElements, string expected)
    //{
    //    var service = new AdditionGameService();
    //    var gameValue = service.NewGame(minValue, maxValue, numberOfElements);
    //    Assert.Equal(expected, gameValue);
    //}

}
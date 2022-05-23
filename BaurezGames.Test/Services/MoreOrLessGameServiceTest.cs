namespace BaurezGames.Test.Services;

public class MoreOrLessGameServiceTest
{
    [Fact]
    public void NewGame_ShouldReturnGuidString()
    {
        var service = new MoreOrLessGameService("c:/Temp/");
        var gameId = service.NewGame(0, 0);

        var guid = Guid.NewGuid().ToString("N");
        Assert.Equal(guid.Length, gameId.Length);
    }

    [Fact]
    public void SubmitResponse_ShouldReturnNull_When_responseIsNull()
    {
        var service = new MoreOrLessGameService("c:/Temp/");
        var request = service.SubmitResponse(null);

        Assert.Null(request);
    }

    [Fact]
    public void SubmitResponse_ShouldReturnNull_When_responseGameIdIsNull()
    {
        var service = new MoreOrLessGameService("c:/Temp/");
        var request = service.SubmitResponse(new MoreOrLessGameResponse(){GameId = ""});

        Assert.Null(request);
    }

    [Fact]
    public void SubmitResponse_ShouldReturnNull_When_responseGameIdIsFantasy()
    {
        var service = new MoreOrLessGameService("c:/Temp/");
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
    public void SubmitResponse_ShouldReturnResponseWithScoreAsZero(int submittedValue)
    {
        var service = new MoreOrLessGameService("c:/Temp/");
        var gameId = service.NewGame(submittedValue, submittedValue);
        var request = service.SubmitResponse(new MoreOrLessGameResponse() { GameId = gameId, Value = submittedValue });

        Assert.Equal(submittedValue, request!.SubmitedResponse);
        Assert.Equal(0, request.Score);
    }

    [Theory]
    [InlineData(int.MinValue, int.MaxValue, int.MaxValue, -1)]
    [InlineData(int.MinValue, int.MaxValue, int.MinValue, 1)]
    public void SubmitResponse_ShouldReturnResponseWithScoreAsExpected(int minValue, int maxValue, int submittedValue, int expectedScore)
    {
        var service = new MoreOrLessGameService("c:/Temp/");
        var gameId = service.NewGame(minValue, maxValue);
        var request = service.SubmitResponse(new MoreOrLessGameResponse() { GameId = gameId, Value = submittedValue });

        Assert.Equal(submittedValue, request!.SubmitedResponse);
        Assert.Equal(expectedScore, request.Score);
    }
}
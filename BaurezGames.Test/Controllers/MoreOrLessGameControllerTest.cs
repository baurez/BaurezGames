using BaurezGames.Server.Controllers;

namespace BaurezGames.Test.Controllers;

public class MoreOrLessGameControllerTest
{
    readonly Mock<ILogger<MoreOrLessGameController>> _mockILogger = new Mock<ILogger<MoreOrLessGameController>>();
    readonly Mock<IMoreOrLessGameService> _mockService = new Mock<IMoreOrLessGameService>();


    [Fact]
    public void Get_ShouldReturn_Guid()
    {
        var mockReturn = Guid.NewGuid().ToString("N");
        _mockService.Setup(x => x.NewGame(It.IsAny<int>(), It.IsAny<int>())).Returns(mockReturn);

        var sut = new MoreOrLessGameController(_mockILogger.Object, _mockService.Object);

        var result = sut.Get();
        result.Should().Be(mockReturn);
    }


    [Fact]  
    public void Post_ShouldReturnNull_When_ResponseIsNull(){
        _mockService.Setup(x => x.SubmitResponse(null)).Returns(null as MoreOrLessGameResult);
        var sut = new MoreOrLessGameController(_mockILogger.Object, _mockService.Object);
        var result = sut.Post(null);
        result.Should().BeNull();
    }


    [Fact]
    public void Post_ShouldReturnMoreOrLessGameResult_When_ResponseReturnedByService()
    {
        _mockService.Setup(x => x.SubmitResponse(It.IsAny<MoreOrLessGameResponse>())).Returns(new MoreOrLessGameResult() {Score = 10, SubmitedResponse = 100});
        var sut = new MoreOrLessGameController(_mockILogger.Object, _mockService.Object);
        var result = sut.Post(new MoreOrLessGameResponse());
        result.Should().NotBeNull();
        result?.Score.Should().Be(10);
        result?.SubmitedResponse.Should().Be(100);
    }




}
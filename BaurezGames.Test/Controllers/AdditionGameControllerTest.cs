using BaurezGames.Server.Controllers;
using BaurezGames.Shared.AdditionGame;

namespace BaurezGames.Test.Controllers
{
    public class AdditionGameControllerTest
    {
        [Theory]
        [InlineData(1, "1")]
        [InlineData(2, "1 + 1")]
        [InlineData(3, "1 + 1 + 1")]
        public void Get_ShouldReturnString_WhenParameterIsValid(int nbElements,string expectedResult)
        {
            var loggerMock = new Mock<ILogger<AdditionGameController>>();
            var additionGameMock = new Mock<IAdditionGameService>();
            additionGameMock.Setup(x => x.NewGame(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(expectedResult);
            var sut = new AdditionGameController(loggerMock.Object, additionGameMock.Object);
            var result = sut.Get(nbElements);
            result.Should().Be(expectedResult);
        }

    }
}

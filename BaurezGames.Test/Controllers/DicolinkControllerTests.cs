using BaurezGames.Server.Controllers;

namespace BaurezGames.Test.Controllers
{
    public class DicolinkControllerTests
    {
        private readonly Mock<IDicolinkService> _service = new Mock<IDicolinkService>();
        
        [Fact()]
        public async void Get_ShouldReturnNull_WhenResponseIsNull()
        {
            _service.Setup(x => x.GetDefinitionAsync(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(Task.FromResult(null as IEnumerable<DefinitionResponse>));

            var sut = new DicolinkController(_service.Object);

            var result = await sut.GetDefinitions("", 0);
            result.Should().BeNull();
        }


        [Fact()]
        public async void Get_ShouldReturnNull_WhenResponseIsNotNull()
        {
            var mockResponse = new List<DefinitionResponse>()
            {
                new DefinitionResponse() { Id = "1" },
                new DefinitionResponse() { Id = "2" },
                new DefinitionResponse() { Id = "3" }
            };

            _service.Setup(x => x.GetDefinitionAsync(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(Task.FromResult(mockResponse as IEnumerable<DefinitionResponse>)!);
            
            var sut = new DicolinkController(_service.Object);

            var result = await sut.GetDefinitions("", 0);
            
            var definitionResponses = result?.ToList();
            definitionResponses?.Should().NotBeNull();
            definitionResponses?.Count.Should().Be(mockResponse.Count);
        }

    }
}
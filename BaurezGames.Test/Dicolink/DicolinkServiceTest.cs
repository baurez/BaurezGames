using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using BaurezGames.Shared.Dicolink;
using FluentAssertions;
using Moq;
using Moq.Protected;

namespace BaurezGames.Test.Dicolink
{
    public class DicolinkServiceTest
    {
        private const string JsonDefinitionNoResultFound = "{\"error\":\"pas de résultats\"}";

        private const string JsonDefinitionsResult = "[{\"id\":\"M14331-37337\",\"nature\":\"nom masculin\",\"source\":\"larousse\",\"attributionText\":\"Grand Dictionnaire de la langue française numérisé\",\"attributionUrl\":\"https://www.larousse.fr/\",\"mot\":\"cheval\",\"definition\":\"Mammifère herbivore de grande taille, à un seul doigt par membre, coureur rapide des steppes et prairies, dont la domestication a joué un grand rôle dans l'essor des civilisations asiatiques et européennes.\",\"dicolinkUrl\":\"https://www.dicolink.com/mots/cheval\"},{\"id\":\"M14331-37338\",\"nature\":\"nom masculin\",\"source\":\"larousse\",\"attributionText\":\"Grand Dictionnaire de la langue française numérisé\",\"attributionUrl\":\"https://www.larousse.fr/\",\"mot\":\"cheval\",\"definition\":\"Équitation: Faire du cheval.\",\"dicolinkUrl\":\"https://www.dicolink.com/mots/cheval\"}]";

        [Fact]
        public async void GetDefinitions_ShouldReturn_ListDefinitionResponse_ShouldHaveResponse()
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                // Setup the PROTECTED method to mock
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                // prepare the expected response of the mocked http call
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonDefinitionsResult),
                })
                .Verifiable();

            // use real http client with mocked handler here
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            var subjectUnderTest = new DicolinkService(httpClient);

                // ACT
            var result = await subjectUnderTest.GetDefinitionAsync("cheval", 1);

            // ASSERT
            result.Should().NotBeNull();
            result.Count().Should().Be(2);
            result.FirstOrDefault()?.Id.Should().NotBeNullOrWhiteSpace();
            result.FirstOrDefault()?.DicolinkUrl.Should().NotBeNullOrWhiteSpace();
            result.FirstOrDefault()?.AttributionText.Should().NotBeNullOrWhiteSpace();
            result.FirstOrDefault()?.AttributionUrl.Should().NotBeNullOrWhiteSpace();
            result.FirstOrDefault()?.Definition.Should().NotBeNullOrWhiteSpace();
            result.FirstOrDefault()?.Mot.Should().NotBeNullOrWhiteSpace();
            result.FirstOrDefault()?.Nature.Should().NotBeNullOrWhiteSpace();
            result.FirstOrDefault()?.Source.Should().NotBeNullOrWhiteSpace();


            // also check the 'http' call was like we expected it
            var expectedUri = new Uri("http://test.com/v1/mot/cheval/definitions?limite=1&api_key=5kH4tq73lhHGxRYt7nfziWCV3KRgBWwh");

            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1), // we expected a single external request
                ItExpr.Is<HttpRequestMessage>(req =>
                        req.Method == HttpMethod.Get  // we expected a GET request
                        && req.RequestUri == expectedUri // to this uri
                ),
                ItExpr.IsAny<CancellationToken>()
            );

        }

        [Fact]
        public async void GetDefinitions_ShouldReturn_Null_IfRequestHaveNoResult()
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                // Setup the PROTECTED method to mock
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                // prepare the expected response of the mocked http call
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonDefinitionNoResultFound),
                })
                .Verifiable();

            // use real http client with mocked handler here
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            var subjectUnderTest = new DicolinkService(httpClient);

            // ACT
            var result = await subjectUnderTest.GetDefinitionAsync("cheval", 1);

            // ASSERT
            result.Should().BeNull();
        }

        [Fact]
        public async void GetDefinitions_ShouldReturn_Null_WhenResponseCodeIsNotOk()
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                // Setup the PROTECTED method to mock
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                // prepare the expected response of the mocked http call
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest
                })
                .Verifiable();

            // use real http client with mocked handler here
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            var subjectUnderTest = new DicolinkService(httpClient);

            // ACT
            var result = await subjectUnderTest.GetDefinitionAsync("cheval", 1);

            // ASSERT
            result.Should().BeNull();
        }


    }
}

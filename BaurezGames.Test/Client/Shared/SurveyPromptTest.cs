using BaurezGames.Client.Component;
using BaurezGames.Client.Shared;
using Bunit;

namespace BaurezGames.Test.Client.Shared
{
    public class SurveyPromptTest
    {
        [Fact]
        public void Creation_ShouldWork()
        {
            using var ctx = new TestContext();

            var cut = ctx.RenderComponent<SurveyPrompt>(
                ComponentParameterFactory.Parameter(nameof(SurveyPrompt.Title), "xxx")
            );

            cut.Find($"#SurveyPromptTitle").TextContent.Should().Be("xxx");
        }
    }
}




using BaurezGames.Client.Component;
using Bunit;

namespace BaurezGames.Test.Client.Component
{
    public class ComponentDtDdTest
    {
        [Fact]
        public void Creation_ShouldReturnParameters()
        {
            using var ctx = new TestContext();

            var cut = ctx.RenderComponent<ComponentDtDd>(
                ComponentParameterFactory.Parameter(nameof(ComponentDtDd.Value), "xxx"),
                ComponentParameterFactory.Parameter(nameof(ComponentDtDd.Text), "yyy"));


            cut.Find($"dt").TextContent.Should().Be("yyy");
            cut.Find($"dd").TextContent.Should().Be("xxx");
        }
    }
}
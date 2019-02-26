using Craeckersoft.AdvancedPipeline.Internal;
using Craeckersoft.AdvancedPipeline.Tests.Fakes;
using FluentAssertions;
using Xunit;

namespace Craeckersoft.AdvancedPipeline.Tests
{
    public class MiddlewareComponentInvokerTests
    {
        [Fact]
        public void Method_Invoke_RequestIsNotNull_ReturnsReversedRequest()
        {
            // Arrange
            IComponentInvoker<string, string> invoker = new MiddlewareComponent<string, string, string, string>(new FakeMiddleware()).CreateInvoker(new FakeComponentInvoker());

            // Act
            string actual = invoker.Invoke("Unit Tests", null);

            // Assert
            // ReSharper disable once StringLiteralTypo
            actual.Should().Be("stseT tinU");
        }

        [Fact]
        public void Method_Invoke_RequestIsNull_InvokesNextInvoker()
        {
            // Arrange
            IComponentInvoker<string, string> invoker = new MiddlewareComponent<string, string, string, string>(new FakeMiddleware()).CreateInvoker(new FakeComponentInvoker());

            // Act
            string actual = invoker.Invoke(null, null);

            // Assert
            actual.Should().Be("Success");
        }
    }
}
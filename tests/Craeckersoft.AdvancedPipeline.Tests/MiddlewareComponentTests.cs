using System;
using Craeckersoft.AdvancedPipeline.Internal;
using Craeckersoft.AdvancedPipeline.Tests.Fakes;
using FluentAssertions;
using Xunit;

namespace Craeckersoft.AdvancedPipeline.Tests
{
    public class MiddlewareComponentTests
    {
        [Fact]
        public void Constructor_MiddlewareIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            Func<MiddlewareComponent<string, string, string, string>> act = () => new MiddlewareComponent<string, string, string, string>(null);

            // Act - Assert
            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("middleware");
        }

        [Fact]
        public void Method_CreateInvoker_NextIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            MiddlewareComponent<string, string, string, string> component = new MiddlewareComponent<string, string, string, string>(new FakeMiddleware());
            Action act = () => component.CreateInvoker(null);

            // Act - Assert
            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("next");
        }

        [Fact]
        public void Method_CreateInvoker_ReturnsNotNull()
        {
            // Arrange
            MiddlewareComponent<string, string, string, string> component = new MiddlewareComponent<string, string, string, string>(new FakeMiddleware());

            // Act
            IComponentInvoker<string, string> invoker = component.CreateInvoker(new FakeComponentInvoker());

            // Assert
            invoker.Should().NotBeNull();
        }

        [Fact]
        public void Property_Middleware_ReturnsMiddlewareFromConstructor()
        {
            // Arrange
            FakeMiddleware expectedMiddleware = new FakeMiddleware();
            MiddlewareComponent<string, string, string, string> component = new MiddlewareComponent<string, string, string, string>(expectedMiddleware);

            // Act
            IMiddleware<string, string, string, string> actualMiddleware = component.Middleware;

            // Assert
            actualMiddleware.Should().BeSameAs(expectedMiddleware);
        }
    }
}
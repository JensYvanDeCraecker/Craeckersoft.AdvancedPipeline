using System;
using Craeckersoft.AdvancedPipeline.Internal;
using Craeckersoft.AdvancedPipeline.Tests.Fakes;
using FluentAssertions;
using Xunit;

namespace Craeckersoft.AdvancedPipeline.Tests
{
    public class DelegateMiddlewareTests
    {
        [Fact]
        public void Constructor_MiddlewareDelegateIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            Func<DelegateMiddleware<string, string, string, string>> act = () => new DelegateMiddleware<string, string, string, string>(null);

            // Act - Assert
            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("middlewareDelegate");
        }

        [Fact]
        public void Method_Invoke_NextIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            Action act = () => new DelegateMiddleware<string, string, string, string>(FakeDelegates.Middleware).Invoke(null, null, null);

            // Act - Assert
            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("next");
        }

        [Fact]
        public void Method_Invoke_RequestIsNotNull_ReturnsReversedRequest()
        {
            // Arrange
            DelegateMiddleware<string, string, string, string> middleware = new DelegateMiddleware<string, string, string, string>(FakeDelegates.Middleware);

            // Act
            string actual = middleware.Invoke("Unit Tests", null, new FakeComponentInvoker());

            // Assert
            // ReSharper disable once StringLiteralTypo
            actual.Should().Be("stseT tinU");
        }

        [Fact]
        public void Method_Invoke_RequestIsNull_InvokesNextInvoker()
        {
            // Arrange
            DelegateMiddleware<string, string, string, string> middleware = new DelegateMiddleware<string, string, string, string>(FakeDelegates.Middleware);

            // Act
            string actual = middleware.Invoke(null, null, new FakeComponentInvoker());

            // Assert
            actual.Should().Be("Success");
        }

        [Fact]
        public void Property_Delegate_ReturnsMiddlewareDelegateFromConstructor()
        {
            // Arrange
            MiddlewareDelegate<string, string, string, string> expectedMiddlewareDelegate = FakeDelegates.Middleware;
            DelegateMiddleware<string, string, string, string> middleware = new DelegateMiddleware<string, string, string, string>(expectedMiddlewareDelegate);

            // Act
            MiddlewareDelegate<string, string, string, string> actualMiddlewareDelegate = middleware.Delegate;

            // Assert
            actualMiddlewareDelegate.Should().Be(expectedMiddlewareDelegate);
        }
    }
}
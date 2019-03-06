using System;
using Craeckersoft.AdvancedPipeline.Tests.Utilities.Assertions;
using Craeckersoft.AdvancedPipeline.Tests.Utilities.Fakes;
using FluentAssertions;
using Xunit;

namespace Craeckersoft.AdvancedPipeline.Tests
{
    public class MiddlewareTests
    {
        [Fact]
        public void Method_FromDelegate_MiddlewareDelegateIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            Action act = () => Middleware.FromDelegate((MiddlewareDelegate<object, object, object, object>)null);

            // Act - Assert
            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("middlewareDelegate");
        }

        [Fact]
        public void Method_FromDelegate_ReturnsDelegateMiddleware()
        {
            // Arrange
            MiddlewareDelegate<object, object, object, object> expectedMiddlewareDelegate = FakeDelegates.Middleware(null);

            // Act
            IMiddleware<object, object, object, object> middleware = Middleware.FromDelegate(expectedMiddlewareDelegate);

            // Assert
            middleware.Should().BeDelegateMiddleware().Which.Delegate.Should().BeSameAs(expectedMiddlewareDelegate);
        }
    }
}
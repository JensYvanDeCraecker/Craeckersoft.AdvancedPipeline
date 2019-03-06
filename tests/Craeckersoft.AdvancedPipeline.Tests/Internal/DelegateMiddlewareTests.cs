using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Craeckersoft.AdvancedPipeline.Internal;
using Craeckersoft.AdvancedPipeline.Tests.Utilities;
using Craeckersoft.AdvancedPipeline.Tests.Utilities.Fakes;
using FluentAssertions;
using Xunit;

namespace Craeckersoft.AdvancedPipeline.Tests.Internal
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
        public async Task Method_InvokeAsync_UsesMiddlewareDelegate()
        {
            // Arrange
            object expected = new object();
            ISet<TestType> tests = new HashSet<TestType>();
            DelegateMiddleware<object, object, object, object> middleware = new DelegateMiddleware<object, object, object, object>(FakeDelegates.Middleware(tests));

            // Act
            object actual = await middleware.InvokeAsync(expected, new FakeInvocationContext(), new FakeComponentInvoker(tests));

            // Assert
            actual.Should().BeSameAs(expected);
            tests.Remove(TestType.CurrentInvoker).Should().BeTrue();
            tests.Remove(TestType.Request).Should().BeTrue();
            tests.Remove(TestType.InvocationContext).Should().BeTrue();
            tests.Remove(TestType.NextInvoker).Should().BeTrue();
            tests.Remove(TestType.NextInvokerInvoked).Should().BeTrue();
            tests.Should().BeEmpty();
        }

        [Fact]
        public void Property_Delegate_ReturnsMiddlewareDelegateFromConstructor()
        {
            // Arrange
            MiddlewareDelegate<object, object, object, object> expectedMiddlewareDelegate = FakeDelegates.Middleware(null);
            DelegateMiddleware<object, object, object, object> middleware = new DelegateMiddleware<object, object, object, object>(expectedMiddlewareDelegate);

            // Act
            MiddlewareDelegate<object, object, object, object> actualMiddlewareDelegate = middleware.Delegate;

            // Assert
            actualMiddlewareDelegate.Should().Be(expectedMiddlewareDelegate);
        }
    }
}
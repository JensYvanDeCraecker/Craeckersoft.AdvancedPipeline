using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Craeckersoft.AdvancedPipeline.Tests.TestUtilities;
using Craeckersoft.AdvancedPipeline.Tests.TestUtilities.Fakes;
using Craeckersoft.AdvancedPipeline.Utilities;
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
            Func<DelegateMiddleware<object, object, object, object>> act = () => Middleware.FromDelegate((MiddlewareDelegate<object, object, object, object>)null);

            // Act - Assert
            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("middlewareDelegate");
        }

        [Fact]
        public async Task Method_InvokeAsync_UsesMiddlewareDelegate()
        {
            // Arrange
            object expected = new object();
            ISet<TestItem> tests = new HashSet<TestItem>();
            DelegateMiddleware<object, object, object, object> middleware = Middleware.FromDelegate(FakeDelegates.Middleware(tests));

            // Act
            object actual = await middleware.InvokeAsync(expected, new FakeInvocationContext(), new FakeComponentInvoker(tests));

            // Assert
            actual.Should().BeSameAs(expected);
            tests.Remove(TestItem.CurrentInvoker).Should().BeTrue();
            tests.Remove(TestItem.Request).Should().BeTrue();
            tests.Remove(TestItem.InvocationContext).Should().BeTrue();
            tests.Remove(TestItem.NextInvoker).Should().BeTrue();
            tests.Remove(TestItem.NextInvokerInvoked).Should().BeTrue();
            tests.Should().BeEmpty();
        }

        [Fact]
        public void Property_Delegate_ReturnsMiddlewareDelegateFromConstructor()
        {
            // Arrange
            MiddlewareDelegate<object, object, object, object> expectedMiddlewareDelegate = FakeDelegates.Middleware(null);
            DelegateMiddleware<object, object, object, object> middleware = Middleware.FromDelegate(expectedMiddlewareDelegate);

            // Act
            MiddlewareDelegate<object, object, object, object> actualMiddlewareDelegate = middleware.Delegate;

            // Assert
            actualMiddlewareDelegate.Should().Be(expectedMiddlewareDelegate);
        }

        [Fact]
        public void Property_Item_IsSameAsDelegate()
        {
            // Arrange
            DelegateMiddleware<object, object, object, object> delegateMiddleware = Middleware.FromDelegate(FakeDelegates.Middleware(null));

            // Act
            MiddlewareDelegate<object, object, object, object> actual1 = ((IWrapper<MiddlewareDelegate<object, object, object, object>>)delegateMiddleware).Item;
            object actual2 = ((IWrapper)delegateMiddleware).Item;

            // Assert
            actual1.Should().BeSameAs(delegateMiddleware.Delegate);
            actual2.Should().BeSameAs(delegateMiddleware.Delegate);
        }
    }
}
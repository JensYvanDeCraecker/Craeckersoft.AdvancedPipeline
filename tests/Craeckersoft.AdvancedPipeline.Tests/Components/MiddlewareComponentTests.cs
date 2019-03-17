using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Craeckersoft.AdvancedPipeline.Components;
using Craeckersoft.AdvancedPipeline.Tests.TestUtilities;
using Craeckersoft.AdvancedPipeline.Tests.TestUtilities.Fakes;
using FluentAssertions;
using Xunit;

namespace Craeckersoft.AdvancedPipeline.Tests.Components
{
    public class MiddlewareComponentTests
    {
        public class InvokerTests
        {
            [Fact]
            public async Task Method_InvokeAsync_UsesMiddleware()
            {
                // Arrange
                object expected = new object();
                ISet<TestItem> tests = new HashSet<TestItem>();
                IInvoker<object, object> componentInvoker = Component.FromMiddleware(new FakeMiddleware(tests)).GetInvoker(new FakeComponentInvoker(tests));

                // Act
                object actual = await componentInvoker.InvokeAsync(expected, new FakeInvocationContext());

                // Assert
                actual.Should().BeSameAs(expected);
                tests.Remove(TestItem.CurrentInvoker).Should().BeTrue();
                tests.Remove(TestItem.Request).Should().BeTrue();
                tests.Remove(TestItem.InvocationContext).Should().BeTrue();
                tests.Remove(TestItem.NextInvoker).Should().BeTrue();
                tests.Remove(TestItem.NextInvokerInvoked).Should().BeTrue();
                tests.Should().BeEmpty();
            }
        }

        [Fact]
        public void Method_CreateInvoker_NextIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            Action act = () => Component.FromMiddleware(new FakeMiddleware(null)).GetInvoker(null);

            // Act - Assert
            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("next");
        }

        [Fact]
        public void Method_CreateInvoker_ReturnsInvoker()
        {
            // Arrange
            MiddlewareComponent<object, object, object, object> component = Component.FromMiddleware(new FakeMiddleware(null));

            // Act
            IInvoker<object, object> invoker = component.GetInvoker(new FakeComponentInvoker(null));

            // Assert
            invoker.Should().NotBeNull();
        }

        [Fact]
        public void Property_Middleware_ReturnsMiddlewareFromConstructor()
        {
            // Arrange
            IMiddleware<object, object, object, object> expectedMiddleware = new FakeMiddleware(null);
            MiddlewareComponent<object, object, object, object> component = Component.FromMiddleware(expectedMiddleware);

            // Act
            IMiddleware<object, object, object, object> actualMiddleware = component.Middleware;

            // Assert
            actualMiddleware.Should().BeSameAs(expectedMiddleware);
        }
    }
}
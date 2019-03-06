using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Craeckersoft.AdvancedPipeline.Components;
using Craeckersoft.AdvancedPipeline.Components.Internal;
using Craeckersoft.AdvancedPipeline.Tests.Utilities;
using Craeckersoft.AdvancedPipeline.Tests.Utilities.Assertions;
using Craeckersoft.AdvancedPipeline.Tests.Utilities.Fakes;
using FluentAssertions;
using Xunit;

namespace Craeckersoft.AdvancedPipeline.Tests.Components.Internal
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
                ISet<TestType> tests = new HashSet<TestType>();
                IComponentInvoker<object, object> componentInvoker = new MiddlewareComponent<object, object, object, object>(new FakeMiddleware(tests)).CreateInvoker(new FakeComponentInvoker(tests));

                // Act
                object actual = await componentInvoker.InvokeAsync(expected, new FakeInvocationContext());

                // Assert
                actual.Should().BeSameAs(expected);
                tests.Remove(TestType.CurrentInvoker).Should().BeTrue();
                tests.Remove(TestType.Request).Should().BeTrue();
                tests.Remove(TestType.InvocationContext).Should().BeTrue();
                tests.Remove(TestType.NextInvoker).Should().BeTrue();
                tests.Remove(TestType.NextInvokerInvoked).Should().BeTrue();
                tests.Should().BeEmpty();
            }
        }

        [Fact]
        public void Constructor_MiddlewareIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            Func<MiddlewareComponent<object, object, object, object>> act = () => new MiddlewareComponent<object, object, object, object>(null);

            // Act - Assert
            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("middleware");
        }

        [Fact]
        public void Method_CreateInvoker_NextIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            Action act = () => new MiddlewareComponent<object, object, object, object>(new FakeMiddleware(null)).CreateInvoker(null);

            // Act - Assert
            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("next");
        }

        [Fact]
        public void Method_CreateInvoker_ReturnsInvoker()
        {
            // Arrange
            MiddlewareComponent<object, object, object, object> component = new MiddlewareComponent<object, object, object, object>(new FakeMiddleware(null));

            // Act
            IComponentInvoker<object, object> invoker = component.CreateInvoker(new FakeComponentInvoker(null));

            // Assert
            invoker.Should().NotBeNull();
        }

        [Fact]
        public void Property_Middleware_ReturnsMiddlewareFromConstructor()
        {
            // Arrange
            IMiddleware<object, object, object, object> expectedMiddleware = new FakeMiddleware(null);
            MiddlewareComponent<object, object, object, object> component = new MiddlewareComponent<object, object, object, object>(expectedMiddleware);

            // Act
            IMiddleware<object, object, object, object> actualMiddleware = component.Middleware;

            // Assert
            actualMiddleware.Should().BeSameAs(expectedMiddleware);
        }
    }
}
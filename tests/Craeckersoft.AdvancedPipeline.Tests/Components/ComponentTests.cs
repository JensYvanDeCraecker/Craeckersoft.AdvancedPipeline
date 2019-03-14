using System;
using Craeckersoft.AdvancedPipeline.Components;
using Craeckersoft.AdvancedPipeline.Tests.TestUtilities.Assertions;
using Craeckersoft.AdvancedPipeline.Tests.TestUtilities.Fakes;
using FluentAssertions;
using Xunit;

namespace Craeckersoft.AdvancedPipeline.Tests.Components
{
    public class ComponentTests
    {
        [Fact]
        public void Method_FromDelegate_ComponentDelegateIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            Action act = () => Component.FromDelegate((ComponentDelegate<object, object, object, object>)null);

            // Act - Assert
            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("componentDelegate");
        }

        [Fact]
        public void Method_FromDelegate_ReturnsDelegateComponent()
        {
            // Arrange
            ComponentDelegate<object, object, object, object> expectedComponentDelegate = FakeDelegates.Component(null);

            // Act
            DelegateComponent<object, object, object, object> component = Component.FromDelegate(expectedComponentDelegate);

            // Assert
            component.Should().NotBeNull();
        }

        [Fact]
        public void Method_FromFilter_FilterIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            Action act = () => Component.FromFilter<object, object, object>(null);

            // Act - Assert
            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("filter");
        }

        [Fact]
        public void Method_FromFilter_ReturnsFilterComponent()
        {
            // Arrange
            IFilter<object, object> expectedFilter = new FakeFilter(null);

            // Act
            FilterComponent<object, object, object> component = Component.FromFilter<object, object, object>(expectedFilter);

            // Act
            component.Should().NotBeNull();
        }

        [Fact]
        public void Method_FromMiddleware_MiddlewareIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            Action act = () => Component.FromMiddleware((IMiddleware<string, string, string, string>)null);

            // Act - Assert
            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("middleware");
        }

        [Fact]
        public void Method_FromMiddleware_ReturnsMiddlewareComponent()
        {
            // Arrange
            IMiddleware<object, object, object, object> expectedMiddleware = new FakeMiddleware(null);

            // Act
            MiddlewareComponent<object, object, object, object> component = Component.FromMiddleware(expectedMiddleware);

            // Assert
            component.Should().NotBeNull();
        }
    }
}
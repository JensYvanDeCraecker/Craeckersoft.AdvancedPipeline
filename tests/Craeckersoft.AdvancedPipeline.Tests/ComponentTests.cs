using System;
using Craeckersoft.AdvancedPipeline.Tests.Fakes;
using FluentAssertions;
using Xunit;

namespace Craeckersoft.AdvancedPipeline.Tests
{
    public class ComponentTests
    {
        [Fact]
        public void Method_FromComponent_ReturnsDelegateComponent()
        {
            // Arrange
            ComponentDelegate<string, string, string, string> expectedComponentDelegate = FakeDelegates.Component;

            // Act
            IComponent<string, string, string, string> component = Component.FromDelegate(expectedComponentDelegate);

            // Assert
            component.Should().BeDelegateComponent().Which.Delegate.Should().BeSameAs(expectedComponentDelegate);
        }

        [Fact]
        public void Method_FromDelegate_ArgumentNull_ThrowsArgumentNullException()
        {
            // Arrange
            Action act = () => Component.FromDelegate((ComponentDelegate<string, string, string, string>)null);

            // Act - Assert
            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("componentDelegate");
        }

        [Fact]
        public void Method_FromFilter_ArgumentNull_ThrowsArgumentNullException()
        {
            // Arrange
            Action act = () => Component.FromFilter<string, string, string>((FilterDelegate<string, string>)null);

            // Act - Assert
            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("filterDelegate");
        }

        [Fact]
        public void Method_FromFilter_Delegate_ArgumentNull_ThrowsArgumentNullException()
        {
            // Arrange
            Action act = () => Component.FromFilter<string, string, string>((IFilter<string, string>)null);

            // Act - Assert
            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("filter");
        }

        [Fact]
        public void Method_FromFilter_Delegate_ReturnsFilterComponentWithDelegateFilter()
        {
            // Arrange
            FilterDelegate<string, string> expectedFilterDelegate = FakeDelegates.Filter;

            // Act
            IComponent<string, string, string, string> component = Component.FromFilter<string, string, string>(expectedFilterDelegate);

            // Assert
            component.Should().BeFilterComponent().Which.Filter.Should().BeDelegateFilter().Which.Delegate.Should().BeSameAs(expectedFilterDelegate);
        }

        [Fact]
        public void Method_FromFilter_ReturnsFilterComponent()
        {
            // Arrange
            FakeFilter expectedFilter = new FakeFilter();

            // Act
            IComponent<string, string, string, string> component = Component.FromFilter<string, string, string>(expectedFilter);

            // Act
            component.Should().BeFilterComponent().Which.Filter.Should().BeSameAs(expectedFilter);
        }

        [Fact]
        public void Method_FromMiddleware_ArgumentNull_ThrowsArgumentNullException()
        {
            // Arrange
            Action act = () => Component.FromMiddleware((IMiddleware<string, string, string, string>)null);

            // Act - Assert
            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("middleware");
        }

        [Fact]
        public void Method_FromMiddleware_Delegate_ArgumentNull_ThrowsArgumentNullException()
        {
            // Arrange
            Action act = () => Component.FromMiddleware((MiddlewareDelegate<string, string, string, string>)null);

            // Act - Assert
            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("middlewareDelegate");
        }

        [Fact]
        public void Method_FromMiddleware_Delegate_ReturnsMiddlewareComponentWithDelegateMiddleware()
        {
            // Arrange
            MiddlewareDelegate<string, string, string, string> expectedMiddlewareDelegate = FakeDelegates.Middleware;

            // Act
            IComponent<string, string, string, string> component = Component.FromMiddleware(expectedMiddlewareDelegate);

            // Assert
            component.Should().BeMiddlewareComponent().Which.Middleware.Should().BeDelegateMiddleware().Which.Delegate.Should().BeSameAs(expectedMiddlewareDelegate);
        }

        [Fact]
        public void Method_FromMiddleware_ReturnsMiddlewareComponent()
        {
            // Arrange
            FakeMiddleware expectedMiddleware = new FakeMiddleware();

            // Act
            IComponent<string, string, string, string> component = Component.FromMiddleware(expectedMiddleware);

            // Assert
            component.Should().BeMiddlewareComponent().Which.Middleware.Should().BeSameAs(expectedMiddleware);
        }
    }
}
using System;
using Craeckersoft.AdvancedPipeline.Tests.Fakes;
using FluentAssertions;
using Xunit;

namespace Craeckersoft.AdvancedPipeline.Tests
{
    public class ComponentTests
    {
        [Fact]
        public void FromComponent_ReturnsDelegateComponent()
        {
            // Arrange
            ComponentDelegate<string, string, string, string> componentDelegate = FakeDelegates.Component;

            // Act
            IComponent<string, string, string, string> component = Component.FromDelegate(componentDelegate);

            // Assert
            component.Should().BeDelegateComponent().Which.ComponentDelegate.Should().BeSameAs(componentDelegate);
        }

        [Fact]
        public void FromDelegate_ArgumentNull_ThrowsArgumentNullException()
        {
            // Arrange
            Action act = () => Component.FromDelegate((ComponentDelegate<string, string, string, string>)null);

            // Act - Assert
            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("componentDelegate");
        }

        [Fact]
        public void FromFilter_ArgumentNull_ThrowsArgumentNullException()
        {
            // Arrange
            Action act = () => Component.FromFilter<string, string, string>((FilterDelegate<string, string>)null);

            // Act - Assert
            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("filterDelegate");
        }

        [Fact]
        public void FromFilter_Delegate_ArgumentNull_ThrowsArgumentNullException()
        {
            // Arrange
            Action act = () => Component.FromFilter<string, string, string>((IFilter<string, string>)null);

            // Act - Assert
            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("filter");
        }

        [Fact]
        public void FromFilter_Delegate_ReturnsFilterComponentWithDelegateFilter()
        {
            // Arrange
            FilterDelegate<string, string> filterDelegate = FakeDelegates.Filter;

            // Act
            IComponent<string, string, string, string> component = Component.FromFilter<string, string, string>(filterDelegate);

            // Assert
            component.Should().BeFilterComponent().Which.Filter.Should().BeDelegateFilter().Which.FilterDelegate.Should().BeSameAs(filterDelegate);
        }

        [Fact]
        public void FromFilter_ReturnsFilterComponent()
        {
            // Arrange
            FakeFilter stubFilter = new FakeFilter();

            // Act
            IComponent<string, string, string, string> component = Component.FromFilter<string, string, string>(stubFilter);

            // Act
            component.Should().BeFilterComponent().Which.Filter.Should().BeSameAs(stubFilter);
        }

        [Fact]
        public void FromMiddleware_ArgumentNull_ThrowsArgumentNullException()
        {
            // Arrange
            Action act = () => Component.FromMiddleware((IMiddleware<string, string, string, string>)null);

            // Act - Assert
            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("middleware");
        }

        [Fact]
        public void FromMiddleware_Delegate_ArgumentNull_ThrowsArgumentNullException()
        {
            // Arrange
            Action act = () => Component.FromMiddleware((MiddlewareDelegate<string, string, string, string>)null);

            // Act - Assert
            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("middlewareDelegate");
        }

        [Fact]
        public void FromMiddleware_Delegate_ReturnsMiddlewareComponentWithDelegateMiddleware()
        {
            // Arrange
            MiddlewareDelegate<string, string, string, string> middlewareDelegate = FakeDelegates.Middleware;

            // Act
            IComponent<string, string, string, string> component = Component.FromMiddleware(middlewareDelegate);

            // Assert
            component.Should().BeMiddlewareComponent().Which.Middleware.Should().BeDelegateMiddleware().Which.MiddlewareDelegate.Should().BeSameAs(middlewareDelegate);
        }

        [Fact]
        public void FromMiddleware_ReturnsMiddlewareComponent()
        {
            // Arrange
            FakeMiddleware stubMiddleware = new FakeMiddleware();

            // Act
            IComponent<string, string, string, string> component = Component.FromMiddleware(stubMiddleware);

            // Assert
            component.Should().BeMiddlewareComponent().Which.Middleware.Should().BeSameAs(stubMiddleware);
        }
    }
}
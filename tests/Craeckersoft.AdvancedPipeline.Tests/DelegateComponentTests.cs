using System;
using Craeckersoft.AdvancedPipeline.Internal;
using Craeckersoft.AdvancedPipeline.Tests.Fakes;
using FluentAssertions;
using Xunit;

namespace Craeckersoft.AdvancedPipeline.Tests
{
    public class DelegateComponentTests
    {
        [Fact]
        public void Constructor_ComponentDelegateIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            Func<DelegateComponent<string, string, string, string>> act = () => new DelegateComponent<string, string, string, string>(null);

            // Act - Assert
            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("componentDelegate");
        }

        [Fact]
        public void Method_CreateInvoker_NextIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            DelegateComponent<string, string, string, string> component = new DelegateComponent<string, string, string, string>(FakeDelegates.Component);
            Action act = () => component.CreateInvoker(null);

            // Act - Assert
            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("next");
        }

        [Fact]
        public void Method_CreateInvoker_ReturnsNotNull()
        {
            // Arrange
            DelegateComponent<string, string, string, string> component = new DelegateComponent<string, string, string, string>(FakeDelegates.Component);

            // Act
            IComponentInvoker<string, string> invoker = component.CreateInvoker(new FakeComponentInvoker());

            // Assert
            invoker.Should().NotBeNull();
        }

        [Fact]
        public void Method_GetInvoker_ComponentDelegateReturnsNull_ThrowsInvalidOperationException()
        {
            // Arrange
            DelegateComponent<string, string, string, string> component = new DelegateComponent<string, string, string, string>(FakeDelegates.ComponentNull);
            Action act = () => component.CreateInvoker(new FakeComponentInvoker());

            // Act - Assert
            act.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Property_Delegate_ReturnsComponentDelegateFromConstructor()
        {
            // Arrange
            ComponentDelegate<string, string, string, string> expectedComponentDelegate = FakeDelegates.Component;
            DelegateComponent<string, string, string, string> component = new DelegateComponent<string, string, string, string>(expectedComponentDelegate);

            // Act
            ComponentDelegate<string, string, string, string> actualComponentDelegate = component.Delegate;

            // Assert
            actualComponentDelegate.Should().BeSameAs(expectedComponentDelegate);
        }
    }
}
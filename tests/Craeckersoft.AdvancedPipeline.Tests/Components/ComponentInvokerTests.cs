using System;
using Craeckersoft.AdvancedPipeline.Components;
using Craeckersoft.AdvancedPipeline.Tests.Utilities.Assertions;
using Craeckersoft.AdvancedPipeline.Tests.Utilities.Fakes;
using FluentAssertions;
using Xunit;

namespace Craeckersoft.AdvancedPipeline.Tests.Components
{
    public class ComponentInvokerTests
    {
        [Fact]
        public void Method_FromComponent_ReturnsDelegateComponentInvoker()
        {
            // Arrange
            ComponentInvokerDelegate<object, object> expectedComponentInvokerDelegateDelegate = FakeDelegates.ComponentInvoker(null);

            // Act
            IComponentInvoker<object, object> componentInvoker = ComponentInvoker.FromDelegate(expectedComponentInvokerDelegateDelegate);

            // Assert
            componentInvoker.Should().BeDelegateComponentInvoker().Which.Delegate.Should().BeSameAs(expectedComponentInvokerDelegateDelegate);
        }

        [Fact]
        public void Method_FromDelegate_ArgumentIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            Action act = () => ComponentInvoker.FromDelegate((ComponentInvokerDelegate<object, object>)null);

            // Act - Assert
            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("componentInvokerDelegate");
        }
    }
}
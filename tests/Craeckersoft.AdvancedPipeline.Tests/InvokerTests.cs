using System;
using Craeckersoft.AdvancedPipeline.Tests.TestUtilities.Fakes;
using FluentAssertions;
using Xunit;

namespace Craeckersoft.AdvancedPipeline.Tests
{
    public class InvokerTests
    {
        [Fact]
        public void Method_FromComponent_ReturnsDelegateInvoker()
        {
            // Arrange
            InvokerDelegate<object, object> expectedComponentInvokerDelegateDelegate = FakeDelegates.ComponentInvoker(null);

            // Act
            DelegateInvoker<object, object> componentInvoker = Invoker.FromDelegate(expectedComponentInvokerDelegateDelegate);

            // Assert
            componentInvoker.Should().NotBeNull();
        }

        [Fact]
        public void Method_FromDelegate_ArgumentIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            Action act = () => Invoker.FromDelegate((InvokerDelegate<object, object>)null);

            // Act - Assert
            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("componentInvokerDelegate");
        }
    }
}
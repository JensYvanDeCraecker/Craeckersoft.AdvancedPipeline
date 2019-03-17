using System.Collections.Generic;
using System.Threading.Tasks;
using Craeckersoft.AdvancedPipeline.Tests.TestUtilities;
using Craeckersoft.AdvancedPipeline.Tests.TestUtilities.Fakes;
using FluentAssertions;
using Xunit;

namespace Craeckersoft.AdvancedPipeline.Tests
{
    public class DelegateInvokerTests
    {
        [Fact]
        public async Task Method_InvokeAsync_UsesInvokerDelegate()
        {
            // Arrange
            object expected = new object();
            ISet<TestItem> tests = new HashSet<TestItem>();
            DelegateInvoker<object, object> componentInvoker = Invoker.FromDelegate(FakeDelegates.ComponentInvoker(tests));

            // Act
            object actual = await componentInvoker.InvokeAsync(expected, new FakeInvocationContext());

            // Assert
            actual.Should().BeSameAs(expected);
            tests.Remove(TestItem.NextInvokerInvoked).Should().BeTrue();
            tests.Should().BeEmpty();
        }

        [Fact]
        public void Property_Delegate_ReturnsInvokerDelegateFromConstructor()
        {
            // Arrange
            InvokerDelegate<object, object> expectedComponentInvokerDelegate = FakeDelegates.ComponentInvoker(null);
            DelegateInvoker<object, object> componentInvoker = Invoker.FromDelegate(expectedComponentInvokerDelegate);

            // Act
            InvokerDelegate<object, object> actualComponentInvokerDelegate = componentInvoker.Delegate;

            // Assert
            actualComponentInvokerDelegate.Should().BeSameAs(expectedComponentInvokerDelegate);
        }
    }
}
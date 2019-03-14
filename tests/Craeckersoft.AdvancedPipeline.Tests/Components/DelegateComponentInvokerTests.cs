using System.Collections.Generic;
using System.Threading.Tasks;
using Craeckersoft.AdvancedPipeline.Components;
using Craeckersoft.AdvancedPipeline.Tests.TestUtilities;
using Craeckersoft.AdvancedPipeline.Tests.TestUtilities.Fakes;
using Craeckersoft.AdvancedPipeline.Utilities;
using FluentAssertions;
using Xunit;

namespace Craeckersoft.AdvancedPipeline.Tests.Components
{
    public class DelegateComponentInvokerTests
    {
        [Fact]
        public async Task Method_InvokeAsync_UsesComponentInvokerDelegate()
        {
            // Arrange
            object expected = new object();
            ISet<TestItem> tests = new HashSet<TestItem>();
            DelegateComponentInvoker<object, object> componentInvoker = ComponentInvoker.FromDelegate(FakeDelegates.ComponentInvoker(tests));

            // Act
            object actual = await componentInvoker.InvokeAsync(expected, new FakeInvocationContext());

            // Assert
            actual.Should().BeSameAs(expected);
            tests.Remove(TestItem.NextInvokerInvoked).Should().BeTrue();
            tests.Should().BeEmpty();
        }

        [Fact]
        public void Property_Delegate_ReturnsComponentInvokerDelegateFromConstructor()
        {
            // Arrange
            ComponentInvokerDelegate<object, object> expectedComponentInvokerDelegate = FakeDelegates.ComponentInvoker(null);
            DelegateComponentInvoker<object, object> componentInvoker = ComponentInvoker.FromDelegate(expectedComponentInvokerDelegate);

            // Act
            ComponentInvokerDelegate<object, object> actualComponentInvokerDelegate = componentInvoker.Delegate;

            // Assert
            actualComponentInvokerDelegate.Should().BeSameAs(expectedComponentInvokerDelegate);
        }

        [Fact]
        public void Property_Item_IsSameAsDelegate()
        {
            // Arrange
            DelegateComponentInvoker<object, object> componentInvoker = ComponentInvoker.FromDelegate(FakeDelegates.ComponentInvoker(null));

            // Act
            ComponentInvokerDelegate<object, object> actual1 = ((IWrapper<ComponentInvokerDelegate<object, object>>)componentInvoker).Item;
            object actual2 = ((IWrapper)componentInvoker).Item;

            // Assert
            actual1.Should().BeSameAs(componentInvoker.Delegate);
            actual2.Should().BeSameAs(componentInvoker.Delegate);
        }
    }
}
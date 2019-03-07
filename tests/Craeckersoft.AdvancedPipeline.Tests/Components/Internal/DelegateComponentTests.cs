using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Craeckersoft.AdvancedPipeline.Components;
using Craeckersoft.AdvancedPipeline.Components.Internal;
using Craeckersoft.AdvancedPipeline.Tests.TestUtilities;
using Craeckersoft.AdvancedPipeline.Tests.TestUtilities.Assertions;
using Craeckersoft.AdvancedPipeline.Tests.TestUtilities.Fakes;
using Craeckersoft.AdvancedPipeline.Utilities;
using FluentAssertions;
using Xunit;

namespace Craeckersoft.AdvancedPipeline.Tests.Components.Internal
{
    public class DelegateComponentTests
    {
        public class InvokerTests
        {
            [Fact]
            public async Task Method_InvokeAsync_UsesComponentDelegate()
            {
                // Arrange
                object expected = new object();
                ISet<TestItem> tests = new HashSet<TestItem>();
                IComponentInvoker<object, object> componentInvoker = new DelegateComponent<object, object, object, object>(FakeDelegates.Component(tests)).CreateInvoker(new FakeComponentInvoker(tests));

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
        public void Constructor_ComponentDelegateIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            Func<DelegateComponent<object, object, object, object>> act = () => new DelegateComponent<object, object, object, object>(null);

            // Act - Assert
            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("componentDelegate");
        }

        [Fact]
        public void Method_CreateInvoker_ReturnsInvoker()
        {
            // Arrange
            DelegateComponent<object, object, object, object> component = new DelegateComponent<object, object, object, object>(FakeDelegates.Component(null));

            // Act
            IComponentInvoker<object, object> invoker = component.CreateInvoker(new FakeComponentInvoker(null));

            // Assert
            invoker.Should().NotBeNull();
        }

        [Fact]
        public void Method_GetInvoker_ComponentDelegateReturnsNull_ThrowsInvalidOperationException()
        {
            // Arrange
            DelegateComponent<object, object, object, object> component = new DelegateComponent<object, object, object, object>(FakeDelegates.ComponentNull);
            Action act = () => component.CreateInvoker(new FakeComponentInvoker(null));

            // Act - Assert
            act.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Property_Delegate_ReturnsComponentDelegateFromConstructor()
        {
            // Arrange
            ComponentDelegate<object, object, object, object> expectedComponentDelegate = FakeDelegates.Component(null);
            DelegateComponent<object, object, object, object> component = new DelegateComponent<object, object, object, object>(expectedComponentDelegate);

            // Act
            ComponentDelegate<object, object, object, object> actualComponentDelegate = component.Delegate;

            // Assert
            actualComponentDelegate.Should().BeSameAs(expectedComponentDelegate);
        }

        [Fact]
        public void Property_Item_IsSameAsDelegate()
        {
            // Arrange
            DelegateComponent<object, object, object, object> component = new DelegateComponent<object, object, object, object>(FakeDelegates.Component(null));

            // Act
            ComponentDelegate<object, object, object, object> actual = ((IWrapper<ComponentDelegate<object, object, object, object>>)component).Item;

            // Assert
            actual.Should().BeSameAs(component.Delegate);
        }
    }
}
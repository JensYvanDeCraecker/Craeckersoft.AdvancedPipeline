using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Craeckersoft.AdvancedPipeline.Components;
using Craeckersoft.AdvancedPipeline.Components.Internal;
using Craeckersoft.AdvancedPipeline.Tests.Utilities;
using Craeckersoft.AdvancedPipeline.Tests.Utilities.Fakes;
using FluentAssertions;
using Xunit;

namespace Craeckersoft.AdvancedPipeline.Tests.Components.Internal
{
    public class DelegateComponentInvokerTests
    {
        [Fact]
        public void Constructor_ComponentInvokerDelegateIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            Func<DelegateComponentInvoker<string, string>> act = () => new DelegateComponentInvoker<string, string>(null);

            // Act - Assert
            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("componentInvokerDelegate");
        }

        [Fact]
        public async Task Method_InvokeAsync_UsesComponentInvokerDelegate()
        {
            // Arrange
            object expected = new object();
            ISet<TestType> tests = new HashSet<TestType>();
            IComponentInvoker<object, object> componentInvoker = new DelegateComponentInvoker<object, object>(FakeDelegates.ComponentInvoker(tests));

            // Act
            object actual = await componentInvoker.InvokeAsync(expected, new FakeInvocationContext());

            // Assert
            actual.Should().BeSameAs(expected);
            tests.Remove(TestType.NextInvokerInvoked).Should().BeTrue();
            tests.Should().BeEmpty();
        }

        [Fact]
        public void Property_Delegate_ReturnsComponentInvokerDelegateFromConstructor()
        {
            // Arrange
            ComponentInvokerDelegate<object, object> expectedComponentInvokerDelegate = FakeDelegates.ComponentInvoker(null);
            DelegateComponentInvoker<object, object> componentInvoker = new DelegateComponentInvoker<object, object>(expectedComponentInvokerDelegate);

            // Act
            ComponentInvokerDelegate<object, object> actualComponentInvokerDelegate = componentInvoker.Delegate;

            // Assert
            actualComponentInvokerDelegate.Should().BeSameAs(expectedComponentInvokerDelegate);
        }
    }
}
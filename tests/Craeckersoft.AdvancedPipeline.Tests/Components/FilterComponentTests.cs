using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Craeckersoft.AdvancedPipeline.Components;
using Craeckersoft.AdvancedPipeline.Tests.TestUtilities;
using Craeckersoft.AdvancedPipeline.Tests.TestUtilities.Fakes;
using FluentAssertions;
using Xunit;

namespace Craeckersoft.AdvancedPipeline.Tests.Components
{
    public class FilterComponentTests
    {
        public class InvokerTests
        {
            [Fact]
            public async Task Method_InvokeAsync_UsesFilter()
            {
                // Arrange
                object expected = new object();
                ISet<TestItem> tests = new HashSet<TestItem>();
                IInvoker<object, object> componentInvoker = Component.FromFilter<object, object, object>(new FakeFilter(tests)).GetInvoker(new FakeComponentInvoker(tests));

                // Act
                object actual = await componentInvoker.InvokeAsync(expected, new FakeInvocationContext());

                // Assert
                actual.Should().BeSameAs(expected);
                tests.Remove(TestItem.CurrentInvoker).Should().BeTrue();
                tests.Remove(TestItem.Request).Should().BeTrue();
                tests.Remove(TestItem.InvocationContext).Should().BeTrue();
                tests.Remove(TestItem.NextInvokerInvoked).Should().BeTrue();
                tests.Should().BeEmpty();
            }
        }

        [Fact]
        public void Method_CreateInvoker_NextIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            FilterComponent<object, object, object> component = Component.FromFilter<object, object, object>(new FakeFilter(null));
            Action act = () => component.GetInvoker(null);

            // Act - Assert
            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("next");
        }

        [Fact]
        public void Method_CreateInvoker_ReturnsInvoker()
        {
            // Arrange
            FilterComponent<object, object, object> component = Component.FromFilter<object, object, object>(new FakeFilter(null));

            // Act
            IInvoker<object, object> invoker = component.GetInvoker(new FakeComponentInvoker(null));

            // Assert
            invoker.Should().NotBeNull();
        }

        [Fact]
        public void Property_Filter_ReturnsFilterFromConstructor()
        {
            // Arrange
            IFilter<object, object> expectedFilter = new FakeFilter(null);
            FilterComponent<object, object, object> component = Component.FromFilter<object, object, object>(expectedFilter);

            // Act
            IFilter<object, object> actualFilter = component.Filter;

            // Assert
            actualFilter.Should().BeSameAs(expectedFilter);
        }
    }
}
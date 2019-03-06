using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Craeckersoft.AdvancedPipeline.Components;
using Craeckersoft.AdvancedPipeline.Components.Internal;
using Craeckersoft.AdvancedPipeline.Tests.Utilities;
using Craeckersoft.AdvancedPipeline.Tests.Utilities.Assertions;
using Craeckersoft.AdvancedPipeline.Tests.Utilities.Fakes;
using FluentAssertions;
using Xunit;

namespace Craeckersoft.AdvancedPipeline.Tests.Components.Internal
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
                ISet<TestType> tests = new HashSet<TestType>();
                IComponentInvoker<object, object> componentInvoker = new FilterComponent<object, object, object>(new FakeFilter(tests)).CreateInvoker(new FakeComponentInvoker(tests));

                // Act
                object actual = await componentInvoker.InvokeAsync(expected, new FakeInvocationContext());

                // Assert
                actual.Should().BeSameAs(expected);
                tests.Remove(TestType.CurrentInvoker).Should().BeTrue();
                tests.Remove(TestType.Request).Should().BeTrue();
                tests.Remove(TestType.InvocationContext).Should().BeTrue();
                tests.Remove(TestType.NextInvokerInvoked).Should().BeTrue();
                tests.Should().BeEmpty();
            }
        }

        [Fact]
        public void Constructor_FilterIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            Func<FilterComponent<object, object, object>> act = () => new FilterComponent<object, object, object>(null);

            // Act - Assert
            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("filter");
        }

        [Fact]
        public void Method_CreateInvoker_NextIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            FilterComponent<object, object, object> component = new FilterComponent<object, object, object>(new FakeFilter(null));
            Action act = () => component.CreateInvoker(null);

            // Act - Assert
            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("next");
        }

        [Fact]
        public void Method_CreateInvoker_ReturnsInvoker()
        {
            // Arrange
            FilterComponent<object, object, object> component = new FilterComponent<object, object, object>(new FakeFilter(null));

            // Act
            IComponentInvoker<object, object> invoker = component.CreateInvoker(new FakeComponentInvoker(null));

            // Assert
            invoker.Should().NotBeNull();
        }

        [Fact]
        public void Property_Filter_ReturnsFilterFromConstructor()
        {
            // Arrange
            IFilter<object, object> expectedFilter = new FakeFilter(null);
            FilterComponent<object, object, object> component = new FilterComponent<object, object, object>(expectedFilter);

            // Act
            IFilter<object, object> actualFilter = component.Filter;

            // Assert
            actualFilter.Should().BeSameAs(expectedFilter);
        }
    }
}
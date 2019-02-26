using System;
using Craeckersoft.AdvancedPipeline.Internal;
using Craeckersoft.AdvancedPipeline.Tests.Fakes;
using FluentAssertions;
using Xunit;

namespace Craeckersoft.AdvancedPipeline.Tests
{
    public class FilterComponentTests
    {
        [Fact]
        public void Constructor_FilterIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            Func<FilterComponent<string, string, string>> act = () => new FilterComponent<string, string, string>(null);

            // Act - Assert
            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("filter");
        }

        [Fact]
        public void Method_CreateInvoker_NextIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            FilterComponent<string, string, string> component = new FilterComponent<string, string, string>(new FakeFilter());
            Action act = () => component.CreateInvoker(null);

            // Act - Assert
            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("next");
        }

        [Fact]
        public void Method_CreateInvoker_ReturnsNotNull()
        {
            // Arrange
            FilterComponent<string, string, string> component = new FilterComponent<string, string, string>(new FakeFilter());

            // Act
            IComponentInvoker<string, string> invoker = component.CreateInvoker(new FakeComponentInvoker());

            // Assert
            invoker.Should().NotBeNull();
        }

        [Fact]
        public void Property_Filter_ReturnsFilterFromConstructor()
        {
            // Arrange
            FakeFilter expectedFilter = new FakeFilter();
            FilterComponent<string, string, string> component = new FilterComponent<string, string, string>(expectedFilter);

            // Act
            IFilter<string, string> actualFilter = component.Filter;

            // Assert
            actualFilter.Should().BeSameAs(expectedFilter);
        }
    }
}
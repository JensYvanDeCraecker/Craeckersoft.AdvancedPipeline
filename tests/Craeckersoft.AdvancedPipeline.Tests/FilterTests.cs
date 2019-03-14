using System;
using Craeckersoft.AdvancedPipeline.Tests.TestUtilities.Assertions;
using Craeckersoft.AdvancedPipeline.Tests.TestUtilities.Fakes;
using FluentAssertions;
using Xunit;

namespace Craeckersoft.AdvancedPipeline.Tests
{
    public class FilterTests
    {
        [Fact]
        public void Method_FromDelegate_FilterDelegateIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            Action act = () => Filter.FromDelegate((FilterDelegate<string, string>)null);

            // Act - Assert
            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("filterDelegate");
        }

        [Fact]
        public void Method_FromDelegate_ReturnsDelegateFilter()
        {
            // Arrange
            FilterDelegate<object, object> expectedFilterDelegate = FakeDelegates.Filter(null);

            // Act
            DelegateFilter<object, object> filter = Filter.FromDelegate(expectedFilterDelegate);

            // Assert
            filter.Should().NotBeNull();
        }
    }
}
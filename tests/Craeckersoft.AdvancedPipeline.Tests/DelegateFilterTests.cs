using System;
using Craeckersoft.AdvancedPipeline.Internal;
using Craeckersoft.AdvancedPipeline.Tests.Fakes;
using FluentAssertions;
using Xunit;

namespace Craeckersoft.AdvancedPipeline.Tests
{
    public class DelegateFilterTests
    {
        [Fact]
        public void Constructor_FilterDelegateIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            Func<DelegateFilter<string, string>> act = () => new DelegateFilter<string, string>(null);

            // Act - Assert
            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("filterDelegate");
        }

        [Fact]
        public void Method_Invoke_RequestIsNotNull_ReturnsReversedRequest()
        {
            // Arrange
            DelegateFilter<string, string> filter = new DelegateFilter<string, string>(FakeDelegates.Filter);

            // Act
            string actual = filter.Invoke("Unit Tests", null);

            // Assert
            // ReSharper disable once StringLiteralTypo
            actual.Should().Be("stseT tinU");
        }

        [Fact]
        public void Method_Invoke_RequestIsNull_ReturnsNull()
        {
            // Arrange
            DelegateFilter<string, string> filter = new DelegateFilter<string, string>(FakeDelegates.Filter);

            // Act
            string actual = filter.Invoke(null, null);

            // Assert
            actual.Should().BeNull();
        }

        [Fact]
        public void Property_Delegate_ReturnsFilterDelegateFromConstructor()
        {
            // Arrange
            FilterDelegate<string, string> expectedFilterDelegate = FakeDelegates.Filter;
            DelegateFilter<string, string> filter = new DelegateFilter<string, string>(expectedFilterDelegate);

            // Act
            FilterDelegate<string, string> actualFilterDelegate = filter.Delegate;

            // Assert
            actualFilterDelegate.Should().BeSameAs(expectedFilterDelegate);
        }
    }
}
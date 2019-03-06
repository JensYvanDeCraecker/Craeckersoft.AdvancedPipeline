using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Craeckersoft.AdvancedPipeline.Internal;
using Craeckersoft.AdvancedPipeline.Tests.Utilities;
using Craeckersoft.AdvancedPipeline.Tests.Utilities.Fakes;
using FluentAssertions;
using Xunit;

namespace Craeckersoft.AdvancedPipeline.Tests.Internal
{
    public class DelegateFilterTests
    {
        [Fact]
        public void Constructor_FilterDelegateIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            Func<DelegateFilter<object, object>> act = () => new DelegateFilter<object, object>(null);

            // Act - Assert
            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("filterDelegate");
        }

        [Fact]
        public async Task Method_InvokeAsync_UsesFilterDelegate()
        {
            // Arrange
            object expected = new object();
            ISet<TestType> tests = new HashSet<TestType>();
            IFilter<object, object> filter = new DelegateFilter<object, object>(FakeDelegates.Filter(tests));

            // Act
            object actual = await filter.InvokeAsync(expected, new FakeInvocationContext());

            // Assert
            actual.Should().BeSameAs(expected);
            tests.Remove(TestType.CurrentInvoker).Should().BeTrue();
            tests.Remove(TestType.Request).Should().BeTrue();
            tests.Remove(TestType.InvocationContext).Should().BeTrue();
            tests.Should().BeEmpty();
        }

        [Fact]
        public void Property_Delegate_ReturnsFilterDelegateFromConstructor()
        {
            // Arrange
            FilterDelegate<object, object> expectedFilterDelegate = FakeDelegates.Filter(null);
            DelegateFilter<object, object> filter = new DelegateFilter<object, object>(expectedFilterDelegate);

            // Act
            FilterDelegate<object, object> actualFilterDelegate = filter.Delegate;

            // Assert
            actualFilterDelegate.Should().BeSameAs(expectedFilterDelegate);
        }
    }
}
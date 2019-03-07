using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Craeckersoft.AdvancedPipeline.Internal;
using Craeckersoft.AdvancedPipeline.Tests.TestUtilities;
using Craeckersoft.AdvancedPipeline.Tests.TestUtilities.Fakes;
using Craeckersoft.AdvancedPipeline.Utilities;
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
            ISet<TestItem> tests = new HashSet<TestItem>();
            IFilter<object, object> filter = new DelegateFilter<object, object>(FakeDelegates.Filter(tests));

            // Act
            object actual = await filter.InvokeAsync(expected, new FakeInvocationContext());

            // Assert
            actual.Should().BeSameAs(expected);
            tests.Remove(TestItem.CurrentInvoker).Should().BeTrue();
            tests.Remove(TestItem.Request).Should().BeTrue();
            tests.Remove(TestItem.InvocationContext).Should().BeTrue();
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

        [Fact]
        public void Property_Item_IsSameAsDelegate()
        {
            // Arrange
            DelegateFilter<object, object> delegateFilter = new DelegateFilter<object, object>(FakeDelegates.Filter(null));

            // Act
            FilterDelegate<object, object> actual = ((IWrapper<FilterDelegate<object, object>>)delegateFilter).Item;

            // Assert
            actual.Should().BeSameAs(delegateFilter.Delegate);
        }
    }
}
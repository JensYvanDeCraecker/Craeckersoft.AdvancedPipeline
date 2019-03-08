using System;
using Craeckersoft.AdvancedPipeline.Components;
using Craeckersoft.AdvancedPipeline.Tests.TestUtilities.Fakes;
using Craeckersoft.AdvancedPipeline.Utilities;
using Craeckersoft.AdvancedPipeline.Utilities.Internal;
using FluentAssertions;
using Xunit;

namespace Craeckersoft.AdvancedPipeline.Tests.Utilities
{
    public class ComponentInfoTests
    {
        [Fact]
        public void Method_From_ArgumentIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            Action act = () => ComponentInfo.From((IComponent<object, object, object, object>)null);

            // Act - Assert
            act.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("component");
        }

        [Fact]
        public void Method_From_ReturnsComponentInfo()
        {
            // Arrange
            IComponent<object, object, object, object> expectedComponent = new FakeComponent();
            
            // Act
            IComponentInfo componentInfo = ComponentInfo.From(expectedComponent);
            
            // Assert
            componentInfo.Should().BeOfType<ComponentInfo<object, object, object, object>>().Which.Component.Should().BeSameAs(expectedComponent);
        }
    }
}
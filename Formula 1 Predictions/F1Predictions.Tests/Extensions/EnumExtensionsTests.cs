using System.Windows;
using F1Predictions.Core.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace F1Predictions.Tests.Extensions;

[TestFixture]
public class EnumExtensionsTests
{
    [Test]
    public void FlipCollapsed_WhenVisibilityIsCollapsed_ShouldReturnVisible()
    {
        // Arrange
        const Visibility visibility = Visibility.Collapsed;

        // Act
        var flippedVisibility = visibility.FlipCollapsed();

        // Assert
        flippedVisibility.Should().Be(Visibility.Visible);
    }
    
    [Test]
    public void FlipCollapsed_WhenVisibilityIsVisible_ShouldReturnCollapsed()
    {
        // Arrange
        const Visibility visibility = Visibility.Visible;

        // Act
        var flippedVisibility = visibility.FlipCollapsed();

        // Assert
        flippedVisibility.Should().Be(Visibility.Collapsed);
    }
    
    [TestCase(Visibility.Hidden)]
    public void FlipCollapsed_WhenVisibilityIsAnythingElse_ShouldReturnSelf(Visibility visibility)
    {
        // Act
        var flippedVisibility = visibility.FlipCollapsed();

        // Assert
        flippedVisibility.Should().Be(visibility);
    }
}
using System.Windows;
using F1Predictions.Core.Config;
using F1Predictions.Core.Interfaces;
using F1Predictions.Core.Services;
using F1Predictions.Core.ViewModels;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace F1Predictions.Tests.Services;

[TestFixture]
public class ToolbarServiceTests
{
    private Mock<IWindowService> mockWindowService;

    [SetUp]
    public void InitializeTest()
    {
        mockWindowService = new Mock<IWindowService>();
    }
    
    
    [TestCase("App Name")]
    [TestCase("F1 Predictions")]
    [TestCase("Best name ever!")]
    public void Initialize_ShouldSetViewModelAppName(string appName)
    {
        // Arrange
        var vm = CreateViewModel();
        var config = CreateConfig(title: appName);

        // Act
        var sut = CreateSut(config);
        sut.Initialize(vm);

        // Assert
        vm.AppName.Should().NotBeNull();
        vm.AppName.Should().NotBeEmpty();
        vm.AppName.Should().Be(appName);
    }
    
    [TestCase("#FFFFFF")]
    [TestCase("#000000")]
    [TestCase("#B39AF6")]
    public void Initialize_ShouldSetViewModelBackgroundColor(string color)
    {
        // Arrange
        var vm = CreateViewModel();
        var config = CreateConfig(color: color);

        // Act
        var sut = CreateSut(config);
        sut.Initialize(vm);

        // Assert
        vm.BackgroundColor.Should().NotBeNull();
        vm.BackgroundColor.Should().NotBeEmpty();
        vm.BackgroundColor.Should().Be(color);
    }
    
    [TestCase("/images/f1.png")]
    [TestCase("/some/cool/logo.bmp")]
    [TestCase("/over/yonder.gif")]
    public void Initialize_ShouldSetViewModelF1ImageRef(string logo)
    {
        // Arrange
        var vm = CreateViewModel();
        var config = CreateConfig(logo: logo);

        // Act
        var sut = CreateSut(config);
        sut.Initialize(vm);

        // Assert
        vm.F1ImageRef.Should().NotBeNull();
        vm.F1ImageRef.Should().NotBeEmpty();
        vm.F1ImageRef.Should().Be(logo);
    }

    [Test]
    public void CloseWindow_ShouldUseWindowDependencyToCloseWindow()
    {
        // Arrange
        var config = CreateConfig();
        var sut = CreateSut(config);

        // Act
        sut.CloseWindow();
        
        // Assert
        mockWindowService.Verify(w => w.Close(), Times.Once);
    }
    
    [Test]
    public void MaximizeWindow_ShouldUseWindowDependencyToMaximizeWindow()
    {
        // Arrange
        var vm = CreateViewModel();
        var config = CreateConfig();
        var sut = CreateSut(config);

        // Act
        sut.Initialize(vm);
        sut.MaximizeWindow();
        
        // Assert
        mockWindowService.Verify(w => w.Maximize(), Times.Once);
    }
    
    [TestCase(Visibility.Collapsed, Visibility.Visible)]
    [TestCase(Visibility.Visible, Visibility.Collapsed)]
    public void MaximizeWindow_RestoreVisibilityCollapsedStateShouldBeFlipped(Visibility visibility, Visibility expected)
    {
        // Arrange
        var vm = CreateViewModel(restoreVis: visibility);
        var config = CreateConfig();
        var sut = CreateSut(config);
        
        Assume.That(vm.RestoreVisibility == visibility, "Restore Visibility incorrectly initialized");

        // Act
        sut.Initialize(vm);
        sut.MaximizeWindow();
        
        // Assert
        vm.RestoreVisibility.Should().Be(expected);
    }
    
    [TestCase(Visibility.Collapsed, Visibility.Visible)]
    [TestCase(Visibility.Visible, Visibility.Collapsed)]
    public void MaximizeWindow_MaximizeVisibilityCollapsedStateShouldBeFlipped(Visibility visibility, Visibility expected)
    {
        // Arrange
        var vm = CreateViewModel(maximizeVis: visibility);
        var config = CreateConfig();
        var sut = CreateSut(config);
        
        Assume.That(vm.MaximizeVisibility == visibility, "Maximize Visibility incorrectly initialized");

        // Act
        sut.Initialize(vm);
        sut.MaximizeWindow();
        
        // Assert
        vm.MaximizeVisibility.Should().Be(expected);
    }
    
    [Test]
    public void MinimizeWindow_ShouldUseWindowDependencyToMinimizeWindow()
    {
        // Arrange
        var config = CreateConfig();
        var sut = CreateSut(config);

        // Act
        sut.MinimizeWindow();
        
        // Assert
        mockWindowService.Verify(w => w.Minimize(), Times.Once);
    }
    
    [Test]
    public void RestoreWindow_ShouldUseWindowDependencyToRestoreWindow()
    {
        // Arrange
        var vm = CreateViewModel();
        var config = CreateConfig();
        var sut = CreateSut(config);

        // Act
        sut.Initialize(vm);
        sut.RestoreWindow();
        
        // Assert
        mockWindowService.Verify(w => w.Restore(), Times.Once);
    }
    
    [TestCase(Visibility.Collapsed, Visibility.Visible)]
    [TestCase(Visibility.Visible, Visibility.Collapsed)]
    public void RestoreWindow_RestoreVisibilityCollapsedStateShouldBeFlipped(Visibility visibility, Visibility expected)
    {
        // Arrange
        var vm = CreateViewModel(restoreVis: visibility);
        var config = CreateConfig();
        var sut = CreateSut(config);
        
        Assume.That(vm.RestoreVisibility == visibility, "Restore Visibility incorrectly initialized");

        // Act
        sut.Initialize(vm);
        sut.RestoreWindow();
        
        // Assert
        vm.RestoreVisibility.Should().Be(expected);
    }
    
    [TestCase(Visibility.Collapsed, Visibility.Visible)]
    [TestCase(Visibility.Visible, Visibility.Collapsed)]
    public void RestoreWindow_MaximizeVisibilityCollapsedStateShouldBeFlipped(Visibility visibility, Visibility expected)
    {
        // Arrange
        var vm = CreateViewModel(maximizeVis: visibility);
        var config = CreateConfig();
        var sut = CreateSut(config);
        
        Assume.That(vm.MaximizeVisibility == visibility, "Maximize Visibility incorrectly initialized");

        // Act
        sut.Initialize(vm);
        sut.RestoreWindow();
        
        // Assert
        vm.MaximizeVisibility.Should().Be(expected);
    }
    
    [Test]
    public void DragWindow_ShouldUseWindowDependencyToDragWindow()
    {
        // Arrange
        var config = CreateConfig();
        var sut = CreateSut(config);

        // Act
        sut.DragWindow();
        
        // Assert
        mockWindowService.Verify(w => w.Drag(), Times.Once);
    }

    private ToolbarViewModel CreateViewModel(Visibility maximizeVis = Visibility.Visible, Visibility restoreVis = Visibility.Collapsed)
    {
        var mockToolbarService = new Mock<IToolbarService>();
        return new ToolbarViewModel(mockToolbarService.Object)
        {
            MaximizeVisibility = maximizeVis,
            RestoreVisibility = restoreVis
        };
    }
    
    private ToolbarConfig CreateConfig(string title = "", string color = "", string logo = "")
    {
        return new ToolbarConfig
        {
            Title = title,
            F1Logo = logo,
            BackgroundColor = color
        };
    }

    private ToolbarService CreateSut(ToolbarConfig config)
    {
        return new ToolbarService(mockWindowService.Object, config);
    }
}
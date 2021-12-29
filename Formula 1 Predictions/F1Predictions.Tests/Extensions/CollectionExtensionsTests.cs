using System.Collections.ObjectModel;
using F1Predictions.Core.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace F1Predictions.Tests.Extensions;

[TestFixture]
public class CollectionExtensionsTests
{
    [TestCase(1, 2, 3)]
    [TestCase(12, 56, 5, 72, 190, 0, -5432)]
    [TestCase("Foo", "Bar", "FooBar", "BarFoo")]
    public void ToObservableCollection_ShouldReturnObservableCollectionInstance(params object[] array)
    {
        // Act
        var observableCollection = array.ToObservableCollection();

        // Assert
        observableCollection.Should().NotBeNull();
        observableCollection.Should().BeOfType<ObservableCollection<object>>();
    }
    
    [TestCase("Foo", "Bar", "FooBar", "BarFoo")]
    public void ToObservableCollection_ShouldReturnObservableCollectionInstance_OfCorrectContainerType(params string[] array)
    {
        // Act
        var observableCollection = array.ToObservableCollection();

        // Assert
        observableCollection.Should().NotBeNull();
        observableCollection.Should().BeOfType<ObservableCollection<string>>();
    }
    
    [TestCase(1)]
    [TestCase(10, 20, 30)]
    [TestCase(12, 56, 5, 72, 190, 0, -5432)]
    public void ToObservableCollection_ShouldReturnObservableCollectionInstance_WithAllOriginalItems(params int[] array)
    {
        // Act
        var observableCollection = array.ToObservableCollection();

        // Assert
        observableCollection.Should().HaveCount(array.Length);
        foreach (var val in array)
        {
            observableCollection.Should().Contain(val);
        }
    }
}
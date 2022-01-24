using System.Windows.Controls;
using F1Predictions.Core.Events;
using Prism.Events;

namespace F1Predictions.Modules.Content.Views;

public partial class QuestionView : UserControl
{
    private readonly IEventAggregator eventAggregator;

    public QuestionView(IEventAggregator eventAggregator)
    {
        this.eventAggregator = eventAggregator;
        
        InitializeComponent();
        
        Loaded += InitialNavigation;
    }

    private void InitialNavigation(object sender, EventArgs e)
    {
        eventAggregator.GetEvent<QuestionChangedEvent>().Publish(new QuestionChangedData
        {
            IsProgression = true,
            QuestionIndex = 0,
            SectionIndex = 0
        });
    }
}
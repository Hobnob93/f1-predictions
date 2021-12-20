using System.Windows;
using F1Predictions.Core.Enums;

namespace F1Predictions.Views
{
    public partial class ShellWindow : Window
    {
        public ShellWindow()
        {
            InitializeComponent();

            DataContext = this;
        }

        
        public string ToolbarRegionName => Regions.ToolbarRegion.ToString();
    }
}
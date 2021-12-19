using System.Windows;
using System.Windows.Input;
using F1Predictions.Core.Enums;
using F1Predictions.Core.Interfaces;

namespace F1Predictions.Views
{
    public partial class ShellWindow : Window
    {
        private readonly IWindowService _window;
        
        public ShellWindow(IWindowService window)
        {
            _window = window;
            
            InitializeComponent();

            DataContext = this;
        }

        
        public string ToolbarRegionName => Regions.ToolbarRegion.ToString();


        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                _window.Drag();
        }
    }
}
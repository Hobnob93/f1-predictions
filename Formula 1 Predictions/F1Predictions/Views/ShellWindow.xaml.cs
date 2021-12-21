using System.Windows;
using F1Predictions.Core.Config;
using F1Predictions.Core.Enums;

namespace F1Predictions.Views
{
    public partial class ShellWindow : Window
    {
        private readonly AppConfig _config;
        
        public ShellWindow(AppConfig config)
        {
            _config = config;
            
            InitializeComponent();

            DataContext = this;
        }

        
        public string ToolbarRegionName => Regions.ToolbarRegion.ToString();
        public string AppName => _config.Name;
        public string BackgroundColor => _config.BackgroundColor;
        public string Font => _config.DefaultFont;
    }
}
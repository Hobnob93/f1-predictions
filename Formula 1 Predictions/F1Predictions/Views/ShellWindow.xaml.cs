using System.Windows;
using F1Predictions.Core.Config;
using F1Predictions.Core.Enums;
using F1Predictions.Core.Interfaces;
using ImTools;

namespace F1Predictions.Views
{
    public partial class ShellWindow : Window
    {
        private readonly AppConfig _config;
        
        public ShellWindow(AppConfig config, IGoogleSheets sheets)
        {
            _config = config;
            
            InitializeComponent();

            DataContext = this;

            var result = sheets.FetchTopQuestion(0, 0);
        }

        
        public string ToolbarRegionName => Regions.Toolbar.ToString();
        public string ProgressRegionName => Regions.Progress.ToString();
        public string ContentRegionName => Regions.Content.ToString();
        public string AppName => _config.Name;
        public string BackgroundColor => _config.BackgroundColor;
        public string Font => _config.DefaultFont;
    }
}
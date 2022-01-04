using System.Windows;
using F1Predictions.Core.Config;
using F1Predictions.Core.Enums;
using F1Predictions.Core.Interfaces;
using ImTools;

namespace F1Predictions.Views
{
    public partial class ShellWindow : Window
    {
        private readonly AppConfig config;
        
        public ShellWindow(AppConfig config, ISectionManager sections)
        {
            this.config = config;
            
            InitializeComponent();

            DataContext = this;
        }

        
        public string ToolbarRegionName => Regions.Toolbar.ToString();
        public string ProgressRegionName => Regions.Progress.ToString();
        public string ContentRegionName => Regions.Content.ToString();
        public string AppName => config.Name;
        public string BackgroundColor => config.BackgroundColor;
        public string Font => config.DefaultFont;
    }
}
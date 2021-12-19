using System.Windows;
using System.Windows.Input;
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


        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
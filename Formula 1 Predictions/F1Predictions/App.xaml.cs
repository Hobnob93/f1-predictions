using MvvmCross.Core;
using MvvmCross.Platforms.Wpf.Views;

namespace F1Predictions
{
    public partial class App : MvxApplication
    {
        protected override void RegisterSetup()
        {
            this.RegisterSetupType<Setup>();
        }
    }
}
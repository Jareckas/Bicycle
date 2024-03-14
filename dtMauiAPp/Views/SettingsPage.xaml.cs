using dtMauiAPp.Services;
using dtMauiAPp.ViewModels;

namespace dtMauiAPp.Views
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage(SettingsViewModel settingsViewModel)
        {
            InitializeComponent();
            BindingContext = settingsViewModel;
        }

    }
}

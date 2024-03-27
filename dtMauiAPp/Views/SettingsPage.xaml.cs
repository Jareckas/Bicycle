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

        private async void OnUsernameChangeClicked(object sender, EventArgs e)
        {
            // Open a popup or modal dialog to allow the user to input the new username
            string newUsername = await DisplayPromptAsync("Change Username", "Enter your new username:", "Change", "Cancel", "Username");

            // Check if the user entered a new username
            if (!string.IsNullOrWhiteSpace(newUsername))
            {
                // Now, you can execute the command to change the username
                // For example, you can call a method from your ViewModel
                ((SettingsViewModel)BindingContext).ChangeUsernameCommand.Execute(newUsername);
            }
        }
    }
}

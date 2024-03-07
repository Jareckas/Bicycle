using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using dtMauiAPp.Views;
using dtMauiAPp.Models;
using dtMauiAPp.Services;
using System.Text.Json;

namespace dtMauiAPp.ViewModels
{
    public partial class AuthenticationPageViewModel : ObservableObject
    {

        [RelayCommand]
        private async Task GoToRegister()
        {
            await Shell.Current.GoToAsync(nameof(RegisterPage));
        }

        [RelayCommand]
        private async Task GoToLogin()
        {
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using dtMauiAPp.Models;
using dtMauiAPp.Services;
using System.Threading.Tasks;
using System.Text.Json;

namespace dtMauiAPp.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        private LoginModel loginModel;

        private readonly ClientService clientService;

        private readonly MainPageViewModel mainPageViewModel; // Add a reference to MainPageViewModel

        public LoginViewModel(ClientService clientService, MainPageViewModel mainPageViewModel)
        {
            this.clientService = clientService;
            this.mainPageViewModel = mainPageViewModel;
            LoginModel = new();
        }

        [RelayCommand]
        public async Task Login()
        {
            // Perform login logic here
            bool loginSuccessful = await clientService.Login(LoginModel);

            if (loginSuccessful)
            {
                await mainPageViewModel.GetUserNameFromSecuredStorage();
                await Shell.Current.GoToAsync("MainPage");
            }   
        }


        [RelayCommand]
        public async Task GoBack()
        {
            await Shell.Current.GoToAsync("//AuthenticationPage");
        }
    }
}

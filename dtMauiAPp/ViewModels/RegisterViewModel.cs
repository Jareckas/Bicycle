using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using dtMauiAPp.Models;
using dtMauiAPp.Services;
using System.Threading.Tasks;
using System.Text.Json;

namespace dtMauiAPp.ViewModels
{
    public partial class RegisterViewModel : ObservableObject
    {
        [ObservableProperty]
        private RegisterModel registerModel;

        private readonly ClientService clientService;

        public RegisterViewModel(ClientService clientService)
        {
            this.clientService = clientService;
            RegisterModel = new();
        }

        [RelayCommand]
        public async Task Register()
        {
            await clientService.Register(RegisterModel);
        }

        [RelayCommand]
        public async Task GoBack()
        {
            await Shell.Current.GoToAsync("//AuthenticationPage");
        }
    }
}

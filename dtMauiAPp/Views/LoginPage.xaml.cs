using dtMauiAPp.Services;
using dtMauiAPp.ViewModels;

namespace dtMauiAPp.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage(LoginViewModel loginViewModel)
        {
            InitializeComponent();
            BindingContext = loginViewModel;
        }

    }
}

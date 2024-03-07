using dtMauiAPp.ViewModels;

namespace dtMauiAPp
{
    public partial class AuthenticationPage : ContentPage
    {
        public AuthenticationPage(AuthenticationPageViewModel authenticationPageViewModel)
        {
            InitializeComponent();
            BindingContext = authenticationPageViewModel;
        }
    }
}

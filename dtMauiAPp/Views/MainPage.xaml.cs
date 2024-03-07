using dtMauiAPp.Services;
using dtMauiAPp.ViewModels;

namespace dtMauiAPp.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel mainPageViewModel)
        {
            InitializeComponent();
            BindingContext = mainPageViewModel;
        }

    }
}

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
            Button_Clicked();
        }
        private void Button_Clicked()
        {
#if ANDROID
            var enable = new Android.Content.Intent(
                        Android.Bluetooth.BluetoothAdapter.ActionRequestEnable);
            enable.SetFlags(Android.Content.ActivityFlags.NewTask);

            var disable = new Android.Content.Intent(
                        Android.Bluetooth.BluetoothAdapter.ActionRequestDiscoverable);
            disable.SetFlags(Android.Content.ActivityFlags.NewTask);

            var bluetoothManager = (Android.Bluetooth.BluetoothManager)Android.App.Application.Context.GetSystemService(Android.Content.Context.BluetoothService);
            var bluetoothAdapter = bluetoothManager.Adapter;
            if (bluetoothAdapter.IsEnabled == true)
            {
                Android.App.Application.Context.StartActivity(disable);
                //  disable the bluetooth;
            }
            else
            {
                // enable the bluetooth
                Android.App.Application.Context.StartActivity(enable);

            }
#endif

        }
    }
}

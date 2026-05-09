using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SensorFusion.App.ViewModels
{
    public partial class StartViewModel : ObservableObject
    {
        private readonly MainViewModel _mainVM;

        [ObservableProperty] private string _ipAddress = "";
        [ObservableProperty] private string _port = "";

        public StartViewModel(MainViewModel mainVM)
        {
            _mainVM = mainVM;
        }

        [RelayCommand]
        private void Connect()
        {
            _mainVM.NavigateToDashboard();
        }
    }
}
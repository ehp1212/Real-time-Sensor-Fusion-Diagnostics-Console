using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SensorFusion.App.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private object _currentView;

        public StartViewModel StartVM { get; }
        public DashboardViewModel DashboardVM { get; }

        public MainViewModel()
        {
            StartVM = new StartViewModel(this);
            DashboardVM = new DashboardViewModel();

            // Start View
            CurrentView = StartVM;
        }

        [RelayCommand]
        public void NavigateToDashboard()
        {
            CurrentView = DashboardVM;
        }
    }
}
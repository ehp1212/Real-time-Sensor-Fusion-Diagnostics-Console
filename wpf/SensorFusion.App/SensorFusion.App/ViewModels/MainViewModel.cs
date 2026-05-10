using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Runtime.InteropServices;

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

        [DllImport("SensorFusion.Native.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void PrintHello();

        [DllImport("SensorFusion.Native.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetTestValue();

        [RelayCommand]
        private void Print()
        {
            try
            {
                System.Windows.MessageBox.Show($"{GetTestValue()} C# 커맨드 진입 성공!"); // <-- 이거 
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
    }
}
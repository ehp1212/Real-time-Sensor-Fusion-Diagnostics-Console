using SensorFusion.App.ViewModels;
using System.Windows;

namespace SensorFusion.App.Views
{
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }
    }
}
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace SensorFusion.App.ViewModels
{
    public partial class DashboardViewModel : ObservableObject
    {
        [ObservableProperty]
        private WriteableBitmap _displayImage;

        [ObservableProperty]
        private int _latency = 15;

        [ObservableProperty]
        private int _fps = 60;

        [ObservableProperty]
        private int _selectedTabIndex = 0;

        public ObservableCollection<string> Logs { get; } = new ObservableCollection<string>();


        private readonly DispatcherTimer _systemTimer;
        private readonly Random _random = new Random();
        private readonly byte[] _pixelBuffer;

        // Data stream configuration
        private const int ImgWidth = 640;
        private const int ImgHeight = 480;
        private const int BytesPerPixel = 4;

        public DashboardViewModel()
        {
            DisplayImage = new WriteableBitmap(ImgWidth, ImgHeight, 96, 96, PixelFormats.Bgr32, null);
            _pixelBuffer = new byte[ImgWidth * ImgHeight * BytesPerPixel];

            _systemTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100)
            };
            _systemTimer.Tick += OnSystemTimerTick;
            _systemTimer.Start();

            Logs.Add("[INFO] Dashboard Engine Initialized.");
            Logs.Add("[INFO] 2D Stream Active (Safe Mode).");
        }


        partial void OnSelectedTabIndexChanged(int value)
        {
            if (value == 0)
            {
                Logs.Add("[SYS] Switched to 2D Stream. 3D Engine throttled.");
                // TODO: Pause 3D rendering loop
            }
            else if (value == 1)
            {
                Logs.Add("[SYS] Switched to 3D LiDAR. 2D buffer writes paused.");
                // TODO: Start 3D rendering loop
            }
        }

        private void OnSystemTimerTick(object sender, EventArgs e)
        {
            if (SelectedTabIndex == 0)
            {
                UpdateDummyImage();
            }

            UpdateMetrics();
        }

        private void UpdateDummyImage()
        {
            if (DisplayImage == null) return;

            _random.NextBytes(_pixelBuffer);

            int stride = ImgWidth * BytesPerPixel;
            DisplayImage.WritePixels(
                new Int32Rect(0, 0, ImgWidth, ImgHeight),
                _pixelBuffer,
                stride,
                0
            );
        }

        private void UpdateMetrics()
        {
            Latency = _random.Next(12, 18);
            Fps = _random.Next(59, 61);

            if (_random.Next(0, 100) > 95)
            {
                Logs.Add($"[DATA] Stream synced at {DateTime.Now:HH:mm:ss}");
            }
        }
    }
}
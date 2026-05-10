using SensorFusion.App.Models;
using System.Runtime.InteropServices;
using Point3D = SensorFusion.App.Models.Point3D;

namespace SensorFusion.App.Services
{
    internal class SensorService : IDisposable
    {
        private byte[] _imageRawBuffer;
        private Point3D[] _pointCloudRawBuffer;

        private GCHandle _imageHandle;
        private GCHandle _pcHandle;

        public void Initialize(string ip, int port, int width, int height, int maxPoints)
        {
            // RGB
            _imageRawBuffer = new byte[width * height * 3];
            _pointCloudRawBuffer = new Point3D[maxPoints];

            // Pinning memory
            _imageHandle = GCHandle.Alloc(_imageRawBuffer, GCHandleType.Pinned);
            _pcHandle = GCHandle.Alloc(_pointCloudRawBuffer, GCHandleType.Pinned);

            var imgBuffer = new ImageBuffer
            {
                Data = _imageHandle.AddrOfPinnedObject(),
                Width = width,
                Height = height,
                Channels = 3
            };

            var pcBuffer = new PointCloudBuffer
            {
                Points = _pcHandle.AddrOfPinnedObject(),
                Length = maxPoints
            };
        }

        public void Dispose()
        {
            if (_imageHandle.IsAllocated) _imageHandle.Free();
            if (_pcHandle.IsAllocated) _pcHandle.Free();
        }
    }
}

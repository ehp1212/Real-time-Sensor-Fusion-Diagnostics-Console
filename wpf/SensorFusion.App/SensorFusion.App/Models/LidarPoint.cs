using System.Windows.Media.Media3D;

namespace SensorFusion.App.Models
{
    public struct LidarPoint
    {
        public float X;
        public float Y;
        public float Z;

        public float Intensity;

        public LidarPoint(float x, float y, float z, float intensity = 1.0f)
        {
            X = x; Y = y; Z = z;
            Intensity = intensity;
        }
    }
}
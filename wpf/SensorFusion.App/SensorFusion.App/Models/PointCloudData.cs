using System.Collections.Generic;

namespace SensorFusion.App.Models
{
    public class PointCloudData
    {
        public LidarPoint[] Points { get; set; } 
        public int Count => Points?.Length ?? 0;
        public DateTime Timestamp { get; set; }

        public PointCloudData(int size)
        {
            Points = new LidarPoint[size];
        }
    }
}
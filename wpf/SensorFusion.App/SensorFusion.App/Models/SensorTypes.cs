using System;
using System.Runtime.InteropServices;

namespace SensorFusion.App.Models
{
    /// <summary>
    /// LiDAR
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Point3D
    {
        public float X;
        public float Y;
        public float Z;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PointCloudBuffer
    {
        public IntPtr Points; 
        public int Length;   
    }

    /// <summary>
    /// Image
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ImageBuffer
    {
        public IntPtr Data; 
        public int Width;
        public int Height;
        public int Channels;
    }
}
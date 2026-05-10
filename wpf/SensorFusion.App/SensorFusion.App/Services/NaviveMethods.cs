using SensorFusion.App.Models;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SensorFusion.App.Services
{
    internal static class NaviveMethods
    {
        private const string DllName = "SensorFusion.Native.dll";

        [DllImport(dllName: DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ProcessImage(ref ImageBuffer imageBuffer);

        [DllImport(dllName: DllName, CallingConvention =CallingConvention.Cdecl)]
        public static extern int FilterPointCloud(ref PointCloudBuffer pointCloudBuffer);
    }
}

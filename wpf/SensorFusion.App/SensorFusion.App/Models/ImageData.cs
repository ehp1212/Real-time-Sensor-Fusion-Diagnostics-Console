namespace SensorFusion.App.Models
{
    public class ImageData
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public byte[] RawBytes { get; set; } 
        public DateTime Timestamp { get; set; }
        public string Format { get; set; }
    }
}
namespace SystemMonitor.Models
{
    public class SensorData
    {
        public string ID { get; set; } = null!;
        public string Label { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Value { get; set; } = null!;
    }

    public class GroupStatistic
    {
        public string Titte { get; set; } = null!;
        public Dictionary<string, string> Devices { get; set; } = null!;
    }
}


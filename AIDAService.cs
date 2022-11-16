using System.Management;
using SystemMonitor.Models;

namespace AIDAS
{
    public class AIDAService
    {
        private readonly ManagementObjectSearcher searcher;

        public AIDAService()
        {
            var scope = new ManagementScope($"\\\\{Environment.MachineName}\\root\\WMI");
            var query = new ObjectQuery("SELECT * FROM AIDA64_SensorValues");
            scope.Connect();
            searcher = new ManagementObjectSearcher(scope, query);
        }

        public async Task<GroupStatistic> GetTempAsync() => await GetData("T");

        public async Task<GroupStatistic> GetSpeedsAsync() => await GetData("S");

        private async Task<GroupStatistic> GetData(string key)
        {
            var queryCollection = searcher.Get();
            var statistic = new GroupStatistic() { Titte = key, Devices = new Dictionary<string, string>() };

            foreach (ManagementObject manegementObject in queryCollection)
            {
                var instance = Descript(manegementObject);
                if (instance.Type.Equals(key)) statistic.Devices.Add(instance.Label, instance.Value);
            }

            return statistic;
        }

        private static SensorData Descript(ManagementObject m)
        {
            static string GetValue(object mo) => mo.ToString() ?? "";

            return new SensorData()
            {
                ID = GetValue(m["ID"]),
                Label = GetValue(m["Label"]),
                Type = GetValue(m["Type"]),
                Value = GetValue(m["Value"])
            };
        }
    }
}

using System.Management;
using SystemMonitor.Models;

namespace AIDAS
{
    public class AIDAService
    {
        private readonly ManagementObjectSearcher searcher;
        private Dictionary<string, Dictionary<string, string>> Data = new ();
        private Dictionary<string, string> Speeds= new ();
        
        public AIDAService()
        {
            var scope = new ManagementScope($"\\\\{Environment.MachineName}\\root\\WMI");
            var query = new ObjectQuery("SELECT * FROM AIDA64_SensorValues");
            scope.Connect();
            searcher = new ManagementObjectSearcher(scope, query);
        }

        private Dictionary<string, string> GetTemp()
        {
            return Temperatures;
        }

        private Dictionary<string, string> GetSpeeds()
        {
            return Temperatures;
        }


        private Task GetData(string type)
        {
            var queryCollection = searcher.Get();

            //var pDict = new Dictionary<string, string>();
            //var voltages = new Dictionary<string, string>(); // voltages
            //var other = new Dictionary<string, string>();

            foreach (ManagementObject manegementObject in queryCollection)
            {
                var instance = Descript(manegementObject);
                var type = instance.Type;

                if (type.Equals("T"))
                {
                    instance.Add(instance.Label, instance.Value);
                }
                else if (type.Equals("S"))
                {
                    Speeds.Add(instance.Label, instance.Value);
                }


                Data.Add(instance.Type, new Dictionary<string, string>(instance.Label, instance.Value));

                //else if (type.Equals("pDict"))
                //{
                //    pDict.Add(data.Label, data.Value);
                //}
                //else if (type.Equals("voltages"))
                //{
                //    voltages.Add(data.Label, data.Value);
                //}
                //else if (type.Equals("other"))
                //{
                //    other.Add(data.Label, data.Value);
                //}
            }
            return Task.CompletedTask;
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

using Models;
using System;
using System.IO;

namespace TestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var incoming = Newtonsoft.Json.JsonConvert.DeserializeObject<DataModel>(File.ReadAllText(@"incoming.json"));

            ProductionLine pl = new ProductionLine(incoming);

            while (pl.HasResourcesForNextTick())
            {
                pl.NextTick();
            }

            pl.GetLogs().ForEach(m => Console.WriteLine(m));
        }
    }
}

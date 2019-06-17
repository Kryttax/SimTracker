using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace SimTracker
{
    //CSV Map Generator
    public class FooMap : CsvHelper.Configuration.ClassMap<BugEvent>
    {
        public FooMap()
        {
           // AutoMap();
            Map(m => m._userId).Index(0);
            Map(m => m._timeStamp).Index(1);
            Map(m => m._dateStamp).Index(2);
            Map(m => m._timeStamp).Index(3);
        }
    }

    //Serializes an event to CSV with CSVHelper's lib. Returns a serialized string.
    class CSVSerializer : ISerializer
    {
        string ISerializer.Serialize(IEvent evnt)
        {
           
            var records = new List<dynamic> { evnt };
            string result;

            using (var mem = new MemoryStream())
            using (var writer = new StreamWriter(mem))
            using (var csv = new CsvWriter(writer))
            {
                using (var csvWriter = new CsvWriter(writer))
                {
                    csvWriter.Configuration.Delimiter = ",";
                    csvWriter.Configuration.HasHeaderRecord = false;
                    //csvWriter.Configuration.AutoMap<IEvent>();
                    //csvWriter.Configuration.RegisterClassMap<FooMap>();
      
                    csvWriter.WriteRecords(records);

                    writer.Flush();
                    result = Encoding.UTF8.GetString(mem.ToArray());
                }
            }
            return result.ToString();
        }
    }
}

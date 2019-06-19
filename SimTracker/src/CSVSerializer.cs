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
    public class BaseCSVMap<T> : CsvHelper.Configuration.ClassMap<T> where T : class
    {
        public void CreateMap(Dictionary<int, dynamic> mappings)
        {
            foreach (var mapping in mappings)
            {

                
                List<dynamic> propname = new List<dynamic>() { mapping.Key };
                var csvIndex = mapping.Value;

                foreach(var ob in propname[0])
                {
                    var member = typeof(T).GetProperty(ob);
                    Map(typeof(T), member).Index(csvIndex);

                }
            }
        }
    }

    ////CSV Map Generator
    //public class FooMap<T> : CsvHelper.Configuration.ClassMap<T> where T : class
    //{
    //    public FooMap()
    //    {
    //       // AutoMap();
    //        Map(m => m._userId).Index(0);
    //        Map(m => m._timeStamp).Index(1);
    //        Map(m => m._dateStamp).Index(2);
    //        Map(m => m._timeStamp).Index(3);
    //    }
    //}

    //Serializes an event to CSV with CSVHelper's lib. Returns a serialized string.
    class CSVSerializer : ISerializer
    {
        
        string ISerializer.Serialize(IEvent evnt)
        {
           
            var records = new List<IEvent> { evnt };
            string result;

            //records.Reverse();
            //records.Sort();

            var dic = records.ToDictionary(x => x, y => y);

            using (var mem = new MemoryStream())
            using (var writer = new StreamWriter(mem))
            using (var csvWriter = new CsvWriter(writer))
            {
                //using (var csvWriter = new CsvWriter(writer))
                {
                    csvWriter.Configuration.Delimiter = ",";
                    csvWriter.Configuration.HasHeaderRecord = false;
                    csvWriter.Configuration.ReferenceHeaderPrefix = (memberType, memberName) => $"_{memberName}";
                    csvWriter.Configuration.DynamicPropertySort = Comparer<string>.Create((x, y) => x.CompareTo(y));
                    //foreach (TrackerEvent record in records)
                    //{
                    //    csvWriter.WriteField(record._userId);
                    //    csvWriter.WriteField(record._timeStamp);


                    //}

                    //var myType = records.GetType();
                    //IList<System.Reflection.PropertyInfo> props = new List<System.Reflection.PropertyInfo>(myType.GetProperties());

                    //foreach (System.Reflection.PropertyInfo prop in props)
                    //{
                    //    //dynamic propValue = prop.GetValue(props);

                    //    csvWriter.WriteField(prop.GetValue(myType, null));
                    //    // Do something with propValue
                    //}



                    csvWriter.NextRecord();

                    ////foreach (var end in records)
                    //{
                    //    end.GetType().GetProperties();
                    //    csvWriter.WriteField(end);
                    //}
                    //csvWriter.WriteField("_userId");
                    //csvWriter.WriteField("_time");
                    //csvWriter.Configuration.map(IEvent);
                    //csvWriter.Configuration.AutoMap<BugEvent>();
                    //csvWriter.Configuration.AutoMap<TrackerEvent>();

                    var map = new BaseCSVMap<dynamic>();
                    map.CreateMap(dic);
                    csvWriter.Configuration.RegisterClassMap(map);

                    csvWriter.WriteRecords(records);

                    writer.Flush();
                    result = Encoding.UTF8.GetString(mem.ToArray());
                }
            }
            return result.ToString();
        }


        string Sort() 
        {
            return null;
        }
    }
}

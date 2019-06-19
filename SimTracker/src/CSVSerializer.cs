using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace SimTracker
{

    //CSV Map Generator
    //Gets keys from event dictionary and orders general event class elements.
    public class BaseCSVMap<T> : CsvHelper.Configuration.ClassMap<T> where T : class
    {
        static int i = 6;

        public void CreateMap(Dictionary<dynamic, dynamic> mappings)
        {
            foreach (var mapping in mappings)
            {
                var propname = mapping.Key;

                Type myType = propname.GetType();
                IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

               
                foreach (PropertyInfo prop in props)
                {
                    object propValue = prop.GetValue(propname, null);

                    switch (prop.Name)
                    {
                        case "_userId":
                            Map(myType, prop).Index(0); 
                            break;
                        case "_dateStamp":
                            Map(myType, prop).Index(1);
                            break;
                        case "_timeStamp":
                            Map(myType, prop).Index(2);
                            break;
                        case "_type":
                            Map(myType, prop).Index(3);
                            break;
                        case "_level":
                            Map(myType, prop).Index(4);
                            break;
                        case "_playerPos":
                            Map(myType, prop).Index(5);          
                            break;
                        default:
                            Map(myType, prop).Index(i);
                            i++;
                            break;
                    }


                }
            }
        }
    }


    //Serializes an event to CSV with CSVHelper's lib. Returns a serialized string.
    class CSVSerializer : ISerializer
    {

        string ISerializer.Serialize(IEvent evnt)
        {

            string result;
            var records = new List<dynamic> { evnt };
            var dic = records.ToDictionary(x => x, y => y);

            using (var mem = new MemoryStream())
            using (var writer = new StreamWriter(mem))
            using (var csvWriter = new CsvWriter(writer))
            {

                csvWriter.Configuration.Delimiter = ",";
                csvWriter.Configuration.HasHeaderRecord = false;

                //Creates new dynamic map with common CSV file order.
                var map = new BaseCSVMap<dynamic>();
                map.CreateMap(dic);
                csvWriter.Configuration.RegisterClassMap(map);

                csvWriter.WriteRecords(records);

                writer.Flush();
                result = Encoding.UTF8.GetString(mem.ToArray());

            }
            return result.ToString();
        }


    }
}

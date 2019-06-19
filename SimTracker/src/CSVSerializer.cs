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
    public class BaseCSVMap<T> : CsvHelper.Configuration.ClassMap<T> where T : class
    {
        public void CreateMap(Dictionary<dynamic, dynamic> mappings)
        {
            foreach (var mapping in mappings)
            {
                var propname = mapping.Key;
                var csvIndex = mapping.Value;

                Type myType = propname.GetType();
                IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

                int i = 6;
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

                    // Do something with propValue
                    // var member = propname.GetType().GetProperty(prop.ToString());//typeof(T).GetProperty(propValue.ToString());
                   
                }

                //foreach (var ob in propname.GetType().GetProperties())
                //{
                //    var member = typeof(T).GetProperty(ob);
                //    Map(typeof(T), member).Index(csvIndex);


                //}

                //PropertyInfo[] props = mapping.GetType().GetProperties();

                //foreach (PropertyInfo prop in props)
                //{
                //    object[] attrs = prop.GetCustomAttributes(true);
                //    foreach (object attr in attrs)
                //    {
                //        //AuthorAttribute authAttr = attr as AuthorAttribute;
                //        //if (authAttr != null)
                //        //{
                //        //    string propName = prop.Name;
                //        //    string auth = authAttr.Name;

                //        //    //_dict.Add(propName, auth);
                //        //}

                //        var member = typeof(T).GetProperty(attr.ToString());
                //        Map(typeof(T), member).Index(csvIndex);
                //    }
                //}

               

              
            }
        }
    }


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
           
            var records = new List<dynamic> { evnt };
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

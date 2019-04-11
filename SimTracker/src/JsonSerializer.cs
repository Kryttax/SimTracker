using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SimTracker
{
    class JSONSerializer : ISerializer
    {
        string ISerializer.Serialize(TrackerEvent evnt)
        {
            JsonSerializer serializer = new JsonSerializer();

            return JsonConvert.SerializeObject(evnt);
        }
    }
}

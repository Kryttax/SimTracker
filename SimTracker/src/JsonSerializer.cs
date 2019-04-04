using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimTracker
{
    class JsonSerializer : ISerializer
    {
        string ISerializer.Serialize(TrackerEvent evnt)
        {
            return "";
        }
    }
}

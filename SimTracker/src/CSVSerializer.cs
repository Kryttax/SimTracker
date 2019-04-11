using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimTracker
{
    class CSVSerializer : ISerializer
    {
        StringBuilder csvFile;

        string ISerializer.Serialize(TrackerEvent evnt)
        {
            csvFile = new StringBuilder();

            string p0, p1, p2,p3;

            //Depende del tipo de evento (por asignar)
            p0 = evnt.timeStamp.ToString();
            p1 = evnt.user.ToString();
            p2 = evnt.level.ToString();
            p3 = evnt.type.ToString();
            
            string newLine = string.Format("{0},{1},{2},{3}", p0, p1, p2, p3);
            csvFile.AppendLine(newLine);


            return csvFile.ToString();
        }
    }
}

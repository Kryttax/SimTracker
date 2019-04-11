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

            string p0, p1, p2;

            //Depende del tipo de evento (por asignar)
            p0 = evnt.timeStamp.ToString();
            p1 = evnt.level.ToString();
            p2 = evnt.type.ToString();

            string newLine = string.Format("{0},{1},{2}", p0, p1, p2);
            csvFile.AppendLine(newLine);


            //return csvFile.ToString();
            return newLine;
        }
    }
}

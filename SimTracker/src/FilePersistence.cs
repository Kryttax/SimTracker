using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SimTracker
{
    class FilePersistence : IPersistence
    {
        string filePath = "..\\..\\logs\\";
        int index = 0;
        void IPersistence.Send(string str)
        {
            //after your loop
            File.WriteAllText(filePath + "newFile" + index + ".txt", str);
            ++index;
        }
    }
}

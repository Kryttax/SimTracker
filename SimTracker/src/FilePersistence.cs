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
        string path = null;
        StreamWriter sw = null;
        void IPersistence.Send(string str)
        {
         
            if(path == null)
            {
                path = filePath + SimTracker.Instance().user + ".txt";
            }
            //after your loop
            if (!File.Exists(path))
            {
                sw = File.AppendText(path);
                sw.WriteLine(str);
                sw.Close();
            }
            else
            {
                sw = File.AppendText(path);
                sw.WriteLine(str);
                sw.Close();
            }
        }
    }
}

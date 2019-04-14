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
        static string enviromentPath = AppDomain.CurrentDomain.BaseDirectory;
        
        string filePath = @"..\\..\\logs\\";    //UNITY: @"Assets\SimTracker\logs\"
        string path = null;
        StreamWriter sw = null;

        void IPersistence.Send(string str)
        {

            if (path == null)
            {
                string fileName = SimTracker.Instance().user + ".txt";
                path = Path.Combine(enviromentPath, filePath, fileName);
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

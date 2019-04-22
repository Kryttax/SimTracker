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

        string enviromentPath = Environment.CurrentDirectory;
        string unityPath = @"Assets\SimTracker\logs\";
        string solutionPath = "..\\..\\logs\\";
        string path = null;
        StreamWriter sw = null;

        void IPersistence.Send(string str)
        {

            if (path == null)
            {
                string fileName = SimTracker.Instance().user + ".txt";
                path = Path.Combine(enviromentPath, solutionPath, fileName);
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

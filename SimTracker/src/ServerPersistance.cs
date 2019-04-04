using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace SimTracker
{
    class ServerPersistance : IPersistence
    {
        void IPersistence.Send(string str)
        {
            //DESCARGAR SERVER GITHUB DE GUILLE (PHYTON)
            string url = "google.es";
            Uri myUri = new Uri(url);
            var ip = Dns.GetHostAddresses(myUri.Host)[0];
        }
    }
}

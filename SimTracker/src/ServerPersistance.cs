using System;
using System.Net;
using System.IO;
namespace SimTracker
{
    class ServerPersistance : IPersistence
    {
        [Serializable]
        public class Data
        {
            public string data { get; set; }
            
        }

        void IPersistence.Send<T>(T str)
        {
            try
            {
                string webAddr = "http://localhost:8080/tracker";

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "POST";
                Data newData = new Data();
                newData.data = str.ToString();

               
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {                 
                    streamWriter.WriteLine(str);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var responseText = streamReader.ReadToEnd();
                    Console.WriteLine(responseText);
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

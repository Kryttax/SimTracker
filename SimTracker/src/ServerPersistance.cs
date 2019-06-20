using System;
using System.Net;
using System.IO;
namespace SimTracker
{
    class ServerPersistance : IPersistence
    {

        private static string webAddr = "http://localhost:8080/tracker";
        private HttpWebRequest httpWebRequest;
        private string requestType = "application/json; charset=utf-8";     //JSON/CSV
        private string requestMethod = "POST";                              //Two methods: GET and POST
        [Serializable]
        public class Data
        {
            public string data { get; set; }
            
        }

        //Sends a serialized event to a given web address
        void IPersistence.Send(string str)
        {
            try
            {
                httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                httpWebRequest.ContentType = requestType;
                httpWebRequest.Method = "POST";
                //Data newData = new Data();
                //newData.data = str.ToString();

               
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {                 
                    streamWriter.WriteLine(str);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    Console.WriteLine(streamReader.ReadToEnd());
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

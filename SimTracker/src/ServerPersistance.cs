using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace SimTracker
{
    class ServerPersistance : IPersistence
    {
        void IPersistence.Send(string str)
        {
            try
            {
                string webAddr = "http://localhost:8080/tracker";

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "POST";
                //string json = "{ \"data\": " + str + " }";
                byte[] byteArray = Encoding.UTF8.GetBytes(str);
                httpWebRequest.ContentLength = byteArray.Length;

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {                    
                    streamWriter.Write(str);
                    streamWriter.Flush();
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


        void JsonParserExample()
        {
            try
            {
                string webAddr = "http://localhost:8080/tracker";

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{ \"method\" : \"example.test\", \"params\" : [ \"exam\" ], \"id\" : 123 }";

                    streamWriter.Write(json);
                    streamWriter.Flush();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var responseText = streamReader.ReadToEnd();
                    Console.WriteLine(responseText);

                    //Now you have your response.
                    //or false depending on information in the response     
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void StringSendExample()
        {
            // Create a request by using a URL that can receive a post.   
            WebRequest request = WebRequest.Create("http://localhost:8080/tracker");
            // Set the Method property of the request to POST.  
            request.Method = "POST";

            // Create POST data and convert it to a byte array.  
            string postData = "example to send";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            // Set the ContentType property of the request.  
            //request.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the request.  
            request.ContentLength = byteArray.Length;

            // Get the request stream.  
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.  
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the stream.  
            dataStream.Close();

            //// Get the response.  
            //WebResponse response = request.GetResponse();
            //// Display the status.  
            //Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            //// Get the stream containing content returned by the server.  
            //dataStream = response.GetResponseStream();
            //// Open the stream by using a StreamReader for easy access.  
            //StreamReader reader = new StreamReader(dataStream);

            //// Read the content.  
            //string responseFromServer = reader.ReadToEnd();
            //// Display the content.  
            //Console.WriteLine(responseFromServer);

            //// Clean up the response.  
            //reader.Close();
            //response.Close();
        }
    }
}

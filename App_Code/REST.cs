using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using FieldLevel.Models;

namespace FieldLevel
{
    public class REST
    {
        static WebRequest CurrentRequest = null;
        static WebResponse CurrentResponse = null;

        public static string GenerateRequest(RESTRequest item)
        {
            CurrentRequest = null;
            CurrentResponse = null;
            string responseFromServer = "";
            WebRequest request = HttpWebRequest.Create(item.url);
            int timeOut = 5000;
            request.Method = item.method;

            request.Headers = new WebHeaderCollection();
            if (item.headers != null)
                request.Headers.Add(item.headers);

            request.ContentType = item.content;
            if ((item.method.ToLower() == "post" | item.method.ToLower() == "put"))
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(item.post);
                request.ContentType = item.content;
                request.ContentLength = byteArray.Length;

                if ((byteArray.Length > 0))
                {
                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(byteArray, 0, byteArray.Length);
                        stream.Close();
                        stream.Dispose();
                    }
                }
            }

            if ((item.toOverRide != 0))
                timeOut = item.toOverRide;

            try
            {
                CurrentRequest = (HttpWebRequest)request;
                request = null/* TODO Change to default(_) if this is not a reference type */;

                if (CurrentRequest != null)
                {
                    CurrentResponse = CurrentRequest.GetResponse();
                }

                using (var resp = CurrentResponse.GetResponseStream())
                {
                    responseFromServer = new StreamReader(resp).ReadToEnd();
                    resp.Dispose();
                }

                CurrentRequest = null;
                CurrentResponse = null;
            }
            catch (Exception ex)
            {
                //do something here
            }

            return responseFromServer;
        }
    }
}

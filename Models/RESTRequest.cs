using System;
using System.Net;

namespace FieldLevel.Models
{
    public class RESTRequest
    {
        public string url { get; set; }
        public string method { get; set; }
        public string post { get; set; }
        public string content { get; set; }
        public string mess { get; set; }
        public int toOverRide { get; set; }
        public WebHeaderCollection headers { get; set; }
        public int headOverride { get; set; }

        public RESTRequest()
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GetRequest
{
    public class GetReq
    {
        HttpWebRequest _request;
        string _addess;

        public Dictionary<string, string> Headers { get; set; }

        public string Response { get; set; }
        public string Accept { get; set; }
        public string Host { get; set; }
        public string Referer { get; set; }
        public string Useragent { get; set; }
        public WebProxy Proxy { get; set; }

        public GetReq(string address)
        {
            _addess = address;
            Headers = new Dictionary<string, string>(); 
        }

        public void Run(CookieContainer cookieContainer)
        {
            _request = (HttpWebRequest)HttpWebRequest.Create(_addess);
            _request.Method = "GET";
            _request.CookieContainer = cookieContainer;
            _request.Proxy = Proxy;
            _request.Accept = Accept;
            _request.Host = Host;
            _request.UserAgent = Useragent;
            _request.Referer = Referer;
            
            foreach(var pair in Headers)
            {
                _request.Headers.Add(pair.Key, pair.Value);
            }

            try 
            { 
                HttpWebResponse response = (HttpWebResponse)_request.GetResponse();
                var stream = response.GetResponseStream();
                if (stream != null) Response = new StreamReader(stream).ReadToEnd();

            }
            catch (Exception ex)
            {

            }
        }
    }
}

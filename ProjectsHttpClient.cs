/*  As of the time of writing this library, Xero Projects has no supported SDK
 *  This Library is for creating authorised REST calls to Xero
 *  
 *  Code is taken largely from https://github.com/XeroAPI/Xero-Net
 * 
 */
using Devpoint.XeroProjectsApi.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.IO;

namespace Devpoint.XeroProjectsApi
{
    public class ProjectsHttpClient
    {
        static readonly int DEFAULT_TIMEOUT = (int)TimeSpan.FromMinutes(5.5).TotalMilliseconds;
        const string BASEURL = "https://api.xero.com";
        const string PROJECTURL = "/projects.xro/1.0/projects";
        private string ConsumerKey { get; set; }        
        private X509Certificate2 Certificate = new X509Certificate2();
        private readonly Dictionary<string, string> _headers;

        public ProjectsHttpClient(string consumerKey,             
            string certificatePath,
            string certificatePassword)
        {
            this.ConsumerKey = consumerKey;
            Certificate.Import(certificatePath, certificatePassword, X509KeyStorageFlags.MachineKeySet);
            _headers = new Dictionary<string, string>();
        }

        public void HttpPost(string json)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var httpWebRequest = CreateRequest(PROJECTURL, "POST");
            httpWebRequest.ContentType = "application/json";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {               
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }

        private HttpWebRequest CreateRequest(string endPoint, string method, string accept = "application/json", string query = null)
        {
            HeaderBuilder headerBuilder = new HeaderBuilder();
            var uri = new UriBuilder(BASEURL)
            {
                Path = endPoint,
            };

            if (!string.IsNullOrWhiteSpace(query))
            {
                uri.Query = query;
            }

            var request = (HttpWebRequest)WebRequest.Create(uri.Uri);

            request.Timeout = DEFAULT_TIMEOUT;

            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            request.Accept = accept;
            request.Method = method;

            //if (ModifiedSince.HasValue)
            //{
            //    request.IfModifiedSince = ModifiedSince.Value;
            //}

            var oauthSignature = headerBuilder.CreateSignature(ConsumerKey, Certificate, request.RequestUri, method);

            AddHeader("Authorization", oauthSignature);

            AddHeaders(request);

            request.UserAgent = "Xero Api wrapper - " + ConsumerKey;

            return request;
        }

        private void AddHeaders(WebRequest request)
        {
            foreach (var pair in _headers)
            {
                request.Headers.Add(pair.Key, pair.Value);
            }
        }

        private void AddHeader(string name, string value)
        {
            _headers[name] = value;
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Devpoint.XeroProjectsApi.Concretes
{
    internal class HeaderBuilder
    {
        private string Key { get; set; }
        private X509Certificate2 certificate { get; set; }

        public string CreateSignature(string consumerKey,
            X509Certificate2 certificate,
            Uri requestUri,
            string method)
        {
            var oAuthParameters = new OAuthParameters(
                consumerKey,
                "RSA-SHA1",
                string.Empty,
                "1.0");

            var signatureBaseString =
               new SignatureBaseString(
                   new Request
                   {
                       Url = requestUri,
                       Verb = method
                   },
                   oAuthParameters);

            var signature = new RsaSha1(certificate).Sign(signatureBaseString);

            oAuthParameters.SetSignature(signature);

            return new AuthorizationHeader(oAuthParameters, string.Empty, false).Value;
        }
    }
}

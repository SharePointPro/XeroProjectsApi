using Devpoint.XeroProjectsApi.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devpoint.XeroProjectsApi.Concretes
{
    public class OAuthParameters
    {
        private readonly string _consumerKey;
        private readonly string _signatureMethod;
        private string _signature;
        private readonly string _version;
        private readonly string _nonce, _timestamp, _verifier, _session;
        private readonly bool _renewToken;
        private readonly string _callback;

        public string GetTimeStamp()
        {
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            int secondsSinceEpoch = (int)t.TotalSeconds;
            return secondsSinceEpoch.ToString();
        }

        public OAuthParameters(
            string key,
            string signatureMethod,
            string signature,
            string version,
            string verifier = null,
            string session = null,
            bool renewToken = false,
            string callback = null
        )
        {
            _consumerKey = key;
            _signatureMethod = signatureMethod;
            _signature = signature;
            _session = session;
            _verifier = verifier;
            _version = version ?? "1.0";
            _nonce = Guid.NewGuid().ToString(); 
            _timestamp = GetTimeStamp();
            _renewToken = renewToken;
            _callback = callback;
        }

        internal Parameters List()
        {
            return new Parameters(
                    ConsumerKey,
                    Version,
                    SignatureMethod,
                    Timestamp,
                    Nonce
                ).Tap(it =>
                {
                    if (!string.IsNullOrWhiteSpace(_consumerKey))
                    {
                        it.Add(Token);
                    }

                    if (!string.IsNullOrWhiteSpace(_verifier))
                    {
                        it.Add(Verifier);
                    }

                    if (_renewToken && (!string.IsNullOrWhiteSpace(_session)))
                    {
                        it.Add(Session);
                    }

                    if (!string.IsNullOrWhiteSpace(_callback))
                    {
                        it.Add(Callback);
                    }
                });
        }

        internal Parameter Token
        {
            get { return new Parameter(Name.Token, _consumerKey); }
        }

        internal Parameter ConsumerKey
        {
            get { return new Parameter(Name.ConsumerKey, _consumerKey); }
        }

        internal Parameter SignatureMethod
        {
            get { return new Parameter(Name.SignatureMethod, _signatureMethod); }
        }

        public void SetSignature(string what)
        {
            _signature = what;
        }

        internal Parameter Signature
        {
            get { return new Parameter(Name.Signature, _signature); }
        }

        internal Parameter Timestamp
        {
            get { return new Parameter(Name.Timestamp, _timestamp); }
        }

        internal Parameter Nonce
        {
            get { return new Parameter(Name.Nonce, _nonce); }
        }

        internal Parameter Version
        {
            get { return new Parameter(Name.Version, _version); }
        }

        internal Parameter Verifier
        {
            get { return new Parameter(Name.Verifier, _verifier); }
        }

        internal Parameter Session
        {
            get { return new Parameter(Name.Session, _session); }
        }

        internal Parameter Callback
        {
            get { return new Parameter(Name.Callback, _callback); }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devpoint.XeroProjectsApi.Concretes
{
    public class SignatureBaseString
    {
        private readonly Request _request;
        private readonly OAuthParameters _oAuthParameters;

        public SignatureBaseString(Request request, OAuthParameters oAuthParameters)
        {
            _request = request;
            _oAuthParameters = oAuthParameters;
        }

        public string Value
        {
            get
            {
                return RequestMethod + Ampersand + RequestUrl + Ampersand + Parameters;
            }
        }

        protected string Ampersand
        {
            get { return "&"; }
        }

        protected string RequestMethod
        {
            get { return new VerbPart(_request).Value; }
        }

        private string RequestUrl
        {
            get { return new EarlPart(_request.Url).Value; }
        }

        protected string Parameters
        {
            get { return new ParameterPart(_request, _oAuthParameters).Value; }
        }

        public static implicit operator string(SignatureBaseString self)
        {
            return self.Value;
        }
    }
}

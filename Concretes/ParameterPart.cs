using Devpoint.XeroProjectsApi.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devpoint.XeroProjectsApi.Concretes
{
    internal class ParameterPart
    {
        private readonly OAuthParameters _oAuthParameters;
        private readonly Request _request;

        internal ParameterPart(Request request, OAuthParameters oAuthParameters)
        {
            _request = request;
            _oAuthParameters = oAuthParameters;
        }

        internal string Value
        {
            get
            {
                return Escape(Parameters.ToString());
            }
        }

        private string Escape(string what)
        {
            return new ParameterEncoding().Escape(what);
        }

        private IEnumerable<Parameter> Parameters
        {
            get
            {
                return new RequestParameters(_request).Tap(it => it.Add(OAuthParameters));
            }
        }

        private Parameters OAuthParameters
        {
            get
            {
                return _oAuthParameters.List();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devpoint.XeroProjectsApi.Concretes
{
    internal class VerbPart
    {
        private readonly Request _request;

        public VerbPart(Request request)
        {
            _request = request;
        }

        public string Value
        {
            get { return _request.Verb.ToUpper(); }
        }
    }
}

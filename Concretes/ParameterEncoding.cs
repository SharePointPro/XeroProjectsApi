using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devpoint.XeroProjectsApi.Concretes
{
    internal class ParameterEncoding
    {
        internal string Escape(string what)
        {
            return UrlEncoder.UrlEncode(what ?? string.Empty);
        }
    }
}

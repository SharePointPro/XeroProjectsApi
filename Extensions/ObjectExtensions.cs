using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devpoint.XeroProjectsApi.Extensions
{
    public static class ObjectExtensions
    {
        public static T Tap<T>(this T self, Action<T> block)
        {
            block(self);

            return self;
        }
    }
}

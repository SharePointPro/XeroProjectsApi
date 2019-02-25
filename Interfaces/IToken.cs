using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devpoint.XeroProjectsApi.Interfaces
{
    public interface IToken
    {
        string UserId { get; set; }
        string OrganisationId { get; set; }

        string ConsumerKey { get; }
        string ConsumerSecret { get; }
        string TokenKey { get; }
        string TokenSecret { get; }
        string Session { get; }

        DateTime? ExpiresAt { get; }
        DateTime? SessionExpiresAt { get; }

        bool HasExpired { get; }
        bool HasSessionExpired { get; }
    }
}

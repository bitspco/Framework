using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitspco.Framework.Filters.Security.Authenticate
{
    public interface IAuthenticator
    {
        string GetToken();
        bool IsTokenValid();
        bool HasPermission(string policy);
    }
}

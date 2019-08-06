using System.ComponentModel;

namespace Bitspco.Framework.Http
{
    public enum HttpMethod
    {
        [Description("GET")]
        Get = 0,
        [Description("POST")]
        Post = 1,
        [Description("PUT")]
        Put = 2,
        [Description("PATCH")]
        Patch = 3,
        [Description("DELETE")]
        Delete = 4,
        [Description("OPTIONS")]
        Options = 5
    }
}
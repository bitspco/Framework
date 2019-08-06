using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Bitspco.Framework.Filters.Security.Models
{
    public class ClientAgentInfo
    {
        public string Agent { get; private set; }
        public string UserAgentFamily { get; private set; }
        public string UserAgentMajor { get; private set; }
        public string UserAgentMinor { get; private set; }
        public string UserAgentPatch { get; private set; }
        public string DeviceBrand { get; private set; }
        public string DeviceFamily { get; private set; }
        public string DeviceModel { get; private set; }
        public string OSFamily { get; private set; }
        public string OSMinor { get; private set; }
        public string OSMajor { get; private set; }
        public string OSPatch { get; private set; }
        public string OSPatchMinor { get; private set; }

        public ClientAgentInfo()
        {

        }
        public ClientAgentInfo(HttpRequestBase request)
        {
            Agent = string.Join(" ", request.Headers.GetValues("User-Agent"));
            var ua = UAParser.Parser.GetDefault().Parse(Agent);
            UserAgentFamily = ua.UA.Family;
            UserAgentMajor = ua.UA.Major;
            UserAgentMinor = ua.UA.Minor;
            UserAgentPatch = ua.UA.Patch;
            DeviceBrand = ua.Device.Brand;
            DeviceFamily = ua.Device.Family;
            DeviceModel = ua.Device.Model;
            OSFamily = ua.OS.Family;
            OSMinor = ua.OS.Minor;
            OSMajor = ua.OS.Major;
            OSPatch = ua.OS.Patch;
            OSPatchMinor = ua.OS.PatchMinor;
        }
    }
}

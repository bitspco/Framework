using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitspco.Framework.Filters.Security.Models
{
    class GeoData
    {
        [JsonProperty("isp")]
        public string ISP { get; set; }
        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }
        [JsonProperty("country")]
        public string CountryName { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("lat")]
        public decimal? Latitude { get; set; }
        [JsonProperty("lon")]
        public decimal? Longitude { get; set; }
        [JsonProperty("timezone")]
        public string TimeZone { get; set; }
        [JsonProperty("")]
        public string ZipCode { get; set; }
        [JsonProperty("region")]
        public string Region { get; set; }
        [JsonProperty("regionName")]
        public string RegionName { get; set; }
        [JsonProperty("org")]
        public string Organization { get; set; }
    }
}

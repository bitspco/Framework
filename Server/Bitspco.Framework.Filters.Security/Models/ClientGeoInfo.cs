using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitspco.Framework.Filters.Security.Models
{
    public class ClientGeoInfo
    {
        private static Dictionary<string, GeoData> _data = new Dictionary<string, GeoData>();

        public string CountryCode { get; private set; }
        public string CountryName { get; private set; }
        public string City { get; private set; }
        public string Region { get; private set; }
        public string RegionName { get; private set; }
        public decimal? Latitude { get; private set; }
        public decimal? Longitude { get; private set; }
        public string TimeZone { get; private set; }
        public string ZipCode { get; private set; }
        public string Organization { get; private set; }
        public string ISP { get; private set; }

        public ClientGeoInfo()
        {

        }
        public ClientGeoInfo(string ip)
        {
            Init(ip);
        }

        public void Init(string ip)
        {
            try
            {
                GeoData geoData;
                lock (_data)
                {
                    if(_data.ContainsKey(ip))
                    {
                        geoData = _data[ip];
                    }
                    else
                    {
                        var provider = new Profit365.GeoIP.GeoLocationProvider();
                        _data[ip] = geoData = Newtonsoft.Json.JsonConvert.DeserializeObject<GeoData>(provider.GetLocationByIP(ip).raw);
                        if (_data.Count > 500) _data = _data.Skip(100).Take(400).ToDictionary(x => x.Key, x => x.Value);
                    }
                }

                CountryCode = geoData.CountryCode;
                CountryName = geoData.CountryName;
                City = geoData.City;
                Region = geoData.Region;
                RegionName = geoData.RegionName;
                Latitude = geoData.Latitude;
                Longitude = geoData.Longitude;
                TimeZone = geoData.TimeZone;
                Organization = geoData.Organization;
                ISP = geoData.ISP;
            }
            catch (Exception) {}
        }
    }
}

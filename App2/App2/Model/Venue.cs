using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using App2.Helpers;
using GoogleApi.Entities.Search.Video.Common;
using Newtonsoft.Json;
using SQLitePCL;

namespace App2.Model
{
    public class Raw
    {
        public int osm_id { get; set; }
        public string name { get; set; }
    }
    public class Datasource
    {
        public Raw raw { get; set; }
    }
    public class Properties
    {
        public double lat { get; set; }
        public double lon { get; set; }
        public string name { get; set; }
        public string address_line1 { get; set; }
        public string address_line2 { get; set; }
        public string formatted { get; set; }
        public IList<string> categories { get; set; }
        public IList<string> details { get; set; }
        public Datasource datasource { get; set; }
        public int distance { get; set; }
        
    }

    public class Feature
    {
        public string type { get; set; }
        public Properties properties { get; set; }

        public async static Task<List<Feature>> GetVenues(double latitude, double longitude)
        {
            List<Feature> venueList = new List<Feature>();
            using (var client = new HttpClient())
            {
                var url = "https://api.geoapify.com/v2/places?categories=accommodation,activity,commercial,catering,education,entertainment,healthcare,leisure,man_made,natural,production,office,parking,pet,service,rental,tourism,religion,camping,amenity,beach,adult,airport,building,ski,sport,public_transport&filter=circle:-0.07071648508463113,51.50848194136378,1000&bias=proximity:-0.07071648508463113,51.50848194136378&limit=20&apiKey=";
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var venue = JsonConvert.DeserializeObject<VenueRoot>(json);

                    venueList = venue.features as List<Feature>;

                }
                return venueList;
            }
        }
    }

    public class VenueRoot
    {
        public IList<Feature> features { get; set; }
    }        
}


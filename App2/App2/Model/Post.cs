using System;
using System.Collections.Generic;
using System.Text;

namespace App2.Model
{
    public class Post
    {
        
        public string Id { get; set; }
                
        public string Experience { get; set; }

        public double Lat { get; set; }

        public double Lon { get; set; }

        public string VenueName { get; set; }

        public string Category { get; set; }

        public string Address { get; set; }

        public string FormattedAddress { get; set; }

        public int Distance { get; set;}

        public string UserId { get; set; }

    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVShowTimeApi.Model
{
    public class User
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Username { get; set; }

        [JsonProperty("all_images")]
        public UserImages Images { get; set; }
    }

    public class UserImages
    {
        [JsonProperty("1")]
        public string Primary { get; set; }

        [JsonProperty("2")]
        public string Secondary { get; set; }

        [JsonProperty("square")]
        public string Square { get; set; }
    }
}

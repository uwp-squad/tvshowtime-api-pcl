using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVShowTimeApi.Model
{
    public class EpisodeNumber
    {
        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("season_number")]
        public int Season { get; set; }
    }
}

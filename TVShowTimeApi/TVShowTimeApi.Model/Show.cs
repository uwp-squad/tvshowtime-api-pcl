using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVShowTimeApi.Model
{
    public class Show
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("overview")]
        public string Overview { get; set; }

        [JsonProperty("last_seen")]
        public DateTime? LastSeen { get; set; }

        [JsonProperty("last_aired")]
        public EpisodeNumber LastAired { get; set; }

        [JsonProperty("next_aired")]
        public EpisodeNumber NextAired { get; set; }

        [JsonProperty("nb_followers")]
        public long NumberOfFollowers { get; set; }

        [JsonProperty("followed")]
        public bool? Followed { get; set; }

        [JsonProperty("archived")]
        public bool? Archived { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("hashtag")]
        public string Hashtag { get; set; }

        [JsonProperty("number_of_seasons")]
        public int NumberOfSeasons { get; set; }

        /// <summary>
        /// Time of an episode (in minutes)
        /// </summary>
        [JsonProperty("runtime")]
        public int RunTime { get; set; }

        [JsonProperty("images")]
        public ShowImages Images { get; set; }

        [JsonProperty("seen_episodes")]
        public int SeenEpisodes { get; set; }

        [JsonProperty("aired_episodes")]
        public int AiredEpisodes { get; set; }

        public int RemainingEpisodesToWatch { get { return AiredEpisodes - SeenEpisodes; } }
    }

    public class ShowImages
    {
        [JsonProperty("poster")]
        public SubShowImages Posters { get; set; }

        [JsonProperty("banner")]
        public List<string> Banners { get; set; }

        [JsonProperty("fanart")]
        public SubShowImages Fanarts { get; set; }
    }

    public class SubShowImages
    {
        [JsonProperty("0")]
        public string Zero { get; set; }

        [JsonProperty("1")]
        public string One { get; set; }

        [JsonProperty("2")]
        public string Two { get; set; }

        [JsonProperty("3")]
        public string Three { get; set; }
    }
}

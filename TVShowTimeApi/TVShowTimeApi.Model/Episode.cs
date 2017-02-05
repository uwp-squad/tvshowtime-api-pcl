using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVShowTimeApi.Model
{
    public class Episode
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("season_number")]
        public int Season { get; set; }

        [JsonProperty("air_date")]
        public DateTime? AirDate { get; set; }

        [JsonProperty("air_time")]
        public DateTimeOffset? AirTime { get; set; }

        [JsonProperty("network")]
        public string Network { get; set; }

        [JsonProperty("overview")]
        public string Overview { get; set; }

        [JsonProperty("show")]
        public Show Show { get; set; }

        [JsonProperty("nb_comments")]
        public long NumberOfComments { get; set; }

        [JsonProperty("is_new")]
        public bool IsNew { get; set; }

        [JsonProperty("seen")]
        public bool Seen { get; set; }

        [JsonProperty("previous_episode")]
        public EpisodePreview PreviousEpisode { get; set; }

        [JsonProperty("next_episode")]
        public EpisodePreview NextEpisode { get; set; }

        [JsonProperty("mean_rate")]
        public decimal MeanReate { get; set; }

        [JsonProperty("emotion")]
        public Emotion Emotion { get; set; }
    }

    public class EpisodePreview
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("season_number")]
        public int Season { get; set; }
    }
}

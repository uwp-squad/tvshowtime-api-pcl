using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVShowTimeApi.Model.Requests
{
    public class EpisodeRequest
    {
    }

    public class EpisodeRequestByFilename : EpisodeRequest
    {
        /// <summary>
        /// The filename of the episode. e.g. "game.of.thrones.s04e10.720p.hdtv.x264-killers.mkv"
        /// </summary>
        [JsonProperty("filename")]
        public string Filename { get; set; }
    }

    public class EpisodeRequestByEpisodeId : EpisodeRequest
    {
        /// <summary>
        /// The TVDB ID of the episode (http://thetvdb.com)
        /// </summary>
        [JsonProperty("episode_id")]
        public int EpisodeId { get; set; }
    }

    public class EpisodeRequestByImdbId : EpisodeRequest
    {
        /// <summary>
        /// The IMDB ID of the episode (The IMDB ID of the episode)
        /// </summary>
        [JsonProperty("imdb_id")]
        public string ImdbId { get; set; }
    }

    public class EpisodeRequestByShowData : EpisodeRequest
    {
        /// <summary>
        /// The TVDB ID of the show (http://thetvdb.com)
        /// </summary>
        [JsonProperty("show_id")]
        public int ShowId { get; set; }

        /// <summary>
        /// Season number of the episode
        /// </summary>
        [JsonProperty("season_number")]
        public int Season { get; set; }

        /// <summary>
        /// Episode number of the episode
        /// </summary>
        [JsonProperty("number")]
        public int Number { get; set; }
    }
}

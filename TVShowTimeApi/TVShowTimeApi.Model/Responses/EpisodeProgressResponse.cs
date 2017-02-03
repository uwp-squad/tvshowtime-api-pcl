using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVShowTimeApi.Model.Responses
{
    public class EpisodeProgressResponse : Response
    {
        /// <summary>
        /// Progress in seconds
        /// </summary>
        [JsonProperty("progress")]
        public int Progress { get; set; }
    }
}

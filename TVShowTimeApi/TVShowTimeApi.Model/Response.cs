using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVShowTimeApi.Model
{
    public class Response
    {
        [JsonProperty("result")]
        public string Result { get; set; }
    }
}

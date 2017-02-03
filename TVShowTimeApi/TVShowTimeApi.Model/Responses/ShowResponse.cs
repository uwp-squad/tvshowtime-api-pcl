using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVShowTimeApi.Model.Responses
{
    public class ShowResponse : Response
    {
        [JsonProperty("show")]
        public Show Show { get; set; }
    }
}

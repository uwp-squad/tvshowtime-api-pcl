using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVShowTimeApi.Model.Responses
{
    public class ErrorResponse : Response
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}

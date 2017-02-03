using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVShowTimeApi.Model.Responses
{
    public class AuthenticationResponse : Response
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}

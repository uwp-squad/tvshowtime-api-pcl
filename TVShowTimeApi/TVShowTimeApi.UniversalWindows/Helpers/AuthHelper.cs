using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TVShowTimeApi.Exceptions;
using TVShowTimeApi.Model.Responses;
#if __IOS__ || __ANDROID__ || NET45
using System.Net.Http;
using System.Net.Http.Headers;
#endif
#if NETFX_CORE
using Windows.Web.Http;
using Windows.Web.Http.Headers;
using UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding;
using Windows.Security.Authentication.Web;
#endif

namespace TVShowTimeApi.Helpers
{
    internal static class AuthHelper
    {
        #region Properties

        /// <summary>
        /// Redirect URL when authenticate
        /// </summary>
        public static string RedirectUrl = "http://localhost";

        #endregion

        #region Methods

        public static async Task<string> RetrieveToken(WebAuthenticationResult result, string oauthKey, string oauthSecret)
        {
            if (result.ResponseStatus == WebAuthenticationStatus.Success)
            {
                string code = GetCode(result.ResponseData);
                return await GetToken(code, oauthKey, oauthSecret);
            }
            if (result.ResponseStatus == WebAuthenticationStatus.ErrorHttp)
            {
                throw new ApiAuthException(WebAuthenticationStatus.ErrorHttp);
            }
            if (result.ResponseStatus == WebAuthenticationStatus.UserCancel)
            {
                throw new ApiAuthException(WebAuthenticationStatus.UserCancel);
            }

            return null;
        }

        private static string GetCode(string webAuthResultResponseData)
        {
            string[] splitResultResponse = webAuthResultResponseData.Split('&');
            string codeString = splitResultResponse.FirstOrDefault(value => value.Contains("code"));
            string[] splitCode = codeString.Split('=');
            return splitCode.Last();
        }

        private static async Task<string> GetToken(string code, string oauthKey, string oauthSecret)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new HttpMediaTypeWithQualityHeaderValue("application/json"));

                var content = new HttpFormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("client_id", oauthKey),
                    new KeyValuePair<string, string>("client_secret", oauthSecret),
                    new KeyValuePair<string, string>("code", code),
                    new KeyValuePair<string, string>("redirect_uri", RedirectUrl)
                });

                var response = await httpClient.PostAsync(new Uri("https://api.tvshowtime.com/v1/oauth/access_token"), content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(result);
                    throw new ApiException(errorResponse.Message, response.StatusCode);
                }

                string accessToken = JsonConvert.DeserializeObject<AuthenticationResponse>(result).AccessToken;

                if (string.IsNullOrWhiteSpace(accessToken))
                {
                    throw new ApiException("The access token retrieved is null or empty.", response.StatusCode);
                }

                return accessToken;
            }
        }

        #endregion
    }
}

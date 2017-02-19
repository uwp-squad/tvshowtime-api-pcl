using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVShowTimeApi.Configuration;
using TVShowTimeApi.Helpers;
using TVShowTimeApi.Model;
using TVShowTimeApi.Model.Requests;
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

namespace TVShowTimeApi.Services
{
    public class TVShowTimeApiService : ITVShowTimeApiService
    {
        #region Fields

        private readonly string _baseApiAddress = $"{ApiConstants.ApiBaseUrl}{ApiConstants.ApiVersion}";

        private HttpClient HttpClient
        {
            get
            {
                var httpClient = new HttpClient();

#if __IOS__ || __ANDROID__ || NET45
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (!string.IsNullOrWhiteSpace(Token))
                    httpClient.DefaultRequestHeaders.Add("TVST_ACCESS_TOKEN", Token);
#endif
#if NETFX_CORE
                httpClient.DefaultRequestHeaders.Accept.Add(new HttpMediaTypeWithQualityHeaderValue("application/json"));
                if (!string.IsNullOrWhiteSpace(Token))
                    httpClient.DefaultRequestHeaders.Add("TVST_ACCESS_TOKEN", Token);
#endif
                httpClient.DefaultRequestHeaders.IfModifiedSince = DateTime.UtcNow;

                return httpClient;
            }
        }

        #endregion

        #region Properties

        public string Token { get; set; }

        #endregion

        #region Constructors

        public TVShowTimeApiService() { }

        public TVShowTimeApiService(string token)
        {
            Token = token;
        }

        #endregion

        #region Authentication

        public async Task<bool?> LoginAsync(string oauthKey, string oauthSecret)
        {
#if NETFX_CORE
            // Create Auth url
            var state = Guid.NewGuid();
            string startUrl = $"https://www.tvshowtime.com/oauth/authorize?state={state}&redirect_uri={AuthHelper.RedirectUrl}&client_id={oauthKey}";
            var startUri = new Uri(startUrl);
            var endUri = new Uri(AuthHelper.RedirectUrl);

            try
            {
                // Launch authentication webview
                var webAuthenticationResult = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, startUri, endUri);
                Token = await AuthHelper.RetrieveToken(webAuthenticationResult, oauthKey, oauthSecret);
                return !string.IsNullOrWhiteSpace(Token);
            }
            catch
            {
                return null;
            }
#else
            throw new NotImplementedException();
#endif
        }

        #endregion

        #region User

        public async Task<UserResponse> GetCurrentUserAsync()
        {
            string url = _baseApiAddress + "user";
            return await HttpClient.GetAsync<UserResponse>(url);
        }

        public async Task<WatchlistResponse> GetWatchlistAsync(int page = 0, int limit = 10, string languageCode = null)
        {
            string url = _baseApiAddress + $"to_watch?page={page}&limit={limit}";

            if (!string.IsNullOrWhiteSpace(languageCode))
                url += $"&lang={languageCode}";

            return await HttpClient.GetAsync<WatchlistResponse>(url);
        }

        public async Task<AgendaResponse> GetAgendaAsync(int page = 0, int limit = 10, bool includeWatchedEpisodes = false)
        {
            string url = _baseApiAddress + $"agenda?page={page}&limit={limit}";

            if (includeWatchedEpisodes)
                url += "&include_watched=1";
            else
                url += "&include_watched=0";

            return await HttpClient.GetAsync<AgendaResponse>(url);
        }

        public async Task<LibraryResponse> GetLibraryAsync(int page = 0, int limit = 10)
        {
            string url = _baseApiAddress + $"library?page={page}&limit={limit}";
            return await HttpClient.GetAsync<LibraryResponse>(url);
        }

        #endregion

        #region Show

        public async Task<ExploreResponse> GetTrendingShowsAsync(int page = 0, int limit = 10)
        {
            string url = _baseApiAddress + $"explore?page={page}&limit={limit}";
            return await HttpClient.GetAsync<ExploreResponse>(url);
        }

        public async Task<ShowResponse> GetShowAsync(long showId, string showName, bool includeEpisodes, bool exactMatchName = false)
        {
            string url = _baseApiAddress + $"show?show_id={showId}&show_name={showName}";

            if (includeEpisodes)
                url += "&include_episodes=1";
            else
                url += "&include_episodes=0";

            if (exactMatchName)
                url += "&exact=1";
            else
                url += "&exact=0";

            return await HttpClient.GetAsync<ShowResponse>(url);
        }

        public async Task<Response> FollowShowAsync(long showId)
        {
            string url = _baseApiAddress + $"follow?show_id={showId}";
            return await HttpClient.PostAsync<Response>(url, null);
        }

        public async Task<BooleanResponse> GetIsShowFollowedAsync(long showId)
        {
            string url = _baseApiAddress + $"follow?show_id={showId}";
            return await HttpClient.GetAsync<BooleanResponse>(url);
        }

        public async Task<Response> UnfollowShowAsync(long showId)
        {
            string url = _baseApiAddress + $"unfollow?show_id={showId}";
            return await HttpClient.PostAsync<Response>(url, null);
        }

        public async Task<Response> ArchiveShowAsync(long showId)
        {
            string url = _baseApiAddress + $"archive";

            var parameters = new Dictionary<string, string>
            {
                { "show_id", showId.ToString() }
            };

#if __IOS__ || __ANDROID__ || NET45
            var content = new FormUrlEncodedContent(parameters);
#endif
#if NETFX_CORE
            var content = new HttpFormUrlEncodedContent(parameters);
#endif

            return await HttpClient.PostAsync<Response>(url, content);
        }

        public async Task<BooleanResponse> GetIsShowArchivedAsync(long showId)
        {
            string url = _baseApiAddress + $"archive?show_id={showId}";
            return await HttpClient.GetAsync<BooleanResponse>(url);
        }

        public async Task<Response> UnarchiveShowAsync(long showId)
        {
            string url = _baseApiAddress + $"unarchive";

            var parameters = new Dictionary<string, string>
            {
                { "show_id", showId.ToString() }
            };

#if __IOS__ || __ANDROID__ || NET45
            var content = new FormUrlEncodedContent(parameters);
#endif
#if NETFX_CORE
            var content = new HttpFormUrlEncodedContent(parameters);
#endif

            return await HttpClient.PostAsync<Response>(url, content);
        }

        public async Task<Response> MarkShowWatchedAsync(long showId)
        {
            string url = _baseApiAddress + $"show_progress";

            var parameters = new Dictionary<string, string>
            {
                { "show_id", showId.ToString() }
            };

#if __IOS__ || __ANDROID__ || NET45
            var content = new FormUrlEncodedContent(parameters);
#endif
#if NETFX_CORE
            var content = new HttpFormUrlEncodedContent(parameters);
#endif

            return await HttpClient.PostAsync<Response>(url, content);
        }

        public async Task<Response> SaveShowProgressAsync(long showId, int season, int? episode)
        {
            string url = _baseApiAddress + $"show_progress";

            var parameters = new Dictionary<string, string>
            {
                { "show_id", showId.ToString() },
                { "season", season.ToString() }
            };

            if (episode.HasValue)
                parameters.Add("episode", episode.Value.ToString());

#if __IOS__ || __ANDROID__ || NET45
            var content = new FormUrlEncodedContent(parameters);
#endif
#if NETFX_CORE
            var content = new HttpFormUrlEncodedContent(parameters);
#endif

            return await HttpClient.PostAsync<Response>(url, content);
        }

        public async Task<Response> UnmarkShowWatchedAsync(long showId)
        {
            string url = _baseApiAddress + $"delete_show_progress";

            var parameters = new Dictionary<string, string>
            {
                { "show_id", showId.ToString() }
            };

#if __IOS__ || __ANDROID__ || NET45
            var content = new FormUrlEncodedContent(parameters);
#endif
#if NETFX_CORE
            var content = new HttpFormUrlEncodedContent(parameters);
#endif

            return await HttpClient.PostAsync<Response>(url, content);
        }

        public async Task<Response> DeleteShowProgressAsync(long showId, int season, int? episode)
        {
            string url = _baseApiAddress + $"delete_show_progress";

            var parameters = new Dictionary<string, string>
            {
                { "show_id", showId.ToString() },
                { "season", season.ToString() }
            };

            if (episode.HasValue)
                parameters.Add("episode", episode.Value.ToString());

#if __IOS__ || __ANDROID__ || NET45
            var content = new FormUrlEncodedContent(parameters);
#endif
#if NETFX_CORE
            var content = new HttpFormUrlEncodedContent(parameters);
#endif

            return await HttpClient.PostAsync<Response>(url, content);
        }

        #endregion

        #region Episode

        private void ExtractParametersFromEpisodeRequest(EpisodeRequest episodeRequest, Dictionary<string, string> parameters)
        {
            if (episodeRequest is EpisodeRequestByEpisodeId episodeRequestByEpisodeId)
            {
                parameters.Add("episode_id", episodeRequestByEpisodeId.EpisodeId.ToString());
            }

            if (episodeRequest is EpisodeRequestByFilename episodeRequestByFilename)
            {
                parameters.Add("filename", episodeRequestByFilename.Filename);
            }

            if (episodeRequest is EpisodeRequestByImdbId episodeRequestByImdbId)
            {
                parameters.Add("imdb_id", episodeRequestByImdbId.ImdbId);
            }

            if (episodeRequest is EpisodeRequestByShowData episodeRequestByShowData)
            {
                parameters.Add("show_id", episodeRequestByShowData.ShowId.ToString());
                parameters.Add("season_number", episodeRequestByShowData.Season.ToString());
                parameters.Add("number", episodeRequestByShowData.Number.ToString());
            }
        }

        public async Task<EpisodeResponse> GetEpisodeAsync(EpisodeRequest episodeRequest)
        {
            string url = _baseApiAddress + $"episode?";

            var parameters = new Dictionary<string, string>();
            ExtractParametersFromEpisodeRequest(episodeRequest, parameters);

            url += string.Join("&", parameters.Select(param => $"{param.Key}={param.Value}"));

            return await HttpClient.GetAsync<EpisodeResponse>(url);
        }

        public async Task<Response> MarkEpisodeWatchedAsync(EpisodeRequest episodeRequest, bool publishOnFacebook, bool publishOnTwitter, bool autoFollow = true)
        {
            string url = _baseApiAddress + $"checkin";

            var parameters = new Dictionary<string, string>();
            ExtractParametersFromEpisodeRequest(episodeRequest, parameters);

            parameters.Add("publish_on_ticker", publishOnFacebook ? "1" : "0");
            parameters.Add("publish_on_twitter", publishOnTwitter ? "1" : "0");
            parameters.Add("auto_follow", autoFollow ? "1" : "0");


#if __IOS__ || __ANDROID__ || NET45
            var content = new FormUrlEncodedContent(parameters);
#endif
#if NETFX_CORE
            var content = new HttpFormUrlEncodedContent(parameters);
#endif

            return await HttpClient.PostAsync<Response>(url, content);
        }

        public async Task<BooleanResponse> GetIsEpisodeWatchedAsync(EpisodeRequest episodeRequest)
        {
            string url = _baseApiAddress + $"checkin?";

            var parameters = new Dictionary<string, string>();
            ExtractParametersFromEpisodeRequest(episodeRequest, parameters);

            url += string.Join("&", parameters.Select(param => $"{param.Key}={param.Value}"));

            return await HttpClient.GetAsync<BooleanResponse>(url);
        }

        public async Task<Response> UnmarkEpisodeWatchedAsync(EpisodeRequest episodeRequest)
        {
            string url = _baseApiAddress + $"checkout";

            var parameters = new Dictionary<string, string>();
            ExtractParametersFromEpisodeRequest(episodeRequest, parameters);

#if __IOS__ || __ANDROID__ || NET45
            var content = new FormUrlEncodedContent(parameters);
#endif
#if NETFX_CORE
            var content = new HttpFormUrlEncodedContent(parameters);
#endif

            return await HttpClient.PostAsync<Response>(url, content);
        }

        public async Task<EpisodeProgressResponse> GetEpisodeProgressAsync(EpisodeRequest episodeRequest)
        {
            string url = _baseApiAddress + $"progress?";

            var parameters = new Dictionary<string, string>();
            ExtractParametersFromEpisodeRequest(episodeRequest, parameters);

            url += string.Join("&", parameters.Select(param => $"{param.Key}={param.Value}"));

            return await HttpClient.GetAsync<EpisodeProgressResponse>(url);
        }

        public async Task<EpisodeProgressResponse> SaveEpisodeProgressAsync(EpisodeRequest episodeRequest)
        {
            string url = _baseApiAddress + $"progress";

            var parameters = new Dictionary<string, string>();
            ExtractParametersFromEpisodeRequest(episodeRequest, parameters);

#if __IOS__ || __ANDROID__ || NET45
            var content = new FormUrlEncodedContent(parameters);
#endif
#if NETFX_CORE
            var content = new HttpFormUrlEncodedContent(parameters);
#endif

            return await HttpClient.PostAsync<EpisodeProgressResponse>(url, content);
        }

        public async Task<EmotionResponse> SetEmotionForEpisodeAsync(long episodeId, Emotion emotion)
        {
            string url = _baseApiAddress + $"emotion?";

            var parameters = new Dictionary<string, string>
            {
                { "episode_id", episodeId.ToString() },
                { "emotion_id", ((int)emotion).ToString() }
            };

            url += string.Join("&", parameters.Select(param => $"{param.Key}={param.Value}"));

            return await HttpClient.PostAsync<EmotionResponse>(url, null);
        }

        public async Task<Response> DeleteEmotionForEpisodeAsync(long episodeId)
        {
            string url = _baseApiAddress + $"delete_emotion";

            var parameters = new Dictionary<string, string>
            {
                { "episode_id", episodeId.ToString() }
            };

#if __IOS__ || __ANDROID__ || NET45
            var content = new FormUrlEncodedContent(parameters);
#endif
#if NETFX_CORE
            var content = new HttpFormUrlEncodedContent(parameters);
#endif

            return await HttpClient.PostAsync<Response>(url, content);
        }

        #endregion
    }
}

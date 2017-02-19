using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVShowTimeApi.Model;
using TVShowTimeApi.Model.Requests;
using TVShowTimeApi.Model.Responses;

namespace TVShowTimeApi.Services
{
    public interface ITVShowTimeApiService
    {
        #region Properties

        /// <summary>
        /// Token used by the TVShowTime API to provide access to the entire API
        /// </summary>
        string Token { get; set; }

        #endregion

        #region Authentication

        /// <summary>
        /// Execute login process through OAuth2 authentication mechanism
        /// </summary>
        /// <param name="oauthKey">OAuth client key (provided by TVShowTime website)</param>
        /// <param name="oauthSecret">OAuth secret key (provided by TVShowTime website)</param>
        /// <returns>true: login success / false: login failed / null: exception occured</returns>
        Task<bool?> LoginAsync(string oauthKey, string oauthSecret);

        #endregion

        #region User

        /// <summary>
        /// Returns current user info
        /// (https://api.tvshowtime.com/doc#user)
        /// </summary>
        /// <returns></returns>
        Task<UserResponse> GetCurrentUserAsync();

        /// <summary>
        /// Returns the user to-watch list
        /// (https://api.tvshowtime.com/doc#to_watch)
        /// </summary>
        /// <param name="page">Page number</param>
        /// <param name="limit">Elements per page</param>
        /// <param name="languageCode">Language code of the content (<see cref="LanguageCodes"/>), by default : user language</param>
        /// <returns></returns>
        Task<WatchlistResponse> GetWatchlistAsync(int page = 0, int limit = 10, string languageCode = null);

        /// <summary>
        /// Returns the user agenda
        /// (https://api.tvshowtime.com/doc#agenda)
        /// </summary>
        /// <param name="page">Page number</param>
        /// <param name="limit">Elements per page</param>
        /// <param name="includeWatchedEpisodes">Include watched episodes in results</param>
        /// <returns></returns>
        Task<AgendaResponse> GetAgendaAsync(int page = 0, int limit = 10, bool includeWatchedEpisodes = false);

        /// <summary>
        /// Returns the user library
        /// (https://api.tvshowtime.com/doc#library)
        /// </summary>
        /// <param name="page">Page number</param>
        /// <param name="limit">Elements per page</param>
        /// <returns></returns>
        Task<LibraryResponse> GetLibraryAsync(int page = 0, int limit = 10);

        #endregion

        #region Show

        /// <summary>
        /// Discover trending shows
        /// (https://api.tvshowtime.com/doc#explore)
        /// </summary>
        /// <param name="page">Page number</param>
        /// <param name="limit">Elements per page</param>
        /// <returns></returns>
        Task<ExploreResponse> GetTrendingShowsAsync(int page = 0, int limit = 10);

        /// <summary>
        /// Returns show data
        /// (https://api.tvshowtime.com/doc#show)
        /// </summary>
        /// <param name="showId">The TVDB ID of the show (http://thetvdb.com)</param>
        /// <param name="showName">The name of the show</param>
        /// <param name="includeEpisodes">Include all episodes in results</param>
        /// <param name="exactMatchName">Do exact match for show name</param>
        /// <returns></returns>
        Task<ShowResponse> GetShowAsync(long showId, string showName, bool includeEpisodes, bool exactMatchName = false);

        /// <summary>
        /// Follow a show 
        /// (https://api.tvshowtime.com/doc#follow)
        /// </summary>
        /// <param name="showId">The TVDB ID of the show (http://thetvdb.com)</param>
        /// <returns></returns>
        Task<Response> FollowShowAsync(long showId);

        /// <summary>
        /// Returns a value indicating if the show is followed or not by the user 
        /// (https://api.tvshowtime.com/doc#is_followed)
        /// </summary>
        /// <param name="showId">The TVDB ID of the show (http://thetvdb.com)</param>
        /// <returns></returns>
        Task<BooleanResponse> GetIsShowFollowedAsync(long showId);

        /// <summary>
        /// Unfollow a show 
        /// (https://api.tvshowtime.com/doc#unfollow)
        /// </summary>
        /// <param name="showId">The TVDB ID of the show (http://thetvdb.com)</param>
        /// <returns></returns>
        Task<Response> UnfollowShowAsync(long showId);

        /// <summary>
        /// Archive a show 
        /// (https://api.tvshowtime.com/doc#archive)
        /// </summary>
        /// <param name="showId">The TVDB ID of the show (http://thetvdb.com)</param>
        /// <returns></returns>
        Task<Response> ArchiveShowAsync(long showId);

        /// <summary>
        /// Returns a value indicating if the show is archived or not by the user 
        /// (https://api.tvshowtime.com/doc#is_archived)
        /// </summary>
        /// <param name="showId">The TVDB ID of the show (http://thetvdb.com)</param>
        /// <returns></returns>
        Task<BooleanResponse> GetIsShowArchivedAsync(long showId);

        /// <summary>
        /// Unarchive a show 
        /// (https://api.tvshowtime.com/doc#unarchive)
        /// </summary>
        /// <param name="showId">The TVDB ID of the show (http://thetvdb.com)</param>
        /// <returns></returns>
        Task<Response> UnarchiveShowAsync(long showId);

        /// <summary>
        /// Mark the show (all seasons, all episodes seen)
        /// (https://api.tvshowtime.com/doc#save_show_progress)
        /// </summary>
        /// <param name="showId">The TVDB ID of the show (http://thetvdb.com)</param>
        /// <returns></returns>
        Task<Response> MarkShowWatchedAsync(long showId);

        /// <summary>
        /// Set the progress for a show 
        /// (https://api.tvshowtime.com/doc#save_show_progress)
        /// </summary>
        /// <param name="showId">The TVDB ID of the show (http://thetvdb.com)</param>
        /// <param name="season">The season number</param>
        /// <param name="episode">The episode number (if not set, mark the whole season seen)</param>
        /// <returns></returns>
        Task<Response> SaveShowProgressAsync(long showId, int season, int? episode);

        /// <summary>
        /// Unmark the show (all seasons, all episodes not seen)
        /// (https://api.tvshowtime.com/doc#delete_show_progress)
        /// </summary>
        /// <param name="showId">The TVDB ID of the show (http://thetvdb.com)</param>
        /// <returns></returns>
        Task<Response> UnmarkShowWatchedAsync(long showId);

        /// <summary>
        /// Delete the progress for a show 
        /// (https://api.tvshowtime.com/doc#delete_show_progress)
        /// </summary>
        /// <param name="showId">The TVDB ID of the show (http://thetvdb.com)</param>
        /// <param name="season">The season number</param>
        /// <param name="episode">The episode number (if not set, unmark the whole season seen)</param>
        /// <returns></returns>
        Task<Response> DeleteShowProgressAsync(long showId, int season, int? episode);

        #endregion

        #region Episode

        /// <summary>
        /// Returns episode data
        /// (https://api.tvshowtime.com/doc#episode)
        /// </summary>
        /// <param name="episodeRequest">Episode request parameters</param>
        /// <returns></returns>
        Task<EpisodeResponse> GetEpisodeAsync(EpisodeRequest episodeRequest);

        /// <summary>
        /// Mark an episode as watched 
        /// (https://api.tvshowtime.com/doc#checkin)
        /// </summary>
        /// <param name="episodeRequest">Episode request parameters</param>
        /// <param name="publishOnFacebook">Publish on Facebook</param>
        /// <param name="publishOnTwitter">Publish on Twitter</param>
        /// <param name="autoFollow">Auto-follow the show if not already followed</param>
        /// <returns></returns>
        Task<Response> MarkEpisodeWatchedAsync(EpisodeRequest episodeRequest, bool publishOnFacebook, bool publishOnTwitter, bool autoFollow = true);

        /// <summary>
        /// Returns a value indicating if the episode is watched or not by the user 
        /// (https://api.tvshowtime.com/doc#is_watched)
        /// </summary>
        /// <param name="episodeRequest">Episode request parameters</param>
        /// <returns></returns>
        Task<BooleanResponse> GetIsEpisodeWatchedAsync(EpisodeRequest episodeRequest);

        /// <summary>
        /// Unmark an episode as watched 
        /// (https://api.tvshowtime.com/doc#checkout)
        /// </summary>
        /// <param name="episodeRequest">Episode request parameters</param>
        /// <returns></returns>
        Task<Response> UnmarkEpisodeWatchedAsync(EpisodeRequest episodeRequest);

        /// <summary>
        /// Returns the progress for an episode
        /// (https://api.tvshowtime.com/doc#retrieve_progress)
        /// </summary>
        /// <param name="episodeRequest">Episode request parameters</param>
        /// <returns></returns>
        Task<EpisodeProgressResponse> GetEpisodeProgressAsync(EpisodeRequest episodeRequest);

        /// <summary>
        /// Set the progress for an episode
        /// (https://api.tvshowtime.com/doc#save_progress)
        /// </summary>
        /// <param name="episodeRequest">Episode request parameters</param>
        /// <returns></returns>
        Task<EpisodeProgressResponse> SaveEpisodeProgressAsync(EpisodeRequest episodeRequest);

        /// <summary>
        /// Set the emotion for an episode 
        /// (https://api.tvshowtime.com/doc#set_emotion)
        /// </summary>
        /// <param name="episodeId">The TVDB ID of the episode (http://thetvdb.com)</param>
        /// <param name="emotion">The emotion the user felt</param>
        /// <returns></returns>
        Task<EmotionResponse> SetEmotionForEpisodeAsync(long episodeId, Emotion emotion);

        /// <summary>
        /// Delete the emotion for an episode
        /// (https://api.tvshowtime.com/doc#delete_emotion)
        /// </summary>
        /// <param name="episodeId">The TVDB ID of the episode (http://thetvdb.com)</param>
        /// <returns></returns>
        Task<Response> DeleteEmotionForEpisodeAsync(long episodeId);

        #endregion
    }
}

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
    public interface IReactiveTVShowTimeApiService
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
        IObservable<bool?> Login(string oauthKey, string oauthSecret);

        #endregion

        #region User

        /// <summary>
        /// Returns current user info
        /// (https://api.tvshowtime.com/doc#user)
        /// </summary>
        /// <returns></returns>
        IObservable<UserResponse> GetCurrentUser();

        /// <summary>
        /// Returns the user to-watch list
        /// (https://api.tvshowtime.com/doc#to_watch)
        /// </summary>
        /// <param name="page">Page number</param>
        /// <param name="limit">Elements per page</param>
        /// <param name="languageCode">Language code of the content (<see cref="LanguageCodes"/>), by default : user language</param>
        /// <returns></returns>
        IObservable<WatchlistResponse> GetWatchlist(int page = 0, int limit = 10, string languageCode = null);

        /// <summary>
        /// Returns the user agenda
        /// (https://api.tvshowtime.com/doc#agenda)
        /// </summary>
        /// <param name="page">Page number</param>
        /// <param name="limit">Elements per page</param>
        /// <param name="includeWatchedEpisodes">Include watched episodes in results</param>
        /// <returns></returns>
        IObservable<AgendaResponse> GetAgenda(int page = 0, int limit = 10, bool includeWatchedEpisodes = false);

        /// <summary>
        /// Returns the user library
        /// (https://api.tvshowtime.com/doc#library)
        /// </summary>
        /// <param name="page">Page number</param>
        /// <param name="limit">Elements per page</param>
        /// <returns></returns>
        IObservable<LibraryResponse> GetLibrary(int page = 0, int limit = 10);

        #endregion

        #region Show

        /// <summary>
        /// Discover trending shows
        /// (https://api.tvshowtime.com/doc#explore)
        /// </summary>
        /// <param name="page">Page number</param>
        /// <param name="limit">Elements per page</param>
        /// <returns></returns>
        IObservable<ExploreResponse> GetTrendingShows(int page = 0, int limit = 10);

        /// <summary>
        /// Returns show data
        /// (https://api.tvshowtime.com/doc#show)
        /// </summary>
        /// <param name="showId">The TVDB ID of the show (http://thetvdb.com)</param>
        /// <param name="showName">The name of the show</param>
        /// <param name="includeEpisodes">Include all episodes in results</param>
        /// <param name="exactMatchName">Do exact match for show name</param>
        /// <returns></returns>
        IObservable<ShowResponse> GetShow(long showId, string showName = "", bool includeEpisodes = false, bool exactMatchName = false);

        /// <summary>
        /// Follow a show 
        /// (https://api.tvshowtime.com/doc#follow)
        /// </summary>
        /// <param name="showId">The TVDB ID of the show (http://thetvdb.com)</param>
        /// <returns></returns>
        IObservable<Response> FollowShow(long showId);

        /// <summary>
        /// Returns a value indicating if the show is followed or not by the user 
        /// (https://api.tvshowtime.com/doc#is_followed)
        /// </summary>
        /// <param name="showId">The TVDB ID of the show (http://thetvdb.com)</param>
        /// <returns></returns>
        IObservable<BooleanResponse> GetIsShowFollowed(long showId);

        /// <summary>
        /// Unfollow a show 
        /// (https://api.tvshowtime.com/doc#unfollow)
        /// </summary>
        /// <param name="showId">The TVDB ID of the show (http://thetvdb.com)</param>
        /// <returns></returns>
        IObservable<Response> UnfollowShow(long showId);

        /// <summary>
        /// Archive a show 
        /// (https://api.tvshowtime.com/doc#archive)
        /// </summary>
        /// <param name="showId">The TVDB ID of the show (http://thetvdb.com)</param>
        /// <returns></returns>
        IObservable<Response> ArchiveShow(long showId);

        /// <summary>
        /// Returns a value indicating if the show is archived or not by the user 
        /// (https://api.tvshowtime.com/doc#is_archived)
        /// </summary>
        /// <param name="showId">The TVDB ID of the show (http://thetvdb.com)</param>
        /// <returns></returns>
        IObservable<BooleanResponse> GetIsShowArchived(long showId);

        /// <summary>
        /// Unarchive a show 
        /// (https://api.tvshowtime.com/doc#unarchive)
        /// </summary>
        /// <param name="showId">The TVDB ID of the show (http://thetvdb.com)</param>
        /// <returns></returns>
        IObservable<Response> UnarchiveShow(long showId);

        /// <summary>
        /// Mark the show (all seasons, all episodes seen)
        /// (https://api.tvshowtime.com/doc#save_show_progress)
        /// </summary>
        /// <param name="showId">The TVDB ID of the show (http://thetvdb.com)</param>
        /// <returns></returns>
        IObservable<Response> MarkShowWatched(long showId);

        /// <summary>
        /// Set the progress for a show 
        /// (https://api.tvshowtime.com/doc#save_show_progress)
        /// </summary>
        /// <param name="showId">The TVDB ID of the show (http://thetvdb.com)</param>
        /// <param name="season">The season number</param>
        /// <param name="episode">The episode number (if not set, mark the whole season seen)</param>
        /// <returns></returns>
        IObservable<Response> SaveShowProgress(long showId, int season, int? episode);

        /// <summary>
        /// Unmark the show (all seasons, all episodes not seen)
        /// (https://api.tvshowtime.com/doc#delete_show_progress)
        /// </summary>
        /// <param name="showId">The TVDB ID of the show (http://thetvdb.com)</param>
        /// <returns></returns>
        IObservable<Response> UnmarkShowWatched(long showId);

        /// <summary>
        /// Delete the progress for a show 
        /// (https://api.tvshowtime.com/doc#delete_show_progress)
        /// </summary>
        /// <param name="showId">The TVDB ID of the show (http://thetvdb.com)</param>
        /// <param name="season">The season number</param>
        /// <param name="episode">The episode number (if not set, unmark the whole season seen)</param>
        /// <returns></returns>
        IObservable<Response> DeleteShowProgress(long showId, int season, int? episode);

        #endregion

        #region Episode

        /// <summary>
        /// Returns episode data
        /// (https://api.tvshowtime.com/doc#episode)
        /// </summary>
        /// <param name="episodeRequest">Episode request parameters</param>
        /// <returns></returns>
        IObservable<EpisodeResponse> GetEpisode(EpisodeRequest episodeRequest);

        /// <summary>
        /// Mark an episode as watched 
        /// (https://api.tvshowtime.com/doc#checkin)
        /// </summary>
        /// <param name="episodeRequest">Episode request parameters</param>
        /// <param name="publishOnFacebook">Publish on Facebook</param>
        /// <param name="publishOnTwitter">Publish on Twitter</param>
        /// <param name="autoFollow">Auto-follow the show if not already followed</param>
        /// <returns></returns>
        IObservable<Response> MarkEpisodeWatched(EpisodeRequest episodeRequest, bool publishOnFacebook, bool publishOnTwitter, bool autoFollow = true);

        /// <summary>
        /// Returns a value indicating if the episode is watched or not by the user 
        /// (https://api.tvshowtime.com/doc#is_watched)
        /// </summary>
        /// <param name="episodeRequest">Episode request parameters</param>
        /// <returns></returns>
        IObservable<BooleanResponse> GetIsEpisodeWatched(EpisodeRequest episodeRequest);

        /// <summary>
        /// Unmark an episode as watched 
        /// (https://api.tvshowtime.com/doc#checkout)
        /// </summary>
        /// <param name="episodeRequest">Episode request parameters</param>
        /// <returns></returns>
        IObservable<Response> UnmarkEpisodeWatched(EpisodeRequest episodeRequest);

        /// <summary>
        /// Returns the progress for an episode
        /// (https://api.tvshowtime.com/doc#retrieve_progress)
        /// </summary>
        /// <param name="episodeRequest">Episode request parameters</param>
        /// <returns></returns>
        IObservable<EpisodeProgressResponse> GetEpisodeProgress(EpisodeRequest episodeRequest);

        /// <summary>
        /// Set the progress for an episode
        /// (https://api.tvshowtime.com/doc#save_progress)
        /// </summary>
        /// <param name="episodeRequest">Episode request parameters</param>
        /// <returns></returns>
        IObservable<EpisodeProgressResponse> SaveEpisodeProgress(EpisodeRequest episodeRequest);

        /// <summary>
        /// Set the emotion for an episode 
        /// (https://api.tvshowtime.com/doc#set_emotion)
        /// </summary>
        /// <param name="episodeId">The TVDB ID of the episode (http://thetvdb.com)</param>
        /// <param name="emotion">The emotion the user felt</param>
        /// <returns></returns>
        IObservable<EmotionResponse> SetEmotionForEpisode(long episodeId, Emotion emotion);

        /// <summary>
        /// Delete the emotion for an episode
        /// (https://api.tvshowtime.com/doc#delete_emotion)
        /// </summary>
        /// <param name="episodeId">The TVDB ID of the episode (http://thetvdb.com)</param>
        /// <returns></returns>
        IObservable<Response> DeleteEmotionForEpisode(long episodeId);

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;
using TVShowTimeApi.Model;
using TVShowTimeApi.Model.Requests;
using TVShowTimeApi.Model.Responses;

namespace TVShowTimeApi.Services
{
    public class ReactiveTVShowTimeApiService : IReactiveTVShowTimeApiService
    {
        #region Fields

        private ITVShowTimeApiService _apiService = new TVShowTimeApiService();

        #endregion

        #region Properties

        public string Token
        {
            get { return _apiService.Token; }
            set { _apiService.Token = value; }
        }

        #endregion

        #region Constructors

        public ReactiveTVShowTimeApiService() { }

        public ReactiveTVShowTimeApiService(string token)
        {
            Token = token;
        }

        #endregion

        #region Authentication

        public IObservable<bool?> Login(string oauthKey, string oauthSecret)
        {
            return _apiService.LoginAsync(oauthKey, oauthSecret).ToObservable();
        }

        #endregion

        #region User

        public IObservable<UserResponse> GetCurrentUser()
        {
            return _apiService.GetCurrentUserAsync().ToObservable();
        }

        public IObservable<WatchlistResponse> GetWatchlist(int page = 0, int limit = 10, string languageCode = null)
        {
            return _apiService.GetWatchlistAsync(page, limit, languageCode).ToObservable();
        }

        public IObservable<AgendaResponse> GetAgenda(int page = 0, int limit = 10, bool includeWatchedEpisodes = false)
        {
            return _apiService.GetAgendaAsync(page, limit, includeWatchedEpisodes).ToObservable();
        }

        public IObservable<LibraryResponse> GetLibrary(int page = 0, int limit = 10)
        {
            return _apiService.GetLibraryAsync(page, limit).ToObservable();
        }

        #endregion

        #region Show

        public IObservable<ExploreResponse> GetTrendingShows(int page = 0, int limit = 10)
        {
            return _apiService.GetTrendingShowsAsync(page, limit).ToObservable();
        }

        public IObservable<ShowResponse> GetShow(string showId, string showName, bool includeEpisodes, bool exactMatchName = false)
        {
            return _apiService.GetShowAsync(showId, showName, includeEpisodes, exactMatchName).ToObservable();
        }

        public IObservable<Response> FollowShow(string showId)
        {
            return _apiService.FollowShowAsync(showId).ToObservable();
        }

        public IObservable<BooleanResponse> GetIsShowFollowed(string showId)
        {
            return _apiService.GetIsShowFollowedAsync(showId).ToObservable();
        }

        public IObservable<Response> UnfollowShow(string showId)
        {
            return _apiService.UnfollowShowAsync(showId).ToObservable();
        }

        public IObservable<Response> ArchiveShow(string showId)
        {
            return _apiService.ArchiveShowAsync(showId).ToObservable();
        }

        public IObservable<BooleanResponse> GetIsShowArchived(string showId)
        {
            return _apiService.GetIsShowArchivedAsync(showId).ToObservable();
        }

        public IObservable<Response> UnarchiveShow(string showId)
        {
            return _apiService.UnarchiveShowAsync(showId).ToObservable();
        }

        public IObservable<Response> MarkShowWatched(string showId)
        {
            return _apiService.MarkShowWatchedAsync(showId).ToObservable();
        }

        public IObservable<Response> SaveShowProgress(string showId, int season, int? episode)
        {
            return _apiService.SaveShowProgressAsync(showId, season, episode).ToObservable();
        }

        public IObservable<Response> UnmarkShowWatched(string showId)
        {
            return _apiService.UnmarkShowWatchedAsync(showId).ToObservable();
        }

        public IObservable<Response> DeleteShowProgress(string showId, int season, int? episode)
        {
            return _apiService.DeleteShowProgressAsync(showId, season, episode).ToObservable();
        }

        #endregion

        #region Episode

        public IObservable<EpisodeResponse> GetEpisode(EpisodeRequest episodeRequest)
        {
            return _apiService.GetEpisodeAsync(episodeRequest).ToObservable();
        }

        public IObservable<Response> MarkEpisodeWatched(EpisodeRequest episodeRequest, bool publishOnFacebook, bool publishOnTwitter, bool autoFollow = true)
        {
            return _apiService.MarkEpisodeWatchedAsync(episodeRequest, publishOnFacebook, publishOnTwitter, autoFollow).ToObservable();
        }

        public IObservable<BooleanResponse> GetIsEpisodeWatched(EpisodeRequest episodeRequest)
        {
            return _apiService.GetIsEpisodeWatchedAsync(episodeRequest).ToObservable();
        }

        public IObservable<Response> UnmarkEpisodeWatched(EpisodeRequest episodeRequest)
        {
            return _apiService.UnmarkEpisodeWatchedAsync(episodeRequest).ToObservable();
        }

        public IObservable<EpisodeProgressResponse> GetEpisodeProgress(EpisodeRequest episodeRequest)
        {
            return _apiService.GetEpisodeProgressAsync(episodeRequest).ToObservable();
        }

        public IObservable<EpisodeProgressResponse> SaveEpisodeProgress(EpisodeRequest episodeRequest)
        {
            return _apiService.SaveEpisodeProgressAsync(episodeRequest).ToObservable();
        }

        public IObservable<EmotionResponse> SetEmotionForEpisode(int episodeId, Emotion emotion)
        {
            return _apiService.SetEmotionForEpisodeAsync(episodeId, emotion).ToObservable();
        }

        public IObservable<Response> DeleteEmotionForEpisode(int episodeId)
        {
            return _apiService.DeleteEmotionForEpisodeAsync(episodeId).ToObservable();
        }

        #endregion
    }
}

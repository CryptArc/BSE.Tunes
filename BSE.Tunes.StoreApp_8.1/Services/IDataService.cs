﻿using BSE.Tunes.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BSE.Tunes.StoreApp.Services
{
    public interface IDataService
    {
        /// <summary>
        /// Gets the url that contains the web api service.
        /// </summary>
        string ServiceUrl { get; }
        string UserName { get; }
        Task<bool> IsHostAccessible();
        Task<HttpClient> GetHttpClient(bool withRefreshToken = true);
        Task<ObservableCollection<Genre>> GetGenres();
        Task<ObservableCollection<Album>> GetAlbums(Query query);
        Task<ObservableCollection<Album>> GetNewestAlbums(int limit);
        Task<int> GetNumberOfPlayableAlbums();
		Task<Album> GetAlbumById(int albumId);
        Task<Track> GetTrackById(int trackId);
        Task<SearchResult> GetSearchResults(Query query);
        Task<ObservableCollection<Album>> GetAlbumSearchResults(Query query);
        Task<ObservableCollection<Track>> GetTrackSearchResults(Query query);
        Task<ObservableCollection<Guid>> GetPlaylistImageIdsById(int playlistId, string userName, int limit);
		Task<Playlist> GetPlaylistById(int playlistId, string userName);
        Task<ObservableCollection<Playlist>> GetPlaylistsByUserName(string userName);
        Task<ObservableCollection<Playlist>> GetPlaylistsByUserName(string userName, int limit);
        Task<Playlist> GetPlaylistByIdWithNumberOfEntries(int playlistId, string userName);
		Task<ObservableCollection<int>> GetTrackIdsByFilters(Filter filter);
        Task<ObservableCollection<Track>> GetTracksByFilters(Filter filter);
        Task<Playlist> InsertPlaylist(Playlist playlist);
        Task<Playlist> AppendToPlaylist(Playlist playlist);
        Task<bool> UpdatePlaylistEntries(Playlist playlist);
        Task<bool> DeletePlaylists(ObservableCollection<Playlist> playlists);
        Task<bool> UpdateHistory(History history);
		Uri GetImage(Guid imageId, bool asThumbnail = false );
    }
}

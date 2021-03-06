﻿using BSE.Tunes.Data;
using BSE.Tunes.StoreApp.Interfaces;
using BSE.Tunes.StoreApp.Services;
using BSE.Tunes.StoreApp.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;

namespace BSE.Tunes.StoreApp.ViewModels
{
    public class PlaylistPageViewModel : ViewModelBase, INavigationAware
    {
        #region FieldsPrivate
        private IResourceService m_resourceService;
        private IDataService m_dataService;
        private IAccountService m_accountService;
        private INavigationService m_navigationService;
        private ObservableCollection<PlaylistViewModel> m_playlists;
        private ObservableCollection<PlaylistViewModel> m_selectedPlaylists;
        private ICommand m_selectCommand;
        private RelayCommand m_deleteSelectedPlaylistsCommand;
        private bool m_hasSelectedItems;
        private ICommand m_goBackCommand;
        #endregion

        #region Properties
        public ICommand GoBackCommand
        {
            get
            {
                return this.m_goBackCommand ??
                    (this.m_goBackCommand = new RelayCommand(this.m_navigationService.GoBack));
            }
        }
        public ObservableCollection<PlaylistViewModel> Playlists
        {
            get
            {
                return this.m_playlists ??
                    (this.m_playlists = new ObservableCollection<PlaylistViewModel>());
            }
        }
        public ObservableCollection<PlaylistViewModel> SelectedPlaylists
        {
            get
            {
                return this.m_selectedPlaylists;
            }
            set
            {
                this.m_selectedPlaylists = value;
                RaisePropertyChanged("SelectedPlaylists");
            }
        }
        public ICommand SelectCommand
        {
            get
            {
                return this.m_selectCommand ??
                    (this.m_selectCommand = new RelayCommand<PlaylistViewModel>(this.SelectItem));
            }
        }
        public RelayCommand DeleteSelectedPlaylistCommand
        {
            get
            {
                return this.m_deleteSelectedPlaylistsCommand ??
                    (this.m_deleteSelectedPlaylistsCommand = new RelayCommand(DeleteSelectedPlaylists, CanDeleteSelectedPlaylists));
            }
        }
        public bool HasSelectedItems
        {
            get
            {
                return this.m_hasSelectedItems;
            }
            set
            {
                this.m_hasSelectedItems = value;
                RaisePropertyChanged("HasSelectedItems");
            }
        }
        #endregion

        #region MethodsPublic
        public PlaylistPageViewModel(IDataService dataService, IAccountService accountService, INavigationService navigationService, IResourceService resourceService)
        {
            this.m_dataService = dataService;
            this.m_accountService = accountService;
            this.m_navigationService = navigationService;
            this.m_resourceService = resourceService;
        }
        public void OnNavigatedTo(object navigationParameter, NavigationMode navigationMode)
        {
            this.LoadData();
            this.SelectedPlaylists = new ObservableCollection<PlaylistViewModel>();
            this.SelectedPlaylists.CollectionChanged += OnSelectedPlaylistsCollectionChanged;
        }
        public void OnNavigatedFrom(bool suspending)
        {
            this.Playlists.Clear();
            if (this.SelectedPlaylists != null)
            {
                this.SelectedPlaylists.CollectionChanged -= OnSelectedPlaylistsCollectionChanged;
                this.SelectedPlaylists = null;
            }
        }
        #endregion

        #region MethodsPrivate
        private void LoadData()
        {
            TunesUser user = this.m_accountService.User;
            if (user != null && !string.IsNullOrEmpty(user.UserName))
            {
                var playlists = Task.Run(() =>
                {
                    try
                    {
                        return this.m_dataService.GetPlaylistsByUserName(user.UserName);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                });

                try
                {
                    this.Playlists.Clear();
                    playlists.Wait();
                    foreach (var playlist in playlists.Result)
                    {
                        if (playlist != null)
                        {
                            this.Playlists.Add(new PlaylistViewModel(this.m_dataService, this.m_accountService, this.m_resourceService, playlist.Id));
                        }
                    }
                }
                catch (Exception exception)
                {
                }
            }
        }
        private void SelectItem(PlaylistViewModel playlistViewModel)
        {
            if (playlistViewModel != null && playlistViewModel.Playlist != null)
            {
                this.m_navigationService.Navigate(typeof(PlaylistDetailPage), playlistViewModel.Playlist.Id);
            }
        }
        private void OnSelectedPlaylistsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.DeleteSelectedPlaylistCommand.RaiseCanExecuteChanged();
        }
        private bool CanDeleteSelectedPlaylists()
        {
            return this.HasSelectedItems = this.SelectedPlaylists != null && this.SelectedPlaylists.Count > 0;
        }
        private void DeleteSelectedPlaylists()
        {
            var selectedPlaylistViewModels = new System.Collections.ObjectModel.ObservableCollection<PlaylistViewModel>(this.SelectedPlaylists);
            if (selectedPlaylistViewModels != null)
            {
                try
                {
                    var list = this.SelectedPlaylists.ToList();
                    var playlistsToDelete = new System.Collections.ObjectModel.ObservableCollection<Playlist>(this.SelectedPlaylists.Select(p => p.Playlist));
                    if (playlistsToDelete != null)
                    {
                        Task<bool> task = Task.Run(() => this.m_dataService.DeletePlaylists(playlistsToDelete));
                        var hasDeleted = task.Result;
                        if (hasDeleted)
                        {
                            foreach (var playlistViewModel in list)
                            {
                                if (playlistViewModel != null)
                                {
                                    this.Playlists.Remove(playlistViewModel);
                                }
                            }
                            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<BSE.Tunes.StoreApp.Messaging.PlaylistChangeMessage>(
                                new BSE.Tunes.StoreApp.Messaging.PlaylistChangeMessage());
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
        #endregion
    }
}

﻿using BSE.Tunes.Data;
using BSE.Tunes.StoreApp.Mvvm;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BSE.Tunes.StoreApp.ViewModels
{
    public class SearchSuggestionsViewModel : ViewModelBase
    {
        private bool m_hasSuggestionChosen;
        private string m_queryText;
        private ObservableCollection<string> m_searchSuggestions;
        private ICommand m_suggestionChosenCommand;
        private ICommand m_querySubmittedCommand;
        private ICommand m_textChangedCommand;

        public ObservableCollection<String> SearchSuggestions => m_searchSuggestions ?? (m_searchSuggestions = new ObservableCollection<string>());
        public string QueryText
        {
            get
            {
                return m_queryText;
            }
            set
            {
                m_queryText = value;
                RaisePropertyChanged(() => QueryText);
            }
        }

        public ICommand QuerySubmittedCommand => m_querySubmittedCommand ?? (m_querySubmittedCommand = new RelayCommand<object>(async (queryText) =>
        {
            m_hasSuggestionChosen = false;
            if (!string.IsNullOrEmpty(QueryText) && QueryText.Length >= 3)
            {
                await NavigationService.NavigateAsync(typeof(Views.SearchResultPage), queryText);
            }
        }));

        public ICommand SuggestionChosenCommand => m_suggestionChosenCommand ?? (m_suggestionChosenCommand = new RelayCommand(() =>
        {
            m_hasSuggestionChosen = true;
        }));

        public ICommand TextChangedCommand => m_textChangedCommand ?? (m_textChangedCommand = new RelayCommand(async () =>
        {
            if (!string.IsNullOrEmpty(QueryText) && QueryText.Length > 3)
            {
                if (!m_hasSuggestionChosen)
                {
                    SearchSuggestions.Clear();

                    var suggestions = await DataService.GetSearchSuggestions(new Query
                    {
                        SearchPhrase = QueryText
                    });
                    foreach (var suggestion in suggestions)
                    {
                        if (!string.IsNullOrEmpty(suggestion))
                        {
                            SearchSuggestions.Add(suggestion);
                        }
                    }
                }
            }
        }));

        #region MethodsPublic
        public async void LoadSuggestions()
        {
            if (!string.IsNullOrEmpty(QueryText) && QueryText.Length >= 3)
            {
                if (!m_hasSuggestionChosen)
                {
                    SearchSuggestions.Clear();

                    var suggestions = await DataService.GetSearchSuggestions(new Query
                    {
                        SearchPhrase = QueryText
                    });
                    foreach (var suggestion in suggestions)
                    {
                        if (!string.IsNullOrEmpty(suggestion))
                        {
                            SearchSuggestions.Add(suggestion);
                        }
                    }
                }
            }
        }
        #endregion
    }
}

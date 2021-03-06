﻿using BSE.Tunes.StoreApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSE.Tunes.StoreApp.Mvvm;
using GalaSoft.MvvmLight.Command;
using BSE.Tunes.StoreApp.Models;
using System.Windows.Input;

namespace BSE.Tunes.StoreApp.ViewModels
{
    public class ServiceUrlSettingsPageViewModel : ViewModelBase
    {
        #region FieldsPrivate
        private SettingsService m_settingsService;
        private IDialogService m_dialogSService;
        private IAuthenticationService m_authenticationHandler;
        private ICommand m_removeUrlCommand;
        private string m_strServiceUrl;
        #endregion

        #region Properties
        public string ServiceUrl
        {
            get
            {
                return this.m_strServiceUrl;
            }
            set
            {
                this.m_strServiceUrl = value;
            }
        }
        #endregion

        #region MethodsPublic
        public ServiceUrlSettingsPageViewModel()
        {
            if (!Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                m_settingsService = SettingsService.Instance;
                m_dialogSService = DialogService.Instance;
                m_authenticationHandler = AuthenticationService.Instance;
                ServiceUrl = m_settingsService.ServiceUrl;
            }
        }
        public ICommand RemoveUrlCommand => m_removeUrlCommand ?? (m_removeUrlCommand = new RelayCommand(RemoveUrl));
        #endregion

        #region MethodsPrivate
        public async void RemoveUrl()
        {
            m_settingsService.ServiceUrl = null;
            m_settingsService.IsFullScreen = true;
            await NavigationService.NavigateAsync(typeof(Views.ServiceUrlWizzardPage));
            //var serviceUrl = ServiceUrl;
            //try
            //{
            //    if (!string.IsNullOrEmpty(serviceUrl))
            //    {
            //        UriBuilder uriBuilder = new UriBuilder(serviceUrl);
            //        serviceUrl = uriBuilder.Uri.AbsoluteUri;
            //    }

            //    await this.m_dataService.IsHostAccessible(serviceUrl);
            //    var oldServiceUrl = m_settingsService.ServiceUrl;
            //    if (serviceUrl.CompareTo(oldServiceUrl) != 0)
            //    {
            //        m_settingsService.ServiceUrl = serviceUrl;
            //        User user = await m_authenticationHandler.VerifyUserAuthenticationAsync();
            //        if (user == null)
            //        {
            //            m_settingsService.IsFullScreen = true;
            //            await NavigationService.NavigateAsync(typeof(Views.ServiceUrlWizzardPage));
            //        }
            //    }
            //}
            //catch (Exception)
            //{
            //    await m_dialogSService.ShowAsync(
            //        ResourceService.GetString("ServiceUrlNotAvailableExceptionMessage", "The address of your webserver was entered incorrectly or the webserver is not available."),
            //        ResourceService.GetString("ExceptionMessageDialogHeader", "Error"));
            //}
        }
        #endregion
    }
}

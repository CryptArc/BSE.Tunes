﻿using BSE.Tunes.Data;
using BSE.Tunes.StoreApp.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BSE.Tunes.StoreApp.ViewModels
{
    public class MenuPlaylistViewModel : MenuItemViewModel
    {
        #region Properties
        public Playlist Playlist
        {
            get;
            set;
        }
        #endregion
    }
}

﻿using BSE.Tunes.StoreApp.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSE.Tunes.StoreApp.ViewModels
{
    [DataTemplateName("Standard160x160ItemTemplate")]
    public class DataItemViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        #region FieldsPrivate
        private string m_title;
        private string m_subTitle;
        private string m_description;
		private Uri m_imageSource;
        private object m_data;
        #endregion

        #region Properties
        public string Title
        {
            get
            {
                return this.m_title;
            }
            set
            {
                this.m_title = value;
                RaisePropertyChanged("Title");
            }
        }
        public string Subtitle
        {
            get
            {
                return this.m_subTitle;
            }
            set
            {
                this.m_subTitle = value;
                RaisePropertyChanged("Subtitle");
            }
        }
        public string Description
        {
            get
            {
                return this.m_description;
            }
            set
            {
                this.m_description = value;
                RaisePropertyChanged("Description");
            }
        }
		public Uri ImageSource
		{
			get
			{
				return this.m_imageSource;
			}
			set
			{
				this.m_imageSource = value;
				RaisePropertyChanged("ImageSource");
			}
		}
        public object Data
        {
            get
            {
                return this.m_data;
            }
            set
            {
                this.m_data = value;
                RaisePropertyChanged("Data");
            }
        }
        #endregion
    }
}

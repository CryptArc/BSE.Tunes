﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace BSE.Tunes.StoreApp.Converter
{
    /// <summary>
    /// Value converter that translates true to <see cref="Visibility.Visible"/> and false to
    /// <see cref="Visibility.Collapsed"/>.
    /// </summary>
    public sealed class StringToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            //return (value is bool && (bool)value) ? Visibility.Visible : Visibility.Collapsed;
            return (string.IsNullOrEmpty((string)value)) ? Visibility.Collapsed : Visibility.Visible; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value is Visibility && (Visibility)value == Visibility.Visible;
        }
    }
}

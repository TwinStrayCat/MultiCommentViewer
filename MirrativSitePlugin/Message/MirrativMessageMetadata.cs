﻿using SitePlugin;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace MirrativSitePlugin
{
    internal class MirrativMessageMetadata : IMessageMetadata
    {
        private readonly IMirrativMessage _message;
        private readonly ICommentOptions _options;
        private readonly IMirrativSiteOptions _siteOptions;
        private readonly bool _isFirstComment;

        public Color BackColor
        {
            get
            {
                //if (_message is IMirrativSuperchat item)
                //{
                //    return _siteOptions.PaidCommentBackColor;
                //}
                //else
                //{
                return _options.BackColor;
                //}
            }
        }

        public Color ForeColor
        {
            get
            {
                //if (_message is IMirrativSuperchat item)
                //{
                //    return _siteOptions.PaidCommentForeColor;
                //}
                //else
                //{
                return _options.ForeColor;
                //}
            }
        }

        public FontFamily FontFamily
        {
            get
            {
                if (_isFirstComment)
                {
                    return _options.FirstCommentFontFamily;
                }
                else
                {
                    return _options.FontFamily;
                }
            }
        }

        public int FontSize
        {
            get
            {
                if (_isFirstComment)
                {
                    return _options.FirstCommentFontSize;
                }
                else
                {
                    return _options.FontSize;
                }
            }
        }

        public FontWeight FontWeight
        {
            get
            {
                if (_isFirstComment)
                {
                    return _options.FirstCommentFontWeight;
                }
                else
                {
                    return _options.FontWeight;
                }
            }
        }

        public FontStyle FontStyle
        {
            get
            {
                if (_isFirstComment)
                {
                    return _options.FirstCommentFontStyle;
                }
                else
                {
                    return _options.FontStyle;
                }
            }
        }

        public bool IsNgUser { get; }
        public bool IsSiteNgUser { get; }
        public bool IsFirstComment { get; }
        public string SiteName { get; }
        public bool Is184 { get; }
        public IUser User { get; }
        public ICommentProvider CommentProvider { get; }
        public bool IsVisible
        {
            get
            {
                if (IsNgUser || IsSiteNgUser) return false;

                //TODO:ConnectedとかDisconnectedの場合、表示するエラーレベルがError以下の場合にfalseにしたい
                return true;
            }
        }
        public bool IsInitialComment { get; set; }
        public bool IsNameWrapping => _options.IsUserNameWrapping;

        public MirrativMessageMetadata(IMirrativMessage message, ICommentOptions options, IMirrativSiteOptions siteOptions, IUser user, ICommentProvider cp, bool isFirstComment)
        {
            _options = options;
            _siteOptions = siteOptions;
            User = user;
            CommentProvider = cp;
            _isFirstComment = isFirstComment;

            options.PropertyChanged += Options_PropertyChanged;
            siteOptions.PropertyChanged += SiteOptions_PropertyChanged;
        }

        private void SiteOptions_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(_siteOptions.NeedAutoSubNickname):
                    RaisePropertyChanged(nameof(IsNameWrapping));
                    break;
            }
        }

        private void Options_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(_options.BackColor):
                    RaisePropertyChanged(nameof(BackColor));
                    break;
                case nameof(_options.ForeColor):
                    RaisePropertyChanged(nameof(ForeColor));
                    break;
                case nameof(_options.FontFamily):
                    RaisePropertyChanged(nameof(FontFamily));
                    break;
                case nameof(_options.FontStyle):
                    RaisePropertyChanged(nameof(FontStyle));
                    break;
                case nameof(_options.FontWeight):
                    RaisePropertyChanged(nameof(FontWeight));
                    break;
                case nameof(_options.FontSize):
                    RaisePropertyChanged(nameof(FontSize));
                    break;
                case nameof(_options.FirstCommentFontFamily):
                    RaisePropertyChanged(nameof(FontFamily));
                    break;
                case nameof(_options.FirstCommentFontStyle):
                    RaisePropertyChanged(nameof(FontStyle));
                    break;
                case nameof(_options.FirstCommentFontWeight):
                    RaisePropertyChanged(nameof(FontWeight));
                    break;
                case nameof(_options.FirstCommentFontSize):
                    RaisePropertyChanged(nameof(FontSize));
                    break;
                case nameof(_options.IsUserNameWrapping):
                    RaisePropertyChanged(nameof(IsNameWrapping));
                    break;
            }
        }
        #region INotifyPropertyChanged
        [NonSerialized]
        private System.ComponentModel.PropertyChangedEventHandler _propertyChanged;
        /// <summary>
        /// 
        /// </summary>
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged
        {
            add { _propertyChanged += value; }
            remove { _propertyChanged -= value; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            _propertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}

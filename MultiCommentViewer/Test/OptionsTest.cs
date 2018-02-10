﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using SitePlugin;
using System.Runtime.Serialization;
using System.Xml.Serialization;
namespace MultiCommentViewer.Test
{
    [DataContract]
    public class OptionsTest : IOptions
    {
        public string PluginDir
        {
            get
            {
                var currentDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                return System.IO.Path.Combine(currentDir, "Plugins");
            }
        }
        [DataMember]
        private string _fontFamilyStr;
        public FontFamily FontFamily
        {
            get { return FontFamilyFromString(_fontFamilyStr); }
            set
            { 
                var val = FontFamilyToString(value);
                if (val == _fontFamilyStr)
                    return;
                _fontFamilyStr = val;
                RaisePropertyChanged();
            }
        }
        [DataMember]
        private string _fontStyleStr;
        public FontStyle FontStyle
        {
            get { return FontStyleFromString(_fontStyleStr); }
            set
            {
                var val = FontStyleToString(value);
                if (val == _fontStyleStr)
                    return;
                _fontStyleStr = val;
                RaisePropertyChanged();
            }
        }
        [DataMember]
        private string _fontWeight;
        public FontWeight FontWeight
        {
            get { return FontWeightFromString(_fontWeight); }
            set
            {
                var val = FontWeightToString(value);
                if (val == _fontWeight)
                    return;
                _fontWeight = val;
                RaisePropertyChanged();
            }
        }
        [DataMember]
        private int _fontSize;
        public int FontSize
        {
            get { return _fontSize; }
            set
            {
                if (_fontSize == value)
                    return;
                _fontSize = value;
                RaisePropertyChanged();
            }
        }

        [DataMember]
        private string _firstCommentFontFamilyStr;
        public FontFamily FirstCommentFontFamily
        {
            get { return FontFamilyFromString(_firstCommentFontFamilyStr); }
            set
            {
                var val = FontFamilyToString(value);
                if (val == _firstCommentFontFamilyStr)
                    return;
                _firstCommentFontFamilyStr = val;
                RaisePropertyChanged();
            }
        }
        [DataMember]
        private string _firstCommentFontStyleStr;
        public FontStyle FirstCommentFontStyle
        {
            get { return FontStyleFromString(_firstCommentFontStyleStr); }
            set
            {
                var val = FontStyleToString(value);
                if (val == _firstCommentFontStyleStr)
                    return;
                _firstCommentFontStyleStr = val;
                RaisePropertyChanged();
            }
        }
        [DataMember]
        private string _firstCommentFontWeight;
        public FontWeight FirstCommentFontWeight
        {
            get { return FontWeightFromString(_firstCommentFontWeight); }
            set
            {
                var val = FontWeightToString(value);
                if (val == _firstCommentFontWeight)
                    return;
                _firstCommentFontWeight = val;
                RaisePropertyChanged();
            }
        }
        [DataMember]
        private int _firstCommentFontSize;
        public int FirstCommentFontSize
        {
            get { return _firstCommentFontSize; }
            set
            {
                if (_firstCommentFontSize == value)
                    return;
                _firstCommentFontSize = value;
                RaisePropertyChanged();
            }
        }
        /// <summary>
        /// 各種設定ファイルが置かれるフォルダ
        /// </summary>
        [DataMember]
        public string SettingsDirPath { get; set; } = "settings";

        #region Info
        [DataMember]
        private string _InfoForeColor = "#FF000000";
        /// <summary>
        /// 行グリッド線の色
        /// </summary>
        public Color InfoForeColor
        {
            get { return ColorFromArgb(_InfoForeColor); }
            set
            {
                var str = ColorToArgb(value);
                if (_InfoForeColor == str)
                    return;
                _InfoForeColor = str;
                RaisePropertyChanged();
            }
        }
        [DataMember]
        private string _InfoBackColor = "#FFFFFF00";
        /// <summary>
        /// 行グリッド線の色
        /// </summary>
        public Color InfoBackColor
        {
            get { return ColorFromArgb(_InfoBackColor); }
            set
            {
                var str = ColorToArgb(value);
                if (_InfoBackColor == str)
                    return;
                _InfoBackColor = str;
                RaisePropertyChanged();
            }
        }
        #endregion //Info




        private const double Default_MainViewHeight = 532;
        [DataMember]
        public double MainViewHeight { get; set; }

        private const double Default_MainViewWidth = 773;
        [DataMember]
        public double MainViewWidth { get; set; }

        private const double DEFAULT_MainViewLeft = 0;
        [DataMember]
        public double MainViewLeft { get; set; }

        private const double DEFAULT_MainViewTop = 0;
        [DataMember]
        public double MainViewTop { get; set; }

        private const double Default_ConnectionNameWidth = 100;
        [DataMember]
        public double ConnectionNameWidth { get; set; }

        private const double Default_ThumbnailWidth = 100;
        [DataMember]
        public double ThumbnailWidth { get; set; }

        private const double Default_CommentIdWidth = 100;
        [DataMember]
        public double CommentIdWidth { get; set; }

        private const double Default_UsernameWidth = 100;
        [DataMember]
        public double UsernameWidth { get; set; }

        private const double Default_MessageWidth = 100;
        [DataMember]
        public double MessageWidth { get; set; }

        private const double Default_InfoWidth = 100;
        [DataMember]
        public double InfoWidth { get; set; }

        [DataMember]
        bool _IsShowConnectionName;
        public bool IsShowConnectionName
        {
            get { return _IsShowConnectionName; }
            set
            {
                _IsShowConnectionName = value;
                RaisePropertyChanged();
            }
        }
        [DataMember]
        public int ConnectionNameDisplayIndex { get; set; } = 0;
        [DataMember]
        public int ThumbnailDisplayIndex { get; set; } = 1;
        [DataMember]
        private bool _IsShowThumbnail;
        public bool IsShowThumbnail
        {
            get { return _IsShowThumbnail; }
            set
            {
                _IsShowThumbnail = value;
                RaisePropertyChanged();
            }
        }
        [DataMember]
        public int CommentIdDisplayIndex { get; set; } = 2;
        [DataMember]
        private bool _IsShowCommentId;
        public bool IsShowCommentId
        {
            get { return _IsShowCommentId;}
            set
            {
                _IsShowCommentId = value;
                RaisePropertyChanged();
            }
        }
        [DataMember]
        private bool _IsShowUsername;
        public bool IsShowUsername
        {
            get { return _IsShowUsername; }
            set
            {
                _IsShowUsername = value;
                RaisePropertyChanged();
            }
        }
        [DataMember]
        public int UsernameDisplayIndex { get; set; } = 3;
        [DataMember]
        private bool _IsShowMessage;
        public bool IsShowMessage
        {
            get { return _IsShowMessage; }
            set
            {
                _IsShowMessage = value;
                RaisePropertyChanged();
            }
        }
        [DataMember]
        public int MessageDisplayIndex { get; set; } = 4;
        [DataMember]
        private bool _IsShowInfo;
        public bool IsShowInfo
        {
            get { return _IsShowInfo; }
            set
            {
                _IsShowInfo = value;
                RaisePropertyChanged();
            }
        }
        [DataMember]
        public int InfoDisplayIndex { get; set; } = 5;
        [DataMember]
        private string _horizontalGridLineColor ="#FF000000";
        /// <summary>
        /// 行グリッド線の色
        /// </summary>
        public Color HorizontalGridLineColor
        {
            get { return ColorFromArgb(_horizontalGridLineColor); }
            set
            {
                _horizontalGridLineColor = ColorToArgb(value);
                RaisePropertyChanged();
            }
        }
        [DataMember]
        private string _SelectedRowBackColor;
        public Color SelectedRowBackColor
        {
            get { return ColorFromArgb(_SelectedRowBackColor); }
            set
            {
                _SelectedRowBackColor = ColorToArgb(value);
                RaisePropertyChanged();
            }
        }
        [DataMember]
        private string _SelectedRowForeColor;
        public Color SelectedRowForeColor
        {
            get { return ColorFromArgb(_SelectedRowForeColor); }
            set
            {
                _SelectedRowForeColor = ColorToArgb(value);
                RaisePropertyChanged();
            }
        }
        //以下のようなことをやりたいが共変性とか反変性的に無理そう。どうにかならないかなぁ。
        //var list =new List<Test<object>>
        //{
        //    new Test<Color>(nameof(ForeColor), Colors.Red, null),
        //    new Test<double>(nameof(MainViewWidth), Default_MainViewWidth, c=> c <=0),
        //};
        //public class Test<T>
        //{
        //    public void SetDefault()
        //    {
        //        var prop = this.GetType().GetProperty(_property);
        //        var current = (T)prop.GetValue(this);

        //        var b = _func == null ? true : _func(current);
        //        if (b)
        //        {
        //            prop.SetValue(this, _defaultVal);
        //        }

        //    }
        //    private readonly Func<T, bool> _func;
        //    private readonly string _property;
        //    private readonly T _defaultVal;
        //    public Test(string propertyName, T defaultVal, Func<T, bool> test)
        //    {
        //        _property = propertyName;
        //        _func = test;
        //        _defaultVal = defaultVal;
        //    }
        //}
        [DataMember]
        private string _verticalGridLineColor="#FF000000";
        /// <summary>
        /// 列グリッド線の色
        /// </summary>
        public Color VerticalGridLineColor
        {
            get { return ColorFromArgb(_verticalGridLineColor); }
            set
            {
                _verticalGridLineColor = ColorToArgb(value);
                RaisePropertyChanged();
            }
        }

        #region ForeColor
        [DataMember]
        private string _foreColorArgb = "#FF000000";
        public string ForeColorArgb
        {
            get { return _foreColorArgb; }
            set
            {
                _foreColorArgb = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(ForeColor));
            }
        }
        public Color ForeColor
        {
            get { return ColorFromArgb(ForeColorArgb); }
            set
            {
                ForeColorArgb = ColorToArgb(value);
                RaisePropertyChanged();
            }
        }
        #endregion

        #region BackColor
        [DataMember]
        private string _backColorArgb = "#FFFFFFFF";
        public string BackColorArgb
        {
            get { return _backColorArgb; }
            set
            {
                _backColorArgb = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(BackColor));
            }
        }
        public Color BackColor
        {
            get { return ColorFromArgb(BackColorArgb); }
            set
            {
                BackColorArgb = ColorToArgb(value);
                RaisePropertyChanged();
            }
        }


        #endregion

        public OptionsTest()
        {
            Init();
            Reset();
        }
        [OnDeserialized]
        private void OnDeserialized(StreamingContext ctx)
        {
            Init();
            CheckValidation();
        }
        /// <summary>
        /// constに出来ないDefaultValueを初期化する
        /// </summary>
        private void Init()
        {

        }
        private void CheckValidation()
        {
            if (MainViewWidth <= 0)
                MainViewWidth = Default_MainViewWidth;
            if (MainViewHeight <= 0)
                MainViewHeight = Default_MainViewHeight;
            if (ConnectionNameWidth <= 0)
                ConnectionNameWidth = Default_ConnectionNameWidth;
            if (ThumbnailWidth <= 0)
                ThumbnailWidth = Default_ThumbnailWidth;
            if (CommentIdWidth <= 0)
                CommentIdWidth = Default_CommentIdWidth;
            if (UsernameWidth <= 0)
                UsernameWidth = Default_UsernameWidth;
            if (MessageWidth <= 0)
                MessageWidth = Default_MessageWidth;
            if (InfoWidth <= 0)
                InfoWidth = Default_InfoWidth;
        }
        public void Reset()
        {
            SelectedRowBackColor = SystemColors.HighlightColor;
            SelectedRowForeColor = SystemColors.HighlightTextColor;
            IsShowConnectionName = true;
            IsShowCommentId = true;
            IsShowUsername = true;
            IsShowThumbnail = true;
            IsShowMessage = true;
            IsShowInfo = true;
        }
        public OptionsTest Clone()
        {
            return (OptionsTest)this.MemberwiseClone();
        }
        IOptions IOptions.Clone()
        {
            return this.Clone();
        }
        void IOptions.Set(IOptions options)
        {
            var properties = options.GetType().GetProperties();
            foreach (var property in properties)
            {
                if (property.SetMethod != null)
                {
                    property.SetValue(this, property.GetValue(options));
                }
            }
        }
        public void Set(OptionsTest changedOptions)
        {
            var properties = changedOptions.GetType().GetProperties();
            foreach (var property in properties)
            {
                if (property.SetMethod != null)
                {
                    property.SetValue(this, property.GetValue(changedOptions));
                }
            }
        }
        #region Converters
        private FontFamily FontFamilyFromString(string str)
        {
            return new FontFamily(str);
        }
        private string FontFamilyToString(FontFamily family)
        {
            return family.FamilyNames.Values.First();
        }
        private FontStyle FontStyleFromString(string str)
        {
            return (FontStyle)new FontStyleConverter().ConvertFromString(str);
        }
        private string FontStyleToString(FontStyle style)
        {
            return new FontStyleConverter().ConvertToString(style);
        }
        private FontWeight FontWeightFromString(string str)
        {
            return (FontWeight)new FontWeightConverter().ConvertFromString(str);
        }
        private string FontWeightToString(FontWeight weight)
        {
            return new FontWeightConverter().ConvertToString(weight);
        }
        private Color ColorFromArgb(string argb)
        {
            if (argb == null)
                throw new ArgumentNullException("argb");
            var pattern = "#(?<a>[0-9a-fA-F]{2})(?<r>[0-9a-fA-F]{2})(?<g>[0-9a-fA-F]{2})(?<b>[0-9a-fA-F]{2})";
            var match = System.Text.RegularExpressions.Regex.Match(argb, pattern, System.Text.RegularExpressions.RegexOptions.Compiled);

            if (!match.Success)
            {
                throw new ArgumentException("形式が不正");
            }
            else
            {
                var a = byte.Parse(match.Groups["a"].Value, System.Globalization.NumberStyles.HexNumber);
                var r = byte.Parse(match.Groups["r"].Value, System.Globalization.NumberStyles.HexNumber);
                var g = byte.Parse(match.Groups["g"].Value, System.Globalization.NumberStyles.HexNumber);
                var b = byte.Parse(match.Groups["b"].Value, System.Globalization.NumberStyles.HexNumber);
                return Color.FromArgb(a, r, g, b);
            }
        }
        private string ColorToArgb(Color color)
        {
            var argb = color.ToString();
            return argb;
        }
        #endregion
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

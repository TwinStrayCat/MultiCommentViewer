using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Common;
using MultiCommentViewer.Test;
using SitePlugin;
namespace MultiCommentViewer
{
    /// <summary>
    /// Interaction logic for MainOptionsPanel.xaml
    /// </summary>
    public partial class MainOptionsPanel : UserControl
    {
        public MainOptionsPanel()
        {
            InitializeComponent();
        }
        internal void SetViewModel(MainOptionsViewModel vm)
        {
            this.DataContext = vm;
        }
        internal MainOptionsViewModel GetViewModel()
        {
            return (MainOptionsViewModel)this.DataContext;
        }
    }
    public class FontFamilyViewModel
    {
        public string Text { get; private set; }
        public FontFamily FontFamily { get; private set; }

        public FontFamilyViewModel(FontFamily fontFamily, CultureInfo culture)
        {
            Text = ConvertFontFamilyToName(fontFamily, culture);
            FontFamily = fontFamily;
        }
        public override bool Equals(object obj)
        {
            var b = obj as FontFamilyViewModel;
            if (b == null)
                return false;
            return this.FontFamily.Equals(b.FontFamily);
        }
        public override int GetHashCode()
        {
            return FontFamily.GetHashCode();
        }
        public static string ConvertFontFamilyToName(FontFamily fontFamily, CultureInfo culture)
        {
            string text;
            var lang = XmlLanguage.GetLanguage(culture.IetfLanguageTag);
            if (fontFamily.FamilyNames.ContainsKey(lang))
            {
                text = fontFamily.FamilyNames[lang];
            }
            else
            {
                text = fontFamily.ToString();
            }
            return text;
        }
    }
    class MainOptionsViewModel:GalaSoft.MvvmLight.ViewModelBase
    {
        public ICommand SetDarkCommand { get; private set; }
        public ICommand SetDefaultCommand { get; private set; }

        public Color BackColor
        {
            get { return ChangedOptions.BackColor; }
            set { ChangedOptions.BackColor = value; }
        }
        public Color ForeColor
        {
            get { return ChangedOptions.ForeColor; }
            set { ChangedOptions.ForeColor = value; }
        }
        public Color NoticeCommentBackColor
        {
            get { return ChangedOptions.InfoBackColor; }
            set { ChangedOptions.InfoBackColor = value; }
        }
        public Color NoticeCommentForeColor
        {
            get { return ChangedOptions.InfoForeColor; }
            set { ChangedOptions.InfoForeColor = value; }
        }
        public Color SiteInfoBackColor
        {
            get { return ChangedOptions.BroadcastInfoBackColor; }
            set { ChangedOptions.BroadcastInfoBackColor = value; }
        }
        public Color SiteInfoForeColor
        {
            get { return ChangedOptions.BroadcastInfoForeColor; }
            set { ChangedOptions.BroadcastInfoForeColor = value; }
        }
        public Color SelectedRowBackColor
        {
            get { return ChangedOptions.SelectedRowBackColor; }
            set { ChangedOptions.SelectedRowBackColor = value; }
        }
        public Color SelectedRowForeColor
        {
            get { return ChangedOptions.SelectedRowForeColor; }
            set { ChangedOptions.SelectedRowForeColor = value; }
        }
        public Color FirstCommentBackColor
        {
            get { return ChangedOptions.FirstCommentBackColor; }
            set { ChangedOptions.FirstCommentBackColor = value; }
        }
        public Color FirstCommentForeColor
        {
            get { return ChangedOptions.FirstCommentForeColor; }
            set { ChangedOptions.FirstCommentForeColor = value; }
        }
        public Color VerticalGridLineColor
        {
            get { return ChangedOptions.VerticalGridLineColor; }
            set { ChangedOptions.VerticalGridLineColor = value; }
        }
        public Color HorizontalGridLineColor
        {
            get { return ChangedOptions.HorizontalGridLineColor; }
            set { ChangedOptions.HorizontalGridLineColor = value; }
        }
        public bool IsUserNameWrapping
        {
            get { return ChangedOptions.IsUserNameWrapping; }
            set { ChangedOptions.IsUserNameWrapping = value; }
        }
        public bool IsAddingNewCommentTop
        {
            get { return ChangedOptions.IsAddingNewCommentTop; }
            set { ChangedOptions.IsAddingNewCommentTop = value; }
        }
        public bool IsAutoCheckIfUpdateExists
        {
            get { return ChangedOptions.IsAutoCheckIfUpdateExists; }
            set { ChangedOptions.IsAutoCheckIfUpdateExists = value; }
        }
        public FontFamilyViewModel FontFamily
        {
            get { return new FontFamilyViewModel(ChangedOptions.FontFamily, CultureInfo.CurrentCulture); }
            set { ChangedOptions.FontFamily = value.FontFamily; }
        }
        public int FontSize
        {
            get { return ChangedOptions.FontSize; }
            set { ChangedOptions.FontSize = value; }
        }
        public bool IsBold
        {
            get
            {
                return ChangedOptions.FontWeight == FontWeights.Bold;
            }
            set
            {
                var b = value;
                if (b)
                {
                    ChangedOptions.FontWeight = FontWeights.Bold;
                }
                else
                {
                    ChangedOptions.FontWeight = FontWeights.Normal;
                }
            }
        }
        public FontFamilyViewModel FirstCommentFontFamily
        {
            get { return new FontFamilyViewModel(ChangedOptions.FirstCommentFontFamily, CultureInfo.CurrentCulture); }
            set { ChangedOptions.FirstCommentFontFamily = value.FontFamily; }
        }
        public int FirstCommentFontSize
        {
            get { return ChangedOptions.FirstCommentFontSize; }
            set { ChangedOptions.FirstCommentFontSize = value; }
        }
        public bool IsFirstCommentBold
        {
            get
            {
                return ChangedOptions.FirstCommentFontWeight == FontWeights.Bold;
            }
            set
            {
                var b = value;
                if (b)
                {
                    ChangedOptions.FirstCommentFontWeight = FontWeights.Bold;
                }
                else
                {
                    ChangedOptions.FirstCommentFontWeight = FontWeights.Normal;
                }
            }
        }
        public bool IsPixelScrolling
        {
            get { return ChangedOptions.IsPixelScrolling; }
            set { ChangedOptions.IsPixelScrolling = value; }
        }

        public IOptions OriginOptions { get; private set; }
        public IOptions ChangedOptions { get; private set; }
        public ObservableCollection<FontFamilyViewModel> FontFamillyCollection { get; private set; }
        public ObservableCollection<int> FontSizeCollection { get; private set; }
        public ObservableCollection<InfoType> InfoTypeCollection { get; private set; }
        public InfoType SelectedInfoType
        {
            get => ChangedOptions.ShowingInfoLevel;
            set => ChangedOptions.ShowingInfoLevel = value;
        }
        public MainOptionsViewModel(IOptions options)
        {
            Init(options);
        }
        public Color SystemButtonForeground
        {
            get { return ChangedOptions.SystemButtonForeground; }
            set { ChangedOptions.SystemButtonForeground = value; }
        }
        public Color SystemButtonBackground
        {
            get { return ChangedOptions.SystemButtonBackground; }
            set { ChangedOptions.SystemButtonBackground = value; }
        }
        public Color SystemButtonBorderBrush
        {
            get { return ChangedOptions.SystemButtonBorderBrush; }
            set { ChangedOptions.SystemButtonBorderBrush = value; }
        }
        public Color SystemButtonMouseOverForeground
        {
            get { return ChangedOptions.SystemButtonMouseOverForeground; }
            set { ChangedOptions.SystemButtonMouseOverForeground = value; }
        }
        public Color SystemButtonMouseOverBackground
        {
            get { return ChangedOptions.SystemButtonMouseOverBackground; }
            set { ChangedOptions.SystemButtonMouseOverBackground = value; }
        }
        public Color SystemButtonMouseOverBorderBrush
        {
            get { return ChangedOptions.SystemButtonMouseOverBorderBrush; }
            set { ChangedOptions.SystemButtonMouseOverBorderBrush = value; }
        }
        public Color TitleBarBackground
        {
            get { return ChangedOptions.TitleBarBackground; }
            set { ChangedOptions.TitleBarBackground = value; }
        }
        public Color TitleForeground
        {
            get { return ChangedOptions.TitleForeground; }
            set { ChangedOptions.TitleForeground = value; }
        }
        public Color TitleBackground
        {
            get { return ChangedOptions.TitleBackground; }
            set { ChangedOptions.TitleBackground = value; }
        }
        public Color MenuForeground
        {
            get { return ChangedOptions.MenuForeground; }
            set { ChangedOptions.MenuForeground = value; }
        }
        public Color MenuBackground
        {
            get { return ChangedOptions.MenuBackground; }
            set { ChangedOptions.MenuBackground = value; }
        }
        public Color ClientAreaBackground
        {
            get { return ChangedOptions.ClientAreaBackground; }
            set { ChangedOptions.ClientAreaBackground = value; }
        }
        public Color ConnectionHeaderBackground
        {
            get { return ChangedOptions.ConnectionHeaderBackground; }
            set { ChangedOptions.ConnectionHeaderBackground = value; }
        }
        public Color ConnectionHeaderForeground
        {
            get { return ChangedOptions.ConnectionHeaderForeground; }
            set { ChangedOptions.ConnectionHeaderForeground = value; }
        }
        public Color ConnectionHeaderBorderBrush
        {
            get { return ChangedOptions.ConnectionHeaderBorderBrush; }
            set { ChangedOptions.ConnectionHeaderBorderBrush = value; }
        }
        public Color ConnectionBackground
        {
            get { return ChangedOptions.ConnectionBackground; }
            set { ChangedOptions.ConnectionBackground = value; }
        }
        public Color ConnectionForeground
        {
            get { return ChangedOptions.ConnectionForeground; }
            set { ChangedOptions.ConnectionForeground = value; }
        }

        public Color MetadataViewHeaderForeground
        {
            get { return ChangedOptions.MetadataViewHeaderForeground; }
            set { ChangedOptions.MetadataViewHeaderForeground = value; }
        }
        public Color MetadataViewHeaderBackground
        {
            get { return ChangedOptions.MetadataViewHeaderBackground; }
            set { ChangedOptions.MetadataViewHeaderBackground = value; }
        }
        public Color MetadataViewHeaderBorderBrush
        {
            get { return ChangedOptions.MetadataViewHeaderBorderBrush; }
            set { ChangedOptions.MetadataViewHeaderBorderBrush = value; }
        }
        public Color MetadataViewForeground
        {
            get { return ChangedOptions.MetadataViewForeground; }
            set { ChangedOptions.MetadataViewForeground = value; }
        }
        public Color MetadataViewBackground
        {
            get { return ChangedOptions.MetadataViewBackground; }
            set { ChangedOptions.MetadataViewBackground = value; }
        }
        public Color MetadataViewBorderBrush
        {
            get { return ChangedOptions.MetadataViewBorderBrush; }
            set { ChangedOptions.MetadataViewBorderBrush = value; }
        }
        public Color MetadataViewRowForeground
        {
            get { return ChangedOptions.MetadataViewRowForeground; }
            set { ChangedOptions.MetadataViewRowForeground = value; }
        }
        public Color MetadataViewRowBackground
        {
            get { return ChangedOptions.MetadataViewRowBackground; }
            set { ChangedOptions.MetadataViewRowBackground = value; }
        }
        public Color CommentViewHeaderForeground
        {
            get { return ChangedOptions.CommentViewHeaderForeground; }
            set { ChangedOptions.CommentViewHeaderForeground = value; }
        }
        public Color CommentViewHeaderBackground
        {
            get { return ChangedOptions.CommentViewHeaderBackground; }
            set { ChangedOptions.CommentViewHeaderBackground = value; }
        }
        public Color CommentViewHeaderBorderBrush
        {
            get { return ChangedOptions.CommentViewHeaderBorderBrush; }
            set { ChangedOptions.CommentViewHeaderBorderBrush = value; }
        }
        public Color CommentViewForeground
        {
            get { return ChangedOptions.CommentViewForeground; }
            set { ChangedOptions.CommentViewForeground = value; }
        }
        public Color CommentViewBackground
        {
            get { return ChangedOptions.CommentViewBackground; }
            set { ChangedOptions.CommentViewBackground = value; }
        }
        public Color CommentViewBorderBrush
        {
            get { return ChangedOptions.CommentViewBorderBrush; }
            set { ChangedOptions.CommentViewBorderBrush = value; }
        }
        public Color CommentViewRowForeground
        {
            get { return ChangedOptions.CommentViewRowForeground; }
            set { ChangedOptions.CommentViewRowForeground = value; }
        }
        public Color CommentViewRowBackground
        {
            get { return ChangedOptions.CommentViewRowBackground; }
            set { ChangedOptions.CommentViewRowBackground = value; }
        }

        private void Init(IOptions options)
        {
            OriginOptions = options;
            ChangedOptions = options.Clone() as IOptions;

            var fontList = Fonts.SystemFontFamilies.OrderBy(f => f.ToString()).Select(f => new FontFamilyViewModel(f, CultureInfo.CurrentCulture));
            FontFamillyCollection = new ObservableCollection<FontFamilyViewModel>(fontList);
            //FontFamily = new FontFamilyViewModel(new FontFamily("Meiryo"), CultureInfo.CurrentCulture);
            FontFamily = new FontFamilyViewModel(ChangedOptions.FontFamily, CultureInfo.CurrentCulture);
            FirstCommentFontFamily = new FontFamilyViewModel(ChangedOptions.FirstCommentFontFamily, CultureInfo.CurrentCulture);

            var sizeList = Enumerable.Range(6, 40);
            FontSizeCollection = new ObservableCollection<int>(sizeList);
            //FontSize = 10;

            InfoTypeCollection = new ObservableCollection<InfoType>(Enum.GetValues(typeof(InfoType)).Cast<InfoType>());
            SelectedInfoType = options.ShowingInfoLevel;

            SetDarkCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(() =>
            {
                var _myColor1 = new Color { A = 0xFF, R = 45, G = 45, B = 48 };
                var _myColor2 = new Color { A = 0xFF, R = 62, G = 62, B = 66 };
                SystemButtonForeground = Colors.White;
                SystemButtonBackground = _myColor1;
                SystemButtonBorderBrush = _myColor1;
                SystemButtonMouseOverBackground = _myColor1;
                SystemButtonMouseOverBorderBrush = Colors.White;
                SystemButtonMouseOverForeground = Colors.White;
                TitleBarBackground = _myColor1;
                TitleBackground = _myColor1;
                TitleForeground = Colors.White;
                MenuForeground = Colors.White;
                MenuBackground = _myColor1;
                ClientAreaBackground = _myColor1;
                ConnectionHeaderForeground = Colors.White;
                ConnectionHeaderBackground = _myColor1;
                ConnectionHeaderBorderBrush = Colors.White;
                ConnectionForeground = Colors.White;
                ConnectionBackground = _myColor2;

                MetadataViewHeaderForeground = Colors.White;
                MetadataViewHeaderBackground = _myColor1;
                MetadataViewHeaderBorderBrush = _myColor2;
                MetadataViewForeground = Colors.White;
                MetadataViewBackground = _myColor1;
                MetadataViewBorderBrush = Colors.White;
                MetadataViewRowForeground = Colors.White;
                MetadataViewRowBackground = _myColor2;
            });
            SetDefaultCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(() =>
            {
                var glay= new Color { A = 255, R = 240, G = 240, B = 240 };
                SystemButtonForeground = Colors.Black;
                SystemButtonBackground = Colors.White;
                SystemButtonBorderBrush = Colors.White;
                SystemButtonMouseOverBackground = new Color { A = 255, R = 229, G = 229, B = 229 };
                SystemButtonMouseOverBorderBrush = new Color { A = 255, R = 229, G = 229, B = 229 };
                SystemButtonMouseOverForeground = Colors.Black;
                TitleBarBackground = Colors.White;
                TitleBackground = Colors.White;
                TitleForeground = Colors.Black;
                MenuForeground = Colors.Black;
                MenuBackground = new Color { A = 255, R = 240, G = 240, B = 240 };
                ClientAreaBackground = Colors.White;
                ConnectionHeaderForeground = Colors.Black;
                ConnectionHeaderBackground = new Color { A = 255, R = 247, G = 248, B = 250 };
                ConnectionHeaderBorderBrush = Colors.Yellow;// Colors.Black;
                ConnectionForeground = Colors.Black;
                ConnectionBackground = Colors.White;

                MetadataViewHeaderForeground = Colors.Black;
                MetadataViewHeaderBackground = new Color { A = 255, R = 247, G = 248, B = 250 };
                MetadataViewHeaderBorderBrush = Colors.Black;
                MetadataViewForeground = Colors.Black;
                MetadataViewBackground = new Color { A = 255, R = 240, G = 240, B = 240 };
                MetadataViewBorderBrush = new Color { A = 255, R = 220, G = 220, B = 220 };
                MetadataViewRowForeground = Colors.Black;
                MetadataViewRowBackground = new Color { A = 255, R = 240, G = 240, B = 240 };

                CommentViewHeaderForeground = Colors.Black;
                CommentViewHeaderBackground = new Color { A = 255, R = 247, G = 248, B = 250 };
                CommentViewHeaderBorderBrush = Colors.Black;
                CommentViewForeground = Colors.Black;
                CommentViewBackground = new Color { A = 255, R = 240, G = 240, B = 240 };
                CommentViewBorderBrush = new Color { A = 255, R = 220, G = 220, B = 220 };
                CommentViewRowForeground = Colors.Black;
                CommentViewRowBackground = new Color { A = 255, R = 240, G = 240, B = 240 };
            });

        }
        public MainOptionsViewModel()
        {
            if (GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                var options = new DynamicOptionsTest
                {
                    ForeColor = Colors.Red,
                    BackColor = Colors.Black,
                    InfoBackColor = Colors.Yellow,
                    InfoForeColor = Colors.Black,
                    SelectedRowBackColor = Colors.Aqua,
                    SelectedRowForeColor = Colors.Pink,
                    VerticalGridLineColor = Colors.Green,
                    HorizontalGridLineColor = Colors.LightGray,
                    FontFamily = new FontFamily("Meiryo"),
                    FirstCommentFontFamily = new FontFamily("MS Gothic"),
                    FontSize = 16,
                    FirstCommentFontSize = 24,
                    IsPixelScrolling = false,
                };
                Init(options);
                IsBold = false;
                IsFirstCommentBold = true;
            }
            else
            {
                throw new NotSupportedException();
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

        public Task AfterCommentAdded()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
    class MainTabPage : IOptionsTabPage
    {
        public string HeaderText { get; }

        public UserControl TabPagePanel => _panel;

        public void Apply()
        {
            var optionsVm = _panel.GetViewModel();
            optionsVm.OriginOptions.Set(optionsVm.ChangedOptions);
        }

        public void Cancel()
        {
        }
        private readonly MainOptionsPanel _panel;
        public MainTabPage(string displayName, MainOptionsPanel panel)
        {
            HeaderText = displayName;
            _panel = panel;
        }
    }
    internal class FontFamilyToFontFamilyViewModelConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var fontFamily = value as FontFamily;
            return new FontFamilyViewModel(fontFamily, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var viewModel = value as FontFamilyViewModel;
            return viewModel.FontFamily;

        }
    }
}

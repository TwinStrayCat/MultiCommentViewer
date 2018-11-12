using GalaSoft.MvvmLight;
using SitePlugin;
using System.Windows.Media;
//TODO:過去コメントの取得


namespace MultiCommentViewer
{
    public class MetadataViewModel : ViewModelBase
    {
        #region Style
        private readonly Color _myColor1 = new Color { A = 0xFF, R = 45, G = 45, B = 48 };
        private readonly Color _myColor2 = new Color { A = 0xFF, R = 62, G = 62, B = 66 };
        public Brush MetadataViewRowBackground
        {
            get
            {
                return new SolidColorBrush(_myColor1);
            }
        }
        public Brush MetadataViewRowForeground
        {
            get
            {
                return new SolidColorBrush(Colors.White);
            }
        }
        #endregion //Style
        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }
        private string _elapsed;
        public string Elapsed
        {
            get { return _elapsed; }
            set
            {
                _elapsed = value;
                RaisePropertyChanged();
            }
        }
        private string _currentViewers;
        public string CurrentViewers
        {
            get { return _currentViewers; }
            set
            {
                _currentViewers = value;
                RaisePropertyChanged();
            }
        }
        private string _totalViewers;
        public string TotalViewers
        {
            get { return _totalViewers; }
            set
            {
                _totalViewers = value;
                RaisePropertyChanged();
            }
        }
        private string _active;
        public string Active
        {
            get { return _active; }
            set
            {
                _active = value;
                RaisePropertyChanged();
            }
        }
        private string _others;
        public string Others
        {
            get { return _others; }
            set
            {
                _others = value;
                RaisePropertyChanged();
            }
        }

        public string ConnectionName => _connectionName.Name;
        private readonly ConnectionName _connectionName;
        public MetadataViewModel(ConnectionName connectionName)
        {
            _connectionName = connectionName;
            Title = "-";
            Elapsed = "-";
            CurrentViewers = "-";
            TotalViewers = "-";
            Active = "-";
            Others = "-";
            _connectionName.PropertyChanged += (s, e) =>
            {
                switch (e.PropertyName)
                {
                    case nameof(_connectionName.Name):
                        base.RaisePropertyChanged(nameof(ConnectionName));
                        break;
                }
            };
        }
    }
}

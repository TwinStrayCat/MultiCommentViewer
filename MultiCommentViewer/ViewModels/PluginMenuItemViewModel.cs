﻿using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Plugin;
using System.Diagnostics;

namespace MultiCommentViewer
{
    public class PluginMenuItemPushedEventArgs : EventArgs
    {
        public string Name { get; }
        public PluginMenuItemPushedEventArgs(string name)
        {
            Name = name;
        }
    }
    public class PluginMenuItemViewModel2 : ViewModelBase
    {
        public event EventHandler<PluginMenuItemPushedEventArgs> Pushed;
        public ICommand PushCommand { get; }
        public string Name { get; set; }
        public PluginMenuItemViewModel2(string name)
        {
            Name = name;
            PushCommand = new RelayCommand(() =>
            {
                Pushed?.Invoke(this, new PluginMenuItemPushedEventArgs(Name));
            });
        }
    }
    public class PluginMenuItemViewModel:ViewModelBase
    {
        public string Name { get; set; }
        public ObservableCollection<PluginMenuItemViewModel> Children { get; } = new ObservableCollection<PluginMenuItemViewModel>();
        private RelayCommand _show;
        public ICommand ShowSettingViewCommand
        {
            //以前はコンストラクタ中でICommandに代入していたが、項目をクリックしてもTest()が呼ばれないことがあった。今の状態に書き換えたら問題なくなった。何故だ？IPluginを保持するようにしたから？GCで無くなっちゃってたとか？
            get
            {
                if(_show == null)
                {
                    _show = new RelayCommand(()=> Test(_plugin));
                }
                return _show;
            }
        }
        private readonly IPlugin _plugin;
        public PluginMenuItemViewModel(IPlugin plugin)// PluginContext plugin, string name, ICommand command)
        {
            Name = plugin.Name;
            _plugin = plugin;
        }
        private void Test(IPlugin plugin)
        {
            try
            {
                plugin.ShowSettingView();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}


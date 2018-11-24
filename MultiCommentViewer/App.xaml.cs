﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SitePlugin;
using System.IO;
using System.Reflection;
using System.Text;
using System.Net;
using System.Diagnostics;
using Common;
using System.Net.Http;
using CommentViewerCommon;

namespace MultiCommentViewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        ILogger _logger;
        Test.DynamicOptionsTest options;
        IIo io;
        private string GetOptionsPath()
        {
            var currentDir = Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName;
            return Path.Combine(currentDir, "settings", "options.txt");
        }
        private void SendErrorFile()
        {
            if (System.IO.File.Exists("error.txt"))
            {
                string errorContent;
                using (var sr = new System.IO.StreamReader("error.txt"))
                {
                    errorContent = sr.ReadToEnd();
                }
                SendErrorReport(errorContent, GetTitle(), GetVersion());
                System.IO.File.Delete("error.txt");
            }
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _logger = new Common.LoggerTest();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            
            io = new Test.IOTest();

            //OptionsはMainViewModelのContentRendered()で読み込みたい。しかし、その前にConnectionNameWidth等が参照されるため現状ではコンストラクタ以前に読み込む必要がある。
            //実行される順番は
            //ctor->ConnectionNameWidth->Activated->Loaded->ContentRendered
            //理想は、とりあえずViewを表示して、そこに"読み込み中です"みたいな表示を出している間に必要なものを読み込むこと。
            //しかし、それをやるにはViewの位置はデフォルト値になってしまう。それでも良いか。            
            //これ↓が一番いいかも
            //ここでOptionsのインスタンスを作成し、MainViewModelに渡す。とりあえずデフォルト値で初期化させ、ContentRenderedで保存されたOptionsを読み込み差し替える。
            options = new Test.DynamicOptionsTest();
            try
            {
                var s = io.ReadFile(GetOptionsPath());
                options.Deserialize(s);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
            }
            try
            {
                SendErrorFile();
            }
            catch { }
            ISitePluginLoader sitePluginLoader = new Test.SitePluginLoaderTest();
            IBrowserLoader browserLoader = new BrowserLoader(_logger);

            
            var mainViewModel = new MainViewModel(io, _logger, options, sitePluginLoader,browserLoader);
            var resource = Application.Current.Resources;
            var locator = resource["Locator"] as ViewModels.ViewModelLocator;
            locator.Main = mainViewModel;
        }
        protected override void OnExit(ExitEventArgs e)
        {
            try
            {
                var s = options.Serialize();
                io.WriteFile(GetOptionsPath(), s);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
            }
            try
            {
                var s = _logger.GetExceptions();
                SendErrorReport(s, GetTitle(), GetVersion());
            }
            catch(Exception ex)
            {
                //ここで例外が起きても送れない・・・。
                Debug.WriteLine(ex.Message);
            }
            base.OnExit(e);
        }
        private string GetTitle()
        {
            var asm = System.Reflection.Assembly.GetExecutingAssembly();
            var title = asm.GetName().Name;
            return title;
        }
        private string GetVersion()
        {
            var asm = System.Reflection.Assembly.GetExecutingAssembly();
            var ver = asm.GetName().Version;
            var s = $"v{ver.Major}.{ver.Minor}.{ver.Build}";
            return s;
        }
        private void SendErrorReport(string errorData, string title, string version)
        {
            if (string.IsNullOrEmpty(errorData))
            {
                return;
            }
            var fileStreamContent = new StreamContent(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(errorData)));
            using (var client = new HttpClient())
            using (var formData = new MultipartFormDataContent())
            {
                client.DefaultRequestHeaders.Add("User-Agent", $"{title} {version}");
                formData.Add(fileStreamContent, "error", title + "_" + version + "_" + "error.txt");
                var t = client.PostAsync("http://int-main.net/upload", formData);
                var response = t.Result;
                if (!response.IsSuccessStatusCode)
                {
                }
                else
                {
                }
            }
        }
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;

            try
            {
                _logger.LogException(ex, "UnhandledException");
                var s = _logger.GetExceptions();
                using (var sw = new System.IO.StreamWriter("error.txt", true))
                {
                    sw.WriteLine(s);
                }
            }
            catch { }
        }
    }
}

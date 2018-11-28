﻿using Common;
using SitePlugin;
using System.ComponentModel;

namespace MultiCommentViewer
{
    public interface IOptions : ICommentOptions, INotifyPropertyChanged
    {
        string PluginDir { get; }

        double MainViewHeight { get; set; }
        double MainViewWidth { get; set; }
        double MainViewLeft { get; set; }
        double MainViewTop { get; set; }

        double ConnectionViewHeight { get; set; }
        double MetadataViewHeight { get; set; }

        double ConnectionNameWidth { get; set; }
        bool IsShowConnectionName { get; set; }
        int ConnectionNameDisplayIndex { get; set; }

        double ThumbnailWidth { get; set; }
        int ThumbnailDisplayIndex { get; set; }
        bool IsShowThumbnail { get; set; }

        double CommentIdWidth { get; set; }
        int CommentIdDisplayIndex { get; set; }
        bool IsShowCommentId { get; set; }

        double UsernameWidth { get; set; }
        bool IsShowUsername { get; set; }
        int UsernameDisplayIndex { get; set; }

        double MessageWidth { get; set; }
        bool IsShowMessage { get; set; }
        int MessageDisplayIndex { get; set; }

        double PostTimeWidth { get; set; }
        bool IsShowPostTime { get; set; }
        int PostTimeDisplayIndex { get; set; }

        double InfoWidth { get; set; }
        bool IsShowInfo { get; set; }
        int InfoDisplayIndex { get; set; }
        bool IsAutoCheckIfUpdateExists { get; set; }
        bool IsAddingNewCommentTop { get; set; }

        bool IsTopmost { get; set; }
        bool IsPixelScrolling { get; set; }

        InfoType ShowingInfoLevel { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SitePlugin;
using Common;
using System.Windows.Threading;
using System.Windows.Controls;

namespace MultiCommentViewer.Test
{
    public class SitePluginLoaderTest : ISitePluginLoader
    {
        Dictionary<Guid, ISiteContext> _dict = new Dictionary<Guid, ISiteContext>();
        public void LoadSitePlugins(ICommentOptions options, ILogger logger)
        {
            var list = new List<ISiteContext>
            {
                new YouTubeLiveSitePlugin.Test2.YouTubeLiveSiteContext(options, new YouTubeLiveSitePlugin.Test2.YouTubeLiveServer(), logger),
                new OpenrecSitePlugin.OpenrecSiteContext(options, logger),
                new TwitchSitePlugin.TwitchSiteContext(options,new TwitchSitePlugin.TwitchServer(), logger),
                new NicoSitePlugin.NicoSiteContext(options,new NicoSitePlugin.DataSource(), (addr,port,size,buffer)=> new NicoSitePlugin.StreamSocket(addr,port,size,buffer), logger),
                new TwicasSitePlugin.TwicasSiteContext(options,logger),
                new LineLiveSitePlugin.LineLiveSiteContext(options,new LineLiveSitePlugin.LineLiveServer(), logger),
                new WhowatchSitePlugin.WhowatchSiteContext(options, logger),
                new MirrativSitePlugin.MirrativSiteContext(options,new MirrativSitePlugin.MirrativServer(), logger),
            };
            foreach (var site in list)
            {
                site.Init();
                _dict.Add(site.Guid, site);
            }
        }
        public void Save()
        {
            foreach (var siteContext in GetSiteContexts())
            {
                siteContext.Save();
            }
        }
        public IEnumerable<ISiteContext> GetSiteContexts()
        {
            return _dict.Select(s => s.Value);
        }
        public ISiteContext GetSiteContext(Guid guid)
        {
            return _dict[guid];
        }
        public ICommentProvider CreateCommentProvider(Guid guid)
        {
            var siteContext = GetSiteContext(guid);
            return siteContext.CreateCommentProvider();
        }
        public Guid GetValidSiteGuid(string input)
        {
            foreach(var siteContext in GetSiteContexts())
            {
                var b = siteContext.IsValidInput(input);
                if (b)
                {
                    return siteContext.Guid;
                }
            }
            return Guid.Empty;
        }

        public UserControl GetCommentPostPanel(Guid guid, ICommentProvider commentProvider)
        {
            var site = _dict[guid];
            return site.GetCommentPostPanel(commentProvider);
        }

        //public ISiteContext GetSiteIContext(Guid guid)
        //{
        //    throw new NotImplementedException();
        //}

        public void LoadSiteOptions(Guid guid, string path, IIo io)
        {
            var siteContext = GetSiteContext(guid);
            siteContext.LoadOptions(path, io);
        }
    }
}

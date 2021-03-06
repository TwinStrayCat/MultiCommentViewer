﻿using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MirrativSitePlugin
{
    public class MirrativServer : IDataServer
    {
        public async Task<string> GetAsync(string url, Dictionary<string, string> headers)
        {
            using (var client = new HttpClient())
            {
                if (headers != null)
                {
                    foreach (var kv in headers)
                    {
                        client.DefaultRequestHeaders.Add(kv.Key, kv.Value);
                    }
                }
                var result = await client.GetStringAsync(url);
                return result;
            }
        }
        public async Task<string> GetAsync(string url, Dictionary<string, string> headers, CookieContainer cc)
        {
            using (var handler = new HttpClientHandler { UseCookies = true, CookieContainer = cc })
            using (var client = new HttpClient(handler))
            {
                if (headers != null)
                {
                    foreach (var kv in headers)
                    {
                        client.DefaultRequestHeaders.Add(kv.Key, kv.Value);
                    }
                }
                var result = await client.GetStringAsync(url);
                return result;
            }
        }
        public async Task<string> GetAsync(string url)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetStringAsync(url);
                return result;
            }
        }
        public async Task<string> PostAsync(string url, Dictionary<string, string> data, CookieContainer cc)
        {
            var content = new FormUrlEncodedContent(data);
            using (var handler = new HttpClientHandler { UseCookies = true, CookieContainer = cc })
            using (var client = new HttpClient(handler))
            {
                var result = await client.PostAsync(url, content);
                var resBody = await result.Content.ReadAsStringAsync();
                return resBody;
            }
        }
    }
}

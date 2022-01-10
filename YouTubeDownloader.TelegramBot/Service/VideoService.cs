using System;
using System.Collections.Generic;
using System.Linq;
using VideoLibrary;

namespace YouTubeDownloader.TelegramBot.Service
{
    public class VideoService
    {
        public List<YouTubeVideo> videoList;
        public IEnumerable<int> GetVideoFormats(string url)
        {
            YouTube yt = YouTube.Default;
            HashSet<int> videoIds = new HashSet<int>();
            if (!string.IsNullOrEmpty(url))
            {
                videoList = yt.GetAllVideos(url).ToList();
                foreach (YouTubeVideo video in videoList)
                {
                    int format = video.Resolution;
                    if (format > 0)
                        videoIds.Add(format);
                }
            }
            IOrderedEnumerable<int> sorted = from a in videoIds
                                             orderby a
                                             select a;

            return sorted;
        }
    }
}

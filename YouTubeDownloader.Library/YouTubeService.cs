using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoLibrary;

namespace YouTubeDownloader.Library
{
    public class YouTubeService
    {
        public YouTubeService(string url)
        {
            Url = url;
            YouTube yt = YouTube.Default;
            videoList = yt.GetAllVideos(Url).ToList();
            HashSet<int> videoIds = new HashSet<int>();
            foreach (YouTubeVideo video in videoList)
            {
                int format = video.Resolution;
                if (format > 0)
                    videoIds.Add(format);
            }
        }
        private  string Url { get; set; }
        private  List<YouTubeVideo> videoList;

        public YouTubeVideo GetVideo(VideoFormat format)
        {
            return videoList.FirstOrDefault(video => GetVideoFormat(video.Resolution) == format);   
        }

        private  VideoFormat GetVideoFormat(int a)
        {
            switch (a)
            {
                case 144: return VideoFormat.p144;
                case 244: return VideoFormat.p244;
                case 360: return VideoFormat.p360;
                case 480: return VideoFormat.p480;
                case 720: return VideoFormat.p720;
                case 1080: return VideoFormat.p1080;
                case 1440: return VideoFormat.p1440;
                case 2160: return VideoFormat.p2160;
                case 4320: return VideoFormat.p4320;
            }

            return VideoFormat.p360;
        }

        private  IOrderedEnumerable<int> GetVideosInt()
        {
            YouTube yt = YouTube.Default;
            videoList = yt.GetAllVideos(Url).ToList();
            HashSet<int> videoIds = new HashSet<int>();
            foreach (YouTubeVideo video in videoList)
            {
                int format = video.Resolution;
                if (format > 0)
                    videoIds.Add(format);
            }
            IOrderedEnumerable<int> sorted = from a in videoIds
                         orderby a
                         select a;
            return sorted;
        }
    }

    public enum VideoFormat
    {
        p144,
        p244,
        p360,
        p480,
        p720,
        p1080,
        p1440,
        p2160,
        p4320
    }
}

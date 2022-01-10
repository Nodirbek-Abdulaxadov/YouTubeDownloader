using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoLibrary;

namespace YouTubeDownloader
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string url = Console.ReadLine();//"https://www.youtube.com/watch?v=bON-KPiiNCk";
            DownloadViewAsync(url, "E:/");
            Console.WriteLine("Done!");
        }

        private static void DownloadViewAsync(string url, string v)
        {
            YouTube yt = YouTube.Default;
            List<YouTubeVideo> list = yt.GetAllVideos(url).ToList();
            HashSet<int> videoIds = new HashSet<int>();
            foreach (YouTubeVideo video in list)
            {
                int format = video.Resolution;
                if (format > 0)
                videoIds.Add(format);            
            }
            var sorted = from a in videoIds
                         orderby a
                         select a;
            foreach (int videoId in sorted)
            {
                Console.WriteLine(videoId);
            }
        }
    }
}

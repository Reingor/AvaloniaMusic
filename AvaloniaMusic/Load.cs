using GetRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;
//using System.Drawing;
using System.IO;
using Avalonia.Media.Imaging;

namespace AvaloniaMusic
{
    public class Load
    {
        public Bitmap imageDraw = null;
        public Dictionary<string,List<string>> Data { get; set; }

        public string RecordLabel { get; set; }
        public string ReleaseDate { get; set; }

        public Load()
        {
            Data = new Dictionary<string, List<string>>();
        }

        public void LoadHtml()
        {
            var cookieContainer = new CookieContainer();

            var getRequest = new GetReq("https://www.google.com/search?q=depeche+mode+violator+songs&oq=&aqs=chrome.1.35i39i362l8.1418782j0j7&sourceid=chrome&ie=UTF-8");
            getRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
            getRequest.Useragent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.4896.88 Safari/537.36";
            getRequest.Referer = "https://www.google.com/";
            getRequest.Headers.Add("sec-ch-ua", "\"Not A;Brand\";v=\"99\", \"Chromium\";v=\"100\",\"Google Chrome\";v=\"100\"");
            getRequest.Headers.Add("sec-ch-ua-arch", "\"x86\"");
            getRequest.Headers.Add("sec-ch-ua-bitness", "\"64\"");
            getRequest.Headers.Add("sec-ch-ua-full-version", "\"100.0.4896.88\"");
            getRequest.Headers.Add("sec-ch-ua-mobile", "?0");
            getRequest.Headers.Add("sec-ch-ua-model", "");
            getRequest.Headers.Add("sec-ch-ua-platform", "\"Windows\"");
            getRequest.Headers.Add("sec-ch-ua-platform-version", "\"10.0.0\"");
            getRequest.Headers.Add("Sec-Fetch-Dest", "document");
            getRequest.Headers.Add("Sec-Fetch-Mode", "navigate");
            getRequest.Headers.Add("Sec-Fetch-Site", "same-origin");
            getRequest.Headers.Add("Sec-Fetch-User", "?1");
            getRequest.Headers.Add("Upgrade-Insecure-Requests", "1");

            getRequest.Host = "www.google.com";

            getRequest.Run(cookieContainer);

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(getRequest.Response);

            var title = document.DocumentNode.SelectNodes(".//div[contains(@class, 'title')]");

            Data.Add("title", new List<string>());
            foreach (var item in title.ToList())
            {
                Data["title"].Add(item.InnerText);
            }

            var duration = document.DocumentNode.SelectNodes(".//div[contains(@class, 'uDMnUc')]");

            Data.Add("duration", new List<string>());
            foreach (var item in duration.ToList())
            {
                Data["duration"].Add(item.InnerText);
            }

            var artistName = document.DocumentNode.SelectSingleNode(".//span[contains(@class, 'LrzXr kno-fv wHYlTd z8gr9e')]");

            Data.Add("artistName", new List<string>());
            foreach (var item in title.ToList())
            {
                Data["artistName"].Add(artistName.InnerText);
            }

            var albumName = document.DocumentNode.SelectSingleNode(".//span[contains(@class, 'yKMVIe')]");

            Data.Add("albumName", new List<string>());
            foreach (var item in title.ToList())
            {
                Data["albumName"].Add(albumName.InnerText);
            }

            var releaseDate = document.DocumentNode.SelectSingleNode(".//div[contains(@data-attrid, 'kc:/music/album:release date')]//span[contains(@class, 'LrzXr kno-fv wHYlTd z8gr9e')]");

            ReleaseDate = releaseDate.InnerText;

            var genre = document.DocumentNode.SelectSingleNode("//div[contains(@data-attrid, 'hw:/collection/musical_albums:genre')]//span[contains(@class, 'LrzXr kno-fv wHYlTd z8gr9e')]");

            Data.Add("genre", new List<string>());
            foreach (var item in title.ToList())
            {
                Data["genre"].Add(genre.FirstChild.InnerText);
            }

            var recordLabel = document.DocumentNode.SelectSingleNode(".//div[contains(@data-attrid, 'kc:/music/album:label')]//span[contains(@class, 'LrzXr kno-fv wHYlTd z8gr9e')]");

            RecordLabel = recordLabel.InnerText;

            var image = document.DocumentNode.SelectNodes(".//img[contains(@class, 'kAOS0')]");

            var id = image[0].Attributes["id"].Value;

            var docSearch = document.DocumentNode.OuterHtml;

            var subStringStart = "data:image/jpeg;base64,";

            var indexStart = docSearch.IndexOf(subStringStart);

            var subStringEnd = $"';var ii=['{id}']";

            var indexEnd = docSearch.IndexOf(subStringEnd);

            var base64String = docSearch.Substring(indexStart + subStringStart.Length, indexEnd - subStringEnd.Length - indexStart - 3);

            imageDraw = Base64ToImage(base64String);
        }

        static Bitmap Base64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                Bitmap bitmap = Bitmap.DecodeToWidth(ms, 80);
                return bitmap;
            }
        }
    }
}

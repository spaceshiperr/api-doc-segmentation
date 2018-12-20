using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DocumentSegmentation
{
    public class Parser
    {
        public Parser(Uri url)
        {
            Article = ParseArticle(url);
        }

        private Article Article { get; set; }

        private Article ParseArticle(Uri url)
        {
            using (WebClient web = new WebClient())
            {
                web.Headers["method"] = "GET";
                web.Headers["Content-Type"] = "application/json";
                web.Headers["x-api-key"] = "EGEaBTbtrtbrXIYxkRe9uSaZvq0IuxBUSkB02Cnf";

                var str = web.DownloadString($"https://mercury.postlight.com/parser?url={url}");
                var article = JsonConvert.DeserializeObject<Article>(str);
                return article;
            }
        }
    }
}

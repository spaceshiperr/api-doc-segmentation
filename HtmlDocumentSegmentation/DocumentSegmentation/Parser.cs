using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using HtmlAgilityPack;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DocumentSegmentation
{
    public class Parser
    {
        // it actually removes all the headings from the content 
        public Article GetArticle(Uri url)
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

        public HtmlDocument GetHtmlDocument(Uri url)
        {
            var web = new HtmlWeb();
            var document = web.Load(url);
            return document;
        }

        public HtmlDocument GetUpdatedHtmlDocument(Article article)
        {
            var url = new Uri(article.url);
            var document = GetHtmlDocument(url);

            ReplaceDocumentBody(document, article.content);
            return document;
        }

        public void ReplaceDocumentBody(HtmlDocument document, string replacement)
        {
            var body = document.DocumentNode.SelectNodes("//body").First();

            HtmlNode newBody = document.CreateElement("body");
            newBody.InnerHtml = replacement;

            body.ParentNode.ReplaceChild(newBody, body);
        }

        public void AppendHepsNode(HtmlDocument document)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\scripts\heps.js");
            var text = string.Empty;

            using (StreamReader sr = new StreamReader(path))
            {
                text = sr.ReadToEnd();
            }

            HtmlNode hepsNode = HtmlNode.CreateNode("<script>" + text + "</script>");
            var body = document.DocumentNode.SelectSingleNode("//body");
            body.AppendChild(hepsNode);
        }

        public List<LogEntry> GetConsoleLogs(Uri url)
        {
            var options = new ChromeOptions();
            options.SetLoggingPreference(LogType.Browser, LogLevel.All);

            var driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl(url);

            var entries = driver.Manage().Logs.GetLog(LogType.Browser).ToList();

            return entries;
        }


        // save web page and using AgilityPack download the stylesheets too
        public void DownloadPage(string url)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\contents\index.html");


            using (WebClient client = new WebClient())
            {
                client.DownloadFile(url, path);
            }
        }

        //public void InsertMissingHeadings(HtmlDocument currDoc, HtmlDocument prevDoc)
        //{
        //    var prevDocNode = prevDoc.DocumentNode;
        //    var currDocNode = currDoc.DocumentNode;
        //    foreach (HtmlNode node in prevDocNode.ChildNodes)
        //    {
        //        currDocNode.SelectNodes(node);
        //    }
        //}

        //private void CheckNode(HtmlDocument currDoc, HtmlDocument prevDoc)
        //{
        //    foreach (HtmlNode n in node.ChildNodes)
        //    {
        //        if (n.HasChildNodes)
        //        {
        //            //CheckNode(n);
        //        }
        //        else
        //        {
        //            Console.WriteLine(n.InnerText);
        //        }
        //    }
        //}
    }
}

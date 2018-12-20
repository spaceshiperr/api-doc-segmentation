using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace DocumentSegmentation
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = new Uri("https://www.kernel.org/doc/html/latest/kernel-hacking/hacking.html#introduction");

            var parser = new Parser();
            var article = parser.GetArticle(url);
            var document = parser.GetUpdatedHtmlDocument(article);
            parser.AppendHepsNode(document);

            FileStream sw = new FileStream("index.html", FileMode.Create);
            document.Save(sw);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentSegmentation
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = new Uri("https://vk.com/dev/upload_files");

            var parser = new Parser();
            var article = parser.GetArticle(url);
            var document = parser.GetUpdatedHtmlDocument(article);
            parser.AppendHepsNode(document);

            FileStream sw = new FileStream("index.html", FileMode.Create);
            document.Save(sw);

            //var entries = parser.GetConsoleLogs(new Uri("https://www.tutorialspoint.com/fsharp/fsharp_sequences.htm"));
            var entries = parser.GetConsoleLogs(new Uri("file:///D:/Info/Documents/spaceshiperr/Github/api-doc-segmentation/HtmlDocumentSegmentation/DocumentSegmentation/bin/Debug/index.html"));
        }
    }
}

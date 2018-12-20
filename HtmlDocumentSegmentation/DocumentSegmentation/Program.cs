using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DocumentSegmentation
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = new Uri("https://www.kernel.org/doc/html/latest/kernel-hacking/hacking.html#introduction");

            var parser = new Parser(url);
        }
    }
}

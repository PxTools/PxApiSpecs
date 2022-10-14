using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    internal class Helper
    {
        public string GetExampleJsonstat()
        {
            var fileName = "..\\..\\..\\..\\..\\..\\examples\\table-metadata-example-jsonstat-nocomments.json";
            var jsonString = File.ReadAllText(fileName);

            return jsonString;
        }
    }
}

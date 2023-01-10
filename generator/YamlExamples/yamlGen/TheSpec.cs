using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yamlGen
{
    public class TheSpec
    {
        public const string GEN_START_LINE_FORMAT = "#generated {0} start";
        public const string GEN_END_LINE_FORMAT = "#generated {0} end";

        private string specAsString;
        private string specFile;

        private bool dryRun;

        public TheSpec(string specFile, bool dryRun)
        {
            this.dryRun = dryRun;
            this.specFile = specFile;
            specAsString = File.ReadAllText(specFile);
        }


        public void replaceExample(string exampleName, string exampleYaml)
        {
            string startString = string.Format(GEN_START_LINE_FORMAT,exampleName);
            string endString = string.Format(GEN_END_LINE_FORMAT, exampleName);

            int pos= specAsString.IndexOf(startString);
            string temp = specAsString.Substring(pos);
            string oldExample = temp.Substring(0,temp.IndexOf(endString)+endString.Length);

            specAsString = specAsString.Replace(oldExample, exampleYaml);
        }

        public void Save()
        {
            if (dryRun)
            {
                File.WriteAllText(specFile + "2", specAsString);
                Console.WriteLine("Dry run. Does not overwrite spec");
            }
            else
            {
                File.WriteAllText(specFile, specAsString);
            }
        }

    }
}

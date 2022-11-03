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

        private string specAsString = "";
        private string specFile = @"C:\github.com\statisticssweden\api2Spec\yamlGen\PxApiSpecs\PxAPI-2.yml";

        private bool dryRun = true;

        public TheSpec(string inFile, bool inDryRun )
        {
            dryRun = inDryRun;

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
            }
            else
            {
                File.WriteAllText(specFile, specAsString);
            }
        }

    }
}


using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using PxWeb.Api2.Server.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;

using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace yamlGen
{
    public class DoIt
    {
        private ModelsWithData models;

        private JsonSerializerSettings mySettings;
        private string specFile ;
        private string jsonOutDir;
        private string ymlOutDir;

        private TheSpec theSpec;

        public DoIt(string basePath, bool dryRun)
        {
            specFile = basePath +"PxAPI-2.yml";
            jsonOutDir = basePath + "examplesAsJson/";
            ymlOutDir = basePath + "examplesAsYml/";

            models = new ModelsWithData();

            mySettings = new JsonSerializerSettings
            {
                ContractResolver = new BaseFirstContractResolver()
            };
            

           // theSpec = new TheSpec(specFile,dryRun);

            FixOne(models.rootfolder, "folder-root", "Example for 200 response for navigation endpoint root");
            FixOne(models.befolder, "folder-be", "Example for 200 response for navigation endpoint");
            FixOne(models.tablesResponse, "tablesResponse", "List all tables with pagination");
            FixOne(models.tableMetadata, "tableMetadata", "Example for 200 response for tableMetadata in PXJson");
            FixOne(models.dataset, "dataset-meta", "Example for 200 response for dataset as metadata in Jsonstat2");
            //theSpec.Save();
        }


        private void FixOne(Object modelObject,string exampleName, string description)
        {
            string jsonNew = JsonConvert.SerializeObject(modelObject, Formatting.Indented, mySettings);
            File.WriteAllText(jsonOutDir+ exampleName + ".json", jsonNew);

            string yamlTemp = json2yaml(jsonNew);
            string betterYaml = PrepYaml(yamlTemp, exampleName, 4);
            string intro = "# this file is generated\n" + exampleName + ":\n  description: " + description + "\n  value:\n";
            File.WriteAllText(ymlOutDir + exampleName + ".yml", intro+betterYaml);
            //theSpec.replaceExample(exampleName, betterYaml);
        }


        private string json2yaml(string jsonIn)
        {
            var expConverter = new ExpandoObjectConverter();
            dynamic deserializedObject = JsonConvert.DeserializeObject<ExpandoObject>(jsonIn, expConverter);

            //var serializer2 = new YamlDotNet.Serialization.Serializer();
            var serializer =  new SerializerBuilder().WithQuotingNecessaryStrings().Build();

            string yamlOut = serializer.Serialize(deserializedObject);
            return yamlOut;
        }


        private string PrepYaml(string inYaml, string exampleId, int indentInt)
        {

            string[] lines = inYaml.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            string indent = "                           ".Substring(0, indentInt);



            List<string> outLines = new List<string>();
            //outLines.Add(String.Format(TheSpec.GEN_START_LINE_FORMAT, exampleId));
            foreach (string line in lines)
            {
                outLines.Add(indent + line);
            }
            //outLines.Add(indent + String.Format(TheSpec.GEN_END_LINE_FORMAT, exampleId));
            return String.Join("\r\n", outLines);

        }
    }
}
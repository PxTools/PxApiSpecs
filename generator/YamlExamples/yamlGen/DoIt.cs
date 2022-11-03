﻿
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
        private string specFile = @"C:\github.com\statisticssweden\api2Spec\yamlGen\PxApiSpecs\PxAPI-2.yml";

        private TheSpec theSpec;

        public DoIt()
        {
            models = new ModelsWithData();

            mySettings = new JsonSerializerSettings
            {
                ContractResolver = new BaseFirstContractResolver()
            };
            
            bool dryRun = true;
            theSpec = new TheSpec(specFile,dryRun);

            FixOne(models.rootfolder, "navigation-root");
            FixOne(models.befolder, "navigation-be");
            FixOne(models.tablesResponse, "tablesResponse");
            FixOne(models.tableMetadata, "tableMetadata");
            theSpec.Save();
        }


        private void FixOne(Object modelObject,String exampleName)
        {
            var jsonNew = JsonConvert.SerializeObject(modelObject, Formatting.Indented, mySettings);
            string yamlTemp = json2yaml(jsonNew);
            string betterYaml = PrepYaml(yamlTemp, exampleName, 8);

            theSpec.replaceExample(exampleName, betterYaml);
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
            outLines.Add(String.Format(TheSpec.GEN_START_LINE_FORMAT, exampleId));
            foreach (string line in lines)
            {
                outLines.Add(indent + line);
            }
            outLines.Add(indent + String.Format(TheSpec.GEN_END_LINE_FORMAT, exampleId));
            return String.Join("\r\n", outLines);

        }
    }
}
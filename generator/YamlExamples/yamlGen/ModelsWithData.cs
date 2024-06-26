﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PxWeb.Api2.Server.Models;

namespace yamlGen
{
    internal class ModelsWithData
    {
        private static string API_PATH = "https://my-site.com/api/v2/";

        internal Folder rootfolder { get; private set; }

        internal Folder befolder { get; private set; }

        internal Dataset dataset { get; private set; }

        internal TablesResponse tablesResponse { get; private set; }


        internal TableMetadata tableMetadata { get; private set; }

        internal ModelsWithData()
        {
            rootfolder = getRootFolder();
            befolder = getBeFolder();
            dataset = GenerateDatasetData();

            tablesResponse = getTablesResponse();
            tableMetadata = getTableMetadata();
        }

        private TableMetadata getTableMetadata()
        {
            TableMetadata myOut= new TableMetadata ();
            myOut.Id = "TAB638";
            //myOut.Language = "sv";

            myOut.Label = "Folkmängden efter region; civilstånd; ålder och kön. År 1968 - 2021";
            myOut.Description = "";
            myOut.Source= "SCB";
  
            myOut.OfficalStatistics = true;
            //myOut.Category = "BE";
            myOut.Licence = "CC0-1.0";
            myOut.AggregationAllowed = false;
            //myOut.discontinued = false;

            myOut.Contacts = new List<Contact>();
            myOut.Contacts.Add(new Contact { Name = "Tomas Johansson", Phone = "+46 010-479 64 26", Mail = "tomas.johansson@scb.se" });
            myOut.Contacts.Add(new Contact { Name = "(SCB) Statistikservice", Phone = "+46 010-479 50 0", Mail = "information@scb.se" });

            myOut.Notes = new List<CellNote>();
            myOut.Notes.Add(new CellNote {Text = "Fr o m 2007-01-01 överförs Heby kommun från Västmanlands län till Uppsala län. Hebys kommunkod ändras från 1917 till 0331. ", Mandatory = true});
            myOut.Notes.Add(new CellNote { Text = "Registrerat partnerskap reglerade parförhållanden mellan personer av samma kön och fanns från 1995 till 2009. Registrerade partners räknas som Gifta, Separerade partners som Skilda och Efterlevande partners som Änka/änklingar.", Mandatory = true });

            CellNote temp = new CellNote();
            temp.Conditions = new List<Condition>();
            temp.Conditions.Add(new Condition() { Variable =  "ContentsCode", Value = "BE0101N1" });
            temp.Conditions.Add(new Condition() { Variable = "Region", Value = "03" });
            temp.Mandatory = true;
            temp.Text = "Fr o m 2007-01-01 utökas Uppsala län med Heby kommun. Observera att länssiffrorna inte är jämförbara med länssiffrorna bakåt i tiden.";
            myOut.Notes.Add(temp);
            temp = new CellNote();
            temp.Conditions = new List<Condition>();
            temp.Conditions.Add(new Condition() { Variable = "ContentsCode", Value = "BE0101N1" });
            temp.Conditions.Add(new Condition() { Variable = "Region", Value = "19" });
            temp.Text = "Fr o m 2007-01-01 minskar Västmanlands län med Heby kommun. Observera att länssiffrorna inte är jämförbara med länssiffrorna bakåt i tiden.";
            myOut.Notes.Add(temp);

            myOut.Links = new List<Link>() { FixLink("self", "tables/" + myOut.Id), FixLink("metadata", "tables/" + myOut.Id + "/metadata"), FixLink("data", "tables/" + myOut.Id + "/data") };


            myOut.Variables = new List<AbstractVariable>();
            TimeVariable tempTime = new TimeVariable() { Id="Tid", Label="år", TimeUnit=TimeVariable.TimeUnitEnum.AnnualEnum, FirstPeriod="1968",LastPeriod="2021"};
            tempTime.Values = new List<Value>();
            for(int n= 1968; n <= 2021; n++)
            {
                tempTime.Values.Add(new Value { Code = n.ToString(), Label = n.ToString() });
            }
            myOut.Variables.Add(tempTime);

            ContentsVariable tempContents =  new ContentsVariable() { Id= "ContentsCode", Label= "tabellinnehåll" };
            tempContents.Values = new List<ContentValue>();
            tempContents.Values.Add(new ContentValue()
            {
                Code = "BE0101N1",
                Label = "Folkmängd",
                MeasuringType = ContentValue.MeasuringTypeEnum.StockEnum,
                Adjustment = ContentValue.AdjustmentEnum.NoneEnum,
                Unit = "antall",
                PreferedNumberOfDecimals = 0,
                RefrencePeriod = "31 december repektive år",
                Notes = new List<Note>() { new() { Mandatory = true, Text = "Uppgifterna avser förhållandena den 31 december för valt/valda år enligt den regionala indelning som gäller den 1 januari året efter." } }
            });
            tempContents.Values.Add(new ContentValue()
            {
                Code = "BE0101N2",
                Label = "Folkökning",
                MeasuringType = ContentValue.MeasuringTypeEnum.FlowEnum,
                Adjustment = ContentValue.AdjustmentEnum.NoneEnum,
                Unit = "antall",
                PreferedNumberOfDecimals = 0,
                Notes = new List<Note>() { new() { Mandatory = false, Text = "Folkökningen definieras som skillnaden mellan folkmängden vid årets början och årets slut." } }
            });
            myOut.Variables.Add(tempContents);

 
            myOut.Variables.Add(GetCivilstandRegularVariable());
            myOut.Variables.Add(GetKonRegularVariable());
            myOut.Variables.Add(GetAlderRegularVariable());
            myOut.Variables.Add(GetRegion());

            myOut.VariablesDisplayOrder = new List<string>() { "ContentsCode", "Civilstand", "Alder", "Tid", "Kon" };

            return myOut;
        }


        private Folder getBeFolder()
        {
           Folder folder = newFolder();
           folder.Id = "BE0101";
          // folder.ObjectType = "folder";
           folder.Label = "Balance statistics";
           folder.Description = "Balance statistics";
           folder.Links = new List<Link>() { FixLink("self", "navigation/BE0101") };

           folder.FolderContents = new List<FolderContentItem>();

           var temp = new FolderInformation() { Id = "01", ObjectType = "folder-information", Label = "Annual statistics" };
           temp.Description = "Annual statistics";
           temp.Links = new List<Link>() { FixLink("folder", "navigation/01" ) };
           folder.FolderContents.Add(temp);


           Heading tempH = new Heading()  { Id = "BEH1", ObjectType = "heading", Label = "I am heading" };
           tempH.Description = "I am heading";
           folder.FolderContents.Add(tempH);

           Table tempT = FixTable("TAB0001", "Corporations Financial Assets and Liabilities. Quarterly 1998K2 - 2021K4" );
           tempT.Description = "Corporations Financial Assets and Liabilities by item. Quarterly 1998K2 - 2021K4 [2022-03-11]";

           tempT.Updated = new DateTime(2017, 9, 8, 6, 0, 0, 714, DateTimeKind.Utc);


           tempT.Tags = new List<string>() { "Money", "Gold" };
           tempT.VariableNames = new List<string>() { "observations", "region", "marital status", "age" };
           tempT.FirstPeriod = "1998K2";
           tempT.LastPeriod = "2021K4";
           tempT.Discontinued = false;

           folder.FolderContents.Add(tempT);
           return folder;
       }

       private Table FixTable(string inId, string inLabel)
       {
           Table table = new Table() { ObjectType = "table", Id = inId, Label = inLabel };
           table.Links = new List<Link>() { FixLink("self", "tables/"+inId), FixLink("metadata", "tables/"+inId+"/metadata"), FixLink("data", "tables/" + inId + "/data")  };
            table.Category = Table.CategoryEnum.PublicEnum;
           return table;
       } 

       private Link FixLink(string inRel, string inUrl)
       {
           return new Link()
           {
               Rel = inRel,
               Href = API_PATH + inUrl
           };
       }

       private Folder getRootFolder()
       {
           Folder folder = newFolder();

           folder.Links = new List<Link>() { new Link() { Rel = "self", Href = API_PATH+"navigation" } };

           folder.FolderContents = new List<FolderContentItem>();

           var temp = new FolderInformation() { Id = "JO", ObjectType = "folder-information", Label = "Agriculture, forestry and fishery" };
           temp.Description = "Animals, economy, employment, production, and more.";
           temp.Links = new List<Link>() { new Link() { Rel = "self", Href = API_PATH+"navigation/JO" } };
           folder.FolderContents.Add(temp);

           temp = new FolderInformation() { Id = "NV", ObjectType = "folder-information", Label = "Financial markets" };
           temp.Description = "Assets, banks, insurance companies, liabilities, shares, and more.";
           temp.Links = new List<Link>() { new Link() { Rel = "self", Href = API_PATH+"navigation/NV" } };
           folder.FolderContents.Add(temp);

           return folder;
       }


       private TablesResponse getTablesResponse()
       {
           TablesResponse myOut = new TablesResponse();
           myOut.Page = new PageInfo() { PageNumber = 10, PageSize=100,TotalElements=7000, TotalPages= 70 };
           myOut.Page.Links = new List<Link>() { FixLink("self", "tables/?pageNumber=10")
                                          , FixLink("first","tables/?pageNumber=1")
                                          ,  FixLink("last","tables/?pageNumber=70")
                                          ,  FixLink("next","tables/?pageNumber=11")
                                          ,  FixLink("prev","tables/?pageNumber=9")
               };

           Table tempTab = FixTable("TAB0001", "Population. Year 1968 - 2021");
           tempTab.Description = "Population by region, marital status, age and sex. Year 1968 - 2021";
           tempTab.Updated = new DateTime(2017, 12, 24, 6, 0, 0, 014, DateTimeKind.Utc);
           tempTab.VariableNames = new List<string>() { "observations", "region", "marital status", "age" };
           tempTab.FirstPeriod = "1968";
           tempTab.LastPeriod = "2021";
           tempTab.Discontinued = false;

           myOut.Tables = new List<Table>() { tempTab };

           tempTab = FixTable("TAB0001A", "Tabell A");
           tempTab.Description = "Folkmängd, in- och utvanding, medellivslängd, namn, prognoser mm";
           tempTab.Updated = new DateTime(2018, 12, 24, 6, 0, 0, 014, DateTimeKind.Utc);
           tempTab.VariableNames = new List<string>() { "observations", "region", "marital status", "age" };
           tempTab.FirstPeriod = "2017";
           tempTab.LastPeriod = "2022";
           tempTab.Discontinued = false;
           myOut.Tables.Add(tempTab);

           return myOut;

       }



        private Dataset GenerateDatasetData()
        {
            Dataset ds = new Dataset();

            ds.Label = "Folkmängden efter region, civilstånd, ålder, kön, tabellinnehåll och år";
            ds.Source = "SCB";
            ds.Updated = "2022-02-07T14:32:00Z";
            ds.Link = new Dictionary<string, List<JsonstatLink>>();
            ds.Href = "https://my-site.com/api/v2/tables/TAB638/metadata";

            JsonstatLink temp = new JsonstatLink();
            temp.Href = "https://my-site.com/api/v2/tables/TAB638/data";
            temp.Type = "application/json";
            List<JsonstatLink> temp2 = new List<JsonstatLink>();
            temp2.Add(temp);
            ds.Link.Add("describes", temp2);


            temp = new JsonstatLink();
            temp.Href = "https://my-other-site.com/definitions/TAB638";
            temp.Type = "application/json";
            temp2 = new List<JsonstatLink>();
            temp2.Add(temp);
            ds.Link.Add("describedby", temp2);


            ds.Extension = new ExtensionRoot();
            ds.Extension.Px = new ExtensionRootPx
            {
                Infofile = "BE0101",
                Tableid = "TAB638",
                OfficialStatistics = true,
                Aggregallowed = false,
                Copyright = "CC-0",
                Language = "sv",
                Description = "",
                Matrix = "TAB638",
                SubjectCode = "BE"
            };

            ds.Extension.Tags = new List<string>();
            ds.Extension.Tags.Add("");
            ds.Extension.Discontinued = false;


            ds.Extension.Contact = new List<Contact>();
            ds.Extension.Contact.Add(new Contact
            { Name = "Tomas Johansson", Phone = "+46 010-479 64 26", Mail = "tomas.johansson@scb.se" });
            ds.Extension.Contact.Add(new Contact
            { Name = "Statistikservice", Phone = "+46 010-479 50 0", Mail = "information@scb.se" });

            ds.Role = new DatasetRole();
            ds.Role.Time = new List<string> { "Tid" };

            ds.Role.Geo = new List<string> { "Region" };

            ds.Role.Metric = new List<string> { "ContentsCode" };

            ds.Id = new List<string> { "Region", "Civilstand", "Alder", "Kon", "ContentsCode", "Tid" };

            ds.Size = new List<int> { 312, 4, 1, 2, 2, 1 };


            ds.Dimension = new Dictionary<string, DatasetDimensionValue>();

            ds.Dimension.Add("Region", GetRegionDatasetDimensionValue());
            ds.Dimension.Add("Civilstand", GetCivilstandDatasetDimensionValue());
            ds.Dimension.Add("Alder", GetAlderDatasetDimensionValue());
            ds.Dimension.Add("Kon", GetKonDatasetDimensionValue());
            ds.Dimension.Add("ContentsCode", GetContentsCodeDatasetDimensionValue());
            ds.Dimension.Add("Tid", GetTidDatasetDimensionValue());

            ds.Extension.Note = new List<Note>();
            ds.Extension.Note.Add(new Note()
            {
                Mandatory = true,
                Text = "Fr o m 2007-01-01 överförs Heby kommun från Västmanlands län till Uppsala län. Hebys kommunkod ändras från 1917 till 0331. "
            });
            ds.Extension.Note.Add(new Note()
            {
                Mandatory = true,
                Text = "Registrerat partnerskap reglerade parförhållanden mellan personer av samma kön och fanns från 1995 till 2009. Registrerade partners räknas som Gifta, Separerade partners som Skilda och Efterlevande partners som Änka/änklingar."
            });
            ds.Extension.Note.Add(new Note()
            {
                Mandatory = true,
                Text = "Fr o m 2007-01-01 utökas Uppsala län med Heby kommun. Observera att länssiffrorna inte är jämförbara med länssiffrorna bakåt i tiden."
            });

            ds.Value = new List<double?>();
            return ds;
        }


        private RegularVariable GetKonRegularVariable()
        {
           
            RegularVariable myOut = new RegularVariable() {Id = "Kon",  Label = "kön", Elimination = true };
            myOut.Values = new List<Value>();
            myOut.Values.Add(new Value() { Code = "1", Label = "män" });
            myOut.Values.Add(new Value() { Code = "2", Label = "kvinnor" });

            return myOut;
        }

        private RegularVariable GetCivilstandRegularVariable()
        {
            RegularVariable myOut = new RegularVariable() { Id = "Civilstand", Label = "civilstånd", Elimination = true };
            myOut.Values = new List<Value>();
            myOut.Values.Add(new Value() { Code = "OG", Label = "ogifta" });
            myOut.Values.Add(new Value() { Code = "G", Label = "gifta" });
            myOut.Values.Add(new Value() { Code = "SK", Label = "skilda" });
            myOut.Values.Add(new Value() { Code = "ÄNKL", Label = "änkor/änklingar" });
            return myOut;
        }


        private RegularVariable GetAlderRegularVariable()
        {
            RegularVariable myOut = new RegularVariable() { Id = "Alder", Label = "ålder", Elimination = true , EliminationValueCode = "tot"};
            myOut.Values = new List<Value>();
            #region values


            myOut.Values.Add(new Value() { Code = "0", Label = "0 år" });
            myOut.Values.Add(new Value() { Code = "1", Label = "1 år" });
            myOut.Values.Add(new Value() { Code = "2", Label = "2 år" });
            myOut.Values.Add(new Value() { Code = "3", Label = "3 år" });
            myOut.Values.Add(new Value() { Code = "4", Label = "4 år" });
            myOut.Values.Add(new Value() { Code = "5", Label = "5 år" });
            myOut.Values.Add(new Value() { Code = "6", Label = "6 år" });
            myOut.Values.Add(new Value() { Code = "7", Label = "7 år" });
            myOut.Values.Add(new Value() { Code = "8", Label = "8 år" });
            myOut.Values.Add(new Value() { Code = "9", Label = "9 år" });
            myOut.Values.Add(new Value() { Code = "10", Label = "10 år" });
            myOut.Values.Add(new Value() { Code = "11", Label = "11 år" });
            myOut.Values.Add(new Value() { Code = "12", Label = "12 år" });
            myOut.Values.Add(new Value() { Code = "13", Label = "13 år" });
            myOut.Values.Add(new Value() { Code = "14", Label = "14 år" });
            myOut.Values.Add(new Value() { Code = "15", Label = "15 år" });
            myOut.Values.Add(new Value() { Code = "16", Label = "16 år" });
            myOut.Values.Add(new Value() { Code = "17", Label = "17 år" });
            myOut.Values.Add(new Value() { Code = "18", Label = "18 år" });
            myOut.Values.Add(new Value() { Code = "19", Label = "19 år" });
            myOut.Values.Add(new Value() { Code = "20", Label = "20 år" });
            myOut.Values.Add(new Value() { Code = "21", Label = "21 år" });
            myOut.Values.Add(new Value() { Code = "22", Label = "22 år" });
            myOut.Values.Add(new Value() { Code = "23", Label = "23 år" });
            myOut.Values.Add(new Value() { Code = "24", Label = "24 år" });
            myOut.Values.Add(new Value() { Code = "25", Label = "25 år" });
            myOut.Values.Add(new Value() { Code = "26", Label = "26 år" });
            myOut.Values.Add(new Value() { Code = "27", Label = "27 år" });
            myOut.Values.Add(new Value() { Code = "28", Label = "28 år" });
            myOut.Values.Add(new Value() { Code = "29", Label = "29 år" });
            myOut.Values.Add(new Value() { Code = "30", Label = "30 år" });
            myOut.Values.Add(new Value() { Code = "31", Label = "31 år" });
            myOut.Values.Add(new Value() { Code = "32", Label = "32 år" });
            myOut.Values.Add(new Value() { Code = "33", Label = "33 år" });
            myOut.Values.Add(new Value() { Code = "34", Label = "34 år" });
            myOut.Values.Add(new Value() { Code = "35", Label = "35 år" });
            myOut.Values.Add(new Value() { Code = "36", Label = "36 år" });
            myOut.Values.Add(new Value() { Code = "37", Label = "37 år" });
            myOut.Values.Add(new Value() { Code = "38", Label = "38 år" });
            myOut.Values.Add(new Value() { Code = "39", Label = "39 år" });
            myOut.Values.Add(new Value() { Code = "40", Label = "40 år" });
            myOut.Values.Add(new Value() { Code = "41", Label = "41 år" });
            myOut.Values.Add(new Value() { Code = "42", Label = "42 år" });
            myOut.Values.Add(new Value() { Code = "43", Label = "43 år" });
            myOut.Values.Add(new Value() { Code = "44", Label = "44 år" });
            myOut.Values.Add(new Value() { Code = "45", Label = "45 år" });
            myOut.Values.Add(new Value() { Code = "46", Label = "46 år" });
            myOut.Values.Add(new Value() { Code = "47", Label = "47 år" });
            myOut.Values.Add(new Value() { Code = "48", Label = "48 år" });
            myOut.Values.Add(new Value() { Code = "49", Label = "49 år" });
            myOut.Values.Add(new Value() { Code = "50", Label = "50 år" });
            myOut.Values.Add(new Value() { Code = "51", Label = "51 år" });
            myOut.Values.Add(new Value() { Code = "52", Label = "52 år" });
            myOut.Values.Add(new Value() { Code = "53", Label = "53 år" });
            myOut.Values.Add(new Value() { Code = "54", Label = "54 år" });
            myOut.Values.Add(new Value() { Code = "55", Label = "55 år" });
            myOut.Values.Add(new Value() { Code = "56", Label = "56 år" });
            myOut.Values.Add(new Value() { Code = "57", Label = "57 år" });
            myOut.Values.Add(new Value() { Code = "58", Label = "58 år" });
            myOut.Values.Add(new Value() { Code = "59", Label = "59 år" });
            myOut.Values.Add(new Value() { Code = "60", Label = "60 år" });
            myOut.Values.Add(new Value() { Code = "61", Label = "61 år" });
            myOut.Values.Add(new Value() { Code = "62", Label = "62 år" });
            myOut.Values.Add(new Value() { Code = "63", Label = "63 år" });
            myOut.Values.Add(new Value() { Code = "64", Label = "64 år" });
            myOut.Values.Add(new Value() { Code = "65", Label = "65 år" });
            myOut.Values.Add(new Value() { Code = "66", Label = "66 år" });
            myOut.Values.Add(new Value() { Code = "67", Label = "67 år" });
            myOut.Values.Add(new Value() { Code = "68", Label = "68 år" });
            myOut.Values.Add(new Value() { Code = "69", Label = "69 år" });
            myOut.Values.Add(new Value() { Code = "70", Label = "70 år" });
            myOut.Values.Add(new Value() { Code = "71", Label = "71 år" });
            myOut.Values.Add(new Value() { Code = "72", Label = "72 år" });
            myOut.Values.Add(new Value() { Code = "73", Label = "73 år" });
            myOut.Values.Add(new Value() { Code = "74", Label = "74 år" });
            myOut.Values.Add(new Value() { Code = "75", Label = "75 år" });
            myOut.Values.Add(new Value() { Code = "76", Label = "76 år" });
            myOut.Values.Add(new Value() { Code = "77", Label = "77 år" });
            myOut.Values.Add(new Value() { Code = "78", Label = "78 år" });
            myOut.Values.Add(new Value() { Code = "79", Label = "79 år" });
            myOut.Values.Add(new Value() { Code = "80", Label = "80 år" });
            myOut.Values.Add(new Value() { Code = "81", Label = "81 år" });
            myOut.Values.Add(new Value() { Code = "82", Label = "82 år" });
            myOut.Values.Add(new Value() { Code = "83", Label = "83 år" });
            myOut.Values.Add(new Value() { Code = "84", Label = "84 år" });
            myOut.Values.Add(new Value() { Code = "85", Label = "85 år" });
            myOut.Values.Add(new Value() { Code = "86", Label = "86 år" });
            myOut.Values.Add(new Value() { Code = "87", Label = "87 år" });
            myOut.Values.Add(new Value() { Code = "88", Label = "88 år" });
            myOut.Values.Add(new Value() { Code = "89", Label = "89 år" });
            myOut.Values.Add(new Value() { Code = "90", Label = "90 år" });
            myOut.Values.Add(new Value() { Code = "91", Label = "91 år" });
            myOut.Values.Add(new Value() { Code = "92", Label = "92 år" });
            myOut.Values.Add(new Value() { Code = "93", Label = "93 år" });
            myOut.Values.Add(new Value() { Code = "94", Label = "94 år" });
            myOut.Values.Add(new Value() { Code = "95", Label = "95 år" });
            myOut.Values.Add(new Value() { Code = "96", Label = "96 år" });
            myOut.Values.Add(new Value() { Code = "97", Label = "97 år" });
            myOut.Values.Add(new Value() { Code = "98", Label = "98 år" });
            myOut.Values.Add(new Value() { Code = "99", Label = "99 år" });
            myOut.Values.Add(new Value() { Code = "100+", Label = "100+ år" });
            myOut.Values.Add(new Value() { Code = "tot", Label = "totalt ålder" });

            #endregion values


            #region codelists
            myOut.CodeLists = new List<CodeListInformation>();

            myOut.CodeLists.Add(new CodeListInformation()
            {
                Id = "vs_Ålder1årA",
                Label = "Ålder, 1 års-klasser",
                Links = new List<Link>() { new Link() { Rel = "metadata", Href = "https://my-site.com/api/v2/tables/TAB638/codelists/vs_Ålder1årA" } }
            });

            myOut.CodeLists.Add(new CodeListInformation()
            {
                Id = "vs_ÅlderTotA",
                Label = "Ålder, totalt, alla redovisade åldrar",
                Links = new List<Link>() { new Link() { Rel = "metadata", Href = "https://my-site.com/api/v2/tables/TAB638/codelists/vs_ÅlderTotA" } }
            });

            myOut.CodeLists.Add(new CodeListInformation()
            {
                Id = "agg_Ålder10år",
                Label = "10-årsklasser",
                Links = new List<Link>() { new Link() { Rel = "metadata", Href = "https://my-site.com/api/v2/tables/TAB638/codelists/agg_Ålder10år" } }
            });

            myOut.CodeLists.Add(new CodeListInformation()
            {
                Id = "agg_Ålder5år",
                Label = "5-årsklasser",
                Links = new List<Link>() { new Link() { Rel = "metadata", Href = "https://my-site.com/api/v2/tables/TAB638/codelists/agg_Ålder5år" } }
            });
            #endregion codelists



            return myOut;
        }


        private GeographicalVariable GetRegion()
        {
            GeographicalVariable myOut = new GeographicalVariable() { Id = "Region", Label = "region", Elimination = true, EliminationValueCode = "00" };
            #region values
            myOut.Values = new List<Value>();
            myOut.Values.Add(new Value() { Code = "00", Label = "Riket" });
            myOut.Values.Add(new Value() { Code = "01", Label = "Stockholms län" });
            myOut.Values.Add(new Value() { Code = "0114", Label = "Upplands Väsby" });
            myOut.Values.Add(new Value() { Code = "0115", Label = "Vallentuna" });
            myOut.Values.Add(new Value() { Code = "0117", Label = "Österåker" });
            myOut.Values.Add(new Value() { Code = "0120", Label = "Värmdö" });
            myOut.Values.Add(new Value() { Code = "0123", Label = "Järfälla" });
            myOut.Values.Add(new Value() { Code = "0125", Label = "Ekerö" });
            myOut.Values.Add(new Value() { Code = "0126", Label = "Huddinge" });
            myOut.Values.Add(new Value() { Code = "0127", Label = "Botkyrka" });
            myOut.Values.Add(new Value() { Code = "0128", Label = "Salem" });
            myOut.Values.Add(new Value() { Code = "0136", Label = "Haninge" });
            myOut.Values.Add(new Value() { Code = "0138", Label = "Tyresö" });
            myOut.Values.Add(new Value() { Code = "0139", Label = "Upplands-Bro" });
            myOut.Values.Add(new Value() { Code = "0140", Label = "Nykvarn", Notes = new List<Note>() { new Note() { Mandatory = true, Text = "Ny regional indelning fr.o.m. 1999-01-01. Delar av Södertälje kommun (kod 0181) bildar en ny kommun benämnd Nykvarn (kod 0140)." } } }); 
            myOut.Values.Add(new Value() { Code = "0160", Label = "Täby" });
            myOut.Values.Add(new Value() { Code = "0162", Label = "Danderyd" });
            myOut.Values.Add(new Value() { Code = "0163", Label = "Sollentuna" });
            myOut.Values.Add(new Value() { Code = "0180", Label = "Stockholm" });
            myOut.Values.Add(new Value() { Code = "0181", Label = "Södertälje", Notes = new List<Note>() { new Note() { Mandatory = true, Text = "Ny regional indelning fr.o.m. 1999-01-01. Delar av Södertälje kommun (kod 0181) bildar en ny kommun benämnd Nykvarn (kod 0140)." } } });
            myOut.Values.Add(new Value() { Code = "0182", Label = "Nacka" });
            myOut.Values.Add(new Value() { Code = "0183", Label = "Sundbyberg" });
            myOut.Values.Add(new Value() { Code = "0184", Label = "Solna" });
            myOut.Values.Add(new Value() { Code = "0186", Label = "Lidingö", Notes = new List<Note>() { new Note() { Mandatory = true, Text = "Från och med 2011-01-01 tillhör Storholmen Lidingö (0186) som tidigare tillhörde Vaxholm (0187)." } } });
            myOut.Values.Add(new Value() { Code = "0187", Label = "Vaxholm", Notes = new List<Note>() { new Note() { Mandatory = true, Text = "Från och med 2011-01-01 tillhör Storholmen Lidingö (0186) som tidigare tillhörde Vaxholm (0187)." } } });
            myOut.Values.Add(new Value() { Code = "0188", Label = "Norrtälje" });
            myOut.Values.Add(new Value() { Code = "0191", Label = "Sigtuna" });
            myOut.Values.Add(new Value() { Code = "0192", Label = "Nynäshamn" });
            myOut.Values.Add(new Value() { Code = "03", Label = "Uppsala län" });
            myOut.Values.Add(new Value() { Code = "0305", Label = "Håbo" });
            myOut.Values.Add(new Value() { Code = "0319", Label = "Älvkarleby" });
            myOut.Values.Add(new Value() { Code = "0330", Label = "Knivsta", Notes = new List<Note>() { new Note() { Mandatory = true, Text = "Ny regional indelning fr.o.m. 2003-01-01. Delar av Uppsala kommun bildar en ny kommun benämnd Knivsta kommun." } } });
            myOut.Values.Add(new Value() { Code = "0331", Label = "Heby" });
            myOut.Values.Add(new Value() { Code = "0360", Label = "Tierp" });
            myOut.Values.Add(new Value() { Code = "0380", Label = "Uppsala", Notes = new List<Note>() { new Note() { Mandatory = true, Text = "Ny regional indelning fr.o.m. 2003-01-01. Delar av Uppsala kommun bildar en ny kommun benämnd Knivsta kommun." } } });
            myOut.Values.Add(new Value() { Code = "0381", Label = "Enköping" });
            myOut.Values.Add(new Value() { Code = "0382", Label = "Östhammar" });
            myOut.Values.Add(new Value() { Code = "04", Label = "Södermanlands län" });
            myOut.Values.Add(new Value() { Code = "0428", Label = "Vingåker" });
            myOut.Values.Add(new Value() { Code = "0461", Label = "Gnesta" });
            myOut.Values.Add(new Value() { Code = "0480", Label = "Nyköping" });
            myOut.Values.Add(new Value() { Code = "0481", Label = "Oxelösund" });
            myOut.Values.Add(new Value() { Code = "0482", Label = "Flen" });
            myOut.Values.Add(new Value() { Code = "0483", Label = "Katrineholm" });
            myOut.Values.Add(new Value() { Code = "0484", Label = "Eskilstuna" });
            myOut.Values.Add(new Value() { Code = "0486", Label = "Strängnäs" });
            myOut.Values.Add(new Value() { Code = "0488", Label = "Trosa" });
            myOut.Values.Add(new Value() { Code = "05", Label = "Östergötlands län" });
            myOut.Values.Add(new Value() { Code = "0509", Label = "Ödeshög" });
            myOut.Values.Add(new Value() { Code = "0512", Label = "Ydre" });
            myOut.Values.Add(new Value() { Code = "0513", Label = "Kinda" });
            myOut.Values.Add(new Value() { Code = "0560", Label = "Boxholm" });
            myOut.Values.Add(new Value() { Code = "0561", Label = "Åtvidaberg" });
            myOut.Values.Add(new Value() { Code = "0562", Label = "Finspång" });
            myOut.Values.Add(new Value() { Code = "0563", Label = "Valdemarsvik" });
            myOut.Values.Add(new Value() { Code = "0580", Label = "Linköping" });
            myOut.Values.Add(new Value() { Code = "0581", Label = "Norrköping" });
            myOut.Values.Add(new Value() { Code = "0582", Label = "Söderköping" });
            myOut.Values.Add(new Value() { Code = "0583", Label = "Motala" });
            myOut.Values.Add(new Value() { Code = "0584", Label = "Vadstena" });
            myOut.Values.Add(new Value() { Code = "0586", Label = "Mjölby" });
            myOut.Values.Add(new Value() { Code = "06", Label = "Jönköpings län" });
            myOut.Values.Add(new Value() { Code = "0604", Label = "Aneby" });
            myOut.Values.Add(new Value() { Code = "0617", Label = "Gnosjö" });
            myOut.Values.Add(new Value() { Code = "0642", Label = "Mullsjö" });
            myOut.Values.Add(new Value() { Code = "0643", Label = "Habo" });
            myOut.Values.Add(new Value() { Code = "0662", Label = "Gislaved" });
            myOut.Values.Add(new Value() { Code = "0665", Label = "Vaggeryd" });
            myOut.Values.Add(new Value() { Code = "0680", Label = "Jönköping" });
            myOut.Values.Add(new Value() { Code = "0682", Label = "Nässjö" });
            myOut.Values.Add(new Value() { Code = "0683", Label = "Värnamo" });
            myOut.Values.Add(new Value() { Code = "0684", Label = "Sävsjö" });
            myOut.Values.Add(new Value() { Code = "0685", Label = "Vetlanda" });
            myOut.Values.Add(new Value() { Code = "0686", Label = "Eksjö" });
            myOut.Values.Add(new Value() { Code = "0687", Label = "Tranås" });
            myOut.Values.Add(new Value() { Code = "07", Label = "Kronobergs län" });
            myOut.Values.Add(new Value() { Code = "0760", Label = "Uppvidinge" });
            myOut.Values.Add(new Value() { Code = "0761", Label = "Lessebo" });
            myOut.Values.Add(new Value() { Code = "0763", Label = "Tingsryd" });
            myOut.Values.Add(new Value() { Code = "0764", Label = "Alvesta" });
            myOut.Values.Add(new Value() { Code = "0765", Label = "Älmhult" });
            myOut.Values.Add(new Value() { Code = "0767", Label = "Markaryd" });
            myOut.Values.Add(new Value() { Code = "0780", Label = "Växjö" });
            myOut.Values.Add(new Value() { Code = "0781", Label = "Ljungby" });
            myOut.Values.Add(new Value() { Code = "08", Label = "Kalmar län" });
            myOut.Values.Add(new Value() { Code = "0821", Label = "Högsby" });
            myOut.Values.Add(new Value() { Code = "0834", Label = "Torsås" });
            myOut.Values.Add(new Value() { Code = "0840", Label = "Mörbylånga" });
            myOut.Values.Add(new Value() { Code = "0860", Label = "Hultsfred" });
            myOut.Values.Add(new Value() { Code = "0861", Label = "Mönsterås" });
            myOut.Values.Add(new Value() { Code = "0862", Label = "Emmaboda" });
            myOut.Values.Add(new Value() { Code = "0880", Label = "Kalmar" });
            myOut.Values.Add(new Value() { Code = "0881", Label = "Nybro" });
            myOut.Values.Add(new Value() { Code = "0882", Label = "Oskarshamn" });
            myOut.Values.Add(new Value() { Code = "0883", Label = "Västervik" });
            myOut.Values.Add(new Value() { Code = "0884", Label = "Vimmerby" });
            myOut.Values.Add(new Value() { Code = "0885", Label = "Borgholm" });
            myOut.Values.Add(new Value() { Code = "09", Label = "Gotlands län" });
            myOut.Values.Add(new Value() { Code = "0980", Label = "Gotland" });
            myOut.Values.Add(new Value() { Code = "10", Label = "Blekinge län" });
            myOut.Values.Add(new Value() { Code = "1060", Label = "Olofström" });
            myOut.Values.Add(new Value() { Code = "1080", Label = "Karlskrona" });
            myOut.Values.Add(new Value() { Code = "1081", Label = "Ronneby" });
            myOut.Values.Add(new Value() { Code = "1082", Label = "Karlshamn" });
            myOut.Values.Add(new Value() { Code = "1083", Label = "Sölvesborg" });
            myOut.Values.Add(new Value() { Code = "12", Label = "Skåne län" });
            myOut.Values.Add(new Value() { Code = "1214", Label = "Svalöv" });
            myOut.Values.Add(new Value() { Code = "1230", Label = "Staffanstorp" });
            myOut.Values.Add(new Value() { Code = "1231", Label = "Burlöv" });
            myOut.Values.Add(new Value() { Code = "1233", Label = "Vellinge" });
            myOut.Values.Add(new Value() { Code = "1256", Label = "Östra Göinge" });
            myOut.Values.Add(new Value() { Code = "1257", Label = "Örkelljunga" });
            myOut.Values.Add(new Value() { Code = "1260", Label = "Bjuv" });
            myOut.Values.Add(new Value() { Code = "1261", Label = "Kävlinge" });
            myOut.Values.Add(new Value() { Code = "1262", Label = "Lomma" });
            myOut.Values.Add(new Value() { Code = "1263", Label = "Svedala" });
            myOut.Values.Add(new Value() { Code = "1264", Label = "Skurup" });
            myOut.Values.Add(new Value() { Code = "1265", Label = "Sjöbo" });
            myOut.Values.Add(new Value() { Code = "1266", Label = "Hörby" });
            myOut.Values.Add(new Value() { Code = "1267", Label = "Höör" });
            myOut.Values.Add(new Value() { Code = "1270", Label = "Tomelilla" });
            myOut.Values.Add(new Value() { Code = "1272", Label = "Bromölla" });
            myOut.Values.Add(new Value() { Code = "1273", Label = "Osby" });
            myOut.Values.Add(new Value() { Code = "1275", Label = "Perstorp" });
            myOut.Values.Add(new Value() { Code = "1276", Label = "Klippan" });
            myOut.Values.Add(new Value() { Code = "1277", Label = "Åstorp" });
            myOut.Values.Add(new Value() { Code = "1278", Label = "Båstad" });
            myOut.Values.Add(new Value() { Code = "1280", Label = "Malmö" });
            myOut.Values.Add(new Value() { Code = "1281", Label = "Lund" });
            myOut.Values.Add(new Value() { Code = "1282", Label = "Landskrona" });
            myOut.Values.Add(new Value() { Code = "1283", Label = "Helsingborg" });
            myOut.Values.Add(new Value() { Code = "1284", Label = "Höganäs" });
            myOut.Values.Add(new Value() { Code = "1285", Label = "Eslöv" });
            myOut.Values.Add(new Value() { Code = "1286", Label = "Ystad" });
            myOut.Values.Add(new Value() { Code = "1287", Label = "Trelleborg" });
            myOut.Values.Add(new Value() { Code = "1290", Label = "Kristianstad" });
            myOut.Values.Add(new Value() { Code = "1291", Label = "Simrishamn" });
            myOut.Values.Add(new Value() { Code = "1292", Label = "Ängelholm" });
            myOut.Values.Add(new Value() { Code = "1293", Label = "Hässleholm" });
            myOut.Values.Add(new Value() { Code = "13", Label = "Hallands län" });
            myOut.Values.Add(new Value() { Code = "1315", Label = "Hylte" });
            myOut.Values.Add(new Value() { Code = "1380", Label = "Halmstad" });
            myOut.Values.Add(new Value() { Code = "1381", Label = "Laholm" });
            myOut.Values.Add(new Value() { Code = "1382", Label = "Falkenberg" });
            myOut.Values.Add(new Value() { Code = "1383", Label = "Varberg" });
            myOut.Values.Add(new Value() { Code = "1384", Label = "Kungsbacka" });
            myOut.Values.Add(new Value() { Code = "14", Label = "Västra Götalands län" });
            myOut.Values.Add(new Value() { Code = "1401", Label = "Härryda" });
            myOut.Values.Add(new Value() { Code = "1402", Label = "Partille" });
            myOut.Values.Add(new Value() { Code = "1407", Label = "Öckerö" });
            myOut.Values.Add(new Value() { Code = "1415", Label = "Stenungsund" });
            myOut.Values.Add(new Value() { Code = "1419", Label = "Tjörn" });
            myOut.Values.Add(new Value() { Code = "1421", Label = "Orust" });
            myOut.Values.Add(new Value() { Code = "1427", Label = "Sotenäs" });
            myOut.Values.Add(new Value() { Code = "1430", Label = "Munkedal" });
            myOut.Values.Add(new Value() { Code = "1435", Label = "Tanum" });
            myOut.Values.Add(new Value() { Code = "1438", Label = "Dals-Ed" });
            myOut.Values.Add(new Value() { Code = "1439", Label = "Färgelanda" });
            myOut.Values.Add(new Value() { Code = "1440", Label = "Ale" });
            myOut.Values.Add(new Value() { Code = "1441", Label = "Lerum" });
            myOut.Values.Add(new Value() { Code = "1442", Label = "Vårgårda" });
            myOut.Values.Add(new Value() { Code = "1443", Label = "Bollebygd" });
            myOut.Values.Add(new Value() { Code = "1444", Label = "Grästorp" });
            myOut.Values.Add(new Value() { Code = "1445", Label = "Essunga" });
            myOut.Values.Add(new Value() { Code = "1446", Label = "Karlsborg" });
            myOut.Values.Add(new Value() { Code = "1447", Label = "Gullspång" });
            myOut.Values.Add(new Value() { Code = "1452", Label = "Tranemo" });
            myOut.Values.Add(new Value() { Code = "1460", Label = "Bengtsfors" });
            myOut.Values.Add(new Value() { Code = "1461", Label = "Mellerud" });
            myOut.Values.Add(new Value() { Code = "1462", Label = "Lilla Edet" });
            myOut.Values.Add(new Value() { Code = "1463", Label = "Mark" });
            myOut.Values.Add(new Value() { Code = "1465", Label = "Svenljunga" });
            myOut.Values.Add(new Value() { Code = "1466", Label = "Herrljunga" });
            myOut.Values.Add(new Value() { Code = "1470", Label = "Vara" });
            myOut.Values.Add(new Value() { Code = "1471", Label = "Götene" });
            myOut.Values.Add(new Value() { Code = "1472", Label = "Tibro" });
            myOut.Values.Add(new Value() { Code = "1473", Label = "Töreboda" });
            myOut.Values.Add(new Value() { Code = "1480", Label = "Göteborg" });
            myOut.Values.Add(new Value() { Code = "1481", Label = "Mölndal" });
            myOut.Values.Add(new Value() { Code = "1482", Label = "Kungälv" });
            myOut.Values.Add(new Value() { Code = "1484", Label = "Lysekil" });
            myOut.Values.Add(new Value() { Code = "1485", Label = "Uddevalla" });
            myOut.Values.Add(new Value() { Code = "1486", Label = "Strömstad" });
            myOut.Values.Add(new Value() { Code = "1487", Label = "Vänersborg" });
            myOut.Values.Add(new Value() { Code = "1488", Label = "Trollhättan" });
            myOut.Values.Add(new Value() { Code = "1489", Label = "Alingsås" });
            myOut.Values.Add(new Value() { Code = "1490", Label = "Borås" });
            myOut.Values.Add(new Value() { Code = "1491", Label = "Ulricehamn" });
            myOut.Values.Add(new Value() { Code = "1492", Label = "Åmål" });
            myOut.Values.Add(new Value() { Code = "1493", Label = "Mariestad" });
            myOut.Values.Add(new Value() { Code = "1494", Label = "Lidköping" });
            myOut.Values.Add(new Value() { Code = "1495", Label = "Skara" });
            myOut.Values.Add(new Value() { Code = "1496", Label = "Skövde" });
            myOut.Values.Add(new Value() { Code = "1497", Label = "Hjo" });
            myOut.Values.Add(new Value() { Code = "1498", Label = "Tidaholm" });
            myOut.Values.Add(new Value() { Code = "1499", Label = "Falköping" });
            myOut.Values.Add(new Value() { Code = "17", Label = "Värmlands län" });
            myOut.Values.Add(new Value() { Code = "1715", Label = "Kil" });
            myOut.Values.Add(new Value() { Code = "1730", Label = "Eda" });
            myOut.Values.Add(new Value() { Code = "1737", Label = "Torsby" });
            myOut.Values.Add(new Value() { Code = "1760", Label = "Storfors" });
            myOut.Values.Add(new Value() { Code = "1761", Label = "Hammarö" });
            myOut.Values.Add(new Value() { Code = "1762", Label = "Munkfors" });
            myOut.Values.Add(new Value() { Code = "1763", Label = "Forshaga" });
            myOut.Values.Add(new Value() { Code = "1764", Label = "Grums" });
            myOut.Values.Add(new Value() { Code = "1765", Label = "Årjäng" });
            myOut.Values.Add(new Value() { Code = "1766", Label = "Sunne" });
            myOut.Values.Add(new Value() { Code = "1780", Label = "Karlstad" });
            myOut.Values.Add(new Value() { Code = "1781", Label = "Kristinehamn" });
            myOut.Values.Add(new Value() { Code = "1782", Label = "Filipstad" });
            myOut.Values.Add(new Value() { Code = "1783", Label = "Hagfors" });
            myOut.Values.Add(new Value() { Code = "1784", Label = "Arvika" });
            myOut.Values.Add(new Value() { Code = "1785", Label = "Säffle" });
            myOut.Values.Add(new Value() { Code = "18", Label = "Örebro län" });
            myOut.Values.Add(new Value() { Code = "1814", Label = "Lekeberg" });
            myOut.Values.Add(new Value() { Code = "1860", Label = "Laxå" });
            myOut.Values.Add(new Value() { Code = "1861", Label = "Hallsberg" });
            myOut.Values.Add(new Value() { Code = "1862", Label = "Degerfors" });
            myOut.Values.Add(new Value() { Code = "1863", Label = "Hällefors" });
            myOut.Values.Add(new Value() { Code = "1864", Label = "Ljusnarsberg" });
            myOut.Values.Add(new Value() { Code = "1880", Label = "Örebro" });
            myOut.Values.Add(new Value() { Code = "1881", Label = "Kumla" });
            myOut.Values.Add(new Value() { Code = "1882", Label = "Askersund" });
            myOut.Values.Add(new Value() { Code = "1883", Label = "Karlskoga" });
            myOut.Values.Add(new Value() { Code = "1884", Label = "Nora" });
            myOut.Values.Add(new Value() { Code = "1885", Label = "Lindesberg" });
            myOut.Values.Add(new Value() { Code = "19", Label = "Västmanlands län" });
            myOut.Values.Add(new Value() { Code = "1904", Label = "Skinnskatteberg" });
            myOut.Values.Add(new Value() { Code = "1907", Label = "Surahammar" });
            myOut.Values.Add(new Value() { Code = "1960", Label = "Kungsör" });
            myOut.Values.Add(new Value() { Code = "1961", Label = "Hallstahammar" });
            myOut.Values.Add(new Value() { Code = "1962", Label = "Norberg" });
            myOut.Values.Add(new Value() { Code = "1980", Label = "Västerås" });
            myOut.Values.Add(new Value() { Code = "1981", Label = "Sala" });
            myOut.Values.Add(new Value() { Code = "1982", Label = "Fagersta" });
            myOut.Values.Add(new Value() { Code = "1983", Label = "Köping" });
            myOut.Values.Add(new Value() { Code = "1984", Label = "Arboga" });
            myOut.Values.Add(new Value() { Code = "20", Label = "Dalarnas län" });
            myOut.Values.Add(new Value() { Code = "2021", Label = "Vansbro" });
            myOut.Values.Add(new Value() { Code = "2023", Label = "Malung-Sälen" });
            myOut.Values.Add(new Value() { Code = "2026", Label = "Gagnef" });
            myOut.Values.Add(new Value() { Code = "2029", Label = "Leksand" });
            myOut.Values.Add(new Value() { Code = "2031", Label = "Rättvik" });
            myOut.Values.Add(new Value() { Code = "2034", Label = "Orsa" });
            myOut.Values.Add(new Value() { Code = "2039", Label = "Älvdalen" });
            myOut.Values.Add(new Value() { Code = "2061", Label = "Smedjebacken" });
            myOut.Values.Add(new Value() { Code = "2062", Label = "Mora" });
            myOut.Values.Add(new Value() { Code = "2080", Label = "Falun" });
            myOut.Values.Add(new Value() { Code = "2081", Label = "Borlänge" });
            myOut.Values.Add(new Value() { Code = "2082", Label = "Säter" });
            myOut.Values.Add(new Value() { Code = "2083", Label = "Hedemora" });
            myOut.Values.Add(new Value() { Code = "2084", Label = "Avesta" });
            myOut.Values.Add(new Value() { Code = "2085", Label = "Ludvika" });
            myOut.Values.Add(new Value() { Code = "21", Label = "Gävleborgs län" });
            myOut.Values.Add(new Value() { Code = "2101", Label = "Ockelbo" });
            myOut.Values.Add(new Value() { Code = "2104", Label = "Hofors" });
            myOut.Values.Add(new Value() { Code = "2121", Label = "Ovanåker" });
            myOut.Values.Add(new Value() { Code = "2132", Label = "Nordanstig" });
            myOut.Values.Add(new Value() { Code = "2161", Label = "Ljusdal" });
            myOut.Values.Add(new Value() { Code = "2180", Label = "Gävle" });
            myOut.Values.Add(new Value() { Code = "2181", Label = "Sandviken" });
            myOut.Values.Add(new Value() { Code = "2182", Label = "Söderhamn" });
            myOut.Values.Add(new Value() { Code = "2183", Label = "Bollnäs" });
            myOut.Values.Add(new Value() { Code = "2184", Label = "Hudiksvall" });
            myOut.Values.Add(new Value() { Code = "22", Label = "Västernorrlands län" });
            myOut.Values.Add(new Value() { Code = "2260", Label = "Ånge" });
            myOut.Values.Add(new Value() { Code = "2262", Label = "Timrå" });
            myOut.Values.Add(new Value() { Code = "2280", Label = "Härnösand" });
            myOut.Values.Add(new Value() { Code = "2281", Label = "Sundsvall" });
            myOut.Values.Add(new Value() { Code = "2282", Label = "Kramfors" });
            myOut.Values.Add(new Value() { Code = "2283", Label = "Sollefteå" });
            myOut.Values.Add(new Value() { Code = "2284", Label = "Örnsköldsvik" });
            myOut.Values.Add(new Value() { Code = "23", Label = "Jämtlands län" });
            myOut.Values.Add(new Value() { Code = "2303", Label = "Ragunda" });
            myOut.Values.Add(new Value() { Code = "2305", Label = "Bräcke" });
            myOut.Values.Add(new Value() { Code = "2309", Label = "Krokom" });
            myOut.Values.Add(new Value() { Code = "2313", Label = "Strömsund" });
            myOut.Values.Add(new Value() { Code = "2321", Label = "Åre" });
            myOut.Values.Add(new Value() { Code = "2326", Label = "Berg" });
            myOut.Values.Add(new Value() { Code = "2361", Label = "Härjedalen" });
            myOut.Values.Add(new Value() { Code = "2380", Label = "Östersund" });
            myOut.Values.Add(new Value() { Code = "24", Label = "Västerbottens län" });
            myOut.Values.Add(new Value() { Code = "2401", Label = "Nordmaling" });
            myOut.Values.Add(new Value() { Code = "2403", Label = "Bjurholm" });
            myOut.Values.Add(new Value() { Code = "2404", Label = "Vindeln" });
            myOut.Values.Add(new Value() { Code = "2409", Label = "Robertsfors" });
            myOut.Values.Add(new Value() { Code = "2417", Label = "Norsjö" });
            myOut.Values.Add(new Value() { Code = "2418", Label = "Malå" });
            myOut.Values.Add(new Value() { Code = "2421", Label = "Storuman" });
            myOut.Values.Add(new Value() { Code = "2422", Label = "Sorsele" });
            myOut.Values.Add(new Value() { Code = "2425", Label = "Dorotea" });
            myOut.Values.Add(new Value() { Code = "2460", Label = "Vännäs" });
            myOut.Values.Add(new Value() { Code = "2462", Label = "Vilhelmina" });
            myOut.Values.Add(new Value() { Code = "2463", Label = "Åsele" });
            myOut.Values.Add(new Value() { Code = "2480", Label = "Umeå" });
            myOut.Values.Add(new Value() { Code = "2481", Label = "Lycksele" });
            myOut.Values.Add(new Value() { Code = "2482", Label = "Skellefteå" });
            myOut.Values.Add(new Value() { Code = "25", Label = "Norrbottens län" });
            myOut.Values.Add(new Value() { Code = "2505", Label = "Arvidsjaur" });
            myOut.Values.Add(new Value() { Code = "2506", Label = "Arjeplog" });
            myOut.Values.Add(new Value() { Code = "2510", Label = "Jokkmokk" });
            myOut.Values.Add(new Value() { Code = "2513", Label = "Överkalix" });
            myOut.Values.Add(new Value() { Code = "2514", Label = "Kalix" });
            myOut.Values.Add(new Value() { Code = "2518", Label = "Övertorneå" });
            myOut.Values.Add(new Value() { Code = "2521", Label = "Pajala" });
            myOut.Values.Add(new Value() { Code = "2523", Label = "Gällivare" });
            myOut.Values.Add(new Value() { Code = "2560", Label = "Älvsbyn" });
            myOut.Values.Add(new Value() { Code = "2580", Label = "Luleå" });
            myOut.Values.Add(new Value() { Code = "2581", Label = "Piteå" });
            myOut.Values.Add(new Value() { Code = "2582", Label = "Boden" });
            myOut.Values.Add(new Value() { Code = "2583", Label = "Haparanda" });
            myOut.Values.Add(new Value() { Code = "2584", Label = "Kiruna" });

            #endregion values

            #region codeLists
            myOut.CodeLists = new List<CodeListInformation>();

            myOut.CodeLists.Add(new CodeListInformation()
            {
                Id = "vs_RegionKommun07",
                Label = "Kommuner",
                Links = new List<Link>() { new() { Rel = "metadata", Href = "https://my-site.com/api/v2/tables/TAB638/codelists/vs_RegionKommun07" } }
            });

            myOut.CodeLists.Add(new CodeListInformation()
            {
                Id = "vs_RegionLän07",
                Label = "Län",
                Links = new List<Link>() { new() { Rel = "metadata", Href = "https://my-site.com/api/v2/tables/TAB638/codelists/vs_RegionLän07" } }
            });

            myOut.CodeLists.Add(new CodeListInformation()
            {
                Id = "vs_RegionRiket99",
                Label = "Riket",
                Links = new List<Link>() { new() { Rel = "metadata", Href = "https://my-site.com/api/v2/tables/TAB638/codelists/vs_RegionRiket99" } }
            });

            myOut.CodeLists.Add(new CodeListInformation()
            {
                Id = "agg_RegionA-region_2",
                Label = "A-regioner",
                Links = new List<Link>() { new() { Rel = "metadata", Href = "https://my-site.com/api/v2/tables/TAB638/codelists/agg_RegionA-region_2" } }
            });

            myOut.CodeLists.Add(new CodeListInformation()
            {
                Id = "agg_RegionKommungrupp2005-_1",
                Label = "Kommungrupper (SKL:s) 2005",
                Links = new List<Link>() { new()
                                {
                                    Rel ="metadata",
                                   Href ="https://my-site.com/api/v2/tables/TAB638/codelists/agg_RegionKommungrupp2005-_1"} }
            });
            myOut.CodeLists.Add(new CodeListInformation()
            {
                Id = "agg_RegionKommungrupp2011-",
                Label = "Kommungrupper (SKL:s) 2011",
                Links = new List<Link>() { new()
                                {
                                    Rel ="metadata",
                                   Href ="https://my-site.com/api/v2/tables/TAB638/codelists/agg_RegionKommungrupp2011-"} }
            });
            myOut.CodeLists.Add(new CodeListInformation()
            {
                Id = "agg_RegionKommungrupp2017-",
                Label = "Kommungrupper (SKL:s) 2017",
                Links = new List<Link>() { new()
                                {
                                    Rel ="metadata",
                                   Href ="https://my-site.com/api/v2/tables/TAB638/codelists/agg_RegionKommungrupp2017-"} }
            });
            myOut.CodeLists.Add(new CodeListInformation()
            {
                Id = "agg_RegionLA1998",
                Label = "Lokalaarbetsmarknader 1998",
                Links = new List<Link>() { new()
                                {
                                    Rel ="metadata",
                                   Href ="https://my-site.com/api/v2/tables/TAB638/codelists/agg_RegionLA1998"} }
            });
            myOut.CodeLists.Add(new CodeListInformation()
            {
                Id = "agg_RegionLA2003_1",
                Label = "Lokalaarbetsmarknader 2003",
                Links = new List<Link>() { new()
                                {
                                    Rel ="metadata",
                                   Href ="https://my-site.com/api/v2/tables/TAB638/codelists/agg_RegionLA2003_1"} }
            });
            myOut.CodeLists.Add(new CodeListInformation()
            {
                Id = "agg_RegionLA2008",
                Label = "Lokalaarbetsmarknader 2008",
                Links = new List<Link>() { new()
                                {
                                    Rel ="metadata",
                                   Href ="https://my-site.com/api/v2/tables/TAB638/codelists/agg_RegionLA2008"} }
            });
            myOut.CodeLists.Add(new CodeListInformation()
            {
                Id = "agg_RegionLA2013",
                Label = "Lokalaarbetsmarknader 2013",
                Links = new List<Link>() { new()
                                {
                                    Rel ="metadata",
                                   Href ="https://my-site.com/api/v2/tables/TAB638/codelists/agg_RegionLA2013"} }
            });
            myOut.CodeLists.Add(new CodeListInformation()
            {
                Id = "agg_RegionLA2018",
                Label = "Lokalaarbetsmarknader 2018",
                Links = new List<Link>() { new()
                                {
                                    Rel ="metadata",
                                   Href ="https://my-site.com/api/v2/tables/TAB638/codelists/agg_Ålder5år"} }
            });
            myOut.CodeLists.Add(new CodeListInformation()
            {
                Id = "agg_RegionStoromr-04_2",
                Label = "Sorstadsområder -2004",
                Links = new List<Link>() { new()
                                {
                                    Rel ="metadata",
                                   Href ="https://my-site.com/api/v2/tables/TAB638/codelists/agg_RegionStoromr-04_2"} }
            });
            myOut.CodeLists.Add(new CodeListInformation()
            {
                Id = "agg_RegionStoromr05-_1",
                Label = "Sorstadsområder 2005-",
                Links = new List<Link>() { new()
                                {
                                    Rel ="metadata",
                                   Href ="https://my-site.com/api/v2/tables/TAB638/codelists/agg_RegionStoromr05-_1"} }
            });
            myOut.CodeLists.Add(new CodeListInformation()
            {
                Id = "agg_RegionNUTS1_2008",
                Label = "NUTS1 fr.o.m 2008",
                Links = new List<Link>() { new()
                                {
                                    Rel ="metadata",
                                   Href ="https://my-site.com/api/v2/tables/TAB638/codelists/agg_RegionNUTS1_2008"} }
            });
            myOut.CodeLists.Add(new CodeListInformation()
            {
                Id = "agg_RegionNUTS2_2008",
                Label = "NUTS2 fr.o.m 2008",
                Links = new List<Link>() { new()
                                {
                                    Rel ="metadata",
                                   Href ="https://my-site.com/api/v2/tables/TAB638/codelists/agg_RegionNUTS2_2008"} }
            });
            myOut.CodeLists.Add(new CodeListInformation()
            {
                Id = "agg_RegionNUTS3_2008",
                Label = "NUTS3 fr.o.m 2008",
                Links = new List<Link>() { new()
                                {
                                    Rel ="metadata",
                                   Href ="https://my-site.com/api/v2/tables/TAB638/codelists/agg_RegionNUTS3_2008"} }
            });
            #endregion codeLists

            return myOut;

        }

        



        private DatasetDimensionValue GetKonDatasetDimensionValue()
        {
            DatasetDimensionValue myOut = new DatasetDimensionValue();
            myOut.Label = "kön";

            myOut.Category = new JsonstatCategory();
            myOut.Category.Index = new Dictionary<string, int>();
            myOut.Category.Index.Add("1", 0);
            myOut.Category.Index.Add("2", 1);


            myOut.Category.Label = new Dictionary<string, string>();
            myOut.Category.Label.Add("1", "män");
            myOut.Category.Label.Add("2", "kvinnor");

            myOut.Extension = new ExtensionDimension() { Elimination = true };

            myOut.Link = new JsonstatExtensionLink();
            myOut.Link.Describedby = new List<DimensionExtension>() { new DimensionExtension() { Extension = new Dictionary<string, string>() } };

            myOut.Link.Describedby[0].Extension.Add("Kon", "Kön");

            return myOut;
        }

        private DatasetDimensionValue GetTidDatasetDimensionValue()
        {
            DatasetDimensionValue myOut = new DatasetDimensionValue();
            myOut.Label = "år";

            myOut.Category = new JsonstatCategory();
            myOut.Category.Index = new Dictionary<string, int>();
            myOut.Category.Index.Add("2021", 0);
            //myOut.Category.Index.Add("2", 1);


            myOut.Category.Label = new Dictionary<string, string>();
            myOut.Category.Label.Add("2021", "2021");

            myOut.Extension = new ExtensionDimension() { Elimination = false, Frequency = "A", FirstPeriod = "1968", LastPeriod = "2021" };



            return myOut;
        }

        private DatasetDimensionValue GetContentsCodeDatasetDimensionValue()
        {
            DatasetDimensionValue myOut = new DatasetDimensionValue();
            myOut.Label = "tabellinnehåll";

            myOut.Category = new JsonstatCategory();
            myOut.Extension = new ExtensionDimension();
            myOut.Extension.Elimination = false;
            myOut.Extension.ValueNote = new Dictionary<string, List<Note>>();

            myOut.Extension.ValueNote.Add("BE0101N1",
                new List<Note>()
                {
                    new()
                    {
                        Mandatory = true,
                        Text =
                            "Uppgifterna avser förhållandena den 31 december för valt/valda år enligt den regionala indelning som gäller den 1 januari året efter."
                    }
                });

            myOut.Extension.ValueNote.Add("BE0101N2",
                new List<Note>()
                {
                    new()
                    {
                        Mandatory = true,
                        Text =
                            "Folkökningen definieras som skillnaden mellan folkmängden vid årets början och årets slut."
                    }
                });

            myOut.Extension.Refperiod = new Dictionary<string, string>();
            myOut.Extension.Refperiod.Add("BE0101N1", "31 December each year");
            myOut.Extension.Refperiod.Add("BE0101N2", "31 December each year");

            myOut.Category.Index = new Dictionary<string, int>();
            myOut.Category.Index.Add("BE0101N1", 0);
            myOut.Category.Index.Add("BE0101N2", 1);


            myOut.Category.Label = new Dictionary<string, string>();
            myOut.Category.Label.Add("BE0101N1", "Folkmängd");
            myOut.Category.Label.Add("BE0101N2", "Folkökning");

            myOut.Category.Unit = new Dictionary<string, JsonstatCategoryUnitValue>();
            myOut.Category.Unit.Add("BE0101N1", new JsonstatCategoryUnitValue() { Base = "antal", Decimals = 0 });
            myOut.Category.Unit.Add("BE0101N2", new JsonstatCategoryUnitValue() { Base = "antal", Decimals = 0 });

            //myOut.Extension = new ExtensionDimension() { Elimination = false };

            return myOut;
        }


        private DatasetDimensionValue GetCivilstandDatasetDimensionValue()
        {
            DatasetDimensionValue myOut = new DatasetDimensionValue();
            myOut.Label = "civilstånd";

            myOut.Category = new JsonstatCategory();
            myOut.Category.Index = new Dictionary<string, int>();
            myOut.Category.Index.Add("OG", 0);
            myOut.Category.Index.Add("G", 1);
            myOut.Category.Index.Add("SK", 2);
            myOut.Category.Index.Add("ÄNKL", 3);

            myOut.Category.Label = new Dictionary<string, string>();
            myOut.Category.Label.Add("OG", "ogifta");
            myOut.Category.Label.Add("G", "gifta");
            myOut.Category.Label.Add("SK", "skilda");
            myOut.Category.Label.Add("ÄNKL", "änkor/änklingar");

            myOut.Extension = new ExtensionDimension() { Elimination = true };
            return myOut;
        }


        private DatasetDimensionValue GetAlderDatasetDimensionValue()
        {
            DatasetDimensionValue myOut = new DatasetDimensionValue();
            myOut.Label = "ålder";

            myOut.Category = new JsonstatCategory();
            myOut.Category.Index = new Dictionary<string, int>();
            myOut.Category.Index.Add("0", 0);
            myOut.Category.Index.Add("1", 1);
            myOut.Category.Index.Add("2", 2);
            myOut.Category.Index.Add("3", 3);
            myOut.Category.Index.Add("4", 4);
            myOut.Category.Index.Add("5", 5);
            myOut.Category.Index.Add("6", 6);
            myOut.Category.Index.Add("7", 7);
            myOut.Category.Index.Add("8", 8);
            myOut.Category.Index.Add("9", 9);
            myOut.Category.Index.Add("10", 10);
            myOut.Category.Index.Add("11", 11);
            myOut.Category.Index.Add("12", 12);
            myOut.Category.Index.Add("13", 13);
            myOut.Category.Index.Add("14", 14);
            myOut.Category.Index.Add("15", 15);
            myOut.Category.Index.Add("16", 16);
            myOut.Category.Index.Add("17", 17);
            myOut.Category.Index.Add("18", 18);
            myOut.Category.Index.Add("19", 19);
            myOut.Category.Index.Add("20", 20);
            myOut.Category.Index.Add("21", 21);
            myOut.Category.Index.Add("22", 22);
            myOut.Category.Index.Add("23", 23);
            myOut.Category.Index.Add("24", 24);
            myOut.Category.Index.Add("25", 25);
            myOut.Category.Index.Add("26", 26);
            myOut.Category.Index.Add("27", 27);
            myOut.Category.Index.Add("28", 28);
            myOut.Category.Index.Add("29", 29);
            myOut.Category.Index.Add("30", 30);
            myOut.Category.Index.Add("31", 31);
            myOut.Category.Index.Add("32", 32);
            myOut.Category.Index.Add("33", 33);
            myOut.Category.Index.Add("34", 34);
            myOut.Category.Index.Add("35", 35);
            myOut.Category.Index.Add("36", 36);
            myOut.Category.Index.Add("37", 37);
            myOut.Category.Index.Add("38", 38);
            myOut.Category.Index.Add("39", 39);
            myOut.Category.Index.Add("40", 40);
            myOut.Category.Index.Add("41", 41);
            myOut.Category.Index.Add("42", 42);
            myOut.Category.Index.Add("43", 43);
            myOut.Category.Index.Add("44", 44);
            myOut.Category.Index.Add("45", 45);
            myOut.Category.Index.Add("46", 46);
            myOut.Category.Index.Add("47", 47);
            myOut.Category.Index.Add("48", 48);
            myOut.Category.Index.Add("49", 49);
            myOut.Category.Index.Add("50", 50);
            myOut.Category.Index.Add("51", 51);
            myOut.Category.Index.Add("52", 52);
            myOut.Category.Index.Add("53", 53);
            myOut.Category.Index.Add("54", 54);
            myOut.Category.Index.Add("55", 55);
            myOut.Category.Index.Add("56", 56);
            myOut.Category.Index.Add("57", 57);
            myOut.Category.Index.Add("58", 58);
            myOut.Category.Index.Add("59", 59);
            myOut.Category.Index.Add("60", 60);
            myOut.Category.Index.Add("61", 61);
            myOut.Category.Index.Add("62", 62);
            myOut.Category.Index.Add("63", 63);
            myOut.Category.Index.Add("64", 64);
            myOut.Category.Index.Add("65", 65);
            myOut.Category.Index.Add("66", 66);
            myOut.Category.Index.Add("67", 67);
            myOut.Category.Index.Add("68", 68);
            myOut.Category.Index.Add("69", 69);
            myOut.Category.Index.Add("70", 70);
            myOut.Category.Index.Add("71", 71);
            myOut.Category.Index.Add("72", 72);
            myOut.Category.Index.Add("73", 73);
            myOut.Category.Index.Add("74", 74);
            myOut.Category.Index.Add("75", 75);
            myOut.Category.Index.Add("76", 76);
            myOut.Category.Index.Add("77", 77);
            myOut.Category.Index.Add("78", 78);
            myOut.Category.Index.Add("79", 79);
            myOut.Category.Index.Add("80", 80);
            myOut.Category.Index.Add("81", 81);
            myOut.Category.Index.Add("82", 82);
            myOut.Category.Index.Add("83", 83);
            myOut.Category.Index.Add("84", 84);
            myOut.Category.Index.Add("85", 85);
            myOut.Category.Index.Add("86", 86);
            myOut.Category.Index.Add("87", 87);
            myOut.Category.Index.Add("88", 88);
            myOut.Category.Index.Add("89", 89);
            myOut.Category.Index.Add("90", 90);
            myOut.Category.Index.Add("91", 91);
            myOut.Category.Index.Add("92", 92);
            myOut.Category.Index.Add("93", 93);
            myOut.Category.Index.Add("94", 94);
            myOut.Category.Index.Add("95", 95);
            myOut.Category.Index.Add("96", 96);
            myOut.Category.Index.Add("97", 97);
            myOut.Category.Index.Add("98", 98);
            myOut.Category.Index.Add("99", 99);
            myOut.Category.Index.Add("100+", 100);
            myOut.Category.Index.Add("tot", 101);

            myOut.Category.Label = new Dictionary<string, string>();
            myOut.Category.Label.Add("0", "0 år");
            myOut.Category.Label.Add("1", "1 år");
            myOut.Category.Label.Add("2", "2 år");
            myOut.Category.Label.Add("3", "3 år");
            myOut.Category.Label.Add("4", "4 år");
            myOut.Category.Label.Add("5", "5 år");
            myOut.Category.Label.Add("6", "6 år");
            myOut.Category.Label.Add("7", "7 år");
            myOut.Category.Label.Add("8", "8 år");
            myOut.Category.Label.Add("9", "9 år");
            myOut.Category.Label.Add("10", "10 år");
            myOut.Category.Label.Add("11", "11 år");
            myOut.Category.Label.Add("12", "12 år");
            myOut.Category.Label.Add("13", "13 år");
            myOut.Category.Label.Add("14", "14 år");
            myOut.Category.Label.Add("15", "15 år");
            myOut.Category.Label.Add("16", "16 år");
            myOut.Category.Label.Add("17", "17 år");
            myOut.Category.Label.Add("18", "18 år");
            myOut.Category.Label.Add("19", "19 år");
            myOut.Category.Label.Add("20", "20 år");
            myOut.Category.Label.Add("21", "21 år");
            myOut.Category.Label.Add("22", "22 år");
            myOut.Category.Label.Add("23", "23 år");
            myOut.Category.Label.Add("24", "24 år");
            myOut.Category.Label.Add("25", "25 år");
            myOut.Category.Label.Add("26", "26 år");
            myOut.Category.Label.Add("27", "27 år");
            myOut.Category.Label.Add("28", "28 år");
            myOut.Category.Label.Add("29", "29 år");
            myOut.Category.Label.Add("30", "30 år");
            myOut.Category.Label.Add("31", "31 år");
            myOut.Category.Label.Add("32", "32 år");
            myOut.Category.Label.Add("33", "33 år");
            myOut.Category.Label.Add("34", "34 år");
            myOut.Category.Label.Add("35", "35 år");
            myOut.Category.Label.Add("36", "36 år");
            myOut.Category.Label.Add("37", "37 år");
            myOut.Category.Label.Add("38", "38 år");
            myOut.Category.Label.Add("39", "39 år");
            myOut.Category.Label.Add("40", "40 år");
            myOut.Category.Label.Add("41", "41 år");
            myOut.Category.Label.Add("42", "42 år");
            myOut.Category.Label.Add("43", "43 år");
            myOut.Category.Label.Add("44", "44 år");
            myOut.Category.Label.Add("45", "45 år");
            myOut.Category.Label.Add("46", "46 år");
            myOut.Category.Label.Add("47", "47 år");
            myOut.Category.Label.Add("48", "48 år");
            myOut.Category.Label.Add("49", "49 år");
            myOut.Category.Label.Add("50", "50 år");
            myOut.Category.Label.Add("51", "51 år");
            myOut.Category.Label.Add("52", "52 år");
            myOut.Category.Label.Add("53", "53 år");
            myOut.Category.Label.Add("54", "54 år");
            myOut.Category.Label.Add("55", "55 år");
            myOut.Category.Label.Add("56", "56 år");
            myOut.Category.Label.Add("57", "57 år");
            myOut.Category.Label.Add("58", "58 år");
            myOut.Category.Label.Add("59", "59 år");
            myOut.Category.Label.Add("60", "60 år");
            myOut.Category.Label.Add("61", "61 år");
            myOut.Category.Label.Add("62", "62 år");
            myOut.Category.Label.Add("63", "63 år");
            myOut.Category.Label.Add("64", "64 år");
            myOut.Category.Label.Add("65", "65 år");
            myOut.Category.Label.Add("66", "66 år");
            myOut.Category.Label.Add("67", "67 år");
            myOut.Category.Label.Add("68", "68 år");
            myOut.Category.Label.Add("69", "69 år");
            myOut.Category.Label.Add("70", "70 år");
            myOut.Category.Label.Add("71", "71 år");
            myOut.Category.Label.Add("72", "72 år");
            myOut.Category.Label.Add("73", "73 år");
            myOut.Category.Label.Add("74", "74 år");
            myOut.Category.Label.Add("75", "75 år");
            myOut.Category.Label.Add("76", "76 år");
            myOut.Category.Label.Add("77", "77 år");
            myOut.Category.Label.Add("78", "78 år");
            myOut.Category.Label.Add("79", "79 år");
            myOut.Category.Label.Add("80", "80 år");
            myOut.Category.Label.Add("81", "81 år");
            myOut.Category.Label.Add("82", "82 år");
            myOut.Category.Label.Add("83", "83 år");
            myOut.Category.Label.Add("84", "84 år");
            myOut.Category.Label.Add("85", "85 år");
            myOut.Category.Label.Add("86", "86 år");
            myOut.Category.Label.Add("87", "87 år");
            myOut.Category.Label.Add("88", "88 år");
            myOut.Category.Label.Add("89", "89 år");
            myOut.Category.Label.Add("90", "90 år");
            myOut.Category.Label.Add("91", "91 år");
            myOut.Category.Label.Add("92", "92 år");
            myOut.Category.Label.Add("93", "93 år");
            myOut.Category.Label.Add("94", "94 år");
            myOut.Category.Label.Add("95", "95 år");
            myOut.Category.Label.Add("96", "96 år");
            myOut.Category.Label.Add("97", "97 år");
            myOut.Category.Label.Add("98", "98 år");
            myOut.Category.Label.Add("99", "99 år");
            myOut.Category.Label.Add("100+", "100+ år");
            myOut.Category.Label.Add("tot", "totalt ålder");

            myOut.Extension = new ExtensionDimension() { Elimination = true, EliminationValueCode = "tot" };
            myOut.Extension.CodeLists = new List<CodeListInformation>();

            myOut.Extension.CodeLists.Add(new CodeListInformation()
            {
                Id = "vs_Ålder1årA",
                Label = "Ålder, 1 års-klasser",
                Links = new List<Link>() { new Link() { Rel = "metadata", Href = "https://my-site.com/api/v2/tables/TAB638/codelists/vs_Ålder1årA" } }
            });

            myOut.Extension.CodeLists.Add(new CodeListInformation()
            {
                Id = "vs_ÅlderTotA",
                Label = "Ålder, totalt, alla redovisade åldrar",
                Links = new List<Link>() { new Link() { Rel = "metadata", Href = "https://my-site.com/api/v2/tables/TAB638/codelists/vs_ÅlderTotA" } }
            });

            myOut.Extension.CodeLists.Add(new CodeListInformation()
            {
                Id = "agg_Ålder10år",
                Label = "10-årsklasser",
                Links = new List<Link>() { new Link() { Rel = "metadata", Href = "https://my-site.com/api/v2/tables/TAB638/codelists/agg_Ålder10år" } }
            });

            myOut.Extension.CodeLists.Add(new CodeListInformation()
            {
                Id = "agg_Ålder5år",
                Label = "5-årsklasser",
                Links = new List<Link>() { new Link() { Rel = "metadata", Href = "https://my-site.com/api/v2/tables/TAB638/codelists/agg_Ålder5år" } }
            });

            myOut.Link = new JsonstatExtensionLink();

            myOut.Link.Describedby = new List<DimensionExtension>() { new DimensionExtension() { Extension = new Dictionary<string, string>() } };

            myOut.Link.Describedby[0].Extension.Add("Alder", "Ålder");

            return myOut;
        }


        private DatasetDimensionValue GetRegionDatasetDimensionValue()
        {
            DatasetDimensionValue myOut = new DatasetDimensionValue();
            myOut.Label = "region";
            myOut.Category = new JsonstatCategory();
            myOut.Extension = new ExtensionDimension();
            myOut.Extension.ValueNote = new Dictionary<string, List<Note>>();
            myOut.Extension.Note = new List<Note>
            {
                new()
                {
                    Mandatory = true,
                    Text = "År 1968-1998 redovisas enligt regional indelning 1998-01-01."
                }
            };

            myOut.Extension.ValueNote.Add("0580",
                new List<Note>()
                {
                    new()
                    {
                        Mandatory = true,
                        Text =
                            "Från och med 2011-01-01 tillhör Storholmen Lidingö (0186) som tidigare tillhörde Vaxholm (0187)."
                    }
                });

            myOut.Extension.ValueNote.Add("0187",
                new List<Note>()
                {
                    new()
                    {
                        Mandatory = true,
                        Text =
                            "Från och med 2011-01-01 tillhör Storholmen Lidingö (0186) som tidigare tillhörde Vaxholm (0187)."
                    }
                });
            myOut.Extension.ValueNote.Add("0330",
                new List<Note>()
                {
                    new()
                    {
                        Mandatory = true,
                        Text =
                            "Ny regional indelning fr.o.m. 2003-01-01. Delar av Uppsala kommun bildar en ny kommun benämnd Knivsta kommun."
                    }
                });
            myOut.Extension.ValueNote.Add("0380",
                new List<Note>()
                {
                    new()
                    {
                        Mandatory = true,
                        Text =
                            "Ny regional indelning fr.o.m. 2003-01-01. Delar av Uppsala kommun bildar en ny kommun benämnd Knivsta kommun."
                    }
                });
            myOut.Extension.ValueNote.Add("0140",
                new List<Note>()
                {
                    new()
                    {
                        Mandatory = true,
                        Text =
                            "Ny regional indelning fr.o.m. 1999-01-01. Delar av Södertälje kommun (kod 0181) bildar en ny kommun benämnd Nykvarn (kod 0140)."
                    }
                });
            myOut.Extension.ValueNote.Add("0181",
                new List<Note>()
                {
                    new()
                    {
                        Mandatory = true,
                        Text =
                            "Ny regional indelning fr.o.m. 1999-01-01. Delar av Södertälje kommun (kod 0181) bildar en ny kommun benämnd Nykvarn (kod 0140)."
                    }
                });

            myOut.Category.Index = new Dictionary<string, int>();
            myOut.Category.Index.Add("00", 0);
            myOut.Category.Index.Add("01", 1);
            myOut.Category.Index.Add("0114", 2);
            myOut.Category.Index.Add("0115", 3);
            myOut.Category.Index.Add("0117", 4);
            myOut.Category.Index.Add("0120", 5);
            myOut.Category.Index.Add("0123", 6);
            myOut.Category.Index.Add("0125", 7);
            myOut.Category.Index.Add("0126", 8);
            myOut.Category.Index.Add("0127", 9);
            myOut.Category.Index.Add("0128", 10);
            myOut.Category.Index.Add("0136", 11);
            myOut.Category.Index.Add("0138", 12);
            myOut.Category.Index.Add("0139", 13);
            myOut.Category.Index.Add("0140", 14);
            myOut.Category.Index.Add("0160", 15);
            myOut.Category.Index.Add("0162", 16);
            myOut.Category.Index.Add("0163", 17);
            myOut.Category.Index.Add("0180", 18);
            myOut.Category.Index.Add("0181", 19);
            myOut.Category.Index.Add("0182", 20);
            myOut.Category.Index.Add("0183", 21);
            myOut.Category.Index.Add("0184", 22);
            myOut.Category.Index.Add("0186", 23);
            myOut.Category.Index.Add("0187", 24);
            myOut.Category.Index.Add("0188", 25);
            myOut.Category.Index.Add("0191", 26);
            myOut.Category.Index.Add("0192", 27);
            myOut.Category.Index.Add("03", 28);
            myOut.Category.Index.Add("0305", 29);
            myOut.Category.Index.Add("0319", 30);
            myOut.Category.Index.Add("0330", 31);
            myOut.Category.Index.Add("0331", 32);
            myOut.Category.Index.Add("0360", 33);
            myOut.Category.Index.Add("0380", 34);
            myOut.Category.Index.Add("0381", 35);
            myOut.Category.Index.Add("0382", 36);
            myOut.Category.Index.Add("04", 37);
            myOut.Category.Index.Add("0428", 38);
            myOut.Category.Index.Add("0461", 39);
            myOut.Category.Index.Add("0480", 40);
            myOut.Category.Index.Add("0481", 41);
            myOut.Category.Index.Add("0482", 42);
            myOut.Category.Index.Add("0483", 43);
            myOut.Category.Index.Add("0484", 44);
            myOut.Category.Index.Add("0486", 45);
            myOut.Category.Index.Add("0488", 46);
            myOut.Category.Index.Add("05", 47);
            myOut.Category.Index.Add("0509", 48);
            myOut.Category.Index.Add("0512", 49);
            myOut.Category.Index.Add("0513", 50);
            myOut.Category.Index.Add("0560", 51);
            myOut.Category.Index.Add("0561", 52);
            myOut.Category.Index.Add("0562", 53);
            myOut.Category.Index.Add("0563", 54);
            myOut.Category.Index.Add("0580", 55);
            myOut.Category.Index.Add("0581", 56);
            myOut.Category.Index.Add("0582", 57);
            myOut.Category.Index.Add("0583", 58);
            myOut.Category.Index.Add("0584", 59);
            myOut.Category.Index.Add("0586", 60);
            myOut.Category.Index.Add("06", 61);
            myOut.Category.Index.Add("0604", 62);
            myOut.Category.Index.Add("0617", 63);
            myOut.Category.Index.Add("0642", 64);
            myOut.Category.Index.Add("0643", 65);
            myOut.Category.Index.Add("0662", 66);
            myOut.Category.Index.Add("0665", 67);
            myOut.Category.Index.Add("0680", 68);
            myOut.Category.Index.Add("0682", 69);
            myOut.Category.Index.Add("0683", 70);
            myOut.Category.Index.Add("0684", 71);
            myOut.Category.Index.Add("0685", 72);
            myOut.Category.Index.Add("0686", 73);
            myOut.Category.Index.Add("0687", 74);
            myOut.Category.Index.Add("07", 75);
            myOut.Category.Index.Add("0760", 76);
            myOut.Category.Index.Add("0761", 77);
            myOut.Category.Index.Add("0763", 78);
            myOut.Category.Index.Add("0764", 79);
            myOut.Category.Index.Add("0765", 80);
            myOut.Category.Index.Add("0767", 81);
            myOut.Category.Index.Add("0780", 82);
            myOut.Category.Index.Add("0781", 83);
            myOut.Category.Index.Add("08", 84);
            myOut.Category.Index.Add("0821", 85);
            myOut.Category.Index.Add("0834", 86);
            myOut.Category.Index.Add("0840", 87);
            myOut.Category.Index.Add("0860", 88);
            myOut.Category.Index.Add("0861", 89);
            myOut.Category.Index.Add("0862", 90);
            myOut.Category.Index.Add("0880", 91);
            myOut.Category.Index.Add("0881", 92);
            myOut.Category.Index.Add("0882", 93);
            myOut.Category.Index.Add("0883", 94);
            myOut.Category.Index.Add("0884", 95);
            myOut.Category.Index.Add("0885", 96);
            myOut.Category.Index.Add("09", 97);
            myOut.Category.Index.Add("0980", 98);
            myOut.Category.Index.Add("10", 99);
            myOut.Category.Index.Add("1060", 100);
            myOut.Category.Index.Add("1080", 101);
            myOut.Category.Index.Add("1081", 102);
            myOut.Category.Index.Add("1082", 103);
            myOut.Category.Index.Add("1083", 104);
            myOut.Category.Index.Add("12", 105);
            myOut.Category.Index.Add("1214", 106);
            myOut.Category.Index.Add("1230", 107);
            myOut.Category.Index.Add("1231", 108);
            myOut.Category.Index.Add("1233", 109);
            myOut.Category.Index.Add("1256", 110);
            myOut.Category.Index.Add("1257", 111);
            myOut.Category.Index.Add("1260", 112);
            myOut.Category.Index.Add("1261", 113);
            myOut.Category.Index.Add("1262", 114);
            myOut.Category.Index.Add("1263", 115);
            myOut.Category.Index.Add("1264", 116);
            myOut.Category.Index.Add("1265", 117);
            myOut.Category.Index.Add("1266", 118);
            myOut.Category.Index.Add("1267", 119);
            myOut.Category.Index.Add("1270", 120);
            myOut.Category.Index.Add("1272", 121);
            myOut.Category.Index.Add("1273", 122);
            myOut.Category.Index.Add("1275", 123);
            myOut.Category.Index.Add("1276", 124);
            myOut.Category.Index.Add("1277", 125);
            myOut.Category.Index.Add("1278", 126);
            myOut.Category.Index.Add("1280", 127);
            myOut.Category.Index.Add("1281", 128);
            myOut.Category.Index.Add("1282", 129);
            myOut.Category.Index.Add("1283", 130);
            myOut.Category.Index.Add("1284", 131);
            myOut.Category.Index.Add("1285", 132);
            myOut.Category.Index.Add("1286", 133);
            myOut.Category.Index.Add("1287", 134);
            myOut.Category.Index.Add("1290", 135);
            myOut.Category.Index.Add("1291", 136);
            myOut.Category.Index.Add("1292", 137);
            myOut.Category.Index.Add("1293", 138);
            myOut.Category.Index.Add("13", 139);
            myOut.Category.Index.Add("1315", 140);
            myOut.Category.Index.Add("1380", 141);
            myOut.Category.Index.Add("1381", 142);
            myOut.Category.Index.Add("1382", 143);
            myOut.Category.Index.Add("1383", 144);
            myOut.Category.Index.Add("1384", 145);
            myOut.Category.Index.Add("14", 146);
            myOut.Category.Index.Add("1401", 147);
            myOut.Category.Index.Add("1402", 148);
            myOut.Category.Index.Add("1407", 149);
            myOut.Category.Index.Add("1415", 150);
            myOut.Category.Index.Add("1419", 151);
            myOut.Category.Index.Add("1421", 152);
            myOut.Category.Index.Add("1427", 153);
            myOut.Category.Index.Add("1430", 154);
            myOut.Category.Index.Add("1435", 155);
            myOut.Category.Index.Add("1438", 156);
            myOut.Category.Index.Add("1439", 157);
            myOut.Category.Index.Add("1440", 158);
            myOut.Category.Index.Add("1441", 159);
            myOut.Category.Index.Add("1442", 160);
            myOut.Category.Index.Add("1443", 161);
            myOut.Category.Index.Add("1444", 162);
            myOut.Category.Index.Add("1445", 163);
            myOut.Category.Index.Add("1446", 164);
            myOut.Category.Index.Add("1447", 165);
            myOut.Category.Index.Add("1452", 166);
            myOut.Category.Index.Add("1460", 167);
            myOut.Category.Index.Add("1461", 168);
            myOut.Category.Index.Add("1462", 169);
            myOut.Category.Index.Add("1463", 170);
            myOut.Category.Index.Add("1465", 171);
            myOut.Category.Index.Add("1466", 172);
            myOut.Category.Index.Add("1470", 173);
            myOut.Category.Index.Add("1471", 174);
            myOut.Category.Index.Add("1472", 175);
            myOut.Category.Index.Add("1473", 176);
            myOut.Category.Index.Add("1480", 177);
            myOut.Category.Index.Add("1481", 178);
            myOut.Category.Index.Add("1482", 179);
            myOut.Category.Index.Add("1484", 180);
            myOut.Category.Index.Add("1485", 181);
            myOut.Category.Index.Add("1486", 182);
            myOut.Category.Index.Add("1487", 183);
            myOut.Category.Index.Add("1488", 184);
            myOut.Category.Index.Add("1489", 185);
            myOut.Category.Index.Add("1490", 186);
            myOut.Category.Index.Add("1491", 187);
            myOut.Category.Index.Add("1492", 188);
            myOut.Category.Index.Add("1493", 189);
            myOut.Category.Index.Add("1494", 190);
            myOut.Category.Index.Add("1495", 191);
            myOut.Category.Index.Add("1496", 192);
            myOut.Category.Index.Add("1497", 193);
            myOut.Category.Index.Add("1498", 194);
            myOut.Category.Index.Add("1499", 195);
            myOut.Category.Index.Add("17", 196);
            myOut.Category.Index.Add("1715", 197);
            myOut.Category.Index.Add("1730", 198);
            myOut.Category.Index.Add("1737", 199);
            myOut.Category.Index.Add("1760", 200);
            myOut.Category.Index.Add("1761", 201);
            myOut.Category.Index.Add("1762", 202);
            myOut.Category.Index.Add("1763", 203);
            myOut.Category.Index.Add("1764", 204);
            myOut.Category.Index.Add("1765", 205);
            myOut.Category.Index.Add("1766", 206);
            myOut.Category.Index.Add("1780", 207);
            myOut.Category.Index.Add("1781", 208);
            myOut.Category.Index.Add("1782", 209);
            myOut.Category.Index.Add("1783", 210);
            myOut.Category.Index.Add("1784", 211);
            myOut.Category.Index.Add("1785", 212);
            myOut.Category.Index.Add("18", 213);
            myOut.Category.Index.Add("1814", 214);
            myOut.Category.Index.Add("1860", 215);
            myOut.Category.Index.Add("1861", 216);
            myOut.Category.Index.Add("1862", 217);
            myOut.Category.Index.Add("1863", 218);
            myOut.Category.Index.Add("1864", 219);
            myOut.Category.Index.Add("1880", 220);
            myOut.Category.Index.Add("1881", 221);
            myOut.Category.Index.Add("1882", 222);
            myOut.Category.Index.Add("1883", 223);
            myOut.Category.Index.Add("1884", 224);
            myOut.Category.Index.Add("1885", 225);
            myOut.Category.Index.Add("19", 226);
            myOut.Category.Index.Add("1904", 227);
            myOut.Category.Index.Add("1907", 228);
            myOut.Category.Index.Add("1960", 229);
            myOut.Category.Index.Add("1961", 230);
            myOut.Category.Index.Add("1962", 231);
            myOut.Category.Index.Add("1980", 232);
            myOut.Category.Index.Add("1981", 233);
            myOut.Category.Index.Add("1982", 234);
            myOut.Category.Index.Add("1983", 235);
            myOut.Category.Index.Add("1984", 236);
            myOut.Category.Index.Add("20", 237);
            myOut.Category.Index.Add("2021", 238);
            myOut.Category.Index.Add("2023", 239);
            myOut.Category.Index.Add("2026", 240);
            myOut.Category.Index.Add("2029", 241);
            myOut.Category.Index.Add("2031", 242);
            myOut.Category.Index.Add("2034", 243);
            myOut.Category.Index.Add("2039", 244);
            myOut.Category.Index.Add("2061", 245);
            myOut.Category.Index.Add("2062", 246);
            myOut.Category.Index.Add("2080", 247);
            myOut.Category.Index.Add("2081", 248);
            myOut.Category.Index.Add("2082", 249);
            myOut.Category.Index.Add("2083", 250);
            myOut.Category.Index.Add("2084", 251);
            myOut.Category.Index.Add("2085", 252);
            myOut.Category.Index.Add("21", 253);
            myOut.Category.Index.Add("2101", 254);
            myOut.Category.Index.Add("2104", 255);
            myOut.Category.Index.Add("2121", 256);
            myOut.Category.Index.Add("2132", 257);
            myOut.Category.Index.Add("2161", 258);
            myOut.Category.Index.Add("2180", 259);
            myOut.Category.Index.Add("2181", 260);
            myOut.Category.Index.Add("2182", 261);
            myOut.Category.Index.Add("2183", 262);
            myOut.Category.Index.Add("2184", 263);
            myOut.Category.Index.Add("22", 264);
            myOut.Category.Index.Add("2260", 265);
            myOut.Category.Index.Add("2262", 266);
            myOut.Category.Index.Add("2280", 267);
            myOut.Category.Index.Add("2281", 268);
            myOut.Category.Index.Add("2282", 269);
            myOut.Category.Index.Add("2283", 270);
            myOut.Category.Index.Add("2284", 271);
            myOut.Category.Index.Add("23", 272);
            myOut.Category.Index.Add("2303", 273);
            myOut.Category.Index.Add("2305", 274);
            myOut.Category.Index.Add("2309", 275);
            myOut.Category.Index.Add("2313", 276);
            myOut.Category.Index.Add("2321", 277);
            myOut.Category.Index.Add("2326", 278);
            myOut.Category.Index.Add("2361", 279);
            myOut.Category.Index.Add("2380", 280);
            myOut.Category.Index.Add("24", 281);
            myOut.Category.Index.Add("2401", 282);
            myOut.Category.Index.Add("2403", 283);
            myOut.Category.Index.Add("2404", 284);
            myOut.Category.Index.Add("2409", 285);
            myOut.Category.Index.Add("2417", 286);
            myOut.Category.Index.Add("2418", 287);
            myOut.Category.Index.Add("2421", 288);
            myOut.Category.Index.Add("2422", 289);
            myOut.Category.Index.Add("2425", 290);
            myOut.Category.Index.Add("2460", 291);
            myOut.Category.Index.Add("2462", 292);
            myOut.Category.Index.Add("2463", 293);
            myOut.Category.Index.Add("2480", 294);
            myOut.Category.Index.Add("2481", 295);
            myOut.Category.Index.Add("2482", 296);
            myOut.Category.Index.Add("25", 297);
            myOut.Category.Index.Add("2505", 298);
            myOut.Category.Index.Add("2506", 299);
            myOut.Category.Index.Add("2510", 300);
            myOut.Category.Index.Add("2513", 301);
            myOut.Category.Index.Add("2514", 302);
            myOut.Category.Index.Add("2518", 303);
            myOut.Category.Index.Add("2521", 304);
            myOut.Category.Index.Add("2523", 305);
            myOut.Category.Index.Add("2560", 306);
            myOut.Category.Index.Add("2580", 307);
            myOut.Category.Index.Add("2581", 308);
            myOut.Category.Index.Add("2582", 309);
            myOut.Category.Index.Add("2583", 310);
            myOut.Category.Index.Add("2584", 311);


            myOut.Category.Label = new Dictionary<string, string>();
            myOut.Category.Label.Add("00", "Riket");
            myOut.Category.Label.Add("01", "Stockholms län");
            myOut.Category.Label.Add("0114", "Upplands Väsby");
            myOut.Category.Label.Add("0115", "Vallentuna");
            myOut.Category.Label.Add("0117", "Österåker");
            myOut.Category.Label.Add("0120", "Värmdö");
            myOut.Category.Label.Add("0123", "Järfälla");
            myOut.Category.Label.Add("0125", "Ekerö");
            myOut.Category.Label.Add("0126", "Huddinge");
            myOut.Category.Label.Add("0127", "Botkyrka");
            myOut.Category.Label.Add("0128", "Salem");
            myOut.Category.Label.Add("0136", "Haninge");
            myOut.Category.Label.Add("0138", "Tyresö");
            myOut.Category.Label.Add("0139", "Upplands-Bro");
            myOut.Category.Label.Add("0140", "Nykvarn");
            myOut.Category.Label.Add("0160", "Täby");
            myOut.Category.Label.Add("0162", "Danderyd");
            myOut.Category.Label.Add("0163", "Sollentuna");
            myOut.Category.Label.Add("0180", "Stockholm");
            myOut.Category.Label.Add("0181", "Södertälje");
            myOut.Category.Label.Add("0182", "Nacka");
            myOut.Category.Label.Add("0183", "Sundbyberg");
            myOut.Category.Label.Add("0184", "Solna");
            myOut.Category.Label.Add("0186", "Lidingö");
            myOut.Category.Label.Add("0187", "Vaxholm");
            myOut.Category.Label.Add("0188", "Norrtälje");
            myOut.Category.Label.Add("0191", "Sigtuna");
            myOut.Category.Label.Add("0192", "Nynäshamn");
            myOut.Category.Label.Add("03", "Uppsala län");
            myOut.Category.Label.Add("0305", "Håbo");
            myOut.Category.Label.Add("0319", "Älvkarleby");
            myOut.Category.Label.Add("0330", "Knivsta");
            myOut.Category.Label.Add("0331", "Heby");
            myOut.Category.Label.Add("0360", "Tierp");
            myOut.Category.Label.Add("0380", "Uppsala");
            myOut.Category.Label.Add("0381", "Enköping");
            myOut.Category.Label.Add("0382", "Östhammar");
            myOut.Category.Label.Add("04", "Södermanlands län");
            myOut.Category.Label.Add("0428", "Vingåker");
            myOut.Category.Label.Add("0461", "Gnesta");
            myOut.Category.Label.Add("0480", "Nyköping");
            myOut.Category.Label.Add("0481", "Oxelösund");
            myOut.Category.Label.Add("0482", "Flen");
            myOut.Category.Label.Add("0483", "Katrineholm");
            myOut.Category.Label.Add("0484", "Eskilstuna");
            myOut.Category.Label.Add("0486", "Strängnäs");
            myOut.Category.Label.Add("0488", "Trosa");
            myOut.Category.Label.Add("05", "Östergötlands län");
            myOut.Category.Label.Add("0509", "Ödeshög");
            myOut.Category.Label.Add("0512", "Ydre");
            myOut.Category.Label.Add("0513", "Kinda");
            myOut.Category.Label.Add("0560", "Boxholm");
            myOut.Category.Label.Add("0561", "Åtvidaberg");
            myOut.Category.Label.Add("0562", "Finspång");
            myOut.Category.Label.Add("0563", "Valdemarsvik");
            myOut.Category.Label.Add("0580", "Linköping");
            myOut.Category.Label.Add("0581", "Norrköping");
            myOut.Category.Label.Add("0582", "Söderköping");
            myOut.Category.Label.Add("0583", "Motala");
            myOut.Category.Label.Add("0584", "Vadstena");
            myOut.Category.Label.Add("0586", "Mjölby");
            myOut.Category.Label.Add("06", "Jönköpings län");
            myOut.Category.Label.Add("0604", "Aneby");
            myOut.Category.Label.Add("0617", "Gnosjö");
            myOut.Category.Label.Add("0642", "Mullsjö");
            myOut.Category.Label.Add("0643", "Habo");
            myOut.Category.Label.Add("0662", "Gislaved");
            myOut.Category.Label.Add("0665", "Vaggeryd");
            myOut.Category.Label.Add("0680", "Jönköping");
            myOut.Category.Label.Add("0682", "Nässjö");
            myOut.Category.Label.Add("0683", "Värnamo");
            myOut.Category.Label.Add("0684", "Sävsjö");
            myOut.Category.Label.Add("0685", "Vetlanda");
            myOut.Category.Label.Add("0686", "Eksjö");
            myOut.Category.Label.Add("0687", "Tranås");
            myOut.Category.Label.Add("07", "Kronobergs län");
            myOut.Category.Label.Add("0760", "Uppvidinge");
            myOut.Category.Label.Add("0761", "Lessebo");
            myOut.Category.Label.Add("0763", "Tingsryd");
            myOut.Category.Label.Add("0764", "Alvesta");
            myOut.Category.Label.Add("0765", "Älmhult");
            myOut.Category.Label.Add("0767", "Markaryd");
            myOut.Category.Label.Add("0780", "Växjö");
            myOut.Category.Label.Add("0781", "Ljungby");
            myOut.Category.Label.Add("08", "Kalmar län");
            myOut.Category.Label.Add("0821", "Högsby");
            myOut.Category.Label.Add("0834", "Torsås");
            myOut.Category.Label.Add("0840", "Mörbylånga");
            myOut.Category.Label.Add("0860", "Hultsfred");
            myOut.Category.Label.Add("0861", "Mönsterås");
            myOut.Category.Label.Add("0862", "Emmaboda");
            myOut.Category.Label.Add("0880", "Kalmar");
            myOut.Category.Label.Add("0881", "Nybro");
            myOut.Category.Label.Add("0882", "Oskarshamn");
            myOut.Category.Label.Add("0883", "Västervik");
            myOut.Category.Label.Add("0884", "Vimmerby");
            myOut.Category.Label.Add("0885", "Borgholm");
            myOut.Category.Label.Add("09", "Gotlands län");
            myOut.Category.Label.Add("0980", "Gotland");
            myOut.Category.Label.Add("10", "Blekinge län");
            myOut.Category.Label.Add("1060", "Olofström");
            myOut.Category.Label.Add("1080", "Karlskrona");
            myOut.Category.Label.Add("1081", "Ronneby");
            myOut.Category.Label.Add("1082", "Karlshamn");
            myOut.Category.Label.Add("1083", "Sölvesborg");
            myOut.Category.Label.Add("12", "Skåne län");
            myOut.Category.Label.Add("1214", "Svalöv");
            myOut.Category.Label.Add("1230", "Staffanstorp");
            myOut.Category.Label.Add("1231", "Burlöv");
            myOut.Category.Label.Add("1233", "Vellinge");
            myOut.Category.Label.Add("1256", "Östra Göinge");
            myOut.Category.Label.Add("1257", "Örkelljunga");
            myOut.Category.Label.Add("1260", "Bjuv");
            myOut.Category.Label.Add("1261", "Kävlinge");
            myOut.Category.Label.Add("1262", "Lomma");
            myOut.Category.Label.Add("1263", "Svedala");
            myOut.Category.Label.Add("1264", "Skurup");
            myOut.Category.Label.Add("1265", "Sjöbo");
            myOut.Category.Label.Add("1266", "Hörby");
            myOut.Category.Label.Add("1267", "Höör");
            myOut.Category.Label.Add("1270", "Tomelilla");
            myOut.Category.Label.Add("1272", "Bromölla");
            myOut.Category.Label.Add("1273", "Osby");
            myOut.Category.Label.Add("1275", "Perstorp");
            myOut.Category.Label.Add("1276", "Klippan");
            myOut.Category.Label.Add("1277", "Åstorp");
            myOut.Category.Label.Add("1278", "Båstad");
            myOut.Category.Label.Add("1280", "Malmö");
            myOut.Category.Label.Add("1281", "Lund");
            myOut.Category.Label.Add("1282", "Landskrona");
            myOut.Category.Label.Add("1283", "Helsingborg");
            myOut.Category.Label.Add("1284", "Höganäs");
            myOut.Category.Label.Add("1285", "Eslöv");
            myOut.Category.Label.Add("1286", "Ystad");
            myOut.Category.Label.Add("1287", "Trelleborg");
            myOut.Category.Label.Add("1290", "Kristianstad");
            myOut.Category.Label.Add("1291", "Simrishamn");
            myOut.Category.Label.Add("1292", "Ängelholm");
            myOut.Category.Label.Add("1293", "Hässleholm");
            myOut.Category.Label.Add("13", "Hallands län");
            myOut.Category.Label.Add("1315", "Hylte");
            myOut.Category.Label.Add("1380", "Halmstad");
            myOut.Category.Label.Add("1381", "Laholm");
            myOut.Category.Label.Add("1382", "Falkenberg");
            myOut.Category.Label.Add("1383", "Varberg");
            myOut.Category.Label.Add("1384", "Kungsbacka");
            myOut.Category.Label.Add("14", "Västra Götalands län");
            myOut.Category.Label.Add("1401", "Härryda");
            myOut.Category.Label.Add("1402", "Partille");
            myOut.Category.Label.Add("1407", "Öckerö");
            myOut.Category.Label.Add("1415", "Stenungsund");
            myOut.Category.Label.Add("1419", "Tjörn");
            myOut.Category.Label.Add("1421", "Orust");
            myOut.Category.Label.Add("1427", "Sotenäs");
            myOut.Category.Label.Add("1430", "Munkedal");
            myOut.Category.Label.Add("1435", "Tanum");
            myOut.Category.Label.Add("1438", "Dals-Ed");
            myOut.Category.Label.Add("1439", "Färgelanda");
            myOut.Category.Label.Add("1440", "Ale");
            myOut.Category.Label.Add("1441", "Lerum");
            myOut.Category.Label.Add("1442", "Vårgårda");
            myOut.Category.Label.Add("1443", "Bollebygd");
            myOut.Category.Label.Add("1444", "Grästorp");
            myOut.Category.Label.Add("1445", "Essunga");
            myOut.Category.Label.Add("1446", "Karlsborg");
            myOut.Category.Label.Add("1447", "Gullspång");
            myOut.Category.Label.Add("1452", "Tranemo");
            myOut.Category.Label.Add("1460", "Bengtsfors");
            myOut.Category.Label.Add("1461", "Mellerud");
            myOut.Category.Label.Add("1462", "Lilla Edet");
            myOut.Category.Label.Add("1463", "Mark");
            myOut.Category.Label.Add("1465", "Svenljunga");
            myOut.Category.Label.Add("1466", "Herrljunga");
            myOut.Category.Label.Add("1470", "Vara");
            myOut.Category.Label.Add("1471", "Götene");
            myOut.Category.Label.Add("1472", "Tibro");
            myOut.Category.Label.Add("1473", "Töreboda");
            myOut.Category.Label.Add("1480", "Göteborg");
            myOut.Category.Label.Add("1481", "Mölndal");
            myOut.Category.Label.Add("1482", "Kungälv");
            myOut.Category.Label.Add("1484", "Lysekil");
            myOut.Category.Label.Add("1485", "Uddevalla");
            myOut.Category.Label.Add("1486", "Strömstad");
            myOut.Category.Label.Add("1487", "Vänersborg");
            myOut.Category.Label.Add("1488", "Trollhättan");
            myOut.Category.Label.Add("1489", "Alingsås");
            myOut.Category.Label.Add("1490", "Borås");
            myOut.Category.Label.Add("1491", "Ulricehamn");
            myOut.Category.Label.Add("1492", "Åmål");
            myOut.Category.Label.Add("1493", "Mariestad");
            myOut.Category.Label.Add("1494", "Lidköping");
            myOut.Category.Label.Add("1495", "Skara");
            myOut.Category.Label.Add("1496", "Skövde");
            myOut.Category.Label.Add("1497", "Hjo");
            myOut.Category.Label.Add("1498", "Tidaholm");
            myOut.Category.Label.Add("1499", "Falköping");
            myOut.Category.Label.Add("17", "Värmlands län");
            myOut.Category.Label.Add("1715", "Kil");
            myOut.Category.Label.Add("1730", "Eda");
            myOut.Category.Label.Add("1737", "Torsby");
            myOut.Category.Label.Add("1760", "Storfors");
            myOut.Category.Label.Add("1761", "Hammarö");
            myOut.Category.Label.Add("1762", "Munkfors");
            myOut.Category.Label.Add("1763", "Forshaga");
            myOut.Category.Label.Add("1764", "Grums");
            myOut.Category.Label.Add("1765", "Årjäng");
            myOut.Category.Label.Add("1766", "Sunne");
            myOut.Category.Label.Add("1780", "Karlstad");
            myOut.Category.Label.Add("1781", "Kristinehamn");
            myOut.Category.Label.Add("1782", "Filipstad");
            myOut.Category.Label.Add("1783", "Hagfors");
            myOut.Category.Label.Add("1784", "Arvika");
            myOut.Category.Label.Add("1785", "Säffle");
            myOut.Category.Label.Add("18", "Örebro län");
            myOut.Category.Label.Add("1814", "Lekeberg");
            myOut.Category.Label.Add("1860", "Laxå");
            myOut.Category.Label.Add("1861", "Hallsberg");
            myOut.Category.Label.Add("1862", "Degerfors");
            myOut.Category.Label.Add("1863", "Hällefors");
            myOut.Category.Label.Add("1864", "Ljusnarsberg");
            myOut.Category.Label.Add("1880", "Örebro");
            myOut.Category.Label.Add("1881", "Kumla");
            myOut.Category.Label.Add("1882", "Askersund");
            myOut.Category.Label.Add("1883", "Karlskoga");
            myOut.Category.Label.Add("1884", "Nora");
            myOut.Category.Label.Add("1885", "Lindesberg");
            myOut.Category.Label.Add("19", "Västmanlands län");
            myOut.Category.Label.Add("1904", "Skinnskatteberg");
            myOut.Category.Label.Add("1907", "Surahammar");
            myOut.Category.Label.Add("1960", "Kungsör");
            myOut.Category.Label.Add("1961", "Hallstahammar");
            myOut.Category.Label.Add("1962", "Norberg");
            myOut.Category.Label.Add("1980", "Västerås");
            myOut.Category.Label.Add("1981", "Sala");
            myOut.Category.Label.Add("1982", "Fagersta");
            myOut.Category.Label.Add("1983", "Köping");
            myOut.Category.Label.Add("1984", "Arboga");
            myOut.Category.Label.Add("20", "Dalarnas län");
            myOut.Category.Label.Add("2021", "Vansbro");
            myOut.Category.Label.Add("2023", "Malung-Sälen");
            myOut.Category.Label.Add("2026", "Gagnef");
            myOut.Category.Label.Add("2029", "Leksand");
            myOut.Category.Label.Add("2031", "Rättvik");
            myOut.Category.Label.Add("2034", "Orsa");
            myOut.Category.Label.Add("2039", "Älvdalen");
            myOut.Category.Label.Add("2061", "Smedjebacken");
            myOut.Category.Label.Add("2062", "Mora");
            myOut.Category.Label.Add("2080", "Falun");
            myOut.Category.Label.Add("2081", "Borlänge");
            myOut.Category.Label.Add("2082", "Säter");
            myOut.Category.Label.Add("2083", "Hedemora");
            myOut.Category.Label.Add("2084", "Avesta");
            myOut.Category.Label.Add("2085", "Ludvika");
            myOut.Category.Label.Add("21", "Gävleborgs län");
            myOut.Category.Label.Add("2101", "Ockelbo");
            myOut.Category.Label.Add("2104", "Hofors");
            myOut.Category.Label.Add("2121", "Ovanåker");
            myOut.Category.Label.Add("2132", "Nordanstig");
            myOut.Category.Label.Add("2161", "Ljusdal");
            myOut.Category.Label.Add("2180", "Gävle");
            myOut.Category.Label.Add("2181", "Sandviken");
            myOut.Category.Label.Add("2182", "Söderhamn");
            myOut.Category.Label.Add("2183", "Bollnäs");
            myOut.Category.Label.Add("2184", "Hudiksvall");
            myOut.Category.Label.Add("22", "Västernorrlands län");
            myOut.Category.Label.Add("2260", "Ånge");
            myOut.Category.Label.Add("2262", "Timrå");
            myOut.Category.Label.Add("2280", "Härnösand");
            myOut.Category.Label.Add("2281", "Sundsvall");
            myOut.Category.Label.Add("2282", "Kramfors");
            myOut.Category.Label.Add("2283", "Sollefteå");
            myOut.Category.Label.Add("2284", "Örnsköldsvik");
            myOut.Category.Label.Add("23", "Jämtlands län");
            myOut.Category.Label.Add("2303", "Ragunda");
            myOut.Category.Label.Add("2305", "Bräcke");
            myOut.Category.Label.Add("2309", "Krokom");
            myOut.Category.Label.Add("2313", "Strömsund");
            myOut.Category.Label.Add("2321", "Åre");
            myOut.Category.Label.Add("2326", "Berg");
            myOut.Category.Label.Add("2361", "Härjedalen");
            myOut.Category.Label.Add("2380", "Östersund");
            myOut.Category.Label.Add("24", "Västerbottens län");
            myOut.Category.Label.Add("2401", "Nordmaling");
            myOut.Category.Label.Add("2403", "Bjurholm");
            myOut.Category.Label.Add("2404", "Vindeln");
            myOut.Category.Label.Add("2409", "Robertsfors");
            myOut.Category.Label.Add("2417", "Norsjö");
            myOut.Category.Label.Add("2418", "Malå");
            myOut.Category.Label.Add("2421", "Storuman");
            myOut.Category.Label.Add("2422", "Sorsele");
            myOut.Category.Label.Add("2425", "Dorotea");
            myOut.Category.Label.Add("2460", "Vännäs");
            myOut.Category.Label.Add("2462", "Vilhelmina");
            myOut.Category.Label.Add("2463", "Åsele");
            myOut.Category.Label.Add("2480", "Umeå");
            myOut.Category.Label.Add("2481", "Lycksele");
            myOut.Category.Label.Add("2482", "Skellefteå");
            myOut.Category.Label.Add("25", "Norrbottens län");
            myOut.Category.Label.Add("2505", "Arvidsjaur");
            myOut.Category.Label.Add("2506", "Arjeplog");
            myOut.Category.Label.Add("2510", "Jokkmokk");
            myOut.Category.Label.Add("2513", "Överkalix");
            myOut.Category.Label.Add("2514", "Kalix");
            myOut.Category.Label.Add("2518", "Övertorneå");
            myOut.Category.Label.Add("2521", "Pajala");
            myOut.Category.Label.Add("2523", "Gällivare");
            myOut.Category.Label.Add("2560", "Älvsbyn");
            myOut.Category.Label.Add("2580", "Luleå");
            myOut.Category.Label.Add("2581", "Piteå");
            myOut.Category.Label.Add("2582", "Boden");
            myOut.Category.Label.Add("2583", "Haparanda");
            myOut.Category.Label.Add("2584", "Kiruna");

            myOut.Extension.Elimination = true;
            myOut.Extension.EliminationValueCode = "00";
            myOut.Extension.CodeLists = new List<CodeListInformation>();

            myOut.Extension.CodeLists.Add(new CodeListInformation()
            {
                Id = "vs_RegionKommun07",
                Label = "Kommuner",
                Links = new List<Link>() { new() { Rel = "metadata", Href = "https://my-site.com/api/v2/tables/TAB638/codelists/vs_RegionKommun07" } }
            });

            myOut.Extension.CodeLists.Add(new CodeListInformation()
            {
                Id = "vs_RegionLän07",
                Label = "Län",
                Links = new List<Link>() { new() { Rel = "metadata", Href = "https://my-site.com/api/v2/tables/TAB638/codelists/vs_RegionLän07" } }
            });

            myOut.Extension.CodeLists.Add(new CodeListInformation()
            {
                Id = "vs_RegionRiket99",
                Label = "Riket",
                Links = new List<Link>() { new() { Rel = "metadata", Href = "https://my-site.com/api/v2/tables/TAB638/codelists/vs_RegionRiket99" } }
            });

            myOut.Extension.CodeLists.Add(new CodeListInformation()
            {
                Id = "agg_RegionA-region_2",
                Label = "A-regioner",
                Links = new List<Link>() { new() { Rel = "metadata", Href = "https://my-site.com/api/v2/tables/TAB638/codelists/agg_RegionA-region_2" } }
            });

            myOut.Extension.CodeLists.Add(new CodeListInformation()
            {
                Id = "agg_RegionKommungrupp2005-_1",
                Label = "Kommungrupper (SKL:s) 2005",
                Links = new List<Link>() { new()
                                {
                                    Rel ="metadata",
                                   Href ="https://my-site.com/api/v2/tables/TAB638/codelists/agg_RegionKommungrupp2005-_1"} }
            });
            myOut.Extension.CodeLists.Add(new CodeListInformation()
            {
                Id = "agg_RegionKommungrupp2011-",
                Label = "Kommungrupper (SKL:s) 2011",
                Links = new List<Link>() { new()
                                {
                                    Rel ="metadata",
                                   Href ="https://my-site.com/api/v2/tables/TAB638/codelists/agg_RegionKommungrupp2011-"} }
            });
            myOut.Extension.CodeLists.Add(new CodeListInformation()
            {
                Id = "agg_RegionKommungrupp2017-",
                Label = "Kommungrupper (SKL:s) 2017",
                Links = new List<Link>() { new()
                                {
                                    Rel ="metadata",
                                   Href ="https://my-site.com/api/v2/tables/TAB638/codelists/agg_RegionKommungrupp2017-"} }
            });
            myOut.Extension.CodeLists.Add(new CodeListInformation()
            {
                Id = "agg_RegionLA1998",
                Label = "Lokalaarbetsmarknader 1998",
                Links = new List<Link>() { new()
                                {
                                    Rel ="metadata",
                                   Href ="https://my-site.com/api/v2/tables/TAB638/codelists/agg_RegionLA1998"} }
            });
            myOut.Extension.CodeLists.Add(new CodeListInformation()
            {
                Id = "agg_RegionLA2003_1",
                Label = "Lokalaarbetsmarknader 2003",
                Links = new List<Link>() { new()
                                {
                                    Rel ="metadata",
                                   Href ="https://my-site.com/api/v2/tables/TAB638/codelists/agg_RegionLA2003_1"} }
            });
            myOut.Extension.CodeLists.Add(new CodeListInformation()
            {
                Id = "agg_RegionLA2008",
                Label = "Lokalaarbetsmarknader 2008",
                Links = new List<Link>() { new()
                                {
                                    Rel ="metadata",
                                   Href ="https://my-site.com/api/v2/tables/TAB638/codelists/agg_RegionLA2008"} }
            });
            myOut.Extension.CodeLists.Add(new CodeListInformation()
            {
                Id = "agg_RegionLA2013",
                Label = "Lokalaarbetsmarknader 2013",
                Links = new List<Link>() { new()
                                {
                                    Rel ="metadata",
                                   Href ="https://my-site.com/api/v2/tables/TAB638/codelists/agg_RegionLA2013"} }
            });
            myOut.Extension.CodeLists.Add(new CodeListInformation()
            {
                Id = "agg_RegionLA2018",
                Label = "Lokalaarbetsmarknader 2018",
                Links = new List<Link>() { new()
                                {
                                    Rel ="metadata",
                                   Href ="https://my-site.com/api/v2/tables/TAB638/codelists/agg_Ålder5år"} }
            });
            myOut.Extension.CodeLists.Add(new CodeListInformation()
            {
                Id = "agg_RegionStoromr-04_2",
                Label = "Sorstadsområder -2004",
                Links = new List<Link>() { new()
                                {
                                    Rel ="metadata",
                                   Href ="https://my-site.com/api/v2/tables/TAB638/codelists/agg_RegionStoromr-04_2"} }
            });
            myOut.Extension.CodeLists.Add(new CodeListInformation()
            {
                Id = "agg_RegionStoromr05-_1",
                Label = "Sorstadsområder 2005-",
                Links = new List<Link>() { new()
                                {
                                    Rel ="metadata",
                                   Href ="https://my-site.com/api/v2/tables/TAB638/codelists/agg_RegionStoromr05-_1"} }
            });
            myOut.Extension.CodeLists.Add(new CodeListInformation()
            {
                Id = "agg_RegionNUTS1_2008",
                Label = "NUTS1 fr.o.m 2008",
                Links = new List<Link>() { new()
                                {
                                    Rel ="metadata",
                                   Href ="https://my-site.com/api/v2/tables/TAB638/codelists/agg_RegionNUTS1_2008"} }
            });
            myOut.Extension.CodeLists.Add(new CodeListInformation()
            {
                Id = "agg_RegionNUTS2_2008",
                Label = "NUTS2 fr.o.m 2008",
                Links = new List<Link>() { new()
                                {
                                    Rel ="metadata",
                                   Href ="https://my-site.com/api/v2/tables/TAB638/codelists/agg_RegionNUTS2_2008"} }
            });
            myOut.Extension.CodeLists.Add(new CodeListInformation()
            {
                Id = "agg_RegionNUTS3_2008",
                Label = "NUTS3 fr.o.m 2008",
                Links = new List<Link>() { new()
                                {
                                    Rel ="metadata",
                                   Href ="https://my-site.com/api/v2/tables/TAB638/codelists/agg_RegionNUTS3_2008"} }
            });
            return myOut;
        }







        //Things that migth be moved to partial classes in model

        private Folder newFolder()
        {
            Folder folder = new Folder();
            folder.ObjectType = "folder";
            return folder;
        }
    }
}

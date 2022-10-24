using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PCAxis.OpenAPILib.Models;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private Helper helper = new Helper();
        
        [TestMethod]
        public void TestMethod1()
        {
            var ds = GenerateDatasetData();

            string myOut = ds.ToJson();



            var serializer = new SerializerBuilder()
    .WithNamingConvention(CamelCaseNamingConvention.Instance)
    .Build();
            var yaml = serializer.Serialize(ds);

           

            File.WriteAllText("../../../../../myFile.yaml" , yaml);


var jsonExample = helper.GetExampleJsonstat();

            string a = "a";
        }

        private Dataset GenerateDatasetData()
        {
            Dataset ds = new Dataset();

            ds.Label = "Folkm�ngden efter region, civilst�nd, �lder, k�n, tabellinneh�ll och �r";
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
                Refperiod = "31 December each year",
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


            ds.Note = new List<string>
            {
                "Fr o m 2007-01-01 �verf�rs Heby kommun fr�n V�stmanlands l�n till Uppsala l�n. Hebys kommunkod �ndras fr�n 1917 till 0331. ",
                "Registrerat partnerskap reglerade parf�rh�llanden mellan personer av samma k�n och fanns fr�n 1995 till 2009. Registrerade partners r�knas som Gifta, Separerade partners som Skilda och Efterlevande partners som �nka/�nklingar.",
                "Fr o m 2007-01-01 ut�kas Uppsala l�n med Heby kommun. Observera att l�nssiffrorna inte �r j�mf�rbara med l�nssiffrorna bak�t i tiden."
            };

            ds.Value = new List<decimal>();
            return ds;
        }

        [TestMethod]
        public void CheckIfModelProducesSameJsonOutputAsExampleFile()
        {
            //Arrange
            var ds = GenerateDatasetData();
            var exampleJsonOutPut = helper.GetExampleJsonstat();
            
            //Act
            var jsonOutPut = ds.ToJson();

            //Assert
            Assert.AreEqual(exampleJsonOutPut, jsonOutPut);
        }

        private DatasetDimensionValue GetKonDatasetDimensionValue()
        {
            DatasetDimensionValue myOut = new DatasetDimensionValue();
            myOut.Label = "k�n";

            myOut.Category = new JsonstatCategory();
            myOut.Category.Index = new Dictionary<string, int>();
            myOut.Category.Index.Add("1", 0);
            myOut.Category.Index.Add("2", 1);
           

            myOut.Category.Label = new Dictionary<string, string>();
            myOut.Category.Label.Add("1", "m�n");
            myOut.Category.Label.Add("2", "kvinnor");

            myOut.Extension = new ExtensionDimension() { Elimination = true };

            myOut.Link = new JsonstatExtensionLink();
            myOut.Link.Describedby = new List<DimensionExtension>() { new DimensionExtension() { Extension = new Dictionary<string, string>() } };

            myOut.Link.Describedby[0].Extension.Add("Kon", "K�n");

            return myOut;
        }

        private DatasetDimensionValue GetTidDatasetDimensionValue()
        {
            DatasetDimensionValue myOut = new DatasetDimensionValue();
            myOut.Label = "�r";

            myOut.Category = new JsonstatCategory();
            myOut.Category.Index = new Dictionary<string, int>();
            myOut.Category.Index.Add("2021", 0);
            //myOut.Category.Index.Add("2", 1);


            myOut.Category.Label = new Dictionary<string, string>();
            myOut.Category.Label.Add("2021", "2021");

            myOut.Extension = new ExtensionDimension() { Elimination = false, Frequency = "A" , FirstPeriod = "1968" , LastPeriod = "2021" };

            

            return myOut;
        }

        private DatasetDimensionValue GetContentsCodeDatasetDimensionValue()
        {
            DatasetDimensionValue myOut = new DatasetDimensionValue();
            myOut.Label = "tabellinneh�ll";

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
                            "Uppgifterna avser f�rh�llandena den 31 december f�r valt/valda �r enligt den regionala indelning som g�ller den 1 januari �ret efter."
                    }
                });

            myOut.Extension.ValueNote.Add("BE0101N2",
                new List<Note>()
                {
                    new()
                    {
                        Mandatory = true,
                        Text =
                            "Folk�kningen definieras som skillnaden mellan folkm�ngden vid �rets b�rjan och �rets slut."
                    }
                });

            myOut.Category.Index = new Dictionary<string, int>();
            myOut.Category.Index.Add("BE0101N1", 0);
            myOut.Category.Index.Add("BE0101N2", 1);


            myOut.Category.Label = new Dictionary<string, string>();
            myOut.Category.Label.Add("BE0101N1", "Folkm�ngd");
            myOut.Category.Label.Add("BE0101N2", "Folk�kning");

            myOut.Category.Unit = new Dictionary<string, JsonstatCategoryUnitValue>();
            myOut.Category.Unit.Add("BE0101N1", new JsonstatCategoryUnitValue() { Base = "antal" , Decimals = 0 });
            myOut.Category.Unit.Add("BE0101N2", new JsonstatCategoryUnitValue() { Base = "antal", Decimals = 0 });

            //myOut.Extension = new ExtensionDimension() { Elimination = false };

            return myOut;
        }


        private DatasetDimensionValue GetCivilstandDatasetDimensionValue()
        {
            DatasetDimensionValue myOut = new DatasetDimensionValue();
            myOut.Label = "civilst�nd";

            myOut.Category = new JsonstatCategory();
            myOut.Category.Index = new Dictionary<string, int>();
            myOut.Category.Index.Add("OG", 0);
            myOut.Category.Index.Add("G", 1);
            myOut.Category.Index.Add("SK", 2);
            myOut.Category.Index.Add("�NKL", 3);

            myOut.Category.Label = new Dictionary<string, string>();
            myOut.Category.Label.Add("OG", "ogifta");
            myOut.Category.Label.Add("G", "gifta");
            myOut.Category.Label.Add("SK", "skilda");
            myOut.Category.Label.Add("�NKL", "�nkor/�nklingar");

            myOut.Extension = new ExtensionDimension() { Elimination = true };
            return myOut;
        }


        private DatasetDimensionValue GetAlderDatasetDimensionValue()
        {
            DatasetDimensionValue myOut = new DatasetDimensionValue();
            myOut.Label = "�lder";
            
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
            myOut.Category.Label.Add("0", "0 �r");
            myOut.Category.Label.Add("1", "1 �r");
            myOut.Category.Label.Add("2", "2 �r");
            myOut.Category.Label.Add("3", "3 �r");
            myOut.Category.Label.Add("4", "4 �r");
            myOut.Category.Label.Add("5", "5 �r");
            myOut.Category.Label.Add("6", "6 �r");
            myOut.Category.Label.Add("7", "7 �r");
            myOut.Category.Label.Add("8", "8 �r");
            myOut.Category.Label.Add("9", "9 �r");
            myOut.Category.Label.Add("10", "10 �r");
            myOut.Category.Label.Add("11", "11 �r");
            myOut.Category.Label.Add("12", "12 �r");
            myOut.Category.Label.Add("13", "13 �r");
            myOut.Category.Label.Add("14", "14 �r");
            myOut.Category.Label.Add("15", "15 �r");
            myOut.Category.Label.Add("16", "16 �r");
            myOut.Category.Label.Add("17", "17 �r");
            myOut.Category.Label.Add("18", "18 �r");
            myOut.Category.Label.Add("19", "19 �r");
            myOut.Category.Label.Add("20", "20 �r");
            myOut.Category.Label.Add("21", "21 �r");
            myOut.Category.Label.Add("22", "22 �r");
            myOut.Category.Label.Add("23", "23 �r");
            myOut.Category.Label.Add("24", "24 �r");
            myOut.Category.Label.Add("25", "25 �r");
            myOut.Category.Label.Add("26", "26 �r");
            myOut.Category.Label.Add("27", "27 �r");
            myOut.Category.Label.Add("28", "28 �r");
            myOut.Category.Label.Add("29", "29 �r");
            myOut.Category.Label.Add("30", "30 �r");
            myOut.Category.Label.Add("31", "31 �r");
            myOut.Category.Label.Add("32", "32 �r");
            myOut.Category.Label.Add("33", "33 �r");
            myOut.Category.Label.Add("34", "34 �r");
            myOut.Category.Label.Add("35", "35 �r");
            myOut.Category.Label.Add("36", "36 �r");
            myOut.Category.Label.Add("37", "37 �r");
            myOut.Category.Label.Add("38", "38 �r");
            myOut.Category.Label.Add("39", "39 �r");
            myOut.Category.Label.Add("40", "40 �r");
            myOut.Category.Label.Add("41", "41 �r");
            myOut.Category.Label.Add("42", "42 �r");
            myOut.Category.Label.Add("43", "43 �r");
            myOut.Category.Label.Add("44", "44 �r");
            myOut.Category.Label.Add("45", "45 �r");
            myOut.Category.Label.Add("46", "46 �r");
            myOut.Category.Label.Add("47", "47 �r");
            myOut.Category.Label.Add("48", "48 �r");
            myOut.Category.Label.Add("49", "49 �r");
            myOut.Category.Label.Add("50", "50 �r");
            myOut.Category.Label.Add("51", "51 �r");
            myOut.Category.Label.Add("52", "52 �r");
            myOut.Category.Label.Add("53", "53 �r");
            myOut.Category.Label.Add("54", "54 �r");
            myOut.Category.Label.Add("55", "55 �r");
            myOut.Category.Label.Add("56", "56 �r");
            myOut.Category.Label.Add("57", "57 �r");
            myOut.Category.Label.Add("58", "58 �r");
            myOut.Category.Label.Add("59", "59 �r");
            myOut.Category.Label.Add("60", "60 �r");
            myOut.Category.Label.Add("61", "61 �r");
            myOut.Category.Label.Add("62", "62 �r");
            myOut.Category.Label.Add("63", "63 �r");
            myOut.Category.Label.Add("64", "64 �r");
            myOut.Category.Label.Add("65", "65 �r");
            myOut.Category.Label.Add("66", "66 �r");
            myOut.Category.Label.Add("67", "67 �r");
            myOut.Category.Label.Add("68", "68 �r");
            myOut.Category.Label.Add("69", "69 �r");
            myOut.Category.Label.Add("70", "70 �r");
            myOut.Category.Label.Add("71", "71 �r");
            myOut.Category.Label.Add("72", "72 �r");
            myOut.Category.Label.Add("73", "73 �r");
            myOut.Category.Label.Add("74", "74 �r");
            myOut.Category.Label.Add("75", "75 �r");
            myOut.Category.Label.Add("76", "76 �r");
            myOut.Category.Label.Add("77", "77 �r");
            myOut.Category.Label.Add("78", "78 �r");
            myOut.Category.Label.Add("79", "79 �r");
            myOut.Category.Label.Add("80", "80 �r");
            myOut.Category.Label.Add("81", "81 �r");
            myOut.Category.Label.Add("82", "82 �r");
            myOut.Category.Label.Add("83", "83 �r");
            myOut.Category.Label.Add("84", "84 �r");
            myOut.Category.Label.Add("85", "85 �r");
            myOut.Category.Label.Add("86", "86 �r");
            myOut.Category.Label.Add("87", "87 �r");
            myOut.Category.Label.Add("88", "88 �r");
            myOut.Category.Label.Add("89", "89 �r");
            myOut.Category.Label.Add("90", "90 �r");
            myOut.Category.Label.Add("91", "91 �r");
            myOut.Category.Label.Add("92", "92 �r");
            myOut.Category.Label.Add("93", "93 �r");
            myOut.Category.Label.Add("94", "94 �r");
            myOut.Category.Label.Add("95", "95 �r");
            myOut.Category.Label.Add("96", "96 �r");
            myOut.Category.Label.Add("97", "97 �r");
            myOut.Category.Label.Add("98", "98 �r");
            myOut.Category.Label.Add("99", "99 �r");
            myOut.Category.Label.Add("100+", "100+ �r");
            myOut.Category.Label.Add("tot", "totalt �lder");

            myOut.Extension = new ExtensionDimension() { Elimination = true, EliminationValueCode = "tot" };
            myOut.Extension.Filters = new Filters();
            myOut.Extension.Filters.Vs = new List<CodeListInformation>();

            myOut.Extension.Filters.Vs.Add(new CodeListInformation()
            {
                Id = "vs_�lder1�rA",
                Label = "�lder, 1 �rs-klasser",
                Links = new List<Link>() { new Link() { Rel = "metadata", Href = "https://my-site.com/api/v2/tables/TAB638/filters/vs_�lder1�rA" } }
            });

            myOut.Extension.Filters.Vs.Add(new CodeListInformation()
            {
                Id = "vs_�lderTotA",
                Label = "�lder, totalt, alla redovisade �ldrar",
                Links = new List<Link>() { new Link() { Rel = "metadata", Href = "https://my-site.com/api/v2/tables/TAB638/filters/vs_�lderTotA" } }
            });
            myOut.Extension.Filters.Agg = new List<CodeListInformation>();

            myOut.Extension.Filters.Agg.Add(new CodeListInformation()
            {
                Id = "agg_�lder10�r",
                Label = "10-�rsklasser",
                Links = new List<Link>() { new Link() { Rel = "metadata", Href = "https://my-site.com/api/v2/tables/TAB638/filters/agg_�lder10�r" } }
            });

            myOut.Extension.Filters.Agg.Add(new CodeListInformation()
            {
                Id = "agg_�lder5�r",
                Label = "5-�rsklasser",
                Links = new List<Link>() { new Link() { Rel = "metadata", Href = "https://my-site.com/api/v2/tables/TAB638/filters/agg_�lder5�r" } }
            });

            myOut.Link = new JsonstatExtensionLink();

            myOut.Link.Describedby = new List<DimensionExtension>() { new DimensionExtension() { Extension = new Dictionary<string, string>() } };

            myOut.Link.Describedby[0].Extension.Add("Alder", "�lder");

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
                    Text = "�r 1968-1998 redovisas enligt regional indelning 1998-01-01."
                }
            };

            myOut.Extension.ValueNote.Add("0580",
                new List<Note>()
                {
                    new()
                    {
                        Mandatory = true,
                        Text =
                            "Fr�n och med 2011-01-01 tillh�r Storholmen Liding� (0186) som tidigare tillh�rde Vaxholm (0187)."
                    }
                });

            myOut.Extension.ValueNote.Add("0187",
                new List<Note>()
                {
                    new()
                    {
                        Mandatory = true,
                        Text =
                            "Fr�n och med 2011-01-01 tillh�r Storholmen Liding� (0186) som tidigare tillh�rde Vaxholm (0187)."
                    }
                });
            myOut.Extension.ValueNote.Add("0330",
                new List<Note>()
                {
                    new()
                    {
                        Mandatory = true,
                        Text =
                            "Ny regional indelning fr.o.m. 2003-01-01. Delar av Uppsala kommun bildar en ny kommun ben�mnd Knivsta kommun."
                    }
                });
            myOut.Extension.ValueNote.Add("0380",
                new List<Note>()
                {
                    new()
                    {
                        Mandatory = true,
                        Text =
                            "Ny regional indelning fr.o.m. 2003-01-01. Delar av Uppsala kommun bildar en ny kommun ben�mnd Knivsta kommun."
                    }
                });
            myOut.Extension.ValueNote.Add("0140",
                new List<Note>()
                {
                    new()
                    {
                        Mandatory = true,
                        Text =
                            "Ny regional indelning fr.o.m. 1999-01-01. Delar av S�dert�lje kommun (kod 0181) bildar en ny kommun ben�mnd Nykvarn (kod 0140)."
                    }
                });
            myOut.Extension.ValueNote.Add("0181",
                new List<Note>()
                {
                    new()
                    {
                        Mandatory = true,
                        Text =
                            "Ny regional indelning fr.o.m. 1999-01-01. Delar av S�dert�lje kommun (kod 0181) bildar en ny kommun ben�mnd Nykvarn (kod 0140)."
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
            myOut.Category.Label.Add("01", "Stockholms l�n");
            myOut.Category.Label.Add("0114", "Upplands V�sby");
            myOut.Category.Label.Add("0115", "Vallentuna");
            myOut.Category.Label.Add("0117", "�ster�ker");
            myOut.Category.Label.Add("0120", "V�rmd�");
            myOut.Category.Label.Add("0123", "J�rf�lla");
            myOut.Category.Label.Add("0125", "Eker�");
            myOut.Category.Label.Add("0126", "Huddinge");
            myOut.Category.Label.Add("0127", "Botkyrka");
            myOut.Category.Label.Add("0128", "Salem");
            myOut.Category.Label.Add("0136", "Haninge");
            myOut.Category.Label.Add("0138", "Tyres�");
            myOut.Category.Label.Add("0139", "Upplands-Bro");
            myOut.Category.Label.Add("0140", "Nykvarn");
            myOut.Category.Label.Add("0160", "T�by");
            myOut.Category.Label.Add("0162", "Danderyd");
            myOut.Category.Label.Add("0163", "Sollentuna");
            myOut.Category.Label.Add("0180", "Stockholm");
            myOut.Category.Label.Add("0181", "S�dert�lje");
            myOut.Category.Label.Add("0182", "Nacka");
            myOut.Category.Label.Add("0183", "Sundbyberg");
            myOut.Category.Label.Add("0184", "Solna");
            myOut.Category.Label.Add("0186", "Liding�");
            myOut.Category.Label.Add("0187", "Vaxholm");
            myOut.Category.Label.Add("0188", "Norrt�lje");
            myOut.Category.Label.Add("0191", "Sigtuna");
            myOut.Category.Label.Add("0192", "Nyn�shamn");
            myOut.Category.Label.Add("03", "Uppsala l�n");
            myOut.Category.Label.Add("0305", "H�bo");
            myOut.Category.Label.Add("0319", "�lvkarleby");
            myOut.Category.Label.Add("0330", "Knivsta");
            myOut.Category.Label.Add("0331", "Heby");
            myOut.Category.Label.Add("0360", "Tierp");
            myOut.Category.Label.Add("0380", "Uppsala");
            myOut.Category.Label.Add("0381", "Enk�ping");
            myOut.Category.Label.Add("0382", "�sthammar");
            myOut.Category.Label.Add("04", "S�dermanlands l�n");
            myOut.Category.Label.Add("0428", "Ving�ker");
            myOut.Category.Label.Add("0461", "Gnesta");
            myOut.Category.Label.Add("0480", "Nyk�ping");
            myOut.Category.Label.Add("0481", "Oxel�sund");
            myOut.Category.Label.Add("0482", "Flen");
            myOut.Category.Label.Add("0483", "Katrineholm");
            myOut.Category.Label.Add("0484", "Eskilstuna");
            myOut.Category.Label.Add("0486", "Str�ngn�s");
            myOut.Category.Label.Add("0488", "Trosa");
            myOut.Category.Label.Add("05", "�sterg�tlands l�n");
            myOut.Category.Label.Add("0509", "�desh�g");
            myOut.Category.Label.Add("0512", "Ydre");
            myOut.Category.Label.Add("0513", "Kinda");
            myOut.Category.Label.Add("0560", "Boxholm");
            myOut.Category.Label.Add("0561", "�tvidaberg");
            myOut.Category.Label.Add("0562", "Finsp�ng");
            myOut.Category.Label.Add("0563", "Valdemarsvik");
            myOut.Category.Label.Add("0580", "Link�ping");
            myOut.Category.Label.Add("0581", "Norrk�ping");
            myOut.Category.Label.Add("0582", "S�derk�ping");
            myOut.Category.Label.Add("0583", "Motala");
            myOut.Category.Label.Add("0584", "Vadstena");
            myOut.Category.Label.Add("0586", "Mj�lby");
            myOut.Category.Label.Add("06", "J�nk�pings l�n");
            myOut.Category.Label.Add("0604", "Aneby");
            myOut.Category.Label.Add("0617", "Gnosj�");
            myOut.Category.Label.Add("0642", "Mullsj�");
            myOut.Category.Label.Add("0643", "Habo");
            myOut.Category.Label.Add("0662", "Gislaved");
            myOut.Category.Label.Add("0665", "Vaggeryd");
            myOut.Category.Label.Add("0680", "J�nk�ping");
            myOut.Category.Label.Add("0682", "N�ssj�");
            myOut.Category.Label.Add("0683", "V�rnamo");
            myOut.Category.Label.Add("0684", "S�vsj�");
            myOut.Category.Label.Add("0685", "Vetlanda");
            myOut.Category.Label.Add("0686", "Eksj�");
            myOut.Category.Label.Add("0687", "Tran�s");
            myOut.Category.Label.Add("07", "Kronobergs l�n");
            myOut.Category.Label.Add("0760", "Uppvidinge");
            myOut.Category.Label.Add("0761", "Lessebo");
            myOut.Category.Label.Add("0763", "Tingsryd");
            myOut.Category.Label.Add("0764", "Alvesta");
            myOut.Category.Label.Add("0765", "�lmhult");
            myOut.Category.Label.Add("0767", "Markaryd");
            myOut.Category.Label.Add("0780", "V�xj�");
            myOut.Category.Label.Add("0781", "Ljungby");
            myOut.Category.Label.Add("08", "Kalmar l�n");
            myOut.Category.Label.Add("0821", "H�gsby");
            myOut.Category.Label.Add("0834", "Tors�s");
            myOut.Category.Label.Add("0840", "M�rbyl�nga");
            myOut.Category.Label.Add("0860", "Hultsfred");
            myOut.Category.Label.Add("0861", "M�nster�s");
            myOut.Category.Label.Add("0862", "Emmaboda");
            myOut.Category.Label.Add("0880", "Kalmar");
            myOut.Category.Label.Add("0881", "Nybro");
            myOut.Category.Label.Add("0882", "Oskarshamn");
            myOut.Category.Label.Add("0883", "V�stervik");
            myOut.Category.Label.Add("0884", "Vimmerby");
            myOut.Category.Label.Add("0885", "Borgholm");
            myOut.Category.Label.Add("09", "Gotlands l�n");
            myOut.Category.Label.Add("0980", "Gotland");
            myOut.Category.Label.Add("10", "Blekinge l�n");
            myOut.Category.Label.Add("1060", "Olofstr�m");
            myOut.Category.Label.Add("1080", "Karlskrona");
            myOut.Category.Label.Add("1081", "Ronneby");
            myOut.Category.Label.Add("1082", "Karlshamn");
            myOut.Category.Label.Add("1083", "S�lvesborg");
            myOut.Category.Label.Add("12", "Sk�ne l�n");
            myOut.Category.Label.Add("1214", "Sval�v");
            myOut.Category.Label.Add("1230", "Staffanstorp");
            myOut.Category.Label.Add("1231", "Burl�v");
            myOut.Category.Label.Add("1233", "Vellinge");
            myOut.Category.Label.Add("1256", "�stra G�inge");
            myOut.Category.Label.Add("1257", "�rkelljunga");
            myOut.Category.Label.Add("1260", "Bjuv");
            myOut.Category.Label.Add("1261", "K�vlinge");
            myOut.Category.Label.Add("1262", "Lomma");
            myOut.Category.Label.Add("1263", "Svedala");
            myOut.Category.Label.Add("1264", "Skurup");
            myOut.Category.Label.Add("1265", "Sj�bo");
            myOut.Category.Label.Add("1266", "H�rby");
            myOut.Category.Label.Add("1267", "H��r");
            myOut.Category.Label.Add("1270", "Tomelilla");
            myOut.Category.Label.Add("1272", "Brom�lla");
            myOut.Category.Label.Add("1273", "Osby");
            myOut.Category.Label.Add("1275", "Perstorp");
            myOut.Category.Label.Add("1276", "Klippan");
            myOut.Category.Label.Add("1277", "�storp");
            myOut.Category.Label.Add("1278", "B�stad");
            myOut.Category.Label.Add("1280", "Malm�");
            myOut.Category.Label.Add("1281", "Lund");
            myOut.Category.Label.Add("1282", "Landskrona");
            myOut.Category.Label.Add("1283", "Helsingborg");
            myOut.Category.Label.Add("1284", "H�gan�s");
            myOut.Category.Label.Add("1285", "Esl�v");
            myOut.Category.Label.Add("1286", "Ystad");
            myOut.Category.Label.Add("1287", "Trelleborg");
            myOut.Category.Label.Add("1290", "Kristianstad");
            myOut.Category.Label.Add("1291", "Simrishamn");
            myOut.Category.Label.Add("1292", "�ngelholm");
            myOut.Category.Label.Add("1293", "H�ssleholm");
            myOut.Category.Label.Add("13", "Hallands l�n");
            myOut.Category.Label.Add("1315", "Hylte");
            myOut.Category.Label.Add("1380", "Halmstad");
            myOut.Category.Label.Add("1381", "Laholm");
            myOut.Category.Label.Add("1382", "Falkenberg");
            myOut.Category.Label.Add("1383", "Varberg");
            myOut.Category.Label.Add("1384", "Kungsbacka");
            myOut.Category.Label.Add("14", "V�stra G�talands l�n");
            myOut.Category.Label.Add("1401", "H�rryda");
            myOut.Category.Label.Add("1402", "Partille");
            myOut.Category.Label.Add("1407", "�cker�");
            myOut.Category.Label.Add("1415", "Stenungsund");
            myOut.Category.Label.Add("1419", "Tj�rn");
            myOut.Category.Label.Add("1421", "Orust");
            myOut.Category.Label.Add("1427", "Soten�s");
            myOut.Category.Label.Add("1430", "Munkedal");
            myOut.Category.Label.Add("1435", "Tanum");
            myOut.Category.Label.Add("1438", "Dals-Ed");
            myOut.Category.Label.Add("1439", "F�rgelanda");
            myOut.Category.Label.Add("1440", "Ale");
            myOut.Category.Label.Add("1441", "Lerum");
            myOut.Category.Label.Add("1442", "V�rg�rda");
            myOut.Category.Label.Add("1443", "Bollebygd");
            myOut.Category.Label.Add("1444", "Gr�storp");
            myOut.Category.Label.Add("1445", "Essunga");
            myOut.Category.Label.Add("1446", "Karlsborg");
            myOut.Category.Label.Add("1447", "Gullsp�ng");
            myOut.Category.Label.Add("1452", "Tranemo");
            myOut.Category.Label.Add("1460", "Bengtsfors");
            myOut.Category.Label.Add("1461", "Mellerud");
            myOut.Category.Label.Add("1462", "Lilla Edet");
            myOut.Category.Label.Add("1463", "Mark");
            myOut.Category.Label.Add("1465", "Svenljunga");
            myOut.Category.Label.Add("1466", "Herrljunga");
            myOut.Category.Label.Add("1470", "Vara");
            myOut.Category.Label.Add("1471", "G�tene");
            myOut.Category.Label.Add("1472", "Tibro");
            myOut.Category.Label.Add("1473", "T�reboda");
            myOut.Category.Label.Add("1480", "G�teborg");
            myOut.Category.Label.Add("1481", "M�lndal");
            myOut.Category.Label.Add("1482", "Kung�lv");
            myOut.Category.Label.Add("1484", "Lysekil");
            myOut.Category.Label.Add("1485", "Uddevalla");
            myOut.Category.Label.Add("1486", "Str�mstad");
            myOut.Category.Label.Add("1487", "V�nersborg");
            myOut.Category.Label.Add("1488", "Trollh�ttan");
            myOut.Category.Label.Add("1489", "Alings�s");
            myOut.Category.Label.Add("1490", "Bor�s");
            myOut.Category.Label.Add("1491", "Ulricehamn");
            myOut.Category.Label.Add("1492", "�m�l");
            myOut.Category.Label.Add("1493", "Mariestad");
            myOut.Category.Label.Add("1494", "Lidk�ping");
            myOut.Category.Label.Add("1495", "Skara");
            myOut.Category.Label.Add("1496", "Sk�vde");
            myOut.Category.Label.Add("1497", "Hjo");
            myOut.Category.Label.Add("1498", "Tidaholm");
            myOut.Category.Label.Add("1499", "Falk�ping");
            myOut.Category.Label.Add("17", "V�rmlands l�n");
            myOut.Category.Label.Add("1715", "Kil");
            myOut.Category.Label.Add("1730", "Eda");
            myOut.Category.Label.Add("1737", "Torsby");
            myOut.Category.Label.Add("1760", "Storfors");
            myOut.Category.Label.Add("1761", "Hammar�");
            myOut.Category.Label.Add("1762", "Munkfors");
            myOut.Category.Label.Add("1763", "Forshaga");
            myOut.Category.Label.Add("1764", "Grums");
            myOut.Category.Label.Add("1765", "�rj�ng");
            myOut.Category.Label.Add("1766", "Sunne");
            myOut.Category.Label.Add("1780", "Karlstad");
            myOut.Category.Label.Add("1781", "Kristinehamn");
            myOut.Category.Label.Add("1782", "Filipstad");
            myOut.Category.Label.Add("1783", "Hagfors");
            myOut.Category.Label.Add("1784", "Arvika");
            myOut.Category.Label.Add("1785", "S�ffle");
            myOut.Category.Label.Add("18", "�rebro l�n");
            myOut.Category.Label.Add("1814", "Lekeberg");
            myOut.Category.Label.Add("1860", "Lax�");
            myOut.Category.Label.Add("1861", "Hallsberg");
            myOut.Category.Label.Add("1862", "Degerfors");
            myOut.Category.Label.Add("1863", "H�llefors");
            myOut.Category.Label.Add("1864", "Ljusnarsberg");
            myOut.Category.Label.Add("1880", "�rebro");
            myOut.Category.Label.Add("1881", "Kumla");
            myOut.Category.Label.Add("1882", "Askersund");
            myOut.Category.Label.Add("1883", "Karlskoga");
            myOut.Category.Label.Add("1884", "Nora");
            myOut.Category.Label.Add("1885", "Lindesberg");
            myOut.Category.Label.Add("19", "V�stmanlands l�n");
            myOut.Category.Label.Add("1904", "Skinnskatteberg");
            myOut.Category.Label.Add("1907", "Surahammar");
            myOut.Category.Label.Add("1960", "Kungs�r");
            myOut.Category.Label.Add("1961", "Hallstahammar");
            myOut.Category.Label.Add("1962", "Norberg");
            myOut.Category.Label.Add("1980", "V�ster�s");
            myOut.Category.Label.Add("1981", "Sala");
            myOut.Category.Label.Add("1982", "Fagersta");
            myOut.Category.Label.Add("1983", "K�ping");
            myOut.Category.Label.Add("1984", "Arboga");
            myOut.Category.Label.Add("20", "Dalarnas l�n");
            myOut.Category.Label.Add("2021", "Vansbro");
            myOut.Category.Label.Add("2023", "Malung-S�len");
            myOut.Category.Label.Add("2026", "Gagnef");
            myOut.Category.Label.Add("2029", "Leksand");
            myOut.Category.Label.Add("2031", "R�ttvik");
            myOut.Category.Label.Add("2034", "Orsa");
            myOut.Category.Label.Add("2039", "�lvdalen");
            myOut.Category.Label.Add("2061", "Smedjebacken");
            myOut.Category.Label.Add("2062", "Mora");
            myOut.Category.Label.Add("2080", "Falun");
            myOut.Category.Label.Add("2081", "Borl�nge");
            myOut.Category.Label.Add("2082", "S�ter");
            myOut.Category.Label.Add("2083", "Hedemora");
            myOut.Category.Label.Add("2084", "Avesta");
            myOut.Category.Label.Add("2085", "Ludvika");
            myOut.Category.Label.Add("21", "G�vleborgs l�n");
            myOut.Category.Label.Add("2101", "Ockelbo");
            myOut.Category.Label.Add("2104", "Hofors");
            myOut.Category.Label.Add("2121", "Ovan�ker");
            myOut.Category.Label.Add("2132", "Nordanstig");
            myOut.Category.Label.Add("2161", "Ljusdal");
            myOut.Category.Label.Add("2180", "G�vle");
            myOut.Category.Label.Add("2181", "Sandviken");
            myOut.Category.Label.Add("2182", "S�derhamn");
            myOut.Category.Label.Add("2183", "Bolln�s");
            myOut.Category.Label.Add("2184", "Hudiksvall");
            myOut.Category.Label.Add("22", "V�sternorrlands l�n");
            myOut.Category.Label.Add("2260", "�nge");
            myOut.Category.Label.Add("2262", "Timr�");
            myOut.Category.Label.Add("2280", "H�rn�sand");
            myOut.Category.Label.Add("2281", "Sundsvall");
            myOut.Category.Label.Add("2282", "Kramfors");
            myOut.Category.Label.Add("2283", "Sollefte�");
            myOut.Category.Label.Add("2284", "�rnsk�ldsvik");
            myOut.Category.Label.Add("23", "J�mtlands l�n");
            myOut.Category.Label.Add("2303", "Ragunda");
            myOut.Category.Label.Add("2305", "Br�cke");
            myOut.Category.Label.Add("2309", "Krokom");
            myOut.Category.Label.Add("2313", "Str�msund");
            myOut.Category.Label.Add("2321", "�re");
            myOut.Category.Label.Add("2326", "Berg");
            myOut.Category.Label.Add("2361", "H�rjedalen");
            myOut.Category.Label.Add("2380", "�stersund");
            myOut.Category.Label.Add("24", "V�sterbottens l�n");
            myOut.Category.Label.Add("2401", "Nordmaling");
            myOut.Category.Label.Add("2403", "Bjurholm");
            myOut.Category.Label.Add("2404", "Vindeln");
            myOut.Category.Label.Add("2409", "Robertsfors");
            myOut.Category.Label.Add("2417", "Norsj�");
            myOut.Category.Label.Add("2418", "Mal�");
            myOut.Category.Label.Add("2421", "Storuman");
            myOut.Category.Label.Add("2422", "Sorsele");
            myOut.Category.Label.Add("2425", "Dorotea");
            myOut.Category.Label.Add("2460", "V�nn�s");
            myOut.Category.Label.Add("2462", "Vilhelmina");
            myOut.Category.Label.Add("2463", "�sele");
            myOut.Category.Label.Add("2480", "Ume�");
            myOut.Category.Label.Add("2481", "Lycksele");
            myOut.Category.Label.Add("2482", "Skellefte�");
            myOut.Category.Label.Add("25", "Norrbottens l�n");
            myOut.Category.Label.Add("2505", "Arvidsjaur");
            myOut.Category.Label.Add("2506", "Arjeplog");
            myOut.Category.Label.Add("2510", "Jokkmokk");
            myOut.Category.Label.Add("2513", "�verkalix");
            myOut.Category.Label.Add("2514", "Kalix");
            myOut.Category.Label.Add("2518", "�vertorne�");
            myOut.Category.Label.Add("2521", "Pajala");
            myOut.Category.Label.Add("2523", "G�llivare");
            myOut.Category.Label.Add("2560", "�lvsbyn");
            myOut.Category.Label.Add("2580", "Lule�");
            myOut.Category.Label.Add("2581", "Pite�");
            myOut.Category.Label.Add("2582", "Boden");
            myOut.Category.Label.Add("2583", "Haparanda");
            myOut.Category.Label.Add("2584", "Kiruna");

            myOut.Extension.Elimination = true;
            myOut.Extension.EliminationValueCode = "00";
            myOut.Extension.Filters = new Filters();
            myOut.Extension.Filters.Vs = new List<CodeListInformation>();

            myOut.Extension.Filters.Vs.Add(new CodeListInformation()
            {
                Id = "vs_RegionKommun07",
                Label = "Kommuner",
                Links = new List<Link>() { new() { Rel = "metadata", Href = "https://my-site.com/api/v2/tables/TAB638/filters/vs_RegionKommun07" } }
            });

            myOut.Extension.Filters.Vs.Add(new CodeListInformation()
            {
                Id = "vs_RegionL�n07",
                Label = "L�n",
                Links = new List<Link>() { new() { Rel = "metadata", Href = "https://my-site.com/api/v2/tables/TAB638/filters/vs_RegionL�n07" } }
            });

            myOut.Extension.Filters.Vs.Add(new CodeListInformation()
            {
                Id = "vs_RegionRiket99",
                Label = "Riket",
                Links = new List<Link>() { new() { Rel = "metadata", Href = "https://my-site.com/api/v2/tables/TAB638/filters/vs_RegionRiket99" } }
            });


            myOut.Extension.Filters.Agg = new List<CodeListInformation>();

            myOut.Extension.Filters.Agg.Add(new CodeListInformation()
            {
                Id = "agg_RegionA-region_2",
                Label = "A-regioner",
                Links = new List<Link>() { new() { Rel = "metadata", Href = "https://my-site.com/api/v2/tables/TAB638/filters/agg_RegionA-region_2" } }
            });

            myOut.Extension.Filters.Agg.Add(new CodeListInformation()
            {
                Id = "agg_RegionKommungrupp2005-_1",
                Label = "Kommungrupper (SKL:s) 2005",
                Links = new List<Link>() { new()
                                {
                                    Rel ="metadata",
                                   Href ="https://my-site.com/api/v2/tables/TAB638/filters/agg_RegionKommungrupp2005-_1"} }
            });
            myOut.Extension.Filters.Agg.Add(new CodeListInformation()
            {
                Id = "agg_RegionKommungrupp2011-",
                Label = "Kommungrupper (SKL:s) 2011",
                Links = new List<Link>() { new()
                                {
                                    Rel ="metadata",
                                   Href ="https://my-site.com/api/v2/tables/TAB638/filters/agg_RegionKommungrupp2011-"} }
            });
            myOut.Extension.Filters.Agg.Add(new CodeListInformation()
            {
                Id = "agg_RegionKommungrupp2017-",
                Label = "Kommungrupper (SKL:s) 2017",
                Links = new List<Link>() { new()
                                {
                                    Rel ="metadata",
                                   Href ="https://my-site.com/api/v2/tables/TAB638/filters/agg_RegionKommungrupp2017-"} }
            });
            myOut.Extension.Filters.Agg.Add(new CodeListInformation()
            {
                Id = "agg_RegionLA1998",
                Label = "Lokalaarbetsmarknader 1998",
                Links = new List<Link>() { new()
                                {
                                    Rel ="metadata",
                                   Href ="https://my-site.com/api/v2/tables/TAB638/filters/agg_RegionLA1998"} }
            });
            myOut.Extension.Filters.Agg.Add(new CodeListInformation()
            {
                Id = "agg_RegionLA2003_1",
                Label = "Lokalaarbetsmarknader 2003",
                Links = new List<Link>() { new()
                                {
                                    Rel ="metadata",
                                   Href ="https://my-site.com/api/v2/tables/TAB638/filters/agg_RegionLA2003_1"} }
            });
            myOut.Extension.Filters.Agg.Add(new CodeListInformation()
            {
                Id = "agg_RegionLA2008",
                Label = "Lokalaarbetsmarknader 2008",
                Links = new List<Link>() { new()
                                {
                                    Rel ="metadata",
                                   Href ="https://my-site.com/api/v2/tables/TAB638/filters/agg_RegionLA2008"} }
            });
            myOut.Extension.Filters.Agg.Add(new CodeListInformation()
            {
                Id = "agg_RegionLA2013",
                Label = "Lokalaarbetsmarknader 2013",
                Links = new List<Link>() { new()
                                {
                                    Rel ="metadata",
                                   Href ="https://my-site.com/api/v2/tables/TAB638/filters/agg_RegionLA2013"} }
            });
            myOut.Extension.Filters.Agg.Add(new CodeListInformation()
            {
                Id = "agg_RegionLA2018",
                Label = "Lokalaarbetsmarknader 2018",
                Links = new List<Link>() { new()
                                {
                                    Rel ="metadata",
                                   Href ="https://my-site.com/api/v2/tables/TAB638/filters/agg_�lder5�r"} }
            });
            myOut.Extension.Filters.Agg.Add(new CodeListInformation()
            {
                Id = "agg_RegionStoromr-04_2",
                Label = "Sorstadsomr�der -2004",
                Links = new List<Link>() { new()
                                {
                                    Rel ="metadata",
                                   Href ="https://my-site.com/api/v2/tables/TAB638/filters/agg_RegionStoromr-04_2"} }
            });
            myOut.Extension.Filters.Agg.Add(new CodeListInformation()
            {
                Id = "agg_RegionStoromr05-_1",
                Label = "Sorstadsomr�der 2005-",
                Links = new List<Link>() { new()
                                {
                                    Rel ="metadata",
                                   Href ="https://my-site.com/api/v2/tables/TAB638/filters/agg_RegionStoromr05-_1"} }
            });
            myOut.Extension.Filters.Agg.Add(new CodeListInformation()
            {
                Id = "agg_RegionNUTS1_2008",
                Label = "NUTS1 fr.o.m 2008",
                Links = new List<Link>() { new()
                                {
                                    Rel ="metadata",
                                   Href ="https://my-site.com/api/v2/tables/TAB638/filters/agg_RegionNUTS1_2008"} }
            });
            myOut.Extension.Filters.Agg.Add(new CodeListInformation()
            {
                Id = "agg_RegionNUTS2_2008",
                Label = "NUTS2 fr.o.m 2008",
                Links = new List<Link>() { new()
                                {
                                    Rel ="metadata",
                                   Href ="https://my-site.com/api/v2/tables/TAB638/filters/agg_RegionNUTS2_2008"} }
            });
            myOut.Extension.Filters.Agg.Add(new CodeListInformation()
            {
                Id = "agg_RegionNUTS3_2008",
                Label = "NUTS3 fr.o.m 2008",
                Links = new List<Link>() { new()
                                {
                                    Rel ="metadata",
                                   Href ="https://my-site.com/api/v2/tables/TAB638/filters/agg_RegionNUTS3_2008"} }
            });
            return myOut;
        }


    }
}
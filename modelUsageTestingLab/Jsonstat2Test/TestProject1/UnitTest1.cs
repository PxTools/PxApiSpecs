using Microsoft.VisualStudio.TestTools.UnitTesting;
using PCAxis.OpenAPILib.Models;
using System.Collections.Generic;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private Helper helper = new Helper();

        [TestMethod]
        public void TestMethod1()
        {
            Dataset ds = new Dataset();

            //ds.Class = Dataset.ClassEnum.DatasetEnum;

            // defaults : "version": "2.0", "class": "dataset",
            ds.Label = "Folkm�ngden efter region, civilst�nd, �lder, k�n, tabellinneh�ll och �r";
            ds.Source = "SCB";
            ds.Updated = "2022-02-07T14:32:00Z";
            ds.Link = new Dictionary<string, List<JsonstatLink>>();
            ds.Href = "https://my-site.com/api/v2/tables/TAB638/metadata";


            //ds.AddRootLink(""describes", "https://my-site.com/api/v2/tables/TAB638/data","application/json" );
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
            ds.Extension.Px = new ExtensionRootPx { Infofile = "BE0101", Tableid = "TAB638", Decimals = 0 };
            ds.Extension.Id = "TAB638";
            ds.Extension.Language = "sv";
            ds.Extension.Category = "BE";
            ds.Extension.Description = "";
            ds.Extension.Tags = new List<string>();
            ds.Extension.Tags.Add("");
            ds.Extension.Frequency = "A";
            ds.Extension.OfficialStatistics = true;
            ds.Extension.AggregationPossible = false;
            ds.Extension.Discontinued = false;


            ds.Extension.Contacts = new List<Contact>();
            ds.Extension.Contacts.Add(new Contact { Name = "Tomas Johansson, SCB", Phone = "+46 010-479 64 26", Mail = "tomas.johansson@scb.se" });
            ds.Extension.Contacts.Add(new Contact { Name = "Statistikservice, SCB", Phone = "+46 010-479 50 0", Mail = "information@scb.se" });

            ds.Extension.Refperiod = "31 December each year";
            ds.Extension.Copyright = "CC-0";

            ds.Role = new DatasetRole();
            ds.Role.Time = new List<string> { "Tid" };

            ds.Role.Geo =   new List<string> { "Region" };

            ds.Role.Metric = new List<string> { "ContentsCode" };

            ds.Id = new List<string> { "Region", "Civilstand", "Alder", "Kon", "ContentsCode", "Tid" };

            ds.Size = new List<int> { 312, 4, 1, 2, 2, 1};


            ds.Dimension = new Dictionary<string, DatasetDimensionValue>();

            ds.Dimension.Add("Region", new DatasetDimensionValue());
            ds.Dimension["Region"].Label = "region";
            ds.Dimension["Region"].Note = new List<string> { "�r 1968-1998 redovisas enligt regional indelning 1998-01-01." };
            ds.Dimension["Region"].Category =  new JsonstatCategory();
            ds.Dimension["Region"].Category.Note = new Dictionary<string, List<string>>();
            ds.Dimension["Region"].Category.Note.Add("0580", new List<string> { "Fr�n och med 2011-01-01 tillh�r Storholmen Liding� (0186) som tidigare tillh�rde Vaxholm (0187)." });

            ds.Dimension["Region"].Category.Note.Add("0187", new List<string> { "Fr�n och med 2011-01-01 tillh�r Storholmen Liding� (0186) som tidigare tillh�rde Vaxholm (0187)." });
            ds.Dimension["Region"].Category.Note.Add("0330", new List<string> { "Ny regional indelning fr.o.m. 2003-01-01. Delar av Uppsala kommun bildar en ny kommun ben�mnd Knivsta kommun." });
            ds.Dimension["Region"].Category.Note.Add("0380", new List<string> { "Ny regional indelning fr.o.m. 2003-01-01. Delar av Uppsala kommun bildar en ny kommun ben�mnd Knivsta kommun." });
            ds.Dimension["Region"].Category.Note.Add("0140", new List<string> { "Ny regional indelning fr.o.m. 1999-01-01. Delar av S�dert�lje kommun (kod 0181) bildar en ny kommun ben�mnd Nykvarn (kod 0140)." });
            ds.Dimension["Region"].Category.Note.Add("0181", new List<string> { "Ny regional indelning fr.o.m. 1999-01-01. Delar av S�dert�lje kommun (kod 0181) bildar en ny kommun ben�mnd Nykvarn (kod 0140)." });
            //ds.Dimension["Region"].Category.Note.Add( , new List<string> { });
            




            ds.Note = new List<string> 
            {
                "Fr o m 2007-01-01 �verf�rs Heby kommun fr�n V�stmanlands l�n till Uppsala l�n. Hebys kommunkod �ndras fr�n 1917 till 0331. ",
                "Registrerat partnerskap reglerade parf�rh�llanden mellan personer av samma k�n och fanns fr�n 1995 till 2009. Registrerade partners r�knas som Gifta, Separerade partners som Skilda och Efterlevande partners som �nka/�nklingar.",
                "Fr o m 2007-01-01 ut�kas Uppsala l�n med Heby kommun. Observera att l�nssiffrorna inte �r j�mf�rbara med l�nssiffrorna bak�t i tiden." 
            };

            string myOut = ds.ToJson();

            var jsonExample = helper.GetExampleJsonstat();

            string a = "a";
        }
    }
}
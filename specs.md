# PxApi 2.0

# Readers guide
## What this instruction covers
This document specifies the  queries and response formats and also the endpoints for the 2.0 version of the PX web API.

## Terminology
- *Paxiom* the object model representing a statistical cube
- *table* a multidimensional table containing statistical measures and metadata about these measures stored in a PX-File or a database using the CNMM.
- *CNMM* the Common Nordic Meta Model which is the database structure that is used and maintained by Statistics Sweden, Norway and Denmark.

**References**

[https://www.dst.dk/en/Statistik/statistikbanken/api](https://www.dst.dk/en/Statistik/statistikbanken/api)

[http://api.statbank.dk/console#data](http://api.statbank.dk/console#data)

[https://www.vinaysahni.com/best-practices-for-a-pragmatic-restful-api](https://www.vinaysahni.com/best-practices-for-a-pragmatic-restful-api)

# Information model for a Table
TODO

## Model mappings
### Mapping to Paxiom
Table = PXModel

Value = Value

FilterableVariable = Variable

Filter = ValueSet or Grouping (bad name, what shall we have instead???)

### PX-file database
Filter = Aggregation
Stream = N/A

### CNMM
Filter = ValueSet or Grouping
Stream = Subtable

# Restrictions
Some restrictions in the 2.0 version of the API
-	**Only one database** – Drop the support of many databases that can be served by the same instance of the API. This will lower the complexity of the internals of the API since it does not have to take into account different configuration for different databases. Most organizations only have one database so the effect is limited only to a few. The solution for organizations with multiple databases is to have multiple instances of the API with different configuration.
-	**New table identity** – The id for a table is changed from in the case of a PX-file name to MATRIX  and TableId instead of MainTable . The new id should be stable and not be reused. If a table change in such a way that it is no longer compatible it should be duplicated and assign an new id. This way the API will be able to identify the table even if it has been moved in the database.
-	**Names of aggregation files must be unique** – names of the aggregation files within a database must be unique since they will serve as the id of the aggregation filter.
-	VARIABLETYPE must be formalized.
-	Initially only support for PX, JSON-STAT, CSV,TSV, Excel and JSON-STAT2 (this should be possible to configure) 
-	~~Client should accept new properties in the responses without breaking.~~

# API endpoints
~~All the endpoints are able to handle GET and POST HTTP request. GET is the preferred method in most cases. They result in the same response which is not very RESTful instead it is a pragmatically solution. All the examples bellow uses the GET method if you wish to do a POST request you will do it to the same url but instead of specifying query string parameters you could specify the same parameters as a JSON object having the same name as the parameter and sending that JSON object as the content of the POST request.~~

**Example**
```json
{
  "lang": "en",
}
```
POST is primarily intended to be used when fetching data since the query to select the data can in some cases become very large. So large that it can extends come web browser limits on url’s .

Proposal: Look at Json-Stat collections 

## Configuration endpoint
Get the configuration of the API

**url:** `http://my-site.com/api/v2/config`

**HTTP method:** GET

**Response**
```json
{
    "apiVersion": "2.0",
    "languages": [
        {
            "id": "sv",
            "lable": "Svenska"
        },
        {
            "id": "en",
            "lable": "English"
        }
    ],
    "defaultLanguage": "sv",
    "maxDataCells": 10000,
    "maxCalls": 30,
    "timeWindow": 10,
    "sourceReferences": [
        {
            "language": "sv",
            "text": "Källa: SCB"
        },
        {
            "language": "en",
            "text": "Source: Statistics Sweden"
        }
    ],
    "license": "https://creativecommons.org/share-your-work/public-domain/cc0/",
    "features": [
        {
            "id": "CORS",
            "params": [
                {
                    "key": "enabled",
                    "value": "True"
                }
            ]
        }
    ],
   "dataFormats": ["px", "json-stat", "csv", "tsv"],
   "defaultDataFormat": "px",	
   "streamingFormats": ["csv", "tsv"],
   "defaultStreamingFormat": "csv",
}
```

## Navigation endpoint 

Browse the database structure.

**url:** `http://my-site.com/api/v2/navigation`

Returns the database root folder.

**url:** `http://my-site.com/api/v2/navigation/{id}`

Returns the database folder identified by *id*.

**HTTP method:** GET

**Response example**

The following example shows the response of the API request `http://my-site.com/api/v2/navigation/BE0101`. Metadata about the folder BE0101 is returned together with the folder contents of the BE0101 folder, which is a subfolder BE0101A and the statistical table BefolkningNy.
```json
{
  "id": "BE0101",
  "objectType": "Folder",
  "label": "Population statistics",
  "description": "",
  "tags": null,
  "links": [
    {
      "rel": "self",
      "href": "http://my-site.com/api/v2/navigation/BE0101"
    }
  ],
  "folderContents": [
    {
      "id": "BE0101A",
      "objectType": "FolderInformation",
      "label": "Number of inhabitants",
      "description": "",
      "tags": null,
      "links": [
        {
          "rel": "folder",
          "href": "http://my-site.com/api/v2/navigation/BE0101A"
        }
      ]
    },
    {
      "id": "BefolkningNy",
      "objectType": "Table",
      "label": "Population by region, marital status, age and sex.  Year",
      "description": "",
      "updated": "2019-02-21T09:30:00",
      "category": "official",
      "firstPeriod": "1968",
      "lastPeriod": "2018",
      "discontinued": false,
      "tags": null,
      "links": [
        {
          "rel": "self",
          "href": "http://my-site.com/api/v2/tables/BefolkningNy"
        },
        {
          "rel": "metadata",
          "href": "http://my-site.com/api/v2/tables/BefolkningNy/metadata"
        },
        {
          "rel": "data",
          "href": "http://my-site.com/api/v2/tables/BefolkningNy/data"
        }
      ]
    }
  ]
}
```
**Response described**

The Navigation endpoint returns two objects:
1. A *Folder* object containing metadata about the folder asked for
2. An array *folderContents* containing the contents of the folder (subfolders and statistical tables)

There are three possible values for *objectType*:

1. *Folder* - The folder asked for in the API request.

2. *FolderInformation* - A subfolder to the *Folder* object.

3. *Table* - A statistical table located in the folder.

**Folder metadata**

*id* - Folder id

*objectType* - Can have one of two possible values:
- *Folder* (the folder asked for in the API request)
- *FolderInformation* (subfolder)

*label* - Folder text

*description* - Folder description

*tags* - Folder tags (not implemented yet)

*links* - How to navigate to the folder

**Table metadata**

*id* - Table id

*objectType* - Will have the value "Table"

*label* - Table text

*description* - Table description

*updated* - When the table was last updated

*category* - Presentation category for the table. Possible values are:
- internal
- official
- private
- section

*firstPeriod* - The first data time period in the table

*lastPeriod* - The last data time period in the table

*discontinued* - If the table will be updated with new data or not

*tags* - Table tags (not implemented yet)

*links* - How to navigate to the table. For tables there are three links:

- self - How to navigate to the table
- metadata - How to navigate to the table metadata
- data - How to navigate to the table data

**More ...**

Proposal: 
The querystring parameter recursive=true will return all sub nodes for the specified node.
Possible to define number of recursive levels. 

Question:
Will http://my-site.com/api/v2/tables/TAB0001/data return default values or is it necessary to also specify variables and values to this URL to get data? 
-	Yes, if the number of resulting cells is lower than the max cell limit.
Same selection rules as in the current API.
Do not make it the whole url ony specify the nodeid like
http://my-site.com/api/v2/database/BE and http://my-site.com/api/v2/database/BE0401
and not http://my-site.com/api/v2/database/BE/BE0401.. OK (ge exempel på PX fil fallet)

Should not LINK and HEADLINE menu types be included? Suggestion is to skip the LINK type.

Suggested properties for all types
* Id
* Label
* Description

Suggested properties for table type
* LastUpdated
* Published
* TableCategory (PresCategory)




## Table  endpoint
### List all tables
**url:** `/api/v2/tables/`

**HTTP method:** GET|POST

List all tables in the database

**Response**
```json
[
   {
     "id": "TAB0001",
     "text": "Tabell A",
     "updated": "2018-01-01T09:30:00",
     "links": [{
               "rel": "metadata",
               "href": "http://my-site.com/api/v2/tables/TAB0001"},
             {
               "rel":"data",
               "href": "http://my-site.com/api/v2/tables/TAB0001/data"}]
   },
   {
     "id": "TAB0002",
     "text": "Tabell B",
     "updated": "2018-01-22T09:30:00",
     "links": [{
               "rel": "metadata",
               "href": "http://my-site.com/api/v2/tables/TAB0002"},
             {
               "rel": "data",
               "href": "http://my-site.com/api/v2/tables/TAB0002/data"}]

   }
]
```
#### Parameters
You can restrict the tables return by the following parameters

##### pastDays
Selects only tables that was updated from the time of execution going back number of days stated by the parameter pastDays. Valid values for past days are integers between 1 and ? (Will return error?)
```
http://my-site.com/api/v2/tables?pastDays=5
```
Question: Which date in the database/PX-file shall we check against?
-	PX-file: LAST-UPDATED
-	CNMM: Published

~~##### updatedSince
Selects only tables that was updated after and including the date specified by the parameter updatedSince.
```
http://my-site.com/api/v2/tables?updatedSince=2018-01-15
```
Question: Which date in the database/PX-file shall we check against?
-	PX-file: LAST-UPDATEDCNMM: Published~~

##### query
Selects only tables that that matches a criteria which is specified by the search parameter.
```
http://my-site.com/api/v2/tables?query=befolkning
```
Question: Which metadata shall we check against?
Exempel på hur man kan begränsa till en viss egenskap i sökindex.
Search index match against:
-	Table id
-	Table title
-	Value text
-	Value code
-	Matrix
-	Variable name
-	Period
-	Grouping name
-	Grouping codes
-	Valueset name
-	Valueset codes
- 	(keyword) ???

Proposal: 
endedTables=false will omit ended tables in the table list. There is a new code for ended tables. What is the code? How will this work with PX-files?
From documentation:
D = The table is no longer updated but is accessible to all
-	PXModel needs to be extended
-	No support for this in PX-files (new keyword needed?)
-	Notering om att det kan skilja sig mellan och PX fil baserade databaser.

### List metadata for a table
**url:** `/api/v2/tables/<table-id>`

**HTTP method:** GET|POST

List metadata for the specified table
Proposal: Add a property lanuage to the response
**Response**
```json
{
  "id": "TAB638",
  "label": "Folkmängden efter region; civilstånd; ålder och kön. År 1968 - 2021",
  "description": "",
  "aggregationAllowed": false,
  "officalStatistics": true,
  "source": "SCB",
  "licence": "CC0-1.0",
  "updated": null,
  "notes": [
    {
      "conditions": null,
      "mandatory": true,
      "text": "Fr o m 2007-01-01 överförs Heby kommun från Västmanlands län till Uppsala län. Hebys kommunkod ändras från 1917 till 0331. "
    },
    {
      "conditions": null,
      "mandatory": true,
      "text": "Registrerat partnerskap reglerade parförhållanden mellan personer av samma kön och fanns från 1995 till 2009. Registrerade partners räknas som Gifta, Separerade partners som Skilda och Efterlevande partners som Änka/änklingar."
    },
    {
      "conditions": [
        {
          "variable": "ContentsCode",
          "value": "BE0101N1"
        },
        {
          "variable": "Region",
          "value": "03"
        }
      ],
      "mandatory": true,
      "text": "Fr o m 2007-01-01 utökas Uppsala län med Heby kommun. Observera att länssiffrorna inte är jämförbara med länssiffrorna bakåt i tiden."
    },
    {
      "conditions": [
        {
          "variable": "ContentsCode",
          "value": "BE0101N1"
        },
        {
          "variable": "Region",
          "value": "19"
        }
      ],
      "mandatory": false,
      "text": "Fr o m 2007-01-01 minskar Västmanlands län med Heby kommun. Observera att länssiffrorna inte är jämförbara med länssiffrorna bakåt i tiden."
    }
  ],
  "contacts": [
    {
      "name": "Tomas Johansson",
      "phone": "+46 010-479 64 26",
      "mail": "tomas.johansson@scb.se"
    },
    {
      "name": "(SCB) Statistikservice",
      "phone": "+46 010-479 50 0",
      "mail": "information@scb.se"
    }
  ],
  "variables": [
    {
      "id": "Tid",
      "label": "år",
      "type": 0,
      "timeUnit": "Annual",
      "firstPeriod": "1968",
      "lastPeriod": "2021",
      "values": [
        {
          "code": "1968",
          "label": "1968"
        },
        {
          "code": "1969",
          "label": "1969"
        },
        {
          "code": "1970",
          "label": "1970"
        },
        {
          "code": "1971",
          "label": "1971"
        },
        {
          "code": "1972",
          "label": "1972"
        },
        {
          "code": "1973",
          "label": "1973"
        },
        {
          "code": "1974",
          "label": "1974"
        },
        {
          "code": "1975",
          "label": "1975"
        },
        {
          "code": "1976",
          "label": "1976"
        },
        {
          "code": "1977",
          "label": "1977"
        },
        {
          "code": "1978",
          "label": "1978"
        },
        {
          "code": "1979",
          "label": "1979"
        },
        {
          "code": "1980",
          "label": "1980"
        },
        {
          "code": "1981",
          "label": "1981"
        },
        {
          "code": "1982",
          "label": "1982"
        },
        {
          "code": "1983",
          "label": "1983"
        },
        {
          "code": "1984",
          "label": "1984"
        },
        {
          "code": "1985",
          "label": "1985"
        },
        {
          "code": "1986",
          "label": "1986"
        },
        {
          "code": "1987",
          "label": "1987"
        },
        {
          "code": "1988",
          "label": "1988"
        },
        {
          "code": "1989",
          "label": "1989"
        },
        {
          "code": "1990",
          "label": "1990"
        },
        {
          "code": "1991",
          "label": "1991"
        },
        {
          "code": "1992",
          "label": "1992"
        },
        {
          "code": "1993",
          "label": "1993"
        },
        {
          "code": "1994",
          "label": "1994"
        },
        {
          "code": "1995",
          "label": "1995"
        },
        {
          "code": "1996",
          "label": "1996"
        },
        {
          "code": "1997",
          "label": "1997"
        },
        {
          "code": "1998",
          "label": "1998"
        },
        {
          "code": "1999",
          "label": "1999"
        },
        {
          "code": "2000",
          "label": "2000"
        },
        {
          "code": "2001",
          "label": "2001"
        },
        {
          "code": "2002",
          "label": "2002"
        },
        {
          "code": "2003",
          "label": "2003"
        },
        {
          "code": "2004",
          "label": "2004"
        },
        {
          "code": "2005",
          "label": "2005"
        },
        {
          "code": "2006",
          "label": "2006"
        },
        {
          "code": "2007",
          "label": "2007"
        },
        {
          "code": "2008",
          "label": "2008"
        },
        {
          "code": "2009",
          "label": "2009"
        },
        {
          "code": "2010",
          "label": "2010"
        },
        {
          "code": "2011",
          "label": "2011"
        },
        {
          "code": "2012",
          "label": "2012"
        },
        {
          "code": "2013",
          "label": "2013"
        },
        {
          "code": "2014",
          "label": "2014"
        },
        {
          "code": "2015",
          "label": "2015"
        },
        {
          "code": "2016",
          "label": "2016"
        },
        {
          "code": "2017",
          "label": "2017"
        },
        {
          "code": "2018",
          "label": "2018"
        },
        {
          "code": "2019",
          "label": "2019"
        },
        {
          "code": "2020",
          "label": "2020"
        },
        {
          "code": "2021",
          "label": "2021"
        }
      ]
    },
    {
      "id": "ContentsCode",
      "label": "tabellinnehåll",
      "type": 0,
      "values": [
        {
          "baseperiod": null,
          "adjustment": "None",
          "measuringType": "Stock",
          "preferedNumberOfDecimals": 0,
          "priceType": 0,
          "unit": "antall",
          "refrencePeriod": "31 december repektive år",
          "code": "BE0101N1",
          "label": "Folkmängd",
          "notes": [
            {
              "mandatory": true,
              "text": "Uppgifterna avser förhållandena den 31 december för valt/valda år enligt den regionala indelning som gäller den 1 januari året efter."
            }
          ]
        },
        {
          "baseperiod": null,
          "adjustment": "None",
          "measuringType": "Flow",
          "preferedNumberOfDecimals": 0,
          "priceType": 0,
          "unit": "antall",
          "code": "BE0101N2",
          "label": "Folkökning",
          "notes": [
            {
              "mandatory": false,
              "text": "Folkökningen definieras som skillnaden mellan folkmängden vid årets början och årets slut."
            }
          ]
        }
      ]
    },
    {
      "id": "Civilstand",
      "label": "civilstånd",
      "type": 0,
      "elimination": true,
      "values": [
        {
          "code": "OG",
          "label": "ogifta"
        },
        {
          "code": "G",
          "label": "gifta"
        },
        {
          "code": "SK",
          "label": "skilda"
        },
        {
          "code": "ÄNKL",
          "label": "änkor/änklingar"
        }
      ]
    },
    {
      "id": "Kon",
      "label": "kön",
      "type": 0,
      "elimination": true,
      "values": [
        {
          "code": "1",
          "label": "män"
        },
        {
          "code": "2",
          "label": "kvinnor"
        }
      ]
    },
    {
      "id": "Alder",
      "label": "ålder",
      "type": 0,
      "elimination": true,
      "eliminationValueCode": "tot",
      "values": [
        {
          "code": "0",
          "label": "0 år"
        },
        {
          "code": "1",
          "label": "1 år"
        },
        {
          "code": "2",
          "label": "2 år"
        },
        {
          "code": "3",
          "label": "3 år"
        },
        {
          "code": "4",
          "label": "4 år"
        },
        {
          "code": "5",
          "label": "5 år"
        },
        {
          "code": "6",
          "label": "6 år"
        },
        {
          "code": "7",
          "label": "7 år"
        },
        {
          "code": "8",
          "label": "8 år"
        },
        {
          "code": "9",
          "label": "9 år"
        },
        {
          "code": "10",
          "label": "10 år"
        },
        {
          "code": "11",
          "label": "11 år"
        },
        {
          "code": "12",
          "label": "12 år"
        },
        {
          "code": "13",
          "label": "13 år"
        },
        {
          "code": "14",
          "label": "14 år"
        },
        {
          "code": "15",
          "label": "15 år"
        },
        {
          "code": "16",
          "label": "16 år"
        },
        {
          "code": "17",
          "label": "17 år"
        },
        {
          "code": "18",
          "label": "18 år"
        },
        {
          "code": "19",
          "label": "19 år"
        },
        {
          "code": "20",
          "label": "20 år"
        },
        {
          "code": "21",
          "label": "21 år"
        },
        {
          "code": "22",
          "label": "22 år"
        },
        {
          "code": "23",
          "label": "23 år"
        },
        {
          "code": "24",
          "label": "24 år"
        },
        {
          "code": "25",
          "label": "25 år"
        },
        {
          "code": "26",
          "label": "26 år"
        },
        {
          "code": "27",
          "label": "27 år"
        },
        {
          "code": "28",
          "label": "28 år"
        },
        {
          "code": "29",
          "label": "29 år"
        },
        {
          "code": "30",
          "label": "30 år"
        },
        {
          "code": "31",
          "label": "31 år"
        },
        {
          "code": "32",
          "label": "32 år"
        },
        {
          "code": "33",
          "label": "33 år"
        },
        {
          "code": "34",
          "label": "34 år"
        },
        {
          "code": "35",
          "label": "35 år"
        },
        {
          "code": "36",
          "label": "36 år"
        },
        {
          "code": "37",
          "label": "37 år"
        },
        {
          "code": "38",
          "label": "38 år"
        },
        {
          "code": "39",
          "label": "39 år"
        },
        {
          "code": "40",
          "label": "40 år"
        },
        {
          "code": "41",
          "label": "41 år"
        },
        {
          "code": "42",
          "label": "42 år"
        },
        {
          "code": "43",
          "label": "43 år"
        },
        {
          "code": "44",
          "label": "44 år"
        },
        {
          "code": "45",
          "label": "45 år"
        },
        {
          "code": "46",
          "label": "46 år"
        },
        {
          "code": "47",
          "label": "47 år"
        },
        {
          "code": "48",
          "label": "48 år"
        },
        {
          "code": "49",
          "label": "49 år"
        },
        {
          "code": "50",
          "label": "50 år"
        },
        {
          "code": "51",
          "label": "51 år"
        },
        {
          "code": "52",
          "label": "52 år"
        },
        {
          "code": "53",
          "label": "53 år"
        },
        {
          "code": "54",
          "label": "54 år"
        },
        {
          "code": "55",
          "label": "55 år"
        },
        {
          "code": "56",
          "label": "56 år"
        },
        {
          "code": "57",
          "label": "57 år"
        },
        {
          "code": "58",
          "label": "58 år"
        },
        {
          "code": "59",
          "label": "59 år"
        },
        {
          "code": "60",
          "label": "60 år"
        },
        {
          "code": "61",
          "label": "61 år"
        },
        {
          "code": "62",
          "label": "62 år"
        },
        {
          "code": "63",
          "label": "63 år"
        },
        {
          "code": "64",
          "label": "64 år"
        },
        {
          "code": "65",
          "label": "65 år"
        },
        {
          "code": "66",
          "label": "66 år"
        },
        {
          "code": "67",
          "label": "67 år"
        },
        {
          "code": "68",
          "label": "68 år"
        },
        {
          "code": "69",
          "label": "69 år"
        },
        {
          "code": "70",
          "label": "70 år"
        },
        {
          "code": "71",
          "label": "71 år"
        },
        {
          "code": "72",
          "label": "72 år"
        },
        {
          "code": "73",
          "label": "73 år"
        },
        {
          "code": "74",
          "label": "74 år"
        },
        {
          "code": "75",
          "label": "75 år"
        },
        {
          "code": "76",
          "label": "76 år"
        },
        {
          "code": "77",
          "label": "77 år"
        },
        {
          "code": "78",
          "label": "78 år"
        },
        {
          "code": "79",
          "label": "79 år"
        },
        {
          "code": "80",
          "label": "80 år"
        },
        {
          "code": "81",
          "label": "81 år"
        },
        {
          "code": "82",
          "label": "82 år"
        },
        {
          "code": "83",
          "label": "83 år"
        },
        {
          "code": "84",
          "label": "84 år"
        },
        {
          "code": "85",
          "label": "85 år"
        },
        {
          "code": "86",
          "label": "86 år"
        },
        {
          "code": "87",
          "label": "87 år"
        },
        {
          "code": "88",
          "label": "88 år"
        },
        {
          "code": "89",
          "label": "89 år"
        },
        {
          "code": "90",
          "label": "90 år"
        },
        {
          "code": "91",
          "label": "91 år"
        },
        {
          "code": "92",
          "label": "92 år"
        },
        {
          "code": "93",
          "label": "93 år"
        },
        {
          "code": "94",
          "label": "94 år"
        },
        {
          "code": "95",
          "label": "95 år"
        },
        {
          "code": "96",
          "label": "96 år"
        },
        {
          "code": "97",
          "label": "97 år"
        },
        {
          "code": "98",
          "label": "98 år"
        },
        {
          "code": "99",
          "label": "99 år"
        },
        {
          "code": "100+",
          "label": "100+ år"
        },
        {
          "code": "tot",
          "label": "totalt ålder"
        }
      ],
      "codeLists": [
        {
          "id": "vs_Ålder1årA",
          "label": "Ålder, 1 års-klasser",
          "links": [
            {
              "rel": "metadata",
              "href": "https://my-site.com/api/v2/tables/TAB638/codelists/vs_Ålder1årA"
            }
          ]
        },
        {
          "id": "vs_ÅlderTotA",
          "label": "Ålder, totalt, alla redovisade åldrar",
          "links": [
            {
              "rel": "metadata",
              "href": "https://my-site.com/api/v2/tables/TAB638/codelists/vs_ÅlderTotA"
            }
          ]
        },
        {
          "id": "agg_Ålder10år",
          "label": "10-årsklasser",
          "links": [
            {
              "rel": "metadata",
              "href": "https://my-site.com/api/v2/tables/TAB638/codelists/agg_Ålder10år"
            }
          ]
        },
        {
          "id": "agg_Ålder5år",
          "label": "5-årsklasser",
          "links": [
            {
              "rel": "metadata",
              "href": "https://my-site.com/api/v2/tables/TAB638/codelists/agg_Ålder5år"
            }
          ]
        }
      ]
    },
    {
      "id": "Region",
      "label": "region",
      "type": 0,
      "elimination": true,
      "eliminationValueCode": "00",
      "values": [
        {
          "code": "00",
          "label": "Riket"
        },
        {
          "code": "01",
          "label": "Stockholms län"
        },
        {
          "code": "0114",
          "label": "Upplands Väsby"
        },
        {
          "code": "0115",
          "label": "Vallentuna"
        },
        {
          "code": "0117",
          "label": "Österåker"
        },
        {
          "code": "0120",
          "label": "Värmdö"
        },
        {
          "code": "0123",
          "label": "Järfälla"
        },
        {
          "code": "0125",
          "label": "Ekerö"
        },
        {
          "code": "0126",
          "label": "Huddinge"
        },
        {
          "code": "0127",
          "label": "Botkyrka"
        },
        {
          "code": "0128",
          "label": "Salem"
        },
        {
          "code": "0136",
          "label": "Haninge"
        },
        {
          "code": "0138",
          "label": "Tyresö"
        },
        {
          "code": "0139",
          "label": "Upplands-Bro"
        },
        {
          "code": "0140",
          "label": "Nykvarn",
          "notes": [
            {
              "mandatory": true,
              "text": "Ny regional indelning fr.o.m. 1999-01-01. Delar av Södertälje kommun (kod 0181) bildar en ny kommun benämnd Nykvarn (kod 0140)."
            }
          ]
        },
        {
          "code": "0160",
          "label": "Täby"
        },
        {
          "code": "0162",
          "label": "Danderyd"
        },
        {
          "code": "0163",
          "label": "Sollentuna"
        },
        {
          "code": "0180",
          "label": "Stockholm"
        },
        {
          "code": "0181",
          "label": "Södertälje",
          "notes": [
            {
              "mandatory": true,
              "text": "Ny regional indelning fr.o.m. 1999-01-01. Delar av Södertälje kommun (kod 0181) bildar en ny kommun benämnd Nykvarn (kod 0140)."
            }
          ]
        },
        {
          "code": "0182",
          "label": "Nacka"
        },
        {
          "code": "0183",
          "label": "Sundbyberg"
        },
        {
          "code": "0184",
          "label": "Solna"
        },
        {
          "code": "0186",
          "label": "Lidingö",
          "notes": [
            {
              "mandatory": true,
              "text": "Från och med 2011-01-01 tillhör Storholmen Lidingö (0186) som tidigare tillhörde Vaxholm (0187)."
            }
          ]
        },
        {
          "code": "0187",
          "label": "Vaxholm",
          "notes": [
            {
              "mandatory": true,
              "text": "Från och med 2011-01-01 tillhör Storholmen Lidingö (0186) som tidigare tillhörde Vaxholm (0187)."
            }
          ]
        },
        {
          "code": "0188",
          "label": "Norrtälje"
        },
        {
          "code": "0191",
          "label": "Sigtuna"
        },
        {
          "code": "0192",
          "label": "Nynäshamn"
        },
        {
          "code": "03",
          "label": "Uppsala län"
        },
        {
          "code": "0305",
          "label": "Håbo"
        },
        {
          "code": "0319",
          "label": "Älvkarleby"
        },
        {
          "code": "0330",
          "label": "Knivsta",
          "notes": [
            {
              "mandatory": true,
              "text": "Ny regional indelning fr.o.m. 2003-01-01. Delar av Uppsala kommun bildar en ny kommun benämnd Knivsta kommun."
            }
          ]
        },
        {
          "code": "0331",
          "label": "Heby"
        },
        {
          "code": "0360",
          "label": "Tierp"
        },
        {
          "code": "0380",
          "label": "Uppsala",
          "notes": [
            {
              "mandatory": true,
              "text": "Ny regional indelning fr.o.m. 2003-01-01. Delar av Uppsala kommun bildar en ny kommun benämnd Knivsta kommun."
            }
          ]
        },
        {
          "code": "0381",
          "label": "Enköping"
        },
        {
          "code": "0382",
          "label": "Östhammar"
        },
        {
          "code": "04",
          "label": "Södermanlands län"
        },
        {
          "code": "0428",
          "label": "Vingåker"
        },
        {
          "code": "0461",
          "label": "Gnesta"
        },
        {
          "code": "0480",
          "label": "Nyköping"
        },
        {
          "code": "0481",
          "label": "Oxelösund"
        },
        {
          "code": "0482",
          "label": "Flen"
        },
        {
          "code": "0483",
          "label": "Katrineholm"
        },
        {
          "code": "0484",
          "label": "Eskilstuna"
        },
        {
          "code": "0486",
          "label": "Strängnäs"
        },
        {
          "code": "0488",
          "label": "Trosa"
        },
        {
          "code": "05",
          "label": "Östergötlands län"
        },
        {
          "code": "0509",
          "label": "Ödeshög"
        },
        {
          "code": "0512",
          "label": "Ydre"
        },
        {
          "code": "0513",
          "label": "Kinda"
        },
        {
          "code": "0560",
          "label": "Boxholm"
        },
        {
          "code": "0561",
          "label": "Åtvidaberg"
        },
        {
          "code": "0562",
          "label": "Finspång"
        },
        {
          "code": "0563",
          "label": "Valdemarsvik"
        },
        {
          "code": "0580",
          "label": "Linköping"
        },
        {
          "code": "0581",
          "label": "Norrköping"
        },
        {
          "code": "0582",
          "label": "Söderköping"
        },
        {
          "code": "0583",
          "label": "Motala"
        },
        {
          "code": "0584",
          "label": "Vadstena"
        },
        {
          "code": "0586",
          "label": "Mjölby"
        },
        {
          "code": "06",
          "label": "Jönköpings län"
        },
        {
          "code": "0604",
          "label": "Aneby"
        },
        {
          "code": "0617",
          "label": "Gnosjö"
        },
        {
          "code": "0642",
          "label": "Mullsjö"
        },
        {
          "code": "0643",
          "label": "Habo"
        },
        {
          "code": "0662",
          "label": "Gislaved"
        },
        {
          "code": "0665",
          "label": "Vaggeryd"
        },
        {
          "code": "0680",
          "label": "Jönköping"
        },
        {
          "code": "0682",
          "label": "Nässjö"
        },
        {
          "code": "0683",
          "label": "Värnamo"
        },
        {
          "code": "0684",
          "label": "Sävsjö"
        },
        {
          "code": "0685",
          "label": "Vetlanda"
        },
        {
          "code": "0686",
          "label": "Eksjö"
        },
        {
          "code": "0687",
          "label": "Tranås"
        },
        {
          "code": "07",
          "label": "Kronobergs län"
        },
        {
          "code": "0760",
          "label": "Uppvidinge"
        },
        {
          "code": "0761",
          "label": "Lessebo"
        },
        {
          "code": "0763",
          "label": "Tingsryd"
        },
        {
          "code": "0764",
          "label": "Alvesta"
        },
        {
          "code": "0765",
          "label": "Älmhult"
        },
        {
          "code": "0767",
          "label": "Markaryd"
        },
        {
          "code": "0780",
          "label": "Växjö"
        },
        {
          "code": "0781",
          "label": "Ljungby"
        },
        {
          "code": "08",
          "label": "Kalmar län"
        },
        {
          "code": "0821",
          "label": "Högsby"
        },
        {
          "code": "0834",
          "label": "Torsås"
        },
        {
          "code": "0840",
          "label": "Mörbylånga"
        },
        {
          "code": "0860",
          "label": "Hultsfred"
        },
        {
          "code": "0861",
          "label": "Mönsterås"
        },
        {
          "code": "0862",
          "label": "Emmaboda"
        },
        {
          "code": "0880",
          "label": "Kalmar"
        },
        {
          "code": "0881",
          "label": "Nybro"
        },
        {
          "code": "0882",
          "label": "Oskarshamn"
        },
        {
          "code": "0883",
          "label": "Västervik"
        },
        {
          "code": "0884",
          "label": "Vimmerby"
        },
        {
          "code": "0885",
          "label": "Borgholm"
        },
        {
          "code": "09",
          "label": "Gotlands län"
        },
        {
          "code": "0980",
          "label": "Gotland"
        },
        {
          "code": "10",
          "label": "Blekinge län"
        },
        {
          "code": "1060",
          "label": "Olofström"
        },
        {
          "code": "1080",
          "label": "Karlskrona"
        },
        {
          "code": "1081",
          "label": "Ronneby"
        },
        {
          "code": "1082",
          "label": "Karlshamn"
        },
        {
          "code": "1083",
          "label": "Sölvesborg"
        },
        {
          "code": "12",
          "label": "Skåne län"
        },
        {
          "code": "1214",
          "label": "Svalöv"
        },
        {
          "code": "1230",
          "label": "Staffanstorp"
        },
        {
          "code": "1231",
          "label": "Burlöv"
        },
        {
          "code": "1233",
          "label": "Vellinge"
        },
        {
          "code": "1256",
          "label": "Östra Göinge"
        },
        {
          "code": "1257",
          "label": "Örkelljunga"
        },
        {
          "code": "1260",
          "label": "Bjuv"
        },
        {
          "code": "1261",
          "label": "Kävlinge"
        },
        {
          "code": "1262",
          "label": "Lomma"
        },
        {
          "code": "1263",
          "label": "Svedala"
        },
        {
          "code": "1264",
          "label": "Skurup"
        },
        {
          "code": "1265",
          "label": "Sjöbo"
        },
        {
          "code": "1266",
          "label": "Hörby"
        },
        {
          "code": "1267",
          "label": "Höör"
        },
        {
          "code": "1270",
          "label": "Tomelilla"
        },
        {
          "code": "1272",
          "label": "Bromölla"
        },
        {
          "code": "1273",
          "label": "Osby"
        },
        {
          "code": "1275",
          "label": "Perstorp"
        },
        {
          "code": "1276",
          "label": "Klippan"
        },
        {
          "code": "1277",
          "label": "Åstorp"
        },
        {
          "code": "1278",
          "label": "Båstad"
        },
        {
          "code": "1280",
          "label": "Malmö"
        },
        {
          "code": "1281",
          "label": "Lund"
        },
        {
          "code": "1282",
          "label": "Landskrona"
        },
        {
          "code": "1283",
          "label": "Helsingborg"
        },
        {
          "code": "1284",
          "label": "Höganäs"
        },
        {
          "code": "1285",
          "label": "Eslöv"
        },
        {
          "code": "1286",
          "label": "Ystad"
        },
        {
          "code": "1287",
          "label": "Trelleborg"
        },
        {
          "code": "1290",
          "label": "Kristianstad"
        },
        {
          "code": "1291",
          "label": "Simrishamn"
        },
        {
          "code": "1292",
          "label": "Ängelholm"
        },
        {
          "code": "1293",
          "label": "Hässleholm"
        },
        {
          "code": "13",
          "label": "Hallands län"
        },
        {
          "code": "1315",
          "label": "Hylte"
        },
        {
          "code": "1380",
          "label": "Halmstad"
        },
        {
          "code": "1381",
          "label": "Laholm"
        },
        {
          "code": "1382",
          "label": "Falkenberg"
        },
        {
          "code": "1383",
          "label": "Varberg"
        },
        {
          "code": "1384",
          "label": "Kungsbacka"
        },
        {
          "code": "14",
          "label": "Västra Götalands län"
        },
        {
          "code": "1401",
          "label": "Härryda"
        },
        {
          "code": "1402",
          "label": "Partille"
        },
        {
          "code": "1407",
          "label": "Öckerö"
        },
        {
          "code": "1415",
          "label": "Stenungsund"
        },
        {
          "code": "1419",
          "label": "Tjörn"
        },
        {
          "code": "1421",
          "label": "Orust"
        },
        {
          "code": "1427",
          "label": "Sotenäs"
        },
        {
          "code": "1430",
          "label": "Munkedal"
        },
        {
          "code": "1435",
          "label": "Tanum"
        },
        {
          "code": "1438",
          "label": "Dals-Ed"
        },
        {
          "code": "1439",
          "label": "Färgelanda"
        },
        {
          "code": "1440",
          "label": "Ale"
        },
        {
          "code": "1441",
          "label": "Lerum"
        },
        {
          "code": "1442",
          "label": "Vårgårda"
        },
        {
          "code": "1443",
          "label": "Bollebygd"
        },
        {
          "code": "1444",
          "label": "Grästorp"
        },
        {
          "code": "1445",
          "label": "Essunga"
        },
        {
          "code": "1446",
          "label": "Karlsborg"
        },
        {
          "code": "1447",
          "label": "Gullspång"
        },
        {
          "code": "1452",
          "label": "Tranemo"
        },
        {
          "code": "1460",
          "label": "Bengtsfors"
        },
        {
          "code": "1461",
          "label": "Mellerud"
        },
        {
          "code": "1462",
          "label": "Lilla Edet"
        },
        {
          "code": "1463",
          "label": "Mark"
        },
        {
          "code": "1465",
          "label": "Svenljunga"
        },
        {
          "code": "1466",
          "label": "Herrljunga"
        },
        {
          "code": "1470",
          "label": "Vara"
        },
        {
          "code": "1471",
          "label": "Götene"
        },
        {
          "code": "1472",
          "label": "Tibro"
        },
        {
          "code": "1473",
          "label": "Töreboda"
        },
        {
          "code": "1480",
          "label": "Göteborg"
        },
        {
          "code": "1481",
          "label": "Mölndal"
        },
        {
          "code": "1482",
          "label": "Kungälv"
        },
        {
          "code": "1484",
          "label": "Lysekil"
        },
        {
          "code": "1485",
          "label": "Uddevalla"
        },
        {
          "code": "1486",
          "label": "Strömstad"
        },
        {
          "code": "1487",
          "label": "Vänersborg"
        },
        {
          "code": "1488",
          "label": "Trollhättan"
        },
        {
          "code": "1489",
          "label": "Alingsås"
        },
        {
          "code": "1490",
          "label": "Borås"
        },
        {
          "code": "1491",
          "label": "Ulricehamn"
        },
        {
          "code": "1492",
          "label": "Åmål"
        },
        {
          "code": "1493",
          "label": "Mariestad"
        },
        {
          "code": "1494",
          "label": "Lidköping"
        },
        {
          "code": "1495",
          "label": "Skara"
        },
        {
          "code": "1496",
          "label": "Skövde"
        },
        {
          "code": "1497",
          "label": "Hjo"
        },
        {
          "code": "1498",
          "label": "Tidaholm"
        },
        {
          "code": "1499",
          "label": "Falköping"
        },
        {
          "code": "17",
          "label": "Värmlands län"
        },
        {
          "code": "1715",
          "label": "Kil"
        },
        {
          "code": "1730",
          "label": "Eda"
        },
        {
          "code": "1737",
          "label": "Torsby"
        },
        {
          "code": "1760",
          "label": "Storfors"
        },
        {
          "code": "1761",
          "label": "Hammarö"
        },
        {
          "code": "1762",
          "label": "Munkfors"
        },
        {
          "code": "1763",
          "label": "Forshaga"
        },
        {
          "code": "1764",
          "label": "Grums"
        },
        {
          "code": "1765",
          "label": "Årjäng"
        },
        {
          "code": "1766",
          "label": "Sunne"
        },
        {
          "code": "1780",
          "label": "Karlstad"
        },
        {
          "code": "1781",
          "label": "Kristinehamn"
        },
        {
          "code": "1782",
          "label": "Filipstad"
        },
        {
          "code": "1783",
          "label": "Hagfors"
        },
        {
          "code": "1784",
          "label": "Arvika"
        },
        {
          "code": "1785",
          "label": "Säffle"
        },
        {
          "code": "18",
          "label": "Örebro län"
        },
        {
          "code": "1814",
          "label": "Lekeberg"
        },
        {
          "code": "1860",
          "label": "Laxå"
        },
        {
          "code": "1861",
          "label": "Hallsberg"
        },
        {
          "code": "1862",
          "label": "Degerfors"
        },
        {
          "code": "1863",
          "label": "Hällefors"
        },
        {
          "code": "1864",
          "label": "Ljusnarsberg"
        },
        {
          "code": "1880",
          "label": "Örebro"
        },
        {
          "code": "1881",
          "label": "Kumla"
        },
        {
          "code": "1882",
          "label": "Askersund"
        },
        {
          "code": "1883",
          "label": "Karlskoga"
        },
        {
          "code": "1884",
          "label": "Nora"
        },
        {
          "code": "1885",
          "label": "Lindesberg"
        },
        {
          "code": "19",
          "label": "Västmanlands län"
        },
        {
          "code": "1904",
          "label": "Skinnskatteberg"
        },
        {
          "code": "1907",
          "label": "Surahammar"
        },
        {
          "code": "1960",
          "label": "Kungsör"
        },
        {
          "code": "1961",
          "label": "Hallstahammar"
        },
        {
          "code": "1962",
          "label": "Norberg"
        },
        {
          "code": "1980",
          "label": "Västerås"
        },
        {
          "code": "1981",
          "label": "Sala"
        },
        {
          "code": "1982",
          "label": "Fagersta"
        },
        {
          "code": "1983",
          "label": "Köping"
        },
        {
          "code": "1984",
          "label": "Arboga"
        },
        {
          "code": "20",
          "label": "Dalarnas län"
        },
        {
          "code": "2021",
          "label": "Vansbro"
        },
        {
          "code": "2023",
          "label": "Malung-Sälen"
        },
        {
          "code": "2026",
          "label": "Gagnef"
        },
        {
          "code": "2029",
          "label": "Leksand"
        },
        {
          "code": "2031",
          "label": "Rättvik"
        },
        {
          "code": "2034",
          "label": "Orsa"
        },
        {
          "code": "2039",
          "label": "Älvdalen"
        },
        {
          "code": "2061",
          "label": "Smedjebacken"
        },
        {
          "code": "2062",
          "label": "Mora"
        },
        {
          "code": "2080",
          "label": "Falun"
        },
        {
          "code": "2081",
          "label": "Borlänge"
        },
        {
          "code": "2082",
          "label": "Säter"
        },
        {
          "code": "2083",
          "label": "Hedemora"
        },
        {
          "code": "2084",
          "label": "Avesta"
        },
        {
          "code": "2085",
          "label": "Ludvika"
        },
        {
          "code": "21",
          "label": "Gävleborgs län"
        },
        {
          "code": "2101",
          "label": "Ockelbo"
        },
        {
          "code": "2104",
          "label": "Hofors"
        },
        {
          "code": "2121",
          "label": "Ovanåker"
        },
        {
          "code": "2132",
          "label": "Nordanstig"
        },
        {
          "code": "2161",
          "label": "Ljusdal"
        },
        {
          "code": "2180",
          "label": "Gävle"
        },
        {
          "code": "2181",
          "label": "Sandviken"
        },
        {
          "code": "2182",
          "label": "Söderhamn"
        },
        {
          "code": "2183",
          "label": "Bollnäs"
        },
        {
          "code": "2184",
          "label": "Hudiksvall"
        },
        {
          "code": "22",
          "label": "Västernorrlands län"
        },
        {
          "code": "2260",
          "label": "Ånge"
        },
        {
          "code": "2262",
          "label": "Timrå"
        },
        {
          "code": "2280",
          "label": "Härnösand"
        },
        {
          "code": "2281",
          "label": "Sundsvall"
        },
        {
          "code": "2282",
          "label": "Kramfors"
        },
        {
          "code": "2283",
          "label": "Sollefteå"
        },
        {
          "code": "2284",
          "label": "Örnsköldsvik"
        },
        {
          "code": "23",
          "label": "Jämtlands län"
        },
        {
          "code": "2303",
          "label": "Ragunda"
        },
        {
          "code": "2305",
          "label": "Bräcke"
        },
        {
          "code": "2309",
          "label": "Krokom"
        },
        {
          "code": "2313",
          "label": "Strömsund"
        },
        {
          "code": "2321",
          "label": "Åre"
        },
        {
          "code": "2326",
          "label": "Berg"
        },
        {
          "code": "2361",
          "label": "Härjedalen"
        },
        {
          "code": "2380",
          "label": "Östersund"
        },
        {
          "code": "24",
          "label": "Västerbottens län"
        },
        {
          "code": "2401",
          "label": "Nordmaling"
        },
        {
          "code": "2403",
          "label": "Bjurholm"
        },
        {
          "code": "2404",
          "label": "Vindeln"
        },
        {
          "code": "2409",
          "label": "Robertsfors"
        },
        {
          "code": "2417",
          "label": "Norsjö"
        },
        {
          "code": "2418",
          "label": "Malå"
        },
        {
          "code": "2421",
          "label": "Storuman"
        },
        {
          "code": "2422",
          "label": "Sorsele"
        },
        {
          "code": "2425",
          "label": "Dorotea"
        },
        {
          "code": "2460",
          "label": "Vännäs"
        },
        {
          "code": "2462",
          "label": "Vilhelmina"
        },
        {
          "code": "2463",
          "label": "Åsele"
        },
        {
          "code": "2480",
          "label": "Umeå"
        },
        {
          "code": "2481",
          "label": "Lycksele"
        },
        {
          "code": "2482",
          "label": "Skellefteå"
        },
        {
          "code": "25",
          "label": "Norrbottens län"
        },
        {
          "code": "2505",
          "label": "Arvidsjaur"
        },
        {
          "code": "2506",
          "label": "Arjeplog"
        },
        {
          "code": "2510",
          "label": "Jokkmokk"
        },
        {
          "code": "2513",
          "label": "Överkalix"
        },
        {
          "code": "2514",
          "label": "Kalix"
        },
        {
          "code": "2518",
          "label": "Övertorneå"
        },
        {
          "code": "2521",
          "label": "Pajala"
        },
        {
          "code": "2523",
          "label": "Gällivare"
        },
        {
          "code": "2560",
          "label": "Älvsbyn"
        },
        {
          "code": "2580",
          "label": "Luleå"
        },
        {
          "code": "2581",
          "label": "Piteå"
        },
        {
          "code": "2582",
          "label": "Boden"
        },
        {
          "code": "2583",
          "label": "Haparanda"
        },
        {
          "code": "2584",
          "label": "Kiruna"
        }
      ],
      "codeLists": [
        {
          "id": "vs_RegionKommun07",
          "label": "Kommuner",
          "links": [
            {
              "rel": "metadata",
              "href": "https://my-site.com/api/v2/tables/TAB638/codelists/vs_RegionKommun07"
            }
          ]
        },
        {
          "id": "vs_RegionLän07",
          "label": "Län",
          "links": [
            {
              "rel": "metadata",
              "href": "https://my-site.com/api/v2/tables/TAB638/codelists/vs_RegionLän07"
            }
          ]
        },
        {
          "id": "vs_RegionRiket99",
          "label": "Riket",
          "links": [
            {
              "rel": "metadata",
              "href": "https://my-site.com/api/v2/tables/TAB638/codelists/vs_RegionRiket99"
            }
          ]
        },
        {
          "id": "agg_RegionA-region_2",
          "label": "A-regioner",
          "links": [
            {
              "rel": "metadata",
              "href": "https://my-site.com/api/v2/tables/TAB638/codelists/agg_RegionA-region_2"
            }
          ]
        },
        {
          "id": "agg_RegionKommungrupp2005-_1",
          "label": "Kommungrupper (SKL:s) 2005",
          "links": [
            {
              "rel": "metadata",
              "href": "https://my-site.com/api/v2/tables/TAB638/codelists/agg_RegionKommungrupp2005-_1"
            }
          ]
        },
        {
          "id": "agg_RegionKommungrupp2011-",
          "label": "Kommungrupper (SKL:s) 2011",
          "links": [
            {
              "rel": "metadata",
              "href": "https://my-site.com/api/v2/tables/TAB638/codelists/agg_RegionKommungrupp2011-"
            }
          ]
        },
        {
          "id": "agg_RegionKommungrupp2017-",
          "label": "Kommungrupper (SKL:s) 2017",
          "links": [
            {
              "rel": "metadata",
              "href": "https://my-site.com/api/v2/tables/TAB638/codelists/agg_RegionKommungrupp2017-"
            }
          ]
        },
        {
          "id": "agg_RegionLA1998",
          "label": "Lokalaarbetsmarknader 1998",
          "links": [
            {
              "rel": "metadata",
              "href": "https://my-site.com/api/v2/tables/TAB638/codelists/agg_RegionLA1998"
            }
          ]
        },
        {
          "id": "agg_RegionLA2003_1",
          "label": "Lokalaarbetsmarknader 2003",
          "links": [
            {
              "rel": "metadata",
              "href": "https://my-site.com/api/v2/tables/TAB638/codelists/agg_RegionLA2003_1"
            }
          ]
        },
        {
          "id": "agg_RegionLA2008",
          "label": "Lokalaarbetsmarknader 2008",
          "links": [
            {
              "rel": "metadata",
              "href": "https://my-site.com/api/v2/tables/TAB638/codelists/agg_RegionLA2008"
            }
          ]
        },
        {
          "id": "agg_RegionLA2013",
          "label": "Lokalaarbetsmarknader 2013",
          "links": [
            {
              "rel": "metadata",
              "href": "https://my-site.com/api/v2/tables/TAB638/codelists/agg_RegionLA2013"
            }
          ]
        },
        {
          "id": "agg_RegionLA2018",
          "label": "Lokalaarbetsmarknader 2018",
          "links": [
            {
              "rel": "metadata",
              "href": "https://my-site.com/api/v2/tables/TAB638/codelists/agg_Ålder5år"
            }
          ]
        },
        {
          "id": "agg_RegionStoromr-04_2",
          "label": "Sorstadsområder -2004",
          "links": [
            {
              "rel": "metadata",
              "href": "https://my-site.com/api/v2/tables/TAB638/codelists/agg_RegionStoromr-04_2"
            }
          ]
        },
        {
          "id": "agg_RegionStoromr05-_1",
          "label": "Sorstadsområder 2005-",
          "links": [
            {
              "rel": "metadata",
              "href": "https://my-site.com/api/v2/tables/TAB638/codelists/agg_RegionStoromr05-_1"
            }
          ]
        },
        {
          "id": "agg_RegionNUTS1_2008",
          "label": "NUTS1 fr.o.m 2008",
          "links": [
            {
              "rel": "metadata",
              "href": "https://my-site.com/api/v2/tables/TAB638/codelists/agg_RegionNUTS1_2008"
            }
          ]
        },
        {
          "id": "agg_RegionNUTS2_2008",
          "label": "NUTS2 fr.o.m 2008",
          "links": [
            {
              "rel": "metadata",
              "href": "https://my-site.com/api/v2/tables/TAB638/codelists/agg_RegionNUTS2_2008"
            }
          ]
        },
        {
          "id": "agg_RegionNUTS3_2008",
          "label": "NUTS3 fr.o.m 2008",
          "links": [
            {
              "rel": "metadata",
              "href": "https://my-site.com/api/v2/tables/TAB638/codelists/agg_RegionNUTS3_2008"
            }
          ]
        }
      ]
    }
  ],
  "variablesDisplayOrder": [
    "ContentsCode",
    "Civilstand",
    "Alder",
    "Tid",
    "Kon"
  ],
  "links": [
    {
      "rel": "self",
      "href": "https://my-site.com/api/v2/tables/TAB638"
    },
    {
      "rel": "metadata",
      "href": "https://my-site.com/api/v2/tables/TAB638/metadata"
    },
    {
      "rel": "data",
      "href": "https://my-site.com/api/v2/tables/TAB638/data"
    }
  ]
}
```

Question: domain exists in PX-files but not in CNMM. Is domain really needed? Could we use only filter instead?
We need to rethink. All valuesets and aggregations shall be listed.
The idea behind domain was to have a way to select which map to use when displaying the data on a map. In the CNMM case the domain should be the name of the valueset but when we show all the values for all subtables we can not specify the domain. Maybe it should be a different attribute but the problem still remains.

### Get data for a specific table
**url:** `/api/v2/tables/<table-id>/data/<format>`

**HTTP method:** GET|POST

Retrieves data for a the specified table in the format specified. The available formats are listed in the configuration end point see 4.1 Configuration endpoint.
#### Parameters
The variables of the table can be used to subquery a part of the data see 6 Data selection parameters for how to specify these parameters.

### List all streams for a table
**url:** `/api/v2/tables/<table-id>/streams`

**HTTP method:** GET|POST

List all streams that are associated with the table.

**Response**
```json
[
  {
    "id": "K1", 
    "text": "Befolkning efter kommun, kön och år",
    "links": [
           {"rel": "metadata" ,
            "href": "http://my-site.com/api/v2/tables/TAB0001/streams/K1"},
           {"rel": "data", 
            "href": "http://my-site.com/api/v2/tables/TAB0001/streams/K1/stream"}
            ]},
  {
    "id": "L1", 
    "text": "Befolkning efter län, kön och år",
    "links": [
           {"rel": "metadata",
            "href": "http://my-site.com/api/v2/tables/TAB0001/streams/L1"},
           {"rel": "data",
            "href": "http://my-site.com/api/v2/tables/TAB0001/streams/L1/data"}
            ]}]
  }
]
```
### List metadata for a stream
**url:** `/api/v2/tables/<table-id>/streams/<stream-id>/`

**HTTP method:** GET|POST

List metadata for the specified stream

**Response**
```json
{
  "id": "K1",
  "text": "Befolkning",
  "description": "Befolkning efter kommun, kön och år",
  "updated": "2019-02-21T09:30:00",
  "footnotes": [""],
  "contacts": [{
               "name": "Inga Svensson",
               "phone": "+46111111111",
               "mail": "inga.svensson@my-site.com"}],
  "variables": [{
                "id": "CONTENTS",
                "text": "Befolkning",
                "elimination": false,
                "type": "CONTENTS",
                "values": [ {"id": "A", "text": "Befolkning", "unit": "antal"}]},

              {
                "id": "region",
                "text": "kommun",
                "elimination": true,
                "type": "GEO",
                ~~"domain": "kommun2017",~~
                "values": [ {"id": "0114", "text": "Upplands Väsby"},
                          {"id": "0115", "text": "Vallentuna"},
                          .
                          .
                          .
                          {"id": "2584", "text": "Kiruna"}
                        ]},
              {
                "id": "kon",
                "text": "kön",
                "elimination": true,
                "type": "GENERIC",
                "domain": "kon",
                "values": [
                          {"id": "1", "text": "män"},
                          {"id": "2", "text": "kvinnor"}
                        ]},
              {
                "id": "Tid",
                "text": "år",
                "elimination": false,
                "type": "TIME",
                "domain": "år",
                "values": [
                          {"id": "2000", "text": "2000"},
                          {"id": "2001", "text": "2001"},
                          .
                          .
                          .
                          {"id": "2017", "text": "2017"}

                        ]}],
   "links": [{
             "rel": "data",
             "href": "http://my-site.com/api/v2/tables/TAB0001/streams/K1/data"}]

   }

}
```

### Get data via a stream
**url:** `/api/v2/tables/<table-id>/streams/<stream-id>/data/<format>`

**HTTP method:** GET|POST

List metadata for the specified table
Retrieves data for a the specified table in the format specified. The available formats are listed in the configuration end point see 4.1 Configuration endpoint.
#### Parameters
The variables of the table can be used to subquery a part of the data see 6 Data selection parameters for how to specify these parameters.

Question: Is it possible to get data without specifying any selection? Would this give me the whole table?
-	Yes! If you don´t make any selection you will get all. Same rules as in the Danish API:
Default kanske man ska begränsa default till de senast tider och värden. 

### List all filters for a table
**url:** `/api/v2/tables/<table-id>/filters`

**HTTP method:** GET|POST

List all filters for specified table

**Response**
```json
TODO: Make an example of the response.
```

### Get filter specification
**url:** `/api/v2/tables/<table-id>/filters/<filter-id>/`

**HTTP method:** GET|POST
List the filter specification.

**Response**
```json
{
  "id": "vs_lan",
  "values": [
    {"id": "01", "text": "Stockholm", "map": ["0114","0115", … "0192"]}
    {"id": "02", "text": "Uppsala", "map": ["0305","0319", … "0382"]}
    .
    .
    .
    {"id": "25", "text": "Norrbotten", "map": ["2505","2506", … "2584"]}

  ]
}
```
Question: The above example must be a grouping. How would a valueset look like?
~~TODO: Make examples for valueset and for aggregation.~~

# General parameters
## Language
The parameters controls the language used in the response.
The name of the parameter is lang and the valid values are  language id that is return from the configuration endpoint.
**Example**
```
http://my-site.com/api/v2/data/TAB0001?lang=en
```
## Pretty print
The parameters controls the formatting in the response.
The name of the parameter is prettyPrint and the valid values are true or false and false is the default value. Any other value specified other than the valid values will be treated as false.
**Example**
```
http://my-site.com/api/v2/data/TAB0001?prettyPrint=true
```
# Data selection parameters
Data queries to the API can limit the amount of data that is fetch by specifying a table query. The parameters to the table query are the Id:s for the variables. E.g. imaging the table A with the structure in the example above. We could specify  a table query as
```
CONTENTS=A&region=0114,0115&kon=1,2&Tid=2000
```
This table query will restrict the data that is fetched to the  regions *Upplands Väsby* and *Vallentuna* for the time period *2000*.
The values can be specified using different expressions they could be the 
-	Id of a value
-	The Nth rule see the Danish API 
-	wildcard expression that matches the text of a value 
-	or a combination of them all.
**Example**
```
YEAR=-(4),+(2),1999,*79,*3*,18* 
```
This will select 
-	the last 4 time periods
-	The first two time periods
-	The year 1999
-	All years ending with 79
-	All years containing the number 3
-	All years starting with 18

Question: Will this be possible for other variables than the time variable?
-	Yes. Will work for all variables
Proposal: Look in Lucene.Net for more ideas for selection syntax.
We want “?” as wildcard for one character. Shoudl be ok

Should be able to combine ? and *

~~The variable time can also prefix the code it >, >=, <, <= indicating a matching of all time values that are greater, greater or equal, less, less or equal to the value~~

**Example**
```
YEAR=>1980,<=1990 
```
This will select all years greater than 1980 but not greater than 1990.
Question: How will 1984M02 be handled?
We will try to make it work
-	2000M01
-	2000K2
-	2000H2
Filtered variables can have filters specified. In such cases when a filter is applied the values that are referred in the selection is the values of the filter
**Example**
```
Time=>1980.. From 
      1980..1990 Interval
      ..1980 To
Sortera alfabestiskt på kod och välj därefter.
Man måste ange koderna precist (inga ? och *)
```
## Filters
There is two type of filters: selection and transformation. The selection filters are used to make the selection easier. Imagine you have a variable, *Country*, with values for all countries and that you also have a filter, *agg_continents*, that group all countries to continents. You would like to select all European countries but you do not want to select each individual country (and they can vary over time). What you will do is that you specify that the table query for Country should use the selection filter and that the values that are specified are the filter values. It could look something like this 
```
Country$agg_continents=EUROPE
```
Imagine the same variable as above but you actually do not want each individual European country instead you which to aggregate all data for Europe then you would have to apply a transformation filter like this
```
Country@agg_continents=EUROPE   
```
Question: Will this work also for valuesets?
-No orYes
Proposal: Look how Denmark has implemented this
They do not have aggregtions when streaming

```
Streams cannot have filters and therefor filters cannot be used in table query’s for stream data.   
```

## Elimination
If elimination is set to true the variable can be eliminated and nothing have to be selected for this variable and the result will not contain that variable. If the variable have a value that states that it is the elimination value then that value will be selected to eliminate the variable. If no elimination value is specified the variable will be eliminated from the result by summing up all data points for the all values of that variable. If a variable has elimination set to false then at least one value bust be selected for that variable.

# Response codes
429
404
500

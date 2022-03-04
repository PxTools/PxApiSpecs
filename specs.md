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
**url:** `http://my-site.com/api/v2/config`

**HTTP method:** GET|POST

See the configuration of the API

**Response**
```json
{
   "apiVersion": "2.0",
   "languages": [{"id": "sv", 
                "label": "Svenska"},
               {"id": "en", 
                "label": "English"}],
   "defaultLanguage": "sv",
   "maxDataCells": 10000,
   "maxCalls": 30,
   "timeWindow": 10,
   "features": [{
      "id": "CORS",
      "params": [
        {"key":"enabled", "value":"true"}
      ]
   }],
   "dataFormats": ["px", "json-stat", "csv", "tsv"],
   "defaultDataFormat": "px",	
   "streamingFormats": ["csv", "tsv"],
   "defaultStreamingFormat": "csv",
}
```

Proposal: Add an instanceLabel for each language (some thing like "Forecast database from Konjunkturinstitutet") 
      
## Navigate endpoint 
**url:** `http://my-site.com/api/v2/navigate`

**HTTP method:** GET|POST

Browse the database structure.
There are two types *l* and *t* i.e. level or table. 

**Response**
```json
[
   {
     "id": "BE0101",
     "type": "level",
     "label": "Befolkningsstatistik",
     "links": [{
               "rel": "data",
               "href": "http://my-site.com/api/v2/navigate/BE0101"}]
   },
   {
     "id": "BE0401",
     "type": "level",
     "label": "Befolkningsframskrivningar",
     "links": [{
               "rel": "data",
               "href": "http://my-site.com/api/v2/navigate/BE0401"}]
   },
   {
     "id": "TAB0001",
     "type": "table",
     "label": "Tabell A",
     "links": [{
               "rel": "metadata",
               "href": "http://my-site.com/api/v2/tables/TAB0001"},
             {
               "rel": "data",
               "href": "http://my-site.com/api/v2/tables/TAB0001/data"}]
   }

]
```
Proposal: 
The querystring parameter recursive=true will return all sub nodes for the specified node.
Possible to define number of recursive levels. 

Make the "type": "table" item for a table ( "TableLocator-class" ) the same for this endpoint and for the /tables endpoint 
         add a link to the cube in a gui  

Question:
Will http://my-site.com/api/v2/tables/TAB0001/data return default values or is it necessary to also specify variables and values to this URL to get data? 
-	Yes, if the number of resulting cells is lower than the max cell limit.
Same selection rules as in the current API.
Do not make it the whole url ony specify the nodeid like
http://my-site.com/api/v2/database/BE and http://my-site.com/api/v2/database/BE0401
and not http://my-site.com/api/v2/database/BE/BE0401.. OK (ge exempel på PX fil fallet)

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
  "version": "1.0",
  "id": "TAB0001",
  "label": "Befolkning",
  "description": "Befolkning efter region, kön och år",
  "updated": "2019-02-21T09:30:00",
  "footnotes": [""],
  "contacts": [{
               "name": "Inga Svensson",
               "phone": "+46111111111",
               "mail": "inga.svensson@my-site.com"}],
  "variables": [{
                "id": "CONTENTS",
                "label": "Befolkning",  
                "elimination": false,
                "type": "CONTENTS",
                "values": [ {"id": "A", "text": "Befolkning", "unit": "antal"}]}, 

              {
                "id": "region",
                "label": "region",
                "elimination": true,
                "type": "GEO",
                "domain": "kommun2017",
                "filters": [{"id": "vs_lan", "label": "län", 
                           "links": [
                       {"rel": "metadata",
                        "href": "http://my-site.com/api/v2/tables/TAB0001/filters/vs_lan"}
                                  ]}],
                "values": [ {"id": "0000", "text": "Alla kommuner", "eliminationValue": true},
                          {"id": "0114", "text": "Upplands Väsby"},
                          {"id": "0115", "text": "Vallentuna"},
                          .
                          .
                          .
                          {"id": "2584", "text": "Kiruna"}
                        ]},
              {
                "id": "kon",
                "label": "kön",
                "elimination": true,
                "type": "GENERIC",
                "domain": "kon",
                "values": [
                          {"id": "1", "text": "män"},
                          {"id": "2", "text": "kvinnor"}
                        ]},
              {
                "id": "Tid",
                "label": "år",
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
  "streams": [
             {
               "id": "K1", 
               "label": "Befolkning efter kommun, kön och år ",
               "links": [
                      {"rel": "metadata",
                       "href": "http://my-site.com/api/v2/tables/TAB0001/streams/K1"},
                      {"rel": "data", 
                       "href": "http://my-site.com/api/v2/tables/TAB0001/streams/K1/data"}
                       ]},
             {
               "id": "L1", 
               "label": " Befolkning efter län, kön och år ",
               "links": [
                      {"rel": "metadata", 
                       "href": "http://my-site.com/api/v2/tables/TAB0001/streams/L1"},
                      {"rel": "data", 
                       "href": "http://my-site.com/api/v2/tables/TAB0001/streams/L1/data"}
                       ]}]
             }
           ],
   links: [{
             "rel": "data",
             "href": "http://my-site.com/api/v2/tables/TAB0001/data"}]

   }

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

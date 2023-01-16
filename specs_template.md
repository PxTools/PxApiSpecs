# PxApi 2.0
# Readers guide
## What this instruction covers
This document specifies the  queries and response formats and also the endpoints for the PxApi 2.0.
## References

[https://www.dst.dk/en/Statistik/statistikbanken/api](https://www.dst.dk/en/Statistik/statistikbanken/api)

[http://api.statbank.dk/console#data](http://api.statbank.dk/console#data)

[https://www.vinaysahni.com/best-practices-for-a-pragmatic-restful-api](https://www.vinaysahni.com/best-practices-for-a-pragmatic-restful-api)

# Concepts
The PxAPI uses some abstractions to represent the different parts of the statistical database. This section aims to give the reader a better understanding of those abstractions laying a foundation for understanding the structure of the API and the data that it provides.
## Table
A table is the representation of a statistical table. Others may sometimes refer to the same concept a dataset, cube, or multi-dimensional cube. But we have chose to call it a table due to historical reasons. A table consists of two parts the data that contains the numbers and the metadata that gives the data a context/meaning. E.g. data 10 452 326 and the metadata the population of Sweden December 31 2021.
## Database
A database is a collection of tables that are organized and structured together according to some schema. 
A common mistake is to mix the database concept with a relational database management system like Oracle Database or Microsoft SQL Server. 
Also sometimes when referring to the database one might refer to the instance of the database.
## Folder
Folders are used to organize tables in a database. A folder can contain other folders or tables. 
Usually, the first level of folders in a database are referred to the subject areas of the database. Others might also call them the themes of the database.
## Variable
Tables are multidimensional and variables are the concept used to describe the data (others may refer to variables as dimensions).  Take the example data 10 452 326 and the metadata the population of Sweden December 31 2021. 10 452 326 is described by tree variables 
1. What we are measuring, in our case this is the population. This variable is special and is referred to as the content variable. There and only be one content variable. Sometimes when there is just one content the content variable may be omitted instead the context is given by other metadata for the table. 
2. The point in time that the number is associated to in our case 31 December 2021. This variable type is also special and is referred to as the time variable. There should always be one and only one time variable.
3. The region. In our case it is Sweden. This is also a special kind of variable called a geographical variable. A table might have zero or many geographical variables but usually only have one if they have any.
There is a fourth kind of variable that is just called a variable that is used to describe the data. Imagen, that we had divided 10 452 326 by gender so we have two data cells instead 5 260 707 and 5 191 619 one for male and one for female. Then that fourth variable would be gender.
## Value
Variables have distinct values that make up the space for it. E.g. our gender variable above have two values one for male and one for female.
## Code list
A variable might have code list associated to that defined a new space for the variable by providing different sets of values. E.g. imagine, you have a regional variable with values for each municipality in Sweden. Then you might have a code list that transforms the municipalities into counties.
## Data cell
A data cell is the individual measure in a table.
The total number of cells for a table is given by the product of the number of values for each variable.
## Data source type
The information might be stored in different formats and technologies. We currently support two different data source types PX-file based and Oracle databases and Microsoft SQL Server that uses the Common Nordic Meta Model.
## Other terminology
- *Paxiom* the object model representing a table.
- *CNMM* the Common Nordic Meta Model is a relational database model for representing a `database` and `tables`. It is used and maintained by Statistics Sweden, Norway and Denmark.
- *PX-file* a physical representation of a table by using the PX file format.
- *PX-file database* a collection of PX-files that represents a `database`.
# API endpoints

POST is primarily intended to be used when fetching data since the query to select the data can in some cases become very large. So large that it can extends come web browser limits on url’s .

Proposal: Look at Json-Stat collections 

TODO Throttle protection

## Configuration endpoint
Get API configuration settings.
```
HTTP GET https://my-site.com/api/v2/config
```
### Example response
```json
::: examplesAsJson/configResponse.json
```
- *apiVersion* states the version of the API
- *languages* list the languages that can be used to when querying the API. Almost every endpoint support a lang query string parameter that can be set to the `id` of the language e.g. `lang=en` for english
- *defaultLanguage* specifys which langue is used in the resonses if no language have been specified.
- *maxDataCells* specifys the maximum number of data cells that can be fetch by one request.
- *maxCallsPerTimeWindow* specifys how many requests that can be done during a time window specified by *timeWindow*
- *timeWindow* the time duration in seconds that makes up the time window used the the thotteling protection. 
- *sourceReferences* specifies how one could cite the data fetch from through API.
- *license* specifys the license of the data.

## Navigation endpoints 

Browse the database structure.
```
HTTP GET https://my-site.com/api/v2/navigation
```
**Http method:** GET

### Example response
```json
::: examplesAsJson/folder-root.json
```


Returns the database root folder.

**url:** `http://my-site.com/api/v2/navigation/{id}`

Returns the database folder identified by *id*.

**HTTP method:** GET

### Example response
The following example shows the response of the API request `http://my-site.com/api/v2/navigation/BE0101`. Metadata about the folder BE0101 is returned together with the folder contents of the BE0101 folder, which is a subfolder BE0101A and the statistical table BefolkningNy.
```json
::: examplesAsJson/folder-root.json
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
::: examplesAsJson/tableMetadata.json
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

# Restrictions
Some restrictions in the 2.0 version of the API in comparison to the first version of the API.
-	**Only one database** – Drop the support of many databases that can be served by the same instance of the API. This will lower the complexity of the internals of the API since it does not have to take into account different configuration for different databases. Most organizations only have one database so the effect is limited only to a few inststances of the API. 
The recommended solution for organizations with multiple databases is to have multiple instances of the API with different configuration.
-	**New table identity** – The identifyer for a table is changed. In the case of a PX-file the MATRIX is used instead if the filename and in the case of CNMM the TableId instead of MainTable. The new id should be stable and not be reused. If a table change in such a way that it is no longer compatible it should be duplicated and assign an new id. This way the API will be able to identify the table even if it has been moved in the database.
-	**Names of aggregation files must be unique** – names of the aggregation files within a database must be unique since they will serve as the id of the aggregation filter.
-	VARIABLETYPE must be formalized.
-	Initially only support for PX, JSON-STAT, CSV,TSV, Excel and JSON-STAT2 (this should be possible to configure) 

## TODO  Remove section: Question:
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

### Impelemntation guides
### PX-file database
Filter = Aggregation
Stream = N/A

### CNMM
Filter = ValueSet or Grouping
Stream = Subtable

[comment]: <> (Run this command in a bash promt to updated the specs.md file)
[comment]: <> (./include_refs.sh specs_template.md > specs.md)

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

POST is primarily intended to be used when fetching data since the query to select the data can in some cases become very large. I some cases the url can exceed the maximum number of characteras allowed for a url. In these cases a POST request could be the solution.

Proposal: Look at Json-Stat collections 

TODO Throttle protection

## Configuration endpoint
Get API configuration settings. Instances of the API could be configured diffrently from each other. Clients can then use this endpoint to get information that could be useful for strering the behaivour for the client.
```
HTTP GET https://my-site.com/api/v2/config
```
### Example response
```json
::: examples/config-response.json
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

### Returns the database root folder.
```
HTTP GET https://my-site.com/api/v2/navigation
```
#### Parameters
##### lang
An optional language parameter.
#### Example response
```json
::: examples/navigation-root-response.json
```
### Return the content of a specific folder in the database
```
HTTP GET http://my-site.com/api/v2/navigation/{id}
```

Returns the database folder identified by *id*.

#### Parameters
##### lang
An optional language parameter.

#### Example response
The following example shows the response of the API request `http://my-site.com/api/v2/navigation/BE0101A`. Metadata about the folder BE0101A is returned together with the folder contents of the BE0101A folder

```json
::: examples/navigation-BE0101A-response.json
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
- *Heading* A heading for separationg the content

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


## Tables endpoints
### List all tables
List all tables in the database. The list can be filter by diffrent parameters.
```
HTTP GET http://my-site.com/api/v2/api/v2/tables/
```
#### Parameters
You can restrict the tables return by the following parameters
##### lang
An optional language parameter.
##### query
Selects only tables that that matches a criteria which is specified by the search parameter.
```
http://my-site.com/api/v2/tables?query=befolkning
```
##### pastDays
Selects only tables that was updated from the time of execution going back number of days stated by the parameter pastDays. Valid values for past days are integers between 1 and ? (Will return error?)
```
http://my-site.com/api/v2/tables?pastDays=5
```
##### includeDiscontinued
A true or false if discontinued tables should be included. Tables that do not have an explicit set the discontinued property will be treated as they are not discontinued.

##### pageSize
How many tables that should be in the response.
##### pageNumber
A number that specifies which page of the response to display. Default value is 1.

#### Example response
An exampel filtering on population with a page size of 3
```json
::: examples/tables-tables-response.json
```
**Response described**

*Tables* a list of table objects containing basic metadata for a table.
*Page* paging information about the results 
* *pageNumber* the page number that was returned.
* *pageSize* the pageSize of the response.
* *totalElements* the total number of tables
* *totalPages* the total number of pages.
* *links* links with relation next, previous and last when relevant. That describes the links to next, previous and last page in the result set.

**Note**
The information is cache for performance and there can be a lag in the response directly after release of new data. 

### List basic information for a table
```
HTTP GET http://my-site.com/api/v2/tables/<table-id>
```
#### Parameters
##### lang
An optional language parameter.
#### Example response
An example that returns the basic information for table TAB638
```json
::: examples/tables-table-response.json
```
**Note**
The information may be cached for performance and there can be a short delay in the response directly after release of new data. 
### List metadata for a table
List metadata for the specified table
```
HTTP GET http://my-site.com/api/v2/tables/<table-id>/metadata
```
#### Parameters
##### lang
An optional language parameter.
##### ouputFormat
One of json-px or json-stat2. The default is given by the configuration endpoint.
#### Example response
```json
::: examples/tables-table-metadata-response.json
```
### Get data for a specific table
```
HTTP GET http://my-site.com/api/v2/tables/<table-id>/data/
```
#### Parameters
The variables of the table can be used to query a specific part of the table by using the parameters bellow. If the parameters is not given a default region of the table will be selected.
##### lang
An optional language parameter.
##### valueCodes
A selection specifying a region of the table that will be returned. All variables that cann´t be eliminated must have a selection specified. The selection for a varibale is given in the form:
```
valueCodes[VARIABLE-CODE]=ITEM-SELECTION-1,ITEM-SELECTION-2,ITEM-SELECTION-3, etc
```
Whare `VARIABLE-CODE` is the code of the variable and `ITEM-SELECTION-X` is either a value code of a selection expression.
If the value code of a selection expression contains a comma is should be in brackets e.g. `[TOP(1,12)]` and if the value code is allready in brackets is should be in extra brackets e.g. `[ME01]` should be `[[ME01]]`.
##### codelist
You might what to use a diffrent codelist i.e. have a diffrent aggregate for a variable. I that case use can specify a codelist of one that is available for that variable. The items refered in `valueCodes` will then refeere to the values in the refered codelist. To codelist is specified in the form
```
codelist[VARIABLE-CODE]=CODELIST-ID
```
Whare `VARIABLE-CODE` is the code of the variable and `CODELIST-ID` is the ID of the codelist to use.
##### outputValues
Codelists can either be used to transform the returned variable or as a mean for selecting values. E.g. If a variable as values at municipality level and a codelist a county level. You might either use the codelist to aggregated the municiplaites to countys in the result or select all municipalities by specifying counties and have the municipalities that belongs the selected counties in the result.
The `outputValues` paramater can be used to specify how the codelist is used and is only applicable when a codelist have been secified for a variable.
It is given in the form
```
outputValues[VARIABLE-CODE]=aggregated|single
```
Whare `VARIABLE-CODE` is the code of the variable and the value could be either `aggregated` that is the values should be transformed to the codelist values or `single` the original values should be in the result. If no paramater is given `aggregated` will be used when specifying a codelist for a variable.
##### ouputFormat
Specifies the fomrat that the result should be in. The default is given by the configuration endpoint. See also the configuration endpind for available formats.

### Get data for a specific table
```
HTTP POST http://my-site.com/api/v2/tables/<table-id>/data/
```
This endpoint is similar to the GET but the selection expression is given in the body of the request as a JSON object.
#### Selection expression
The JSON object specifying the request is given in the form
```json
{
    "selection": [
        {
            "variableCode": "VARIABLE-CODE-1",
            "valueCodes": ["ITEM-SELECTION-1-1","ITEM-SELECTION-1-2"]
        },
        {
            "variableCode": "VARIABLE-CODE-1",
            "valueCodes": ["ITEM-SELECTION-2-1"],
            "codelList": "CODE-LIST-A",
            "outputValues": "single"
        }
    ]
}
```
#### Parameters
##### lang
An optional language parameter.
##### ouputFormat
Specifies the fomrat that the result should be in. The default is given by the configuration endpoint. See also the configuration endpind for available formats.

### Selection expression
Instead of specifying all valuecodes one could use a selection expression instead. Bellow follows the available expressions.
#### Wildcard expression 
A wildcard can be used to match all codes e.g. `*01` matches all codes that ends with `01`, `*2*` matches all codes that contains a `2`, `A*` matches all codes that starts with `A` and `*` matches all codes. Maximum of 2 waildcards can be given.

#### Exact match
A questionmark can be used to match exactly one character. E.g. `?` matches all codes that has exactly one character, `?1` matches codes that are 2 character long and ends with `1`.

#### TOP
The `TOP(N, Offset)` expression selects the `N` first values with an offset of `Offset`. E.g. `TOP(5)` will select the first 5 values or `TOP(5,3)` will select 3rd to 8th value. The `Offset` is by default `0` and must not be specified.

#### BOTTOM
`BOTTOM` is just as `TOP` but selects values from the bottom if the values list.

#### RANGE
`RANGE(X,Y)` selectes all values between value code `X` and value code `Y` including `X` and `Y`.

#### FROM
`FROM(X)` selectes all value codes from the value code that has `X` and bellow including `X`.

#### TO
TO(X) väljer alla värden från början till värdet med koden X inklusive värdet med koden X.

## Elimination
If elimination is set to true the variable can be eliminated and nothing have to be selected for this variable and the result will not contain that variable. If the variable have a value that states that it is the elimination value then that value will be selected to eliminate the variable. If no elimination value is specified the variable will be eliminated from the result by summing up all data points for the all values of that variable. If a variable has elimination set to false then at least one value bust be selected for that variable.



### Codelist endpoints
### List codelist for a table
Give information about a specific codelist
```
HTTP GET/api/v2/codelists/<codlist-id>
```
#### Parameters
##### lang
An optional language parameter.
#### Example response
An example response for codelist agg_RegionNUTS2_2008
```json
::: examples/codelists-agg_RegionNUTS2_2008-response.json
```




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

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
    "maxCallsPerTimeWindow": 30,
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
    ]
}
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
{
  "language": "en",
  "objectType": "folder",
  "id": null,
  "label": null,
  "description": null,
  "folderContents": [
    {
      "objectType": "folder-information",
      "id": "AM",
      "label": "Labour market",
      "description": "",
      "links": [
        {
          "rel": "self",
          "hreflang": "sv",
          "href": "https://my-site.com/api/v2/navigation/AM"
        },
        {
          "rel": "self",
          "hreflang": "en",
          "href": "https://my-site.com/api/v2/navigation/AM?lang=en"
        }
      ]
    },
    {
      "objectType": "folder-information",
      "id": "BE",
      "label": "Population",
      "description": "",
      "links": [
        {
          "rel": "self",
          "hreflang": "sv",
          "href": "https://my-site.com/api/v2/navigation/BE"
        },
        {
          "rel": "self",
          "hreflang": "en",
          "href": "https://my-site.com/api/v2/navigation/BE?lang=en"
        }
      ]
    }
  ],
  "links": [
    {
      "rel": "self",
      "hreflang": "sv",
      "href": "https://my-site.com/api/v2/navigation"
    },
    {
      "rel": "self",
      "hreflang": "en",
      "href": "https://my-site.com/api/v2/navigation?lang=en"
    }
  ]
}
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
{
  "language": "en",
  "objectType": "folder",
  "id": "BE0101A",
  "label": "Number of inhabitants",
  "description": "",
  "folderContents": [
    {
      "objectType": "table",
      "id": "TAB5444",
      "label": "Population per month by region, age and sex. Year 2000M01 - 2022M11",
      "description": "",
      "tags": [
        "population",
        "inhabitants"
      ],
      "updated": "2022-01-08T07:00:00.000Z",
      "firstPeriod": "2000M1",
      "lastPeriod": "2022M11",
      "category": "public",
      "variableNames": [
        "region",
        "age",
        "sex",
        "age",
        "month"
      ],
      "discontinued": false,
      "links": [
        {
          "rel": "self",
          "hreflang": "sv",
          "href": "https://my-site.com/api/v2/tables/TAB5444"
        },
        {
          "rel": "self",
          "hreflang": "en",
          "href": "https://my-site.com/api/v2/tables/TAB5444?lang=en"
        },
        {
          "rel": "metadata",
          "hreflang": "en",
          "href": "https://my-site.com/api/v2/tables/TAB5444/metadata?lang=en"
        },
        {
          "rel": "data",
          "hreflang": "en",
          "href": "https://my-site.com/api/v2/tables/TAB5444/data?lang=en"
        }
      ]
    },
    {
      "objectType": "table",
      "id": "TAB638",
      "label": "Population by region, marital status, age, sex, observations and year 1968 - 2021",
      "description": "",
      "tags": [
        "population"
      ],
      "updated": "2022-01-08T07:00:00.000Z",
      "firstPeriod": "1968",
      "lastPeriod": "2021",
      "category": "public",
      "variableNames": [
        "region",
        "marital status",
        "age",
        "sex",
        "age",
        "observations",
        "year"
      ],
      "discontinued": false,
      "links": [
        {
          "rel": "self",
          "hreflang": "sv",
          "href": "https://my-site.com/api/v2/tables/TAB638"
        },
        {
          "rel": "self",
          "hreflang": "en",
          "href": "https://my-site.com/api/v2/tables/TAB638?lang=en"
        },
        {
          "rel": "metadata",
          "hreflang": "en",
          "href": "https://my-site.com/api/v2/tables/TAB638/metadata?lang=en"
        },
        {
          "rel": "data",
          "hreflang": "en",
          "href": "https://my-site.com/api/v2/tables/TAB638/data?lang=en"
        }
      ]
    },
    {
      "objectType": "table",
      "id": "TAB5890",
      "label": "Population by age and sex. Year 1860 - 2021",
      "description": "",
      "tags": [
        "population"
      ],
      "updated": "2022-01-08T07:00:00.000Z",
      "firstPeriod": "1860",
      "lastPeriod": "2021",
      "category": "public",
      "variableNames": [
        "age",
        "sex",
        "observations",
        "year"
      ],
      "discontinued": false,
      "links": [
        {
          "rel": "self",
          "hreflang": "sv",
          "href": "https://my-site.com/api/v2/tables/TAB5890"
        },
        {
          "rel": "self",
          "hreflang": "en",
          "href": "https://my-site.com/api/v2/tables/TAB5890?lang=en"
        },
        {
          "rel": "metadata",
          "hreflang": "en",
          "href": "https://my-site.com/api/v2/tables/TAB5890/metadata?lang=en"
        },
        {
          "rel": "data",
          "hreflang": "en",
          "href": "https://my-site.com/api/v2/tables/TAB5890/data?lang=en"
        }
      ]
    },
    {
      "objectType": "table",
      "id": "TAB4537",
      "label": "Population by district, Landscape or Part of the country by sex. Year 2015 - 2021",
      "description": "",
      "tags": [
        "population"
      ],
      "updated": "2022-01-08T07:00:00.000Z",
      "firstPeriod": "2015",
      "lastPeriod": "2021",
      "category": "public",
      "variableNames": [
        "region",
        "sex",
        "observations",
        "year"
      ],
      "discontinued": false,
      "links": [
        {
          "rel": "self",
          "hreflang": "sv",
          "href": "https://my-site.com/api/v2/tables/TAB4537"
        },
        {
          "rel": "self",
          "hreflang": "en",
          "href": "https://my-site.com/api/v2/tables/TAB4537?lang=en"
        },
        {
          "rel": "metadata",
          "hreflang": "en",
          "href": "https://my-site.com/api/v2/tables/TAB4537/metadata?lang=en"
        },
        {
          "rel": "data",
          "hreflang": "en",
          "href": "https://my-site.com/api/v2/tables/TAB4537/data?lang=en"
        }
      ]
    },
    {
      "objectType": "table",
      "id": "TAB1267",
      "label": "Population 1 November by region, age, sex, observations and year 2002 - 2022",
      "description": "",
      "tags": [
        "population"
      ],
      "updated": "2022-01-08T07:00:00.000Z",
      "firstPeriod": "2015",
      "lastPeriod": "2021",
      "category": "public",
      "variableNames": [
        "region",
        "age",
        "sex",
        "observations",
        "year"
      ],
      "discontinued": false,
      "links": [
        {
          "rel": "self",
          "hreflang": "sv",
          "href": "https://my-site.com/api/v2/tables/TAB1267"
        },
        {
          "rel": "self",
          "hreflang": "en",
          "href": "https://my-site.com/api/v2/tables/TAB1267?lang=en"
        },
        {
          "rel": "metadata",
          "hreflang": "en",
          "href": "https://my-site.com/api/v2/tables/TAB1267/metadata?lang=en"
        },
        {
          "rel": "data",
          "hreflang": "en",
          "href": "https://my-site.com/api/v2/tables/TAB1267/data?lang=en"
        }
      ]
    }
  ],
  "links": [
    {
      "rel": "self",
      "hreflang": "sv",
      "href": "https://my-site.com/api/v2/navigation/BE0101A"
    },
    {
      "rel": "self",
      "hreflang": "en",
      "href": "https://my-site.com/api/v2/navigation/BE0101A?lang=en"
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
{
    "language": "en",
    "tables": [
        {
            "objectType": "table",
            "id": "TAB5444",
            "label": "Population per month by region, age and sex. Year 2000M01 - 2022M11",
            "description": "",
            "tags": [
              "population",
              "inhabitants"
            ],
            "updated": "2022-01-08T07:00:00.000Z",
            "firstPeriod": "2000M1",
            "lastPeriod": "2022M11",
            "category": "public",
            "variableNames": [
              "region",
              "age",
              "sex",
              "age",
              "month"
            ],
            "discontinued": false,
            "links": [
              {
                "rel": "self",
                "hreflang": "sv",
                "href": "https://my-site.com/api/v2/tables/TAB5444"
              },
              {
                "rel": "self",
                "hreflang": "en",
                "href": "https://my-site.com/api/v2/tables/TAB5444?lang=en"
              },
              {
                "rel": "metadata",
                "hreflang": "en",
                "href": "https://my-site.com/api/v2/tables/TAB5444/metadata?lang=en"
              },
              {
                "rel": "data",
                "hreflang": "en",
                "href": "https://my-site.com/api/v2/tables/TAB5444/data?lang=en"
              }
            ]
          },
          {
            "objectType": "table",
            "id": "TAB638",
            "label": "Population by region, marital status, age, sex, observations and year 1968 - 2021",
            "description": "",
            "tags": [
              "population"
            ],
            "updated": "2022-01-08T07:00:00.000Z",
            "firstPeriod": "1968",
            "lastPeriod": "2021",
            "category": "public",
            "variableNames": [
              "region",
              "marital status",
              "age",
              "sex",
              "age",
              "observations",
              "year"
            ],
            "discontinued": false,
            "links": [
              {
                "rel": "self",
                "hreflang": "sv",
                "href": "https://my-site.com/api/v2/tables/TAB638"
              },
              {
                "rel": "self",
                "hreflang": "en",
                "href": "https://my-site.com/api/v2/tables/TAB638?lang=en"
              },
              {
                "rel": "metadata",
                "hreflang": "en",
                "href": "https://my-site.com/api/v2/tables/TAB638/metadata?lang=en"
              },
              {
                "rel": "data",
                "hreflang": "en",
                "href": "https://my-site.com/api/v2/tables/TAB638/data?lang=en"
              }
            ]
          },
          {
            "objectType": "table",
            "id": "TAB5890",
            "label": "Population by age and sex. Year 1860 - 2021",
            "description": "",
            "tags": [
              "population"
            ],
            "updated": "2022-01-08T07:00:00.000Z",
            "firstPeriod": "1860",
            "lastPeriod": "2021",
            "category": "public",
            "variableNames": [
              "age",
              "sex",
              "observations",
              "year"
            ],
            "discontinued": false,
            "links": [
              {
                "rel": "self",
                "hreflang": "sv",
                "href": "https://my-site.com/api/v2/tables/TAB5890"
              },
              {
                "rel": "self",
                "hreflang": "en",
                "href": "https://my-site.com/api/v2/tables/TAB5890?lang=en"
              },
              {
                "rel": "metadata",
                "hreflang": "en",
                "href": "https://my-site.com/api/v2/tables/TAB5890/metadata?lang=en"
              },
              {
                "rel": "data",
                "hreflang": "en",
                "href": "https://my-site.com/api/v2/tables/TAB5890/data?lang=en"
              }
            ]
          }
    ],
    "page": {
        "pageNumber": 1,
        "pageSize": 3,
        "totalElements": 56,
        "totalPages": 19,
        "links": [
            {
                "rel": "next",
                "hreflang": "en",
                "href": "https://my-site.com/api/v2/tables/?lang=en&query=population&pagesize=3,pageNumber=2"
            },
            {
                "rel": "last",
                "hreflang": "en",
                "href": "https://my-site.com/api/v2/tables/?lang=en&query=population&pagesize=3,pageNumber=19"
            }

        ]
    },
    "links": [
        {
            "rel": "self",
            "hreflang": "en",
            "href": "https://my-site.com/api/v2/tables/?lang=en&query=population&pagesize=3"
        }
    ]
}
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
{
    "language": "en",
    "objectType": "table",
    "id": "TAB638",
    "label": "Population by region, marital status, age, sex, observations and year 1968 - 2021",
    "description": "",
    "tags": [
      "population"
    ],
    "updated": "2022-01-08T07:00:00.000Z",
    "firstPeriod": "1968",
    "lastPeriod": "2021",
    "category": "public",
    "variableNames": [
      "region",
      "marital status",
      "age",
      "sex",
      "age",
      "observations",
      "year"
    ],
    "discontinued": false,
    "links": [
      {
        "rel": "self",
        "hreflang": "sv",
        "href": "https://my-site.com/api/v2/tables/TAB638"
      },
      {
        "rel": "self",
        "hreflang": "en",
        "href": "https://my-site.com/api/v2/tables/TAB638?lang=en"
      },
      {
        "rel": "metadata",
        "hreflang": "en",
        "href": "https://my-site.com/api/v2/tables/TAB638/metadata?lang=en"
      },
      {
        "rel": "data",
        "hreflang": "en",
        "href": "https://my-site.com/api/v2/tables/TAB638/data?lang=en"
      }
    ]
  }
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
{
    "id": "TAB638",
    "language": "sv",
    "label": "Folkmängden efter region, civilstånd, ålder och kön. År 1968 - 2021",
    "description": "",
    "aggregationAllowed": true,
    "officialStatistics": true,
    "subjectCode": "BE",
    "subjectLabel": "Befolkning",
    "source": "SCB",
    "licence": "CC0-1.0",
    "tags" : [""],
    "updated": "2022-01-08T07:00:00.000Z",
    "discontinued": false,
    "variables": [
        {
            "id": "Region",
            "label": "region",
            "type": "GeographicalVariable",
            "eliminination": true,
            "eliminationValueCode": "00",
            "values": [
                { "code": "00", "label": "Riket"},
                { "code": "01", "label": "Stockholms län"},
                { "code": "0114", "label": "Upplands Väsby"},
                { "code": "0115", "label": "Vallentuna"},
                { "code": "0117", "label": "Österåker"},
                { "code": "0120", "label": "Värmdö"},
                { "code": "0123", "label": "Järfälla"},
                { "code": "0125", "label": "Ekerö"},
                { "code": "0126", "label": "Huddinge"},
                { "code": "0127", "label": "Botkyrka"},
                { "code": "0128", "label": "Salem"},
                { "code": "0136", "label": "Haninge"},
                { "code": "0138", "label": "Tyresö"},
                { "code": "0139", "label": "Upplands-Bro"},
                { "code": "0140", "label": "Nykvarn", "notes": [{"madantory": true, "text": "Ny regional indelning fr.o.m. 1999-01-01. Delar av Södertälje kommun (kod 0181) bildar en ny kommun benämnd Nykvarn (kod 0140)."}] },
                { "code": "0160", "label": "Täby"},
                { "code": "0162", "label": "Danderyd"},
                { "code": "0163", "label": "Sollentuna"},
                { "code": "0180", "label": "Stockholm"},
                { "code": "0181", "label": "Södertälje", "notes": [{"madantory": true, "text": "Ny regional indelning fr.o.m. 1999-01-01. Delar av Södertälje kommun (kod 0181) bildar en ny kommun benämnd Nykvarn (kod 0140)."}] },
                { "code": "0182", "label": "Nacka"},
                { "code": "0183", "label": "Sundbyberg"},
                { "code": "0184", "label": "Solna"},
                { "code": "0186", "label": "Lidingö", "notes": [{"madantory": true, "text": "Från och med 2011-01-01 tillhör Storholmen Lidingö (0186) som tidigare tillhörde Vaxholm (0187)."}] },
                { "code": "0187", "label": "Vaxholm", "notes": [{"madantory": true, "text": "Från och med 2011-01-01 tillhör Storholmen Lidingö (0186) som tidigare tillhörde Vaxholm (0187)."}] },
                { "code": "0188", "label": "Norrtälje"},
                { "code": "0191", "label": "Sigtuna"},
                { "code": "0192", "label": "Nynäshamn"},
                { "code": "03", "label": "Uppsala län"},
                { "code": "0305", "label": "Håbo"},
                { "code": "0319", "label": "Älvkarleby"},
                { "code": "0330", "label": "Knivsta", "notes": [{"madantory": true, "text": "Ny regional indelning fr.o.m. 2003-01-01. Delar av Uppsala kommun bildar en ny kommun benämnd Knivsta kommun."}] },
                { "code": "0331", "label": "Heby"},
                { "code": "0360", "label": "Tierp"},
                { "code": "0380", "label": "Uppsala", "notes": [{"madantory": true, "text": "Ny regional indelning fr.o.m. 2003-01-01. Delar av Uppsala kommun bildar en ny kommun benämnd Knivsta kommun."}] },
                { "code": "0381", "label": "Enköping"},
                { "code": "0382", "label": "Östhammar"},
                { "code": "04", "label": "Södermanlands län"},
                { "code": "0428", "label": "Vingåker"},
                { "code": "0461", "label": "Gnesta"},
                { "code": "0480", "label": "Nyköping"},
                { "code": "0481", "label": "Oxelösund"},
                { "code": "0482", "label": "Flen"},
                { "code": "0483", "label": "Katrineholm"},
                { "code": "0484", "label": "Eskilstuna"},
                { "code": "0486", "label": "Strängnäs"},
                { "code": "0488", "label": "Trosa"},
                { "code": "05", "label": "Östergötlands län"},
                { "code": "0509", "label": "Ödeshög"},
                { "code": "0512", "label": "Ydre"},
                { "code": "0513", "label": "Kinda"},
                { "code": "0560", "label": "Boxholm"},
                { "code": "0561", "label": "Åtvidaberg"},
                { "code": "0562", "label": "Finspång"},
                { "code": "0563", "label": "Valdemarsvik"},
                { "code": "0580", "label": "Linköping"},
                { "code": "0581", "label": "Norrköping"},
                { "code": "0582", "label": "Söderköping"},
                { "code": "0583", "label": "Motala"},
                { "code": "0584", "label": "Vadstena"},
                { "code": "0586", "label": "Mjölby"},
                { "code": "06", "label": "Jönköpings län"},
                { "code": "0604", "label": "Aneby"},
                { "code": "0617", "label": "Gnosjö"},
                { "code": "0642", "label": "Mullsjö"},
                { "code": "0643", "label": "Habo"},
                { "code": "0662", "label": "Gislaved"},
                { "code": "0665", "label": "Vaggeryd"},
                { "code": "0680", "label": "Jönköping"},
                { "code": "0682", "label": "Nässjö"},
                { "code": "0683", "label": "Värnamo"},
                { "code": "0684", "label": "Sävsjö"},
                { "code": "0685", "label": "Vetlanda"},
                { "code": "0686", "label": "Eksjö"},
                { "code": "0687", "label": "Tranås"},
                { "code": "07", "label": "Kronobergs län"},
                { "code": "0760", "label": "Uppvidinge"},
                { "code": "0761", "label": "Lessebo"},
                { "code": "0763", "label": "Tingsryd"},
                { "code": "0764", "label": "Alvesta"},
                { "code": "0765", "label": "Älmhult"},
                { "code": "0767", "label": "Markaryd"},
                { "code": "0780", "label": "Växjö"},
                { "code": "0781", "label": "Ljungby"},
                { "code": "08", "label": "Kalmar län"},
                { "code": "0821", "label": "Högsby"},
                { "code": "0834", "label": "Torsås"},
                { "code": "0840", "label": "Mörbylånga"},
                { "code": "0860", "label": "Hultsfred"},
                { "code": "0861", "label": "Mönsterås"},
                { "code": "0862", "label": "Emmaboda"},
                { "code": "0880", "label": "Kalmar"},
                { "code": "0881", "label": "Nybro"},
                { "code": "0882", "label": "Oskarshamn"},
                { "code": "0883", "label": "Västervik"},
                { "code": "0884", "label": "Vimmerby"},
                { "code": "0885", "label": "Borgholm"},
                { "code": "09", "label": "Gotlands län"},
                { "code": "0980", "label": "Gotland"},
                { "code": "10", "label": "Blekinge län"},
                { "code": "1060", "label": "Olofström"},
                { "code": "1080", "label": "Karlskrona"},
                { "code": "1081", "label": "Ronneby"},
                { "code": "1082", "label": "Karlshamn"},
                { "code": "1083", "label": "Sölvesborg"},
                { "code": "12", "label": "Skåne län"},
                { "code": "1214", "label": "Svalöv"},
                { "code": "1230", "label": "Staffanstorp"},
                { "code": "1231", "label": "Burlöv"},
                { "code": "1233", "label": "Vellinge"},
                { "code": "1256", "label": "Östra Göinge"},
                { "code": "1257", "label": "Örkelljunga"},
                { "code": "1260", "label": "Bjuv"},
                { "code": "1261", "label": "Kävlinge"},
                { "code": "1262", "label": "Lomma"},
                { "code": "1263", "label": "Svedala"},
                { "code": "1264", "label": "Skurup"},
                { "code": "1265", "label": "Sjöbo"},
                { "code": "1266", "label": "Hörby"},
                { "code": "1267", "label": "Höör"},
                { "code": "1270", "label": "Tomelilla"},
                { "code": "1272", "label": "Bromölla"},
                { "code": "1273", "label": "Osby"},
                { "code": "1275", "label": "Perstorp"},
                { "code": "1276", "label": "Klippan"},
                { "code": "1277", "label": "Åstorp"},
                { "code": "1278", "label": "Båstad"},
                { "code": "1280", "label": "Malmö"},
                { "code": "1281", "label": "Lund"},
                { "code": "1282", "label": "Landskrona"},
                { "code": "1283", "label": "Helsingborg"},
                { "code": "1284", "label": "Höganäs"},
                { "code": "1285", "label": "Eslöv"},
                { "code": "1286", "label": "Ystad"},
                { "code": "1287", "label": "Trelleborg"},
                { "code": "1290", "label": "Kristianstad"},
                { "code": "1291", "label": "Simrishamn"},
                { "code": "1292", "label": "Ängelholm"},
                { "code": "1293", "label": "Hässleholm"},
                { "code": "13", "label": "Hallands län"},
                { "code": "1315", "label": "Hylte"},
                { "code": "1380", "label": "Halmstad"},
                { "code": "1381", "label": "Laholm"},
                { "code": "1382", "label": "Falkenberg"},
                { "code": "1383", "label": "Varberg"},
                { "code": "1384", "label": "Kungsbacka"},
                { "code": "14", "label": "Västra Götalands län"},
                { "code": "1401", "label": "Härryda"},
                { "code": "1402", "label": "Partille"},
                { "code": "1407", "label": "Öckerö"},
                { "code": "1415", "label": "Stenungsund"},
                { "code": "1419", "label": "Tjörn"},
                { "code": "1421", "label": "Orust"},
                { "code": "1427", "label": "Sotenäs"},
                { "code": "1430", "label": "Munkedal"},
                { "code": "1435", "label": "Tanum"},
                { "code": "1438", "label": "Dals-Ed"},
                { "code": "1439", "label": "Färgelanda"},
                { "code": "1440", "label": "Ale"},
                { "code": "1441", "label": "Lerum"},
                { "code": "1442", "label": "Vårgårda"},
                { "code": "1443", "label": "Bollebygd"},
                { "code": "1444", "label": "Grästorp"},
                { "code": "1445", "label": "Essunga"},
                { "code": "1446", "label": "Karlsborg"},
                { "code": "1447", "label": "Gullspång"},
                { "code": "1452", "label": "Tranemo"},
                { "code": "1460", "label": "Bengtsfors"},
                { "code": "1461", "label": "Mellerud"},
                { "code": "1462", "label": "Lilla Edet"},
                { "code": "1463", "label": "Mark"},
                { "code": "1465", "label": "Svenljunga"},
                { "code": "1466", "label": "Herrljunga"},
                { "code": "1470", "label": "Vara"},
                { "code": "1471", "label": "Götene"},
                { "code": "1472", "label": "Tibro"},
                { "code": "1473", "label": "Töreboda"},
                { "code": "1480", "label": "Göteborg"},
                { "code": "1481", "label": "Mölndal"},
                { "code": "1482", "label": "Kungälv"},
                { "code": "1484", "label": "Lysekil"},
                { "code": "1485", "label": "Uddevalla"},
                { "code": "1486", "label": "Strömstad"},
                { "code": "1487", "label": "Vänersborg"},
                { "code": "1488", "label": "Trollhättan"},
                { "code": "1489", "label": "Alingsås"},
                { "code": "1490", "label": "Borås"},
                { "code": "1491", "label": "Ulricehamn"},
                { "code": "1492", "label": "Åmål"},
                { "code": "1493", "label": "Mariestad"},
                { "code": "1494", "label": "Lidköping"},
                { "code": "1495", "label": "Skara"},
                { "code": "1496", "label": "Skövde"},
                { "code": "1497", "label": "Hjo"},
                { "code": "1498", "label": "Tidaholm"},
                { "code": "1499", "label": "Falköping"},
                { "code": "17", "label": "Värmlands län"},
                { "code": "1715", "label": "Kil"},
                { "code": "1730", "label": "Eda"},
                { "code": "1737", "label": "Torsby"},
                { "code": "1760", "label": "Storfors"},
                { "code": "1761", "label": "Hammarö"},
                { "code": "1762", "label": "Munkfors"},
                { "code": "1763", "label": "Forshaga"},
                { "code": "1764", "label": "Grums"},
                { "code": "1765", "label": "Årjäng"},
                { "code": "1766", "label": "Sunne"},
                { "code": "1780", "label": "Karlstad"},
                { "code": "1781", "label": "Kristinehamn"},
                { "code": "1782", "label": "Filipstad"},
                { "code": "1783", "label": "Hagfors"},
                { "code": "1784", "label": "Arvika"},
                { "code": "1785", "label": "Säffle"},
                { "code": "18", "label": "Örebro län"},
                { "code": "1814", "label": "Lekeberg"},
                { "code": "1860", "label": "Laxå"},
                { "code": "1861", "label": "Hallsberg"},
                { "code": "1862", "label": "Degerfors"},
                { "code": "1863", "label": "Hällefors"},
                { "code": "1864", "label": "Ljusnarsberg"},
                { "code": "1880", "label": "Örebro"},
                { "code": "1881", "label": "Kumla"},
                { "code": "1882", "label": "Askersund"},
                { "code": "1883", "label": "Karlskoga"},
                { "code": "1884", "label": "Nora"},
                { "code": "1885", "label": "Lindesberg"},
                { "code": "19", "label": "Västmanlands län"},
                { "code": "1904", "label": "Skinnskatteberg"},
                { "code": "1907", "label": "Surahammar"},
                { "code": "1960", "label": "Kungsör"},
                { "code": "1961", "label": "Hallstahammar"},
                { "code": "1962", "label": "Norberg"},
                { "code": "1980", "label": "Västerås"},
                { "code": "1981", "label": "Sala"},
                { "code": "1982", "label": "Fagersta"},
                { "code": "1983", "label": "Köping"},
                { "code": "1984", "label": "Arboga"},
                { "code": "20", "label": "Dalarnas län"},
                { "code": "2021", "label": "Vansbro"},
                { "code": "2023", "label": "Malung-Sälen"},
                { "code": "2026", "label": "Gagnef"},
                { "code": "2029", "label": "Leksand"},
                { "code": "2031", "label": "Rättvik"},
                { "code": "2034", "label": "Orsa"},
                { "code": "2039", "label": "Älvdalen"},
                { "code": "2061", "label": "Smedjebacken"},
                { "code": "2062", "label": "Mora"},
                { "code": "2080", "label": "Falun"},
                { "code": "2081", "label": "Borlänge"},
                { "code": "2082", "label": "Säter"},
                { "code": "2083", "label": "Hedemora"},
                { "code": "2084", "label": "Avesta"},
                { "code": "2085", "label": "Ludvika"},
                { "code": "21", "label": "Gävleborgs län"},
                { "code": "2101", "label": "Ockelbo"},
                { "code": "2104", "label": "Hofors"},
                { "code": "2121", "label": "Ovanåker"},
                { "code": "2132", "label": "Nordanstig"},
                { "code": "2161", "label": "Ljusdal"},
                { "code": "2180", "label": "Gävle"},
                { "code": "2181", "label": "Sandviken"},
                { "code": "2182", "label": "Söderhamn"},
                { "code": "2183", "label": "Bollnäs"},
                { "code": "2184", "label": "Hudiksvall"},
                { "code": "22", "label": "Västernorrlands län"},
                { "code": "2260", "label": "Ånge"},
                { "code": "2262", "label": "Timrå"},
                { "code": "2280", "label": "Härnösand"},
                { "code": "2281", "label": "Sundsvall"},
                { "code": "2282", "label": "Kramfors"},
                { "code": "2283", "label": "Sollefteå"},
                { "code": "2284", "label": "Örnsköldsvik"},
                { "code": "23", "label": "Jämtlands län"},
                { "code": "2303", "label": "Ragunda"},
                { "code": "2305", "label": "Bräcke"},
                { "code": "2309", "label": "Krokom"},
                { "code": "2313", "label": "Strömsund"},
                { "code": "2321", "label": "Åre"},
                { "code": "2326", "label": "Berg"},
                { "code": "2361", "label": "Härjedalen"},
                { "code": "2380", "label": "Östersund"},
                { "code": "24", "label": "Västerbottens län"},
                { "code": "2401", "label": "Nordmaling"},
                { "code": "2403", "label": "Bjurholm"},
                { "code": "2404", "label": "Vindeln"},
                { "code": "2409", "label": "Robertsfors"},
                { "code": "2417", "label": "Norsjö"},
                { "code": "2418", "label": "Malå"},
                { "code": "2421", "label": "Storuman"},
                { "code": "2422", "label": "Sorsele"},
                { "code": "2425", "label": "Dorotea"},
                { "code": "2460", "label": "Vännäs"},
                { "code": "2462", "label": "Vilhelmina"},
                { "code": "2463", "label": "Åsele"},
                { "code": "2480", "label": "Umeå"},
                { "code": "2481", "label": "Lycksele"},
                { "code": "2482", "label": "Skellefteå"},
                { "code": "25", "label": "Norrbottens län"},
                { "code": "2505", "label": "Arvidsjaur"},
                { "code": "2506", "label": "Arjeplog"},
                { "code": "2510", "label": "Jokkmokk"},
                { "code": "2513", "label": "Överkalix"},
                { "code": "2514", "label": "Kalix"},
                { "code": "2518", "label": "Övertorneå"},
                { "code": "2521", "label": "Pajala"},
                { "code": "2523", "label": "Gällivare"},
                { "code": "2560", "label": "Älvsbyn"},
                { "code": "2580", "label": "Luleå"},
                { "code": "2581", "label": "Piteå"},
                { "code": "2582", "label": "Boden"},
                { "code": "2583", "label": "Haparanda"},
                { "code": "2584", "label": "Kiruna"}
            ],
            "codeLists": [
                {
                    "id": "vs_RegionKommun07",
                    "label": "Kommuner",
                    "type": "Valueset",
                    "links": [
                        {"rel": "metadata", "hreflang": "sv", "href": "https://my-site.com/api/v2/codelists/vs_RegionKommun07"}
                    ]
                },
                {
                    "id": "vs_RegionLän07",
                    "label": "Län",
                    "type": "Valueset",
                    "links": [
                        {"rel": "metadata", "hreflang": "sv", "href": "https://my-site.com/api/v2/codelists/vs_RegionLän07"}
                    ]
                },
                {
                    "id": "vs_RegionRiket99",
                    "label": "Riket",
                    "type": "Valueset",
                    "links": [
                        {"rel": "metadata", "hreflang": "sv", "href": "https://my-site.com/api/v2/codelists/vs_RegionRiket99"}
                    ]
                },
                {
                    "id": "agg_RegionA-region_2",
                    "label": "A-regioner",
                    "type": "Aggregation",
                    "links": [
                        {"rel": "metadata", "hreflang": "sv", "href": "https://my-site.com/api/v2/codelists/agg_RegionA-region_2"}
                    ]
                },
                {
                    "id": "agg_RegionKommungrupp2005-_1",
                    "label": "Kommungrupper (SKL:s) 2005",
                    "type": "Aggregation",
                    "links": [
                        {"rel": "metadata", "hreflang": "sv", "href": "https://my-site.com/api/v2/codelists/agg_RegionKommungrupp2005-_1"}
                    ]
                },
                {
                    "id": "agg_RegionKommungrupp2011-",
                    "label": "Kommungrupper (SKL:s) 2011",
                    "type": "Aggregation",
                    "links": [
                        {"rel": "metadata", "hreflang": "sv", "href": "https://my-site.com/api/v2/codelists/agg_RegionKommungrupp2011-"}
                    ]
                },
                {
                    "id": "agg_RegionKommungrupp2017-",
                    "label": "Kommungrupper (SKL:s) 2017",
                    "type": "Aggregation",
                    "links": [
                        {"rel": "metadata", "hreflang": "sv", "href": "https://my-site.com/api/v2/codelists/agg_RegionKommungrupp2017-"}
                    ]
                },
                {
                    "id": "agg_RegionLA1998",
                    "label": "Lokalaarbetsmarknader 1998",
                    "type": "Aggregation",
                    "links": [
                        {"rel": "metadata", "hreflang": "sv", "href": "https://my-site.com/api/v2/codelists/agg_RegionLA1998"}
                    ]
                },
                {
                    "id": "agg_RegionLA2003_1",
                    "label": "Lokalaarbetsmarknader 2003",
                    "type": "Aggregation",
                    "links": [
                        {"rel": "metadata", "hreflang": "sv", "href": "https://my-site.com/api/v2/codelists/agg_RegionLA2003_1"}
                    ]
                },
                {
                    "id": "agg_RegionLA2008",
                    "label": "Lokalaarbetsmarknader 2008",
                    "type": "Aggregation",
                    "links": [
                        {"rel": "metadata", "hreflang": "sv", "href": "https://my-site.com/api/v2/codelists/agg_RegionLA2008"}
                    ]
                },
                {
                    "id": "agg_RegionLA2013",
                    "label": "Lokalaarbetsmarknader 2013",
                    "type": "Aggregation",
                    "links": [
                        {"rel": "metadata", "hreflang": "sv", "href": "https://my-site.com/api/v2/codelists/agg_RegionLA2013"}
                    ]
                },
                {
                    "id": "agg_RegionLA2018",
                    "label": "Lokalaarbetsmarknader 2018",
                    "type": "Aggregation",
                    "links": [
                        {"rel": "metadata", "hreflang": "sv", "href": "https://my-site.com/api/v2/codelists/agg_Ålder5år"}
                    ]
                },
                {
                    "id": "agg_RegionStoromr-04_2",
                    "label": "Sorstadsområder -2004",
                    "type": "Aggregation",
                    "links": [
                        {"rel": "metadata", "hreflang": "sv", "href": "https://my-site.com/api/v2/codelists/agg_RegionStoromr-04_2"}
                    ]
                },
                {
                    "id": "agg_RegionStoromr05-_1",
                    "label": "Sorstadsområder 2005-",
                    "type": "Aggregation",
                    "links": [
                        {"rel": "metadata", "hreflang": "sv", "href": "https://my-site.com/api/v2/codelists/agg_RegionStoromr05-_1"}
                    ]
                },
                {
                    "id": "agg_RegionNUTS1_2008",
                    "label": "NUTS1 fr.o.m 2008",
                    "type": "Aggregation",
                    "links": [
                        {"rel": "metadata", "hreflang": "sv", "href": "https://my-site.com/api/v2/codelists/agg_RegionNUTS1_2008"}
                    ]
                },
                {
                    "id": "agg_RegionNUTS2_2008",
                    "label": "NUTS2 fr.o.m 2008",
                    "type": "Aggregation",
                    "links": [
                        {"rel": "metadata", "hreflang": "sv", "href": "https://my-site.com/api/v2/codelists/agg_RegionNUTS2_2008"}
                    ]
                },
                {
                    "id": "agg_RegionNUTS3_2008",
                    "label": "NUTS3 fr.o.m 2008",
                    "type": "Aggregation",
                    "links": [
                        {"rel": "metadata", "hreflang": "sv", "href": "https://my-site.com/api/v2/codelists/agg_RegionNUTS3_2008"}
                    ]
                }

            ]
 
        },
        {
            "id": "Civilstand",
            "label": "civilstånd",
            "type": "RegularVariable",
            "eliminination": true,
            "values": [
                {"code": "OG", "label": "ogifta" },
                {"code": "G", "label": "gifta" },
                {"code": "SK", "label": "skilda" },
                {"code": "ÄNKL", "label": "änkor/änklingar" }
            ]
        },
        {
            "id": "Alder",
            "label": "ålder",
            "type": "RegularVariable",
            "eliminination": true,
            "eliminationValueCode": "tot",
            "values": [
                {"code": "0", "label": "0 år" },
                {"code": "1", "label": "1 år" },
                {"code": "2", "label": "2 år" },
                {"code": "3", "label": "3 år" },
                {"code": "4", "label": "4 år" },
                {"code": "5", "label": "5 år" },
                {"code": "6", "label": "6 år" },
                {"code": "7", "label": "7 år" },
                {"code": "8", "label": "8 år" },
                {"code": "9", "label": "9 år" },
                {"code": "10", "label": "10 år" },
                {"code": "11", "label": "11 år" },
                {"code": "12", "label": "12 år" },
                {"code": "13", "label": "13 år" },
                {"code": "14", "label": "14 år" },
                {"code": "15", "label": "15 år" },
                {"code": "16", "label": "16 år" },
                {"code": "17", "label": "17 år" },
                {"code": "18", "label": "18 år" },
                {"code": "19", "label": "19 år" },
                {"code": "20", "label": "20 år" },
                {"code": "21", "label": "21 år" },
                {"code": "22", "label": "22 år" },
                {"code": "23", "label": "23 år" },
                {"code": "24", "label": "24 år" },
                {"code": "25", "label": "25 år" },
                {"code": "26", "label": "26 år" },
                {"code": "27", "label": "27 år" },
                {"code": "28", "label": "28 år" },
                {"code": "29", "label": "29 år" },
                {"code": "30", "label": "30 år" },
                {"code": "31", "label": "31 år" },
                {"code": "32", "label": "32 år" },
                {"code": "33", "label": "33 år" },
                {"code": "34", "label": "34 år" },
                {"code": "35", "label": "35 år" },
                {"code": "36", "label": "36 år" },
                {"code": "37", "label": "37 år" },
                {"code": "38", "label": "38 år" },
                {"code": "39", "label": "39 år" },
                {"code": "40", "label": "40 år" },
                {"code": "41", "label": "41 år" },
                {"code": "42", "label": "42 år" },
                {"code": "43", "label": "43 år" },
                {"code": "44", "label": "44 år" },
                {"code": "45", "label": "45 år" },
                {"code": "46", "label": "46 år" },
                {"code": "47", "label": "47 år" },
                {"code": "48", "label": "48 år" },
                {"code": "49", "label": "49 år" },
                {"code": "50", "label": "50 år" },
                {"code": "51", "label": "51 år" },
                {"code": "52", "label": "52 år" },
                {"code": "53", "label": "53 år" },
                {"code": "54", "label": "54 år" },
                {"code": "55", "label": "55 år" },
                {"code": "56", "label": "56 år" },
                {"code": "57", "label": "57 år" },
                {"code": "58", "label": "58 år" },
                {"code": "59", "label": "59 år" },
                {"code": "60", "label": "60 år" },
                {"code": "61", "label": "61 år" },
                {"code": "62", "label": "62 år" },
                {"code": "63", "label": "63 år" },
                {"code": "64", "label": "64 år" },
                {"code": "65", "label": "65 år" },
                {"code": "66", "label": "66 år" },
                {"code": "67", "label": "67 år" },
                {"code": "68", "label": "68 år" },
                {"code": "69", "label": "69 år" },
                {"code": "70", "label": "70 år" },
                {"code": "71", "label": "71 år" },
                {"code": "72", "label": "72 år" },
                {"code": "73", "label": "73 år" },
                {"code": "74", "label": "74 år" },
                {"code": "75", "label": "75 år" },
                {"code": "76", "label": "76 år" },
                {"code": "77", "label": "77 år" },
                {"code": "78", "label": "78 år" },
                {"code": "79", "label": "79 år" },
                {"code": "80", "label": "80 år" },
                {"code": "81", "label": "81 år" },
                {"code": "82", "label": "82 år" },
                {"code": "83", "label": "83 år" },
                {"code": "84", "label": "84 år" },
                {"code": "85", "label": "85 år" },
                {"code": "86", "label": "86 år" },
                {"code": "87", "label": "87 år" },
                {"code": "88", "label": "88 år" },
                {"code": "89", "label": "89 år" },
                {"code": "90", "label": "90 år" },
                {"code": "91", "label": "91 år" },
                {"code": "92", "label": "92 år" },
                {"code": "93", "label": "93 år" },
                {"code": "94", "label": "94 år" },
                {"code": "95", "label": "95 år" },
                {"code": "96", "label": "96 år" },
                {"code": "97", "label": "97 år" },
                {"code": "98", "label": "98 år" },
                {"code": "99", "label": "99 år" },
                {"code": "100+", "label": "100+ år" },
                {"code": "tot", "label": "totalt ålder" }
            ],
            "codeLists": [
                {
                    "id": "vs_Ålder1årA",
                    "label": "Ålder, 1 års-klasser",
                    "type": "Valueset",
                    "links": [
                        {"rel": "metadata", "hreflang": "sv", "href": "https://my-site.com/api/v2/codelists/vs_Ålder1årA"}
                    ]
                },
                {
                    "id": "vs_ÅlderTotA",
                    "label": "Ålder, totalt, alla redovisade åldrar",
                    "type": "Valueset",
                    "links": [
                        {"rel": "metadata", "hreflang": "sv", "href": "https://my-site.com/api/v2/codelists/vs_ÅlderTotA"}
                    ]
                },
                {
                    "id": "agg_Ålder10år",
                    "label": "10-årsklasser",
                    "type": "Aggregation",
                    "links": [
                        {"rel": "metadata", "hreflang": "sv", "href": "https://my-site.com/api/v2/codelists/vs_Ålder10år"}
                    ]
                },
                {
                    "id": "agg_Ålder5år",
                    "label": "5-årsklasser",
                    "type": "Aggregation",
                    "links": [
                        {"rel": "metadata", "hreflang": "sv", "href": "https://my-site.com/api/v2/codelists/vs_Ålder5år"}
                    ]
                }
            ]
        },
        {
            "id": "Kon",
            "label": "kön",
            "type": "RegularVariable",
            "eliminination": true,
            "values":[
                {"code": "1", "label": "män" },
                {"code": "2", "label": "kvinnor" }
            ]
           
        },
        {
            "id": "ContentsCode",
            "label": "tabellinnehåll",
            "type": "ContentsVariable",
            "values": [
                {"code": "BE0101N1", "label": "Folkmängd", "measuringAttribute": "Stock", "dayAdjusted": false, "seasonallyAdjusted": false, "unit": "antal", "refrencePeriod": "31 december repektive år", "PreferedNumberOfDecimals": 0, "notes": [{"mandantory": true, "text": "Uppgifterna avser förhållandena den 31 december för valt/valda år enligt den regionala indelning som gäller den 1 januari året efter."}] },
                {"code": "BE0101N2", "label": "Folkökning", "measuringAttribute": "Flow", "dayAdjusted": false, "seasonallyAdjusted": false, "unit": "antal", "PreferedNumberOfDecimals": 0, "note": [{"mandantory": false, "text": "Folkökningen definieras som skillnaden mellan folkmängden vid årets början och årets slut."}] }
            ]
        },
        {
            "id": "Tid",
            "label": "år",
            "type": "TimeVariable",
            "timeUnit": "Annual",
            "firstPeriod": "1968",
            "lastPeriod": "2021",
            "values": [
                { "code": "1968", "label": "1968" },
                { "code": "1969", "label": "1969" },
                { "code": "1970", "label": "1970" },
                { "code": "1971", "label": "1971" },
                { "code": "1972", "label": "1972" },
                { "code": "1973", "label": "1973" },
                { "code": "1974", "label": "1974" },
                { "code": "1975", "label": "1975" },
                { "code": "1976", "label": "1976" },
                { "code": "1977", "label": "1977" },
                { "code": "1978", "label": "1978" },
                { "code": "1979", "label": "1979" },
                { "code": "1980", "label": "1980" },
                { "code": "1981", "label": "1981" },
                { "code": "1982", "label": "1982" },
                { "code": "1983", "label": "1983" },
                { "code": "1984", "label": "1984" },
                { "code": "1985", "label": "1985" },
                { "code": "1986", "label": "1986" },
                { "code": "1987", "label": "1987" },
                { "code": "1988", "label": "1988" },
                { "code": "1989", "label": "1989" },
                { "code": "1990", "label": "1990" },
                { "code": "1991", "label": "1991" },
                { "code": "1992", "label": "1992" },
                { "code": "1993", "label": "1993" },
                { "code": "1994", "label": "1994" },
                { "code": "1995", "label": "1995" },
                { "code": "1996", "label": "1996" },
                { "code": "1997", "label": "1997" },
                { "code": "1998", "label": "1998" },
                { "code": "1999", "label": "1999" },
                { "code": "2000", "label": "2000" },
                { "code": "2001", "label": "2001" },
                { "code": "2002", "label": "2002" },
                { "code": "2003", "label": "2003" },
                { "code": "2004", "label": "2004" },
                { "code": "2005", "label": "2005" },
                { "code": "2006", "label": "2006" },
                { "code": "2007", "label": "2007" },
                { "code": "2008", "label": "2008" },
                { "code": "2009", "label": "2009" },
                { "code": "2010", "label": "2010" },
                { "code": "2011", "label": "2011" },
                { "code": "2012", "label": "2012" },
                { "code": "2013", "label": "2013" },
                { "code": "2014", "label": "2014" },
                { "code": "2015", "label": "2015" },
                { "code": "2016", "label": "2016" },
                { "code": "2017", "label": "2017" },
                { "code": "2018", "label": "2018" },
                { "code": "2019", "label": "2019" },
                { "code": "2020", "label": "2020" },
                { "code": "2021", "label": "2021" }
            ]
        }
    ],
    "contacts": [
        {"name": "Tomas Johansson", "mail":"tomas.johansson@scb.se", "phone": "+46 010-479 64 26" },
        {"name": "(SCB) Statistikservice", "mail":"information@scb.se", "phone": "+46 010-479 50 00" }
    ],
    "notes": [
        {"text": "Fr o m 2007-01-01 överförs Heby kommun från Västmanlands län till Uppsala län. Hebys kommunkod ändras från 1917 till 0331. ", "mandatory": true},
        {"text": "Registrerat partnerskap reglerade parförhållanden mellan personer av samma kön och fanns från 1995 till 2009. Registrerade partners räknas som Gifta, Separerade partners som Skilda och Efterlevande partners som Änka/änklingar.", "mandatory": true},
        {"text": "Fr o m 2007-01-01 utökas Uppsala län med Heby kommun. Observera att länssiffrorna inte är jämförbara med länssiffrorna bakåt i tiden.", "mandatory": true,
        "conditions" : [
            {"variableCode": "ContentsCode", "valueCode": "BE0101N1"},
            {"variableCode": "Region", "valueCode": "03"}
        ]},
        {"text": "Fr o m 2007-01-01 minskar Västmanlands län med Heby kommun. Observera att länssiffrorna inte är jämförbara med länssiffrorna bakåt i tiden.", "mandatory": true,
        "conditions" : [
            {"variableCode": "ContentsCode", "valueCode": "BE0101N1"},
            {"variableCode": "Region", "valueCode": "19"}
        ]}

    ],
    "links" : [
        {"rel": "self", "hreflang": "sv", "href": "https://my-site.com/api/v2/tables/TAB638/metadata"},
        {"rel": "self", "hreflang": "en", "href": "https://my-site.com/api/v2/tables/TAB638/metadata?lang=en"},
        {"rel": "data", "hreflang": "sv", "href": "https://my-site.com/api/v2/tables/TAB638/data"},
        {"rel": "data", "hreflang": "en", "href": "https://my-site.com/api/v2/tables/TAB638/data?lang=en"},
        {"rel": "describeby", "hreflang": "sv", "href": "https://my-other-site.com/sv/definitions/TAB638"},
        {"rel": "describeby", "hreflang": "en", "href": "https://my-other-site.com/en/definitions/TAB638"}
    ]
 
}
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
{
    "id": "agg_RegionNUTS2_2008",
    "label": "NUTS2 fr.o.m 2008",
    "language": "sv",
    "type": "Aggregation",
    "values": [
        {"code": "SE11", "value": "Stockholm", "valueMap": ["01"]},
        {"code": "SE12", "value": "Östra Mellansverige", "valueMap": ["03", "04", "05", "18", "19"]},
        {"code": "SE21", "value": "Småland med öarna", "valueMap": ["06", "07", "08", "09"]},
        {"code": "SE22", "value": "Sydsverige", "valueMap": ["10", "12"]},
        {"code": "SE23", "value": "Västsverige", "valueMap": ["13", "14"]},
        {"code": "SE31", "value": "Norra Mellansverige", "valueMap": ["17", "20", "21"]},
        {"code": "SE32", "value": "Mellersta Norrland", "valueMap": ["22", "23"]},
        {"code": "SE33", "value": "Övre Norrland", "valueMap": ["24", "25"]}
    ],
    "links": [
        {
            "rel": "self",
            "hreflang": "sv",
            "href": "https://my-site.com/api/v2/codelist/agg_RegionNUTS2_2008"
        }        
    ]
}
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

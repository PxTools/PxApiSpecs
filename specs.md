# PxApi 2.0

# Readers guide
## What this instruction covers
This document specifies the  queries and response formats of 2.0 version of the PX web API.

## Terminology
- *Paxiom* the object model representing a statistical cube
- *table* a multidimensional table containing statistical measures and metadata about these measures stored in a PX-File or a database using the CNMM.
- *CNMM* the Common Nordic Meta Model which is the database structure that is used and maintained by Statistics Sweden, Norway and Denmark.

References
[https://www.dst.dk/en/Statistik/statistikbanken/api](https://www.dst.dk/en/Statistik/statistikbanken/api)
[http://api.statbank.dk/console#data](http://api.statbank.dk/console#data)
[https://www.vinaysahni.com/best-practices-for-a-pragmatic-restful-api](https://www.vinaysahni.com/best-practices-for-a-pragmatic-restful-api)

#Information model for a Table

## Mapping
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
-	Client should accept new properties in the responses without breaking.

# API endpoints
All the endpoints are able to handle GET and POST HTTP request. GET is the preferred method in most cases. They result in the same response which is not very RESTful instead it is a pragmatically solution. All the examples bellow uses the GET method if you wish to do a POST request you will do it to the same url but instead of specifying query string parameters you could specify the same parameters as a JSON object having the same name as the parameter and sending that JSON object as the content of the POST request.

**Example**
```json
{
  lang: "en",
  Tid: [">1968","<1977"]
}
```
POST is primarily intended to be used when fetching data since the query to select the data can in some cases become very large. So large that it can extends come web browser limits on url’s .

Proposal: Look at Json-Stat collections 

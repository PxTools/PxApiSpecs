# PxApiSpecs
Documentation for the PxApi 

```
specs.md        Markdown version of spesification
PxApi-2.0.json  Early json schema
PxAPI-2.yml     OpenAPI 3.0.x spesification
examples/       Folder with json responses
```

## OpenAPI and JSON-stat
https://json-stat.org/format/ list the different JSON-stat schemas in `draft-04` dialect. There has been a few differences between json schema syntax and OpenAPI schema syntax. In OAS 3.1 however there is compability, but tooling for 3.1 at the moment is quite poor https://openapi.tools/  

The solution was to use av converter [JSON Schema to OpenAPI Schema](https://www.npmjs.com/package/@openapi-contrib/json-schema-to-openapi-schema)

```
wget https://json-stat.org/format/schema/2.0/dataset.json
npm install --save @openapi-contrib/json-schema-to-openapi-schema
npx json-schema-to-openapi-schema convert dataset.json 
```
After converting back to YAML and some other manual fixing it can be included in the OpenAPI file.

# Generate server code from 
Run the following command from a PowerShell prompt

```PowerShell
docker run --rm -v ${PWD}:/local openapitools/openapi-generator-cli generate -i /local/PxAPI-2.yml -g aspnetcore -o /local/out/test/pxapi2 --additional-properties='aspnetCoreVersion=6.0' --additional-properties=nullableReferenceTypes=true --additional-properties=buildTarget=library
```

Replace `${PWD}` with `$(pwd)` when running from bash.

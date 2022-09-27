# PxApiSpecs
Documentation for the PxApi 


# Generate server code from 
Run the following command from a PowerShell prompt

```PowerShell
docker run --rm -v ${PWD}:/local openapitools/openapi-generator-cli generate -i /local/PxAPI-2.yml -g aspnetcore -o /local/out/test/pxapi2 --additional-properties='aspnetCoreVersion=6.0' --additional-properties=nullableReferenceTypes=true --additional-properties=buildTarget=library
```
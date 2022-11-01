@echo off 
rem swagger uses -l aspnetcore oaTools uses -g aspnetcore

set gen_swag=swaggerapi/swagger-codegen-cli-v3:3.0.35
set gen_oaTool=openapitools/openapi-generator-cli
set props1=--additional-properties=aspnetCoreVersion=6.0 --additional-properties=nullableReferenceTypes=true 
set props2=--additional-properties=buildTarget=library --additional-properties=packageName=PCAxis.OpenAPILib 
set props3=--additional-properties=useSeperateModelProject=true
set props4=--additional-properties=legacyDiscriminatorBehavior=false
set props5=--additional-properties=swashbuckleVersion=6.4.0 --additional-properties=useSwashbuckle=true

set myProps=%props1% %props2% %props3% %props4% %props5%

set yaml_doc=/local/spec.yaml
rem set yaml_doc=https://raw.githubusercontent.com/statisticssweden/PxApiSpecs/feature/tablemetadata/PxAPI-2.yml



echo hei

rem docker run --rm -v %cd%:/local %gen_oaTool% help > openapi_help.txt
rem docker run --rm -v %cd%:/local %gen_oaTool% help generate > openapi_help_generate.txt
rem docker run --rm -v %cd%:/local %gen_oaTool% config-help -g aspnetcore > openapi_config_help.txt

@echo on
copy ..\PxAPI-2.yml .\spec.yaml

rem docker run --rm -v %cd%:/local %gen_oaTool% generate -i %yaml_doc% -g aspnetcore -o /local/out %myProps%  > log.txt
docker run --rm -v %cd%:/local %gen_oaTool% generate -i %yaml_doc% -g aspnetcore -o /local/out -c /local/aspnetcore-generator-config.yml > log.txt

@echo off 

rem docker run --rm -v %cd%:/local %gen_swag% generate -i /local/spec.yaml -l aspnetcore -o /local/out/fra_spec1 --additional-properties=aspnetCoreVersion=6.0 --additional-properties=alalalala=6.0 --additional-properties=nullableReferenceTypes=true  > fra_spec1.txt
rem docker run --rm -v %cd%:/local %gen_swag% config-help -l aspnetcore  > helpSwag.txt
rem docker run --rm -v %cd%:/local %gen_oaTool% help
rem docker run --rm -v %cd%:/local %gen_oaTool% help batch
rem docker run --rm -v %cd%:/local %gen_oaTool% generate -i /local/spec.yaml -g aspnetcore -o /local/out/fra_spec1 --additional-properties='aspnetCoreVersion=6.0' --additional-properties=nullableReferenceTypes=true --additional-properties=buildTarget=library  > fra_spec1.txt

rem docker run --rm -v C:\dev\code\slask\openapi:/local openapitools/openapi-generator-cli generate -i  -g aspnetcore -o /local/out/pxapi2 --additional-properties='aspnetCoreVersion=6.0' --additional-properties=nullableReferenceTypes=true --additional-properties=buildTarget=library
@echo off 

docker run --rm -v %cd%:/local openapitools/openapi-generator-cli generate -i /local/PxAPI-2.yml -g aspnetcore -o /local/generator/out/ -c /local/aspnetcore-generator-config.yml

@echo on 


# PxApiSpecs

https://petstore.swagger.io/?url=https://raw.githubusercontent.com/PxTools/PxApiSpecs/master/PxAPI-2.yml

Documentation for the PxApi

# Generate server code from

Run the following command from a PowerShell prompt

```PowerShell
docker run --rm -v ${PWD}:/local openapitools/openapi-generator-cli generate -i /local/PxAPI-2.yml -g aspnetcore -o /local/out/test/pxapi2 --additional-properties='aspnetCoreVersion=6.0' --additional-properties=nullableReferenceTypes=true --additional-properties=buildTarget=library
```

Replace `${PWD}` with `$(pwd)` when running from bash.

Add `-c /local/aspnetcore-generator-config.yml` if you want the same additional-properties as the nuget action uses

`docker pull openapitools/openapi-generator-cli:v6.5.0`

# Updating PxApi-2.yml

When creating a PR for updatedating [PxAPI-2.yml](PxAPI-2.yml)
please use the following command to also include changes in generated server code.

```sh
docker run --rm -v ${PWD}:/local openapitools/openapi-generator-cli:v6.5.0  generate -i /local/PxAPI-2.yml -g aspnetcore -c /local/aspnetcore-generator-config.yml -o /local
```

# PxWeb.Api2.Server - ASP.NET Core 6.0 Server

This api lets you do 2 things; Find a table(Navigation) and use a table (Table).

_Table below is added to show how tables can be described in yml._

**Table contains status code this API may return**
| Status code | Description | Reason |
| ----------- | --------------------- | -------------------- |
| 200 | Success | The endpoint has delivered response for the request |
| 400 | Bad request | If the request is not valid |
| 403 | Forbidden | number of cells exceed the API limit |
| 404 | Not found | If the URL in request does not exist |
| 429 | Too many request | Requests exceed the API time limit. Large queries should be run in sequence |
| 50X | Internal Server Error | The service might be down |

## Upgrade NuGet Packages

NuGet packages get frequently updated.

To upgrade this solution to the latest version of all NuGet packages, use the dotnet-outdated tool.

Install dotnet-outdated tool:

```
dotnet tool install --global dotnet-outdated-tool
```

Upgrade only to new minor versions of packages

```
dotnet outdated --upgrade --version-lock Major
```

Upgrade to all new versions of packages (more likely to include breaking API changes)

```
dotnet outdated --upgrade
```

## Run

Linux/OS X:

```
sh build.sh
```

Windows:

```
build.bat
```

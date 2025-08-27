# PxWeb API 2.0 Specification
This is the home of the PxWeb API 2.0 OpenAPI Specification. You can look at the current state of the specification in [Swagger UI](https://petstore.swagger.io/?url=https://raw.githubusercontent.com/PxTools/PxApiSpecs/master/PxAPI-2.yml).


## Updating PxApi-2.yml

When creating a PR for updatedating [PxAPI-2.yml](PxAPI-2.yml)
please use the following command to also include changes in generated server code.

ASP.NET Core 8.0 Server

```sh
docker run --rm -v ${PWD}:/local openapitools/openapi-generator-cli:v7.11.0  generate -i /local/PxAPI-2.yml -g aspnetcore -c /local/aspnetcore-generator-config.yml -o /local
```

TypeScript/JavaScript client that utilizes [Fetch API](https://fetch.spec.whatwg.org/)

```sh
docker run --rm -v ${PWD}:/local openapitools/openapi-generator-cli:v7.11.0  generate -i /local/PxAPI-2.yml -g typescript-fetch -c /local/typescript-fetch-generator-config.yml -o /local/typescript-fetch
```

Replace `${PWD}` with `$(pwd)` when running on Linux

### Upgrade NuGet Packages

NuGet packages get frequently updated.

To upgrade this solution to the latest version of all NuGet packages, use the dotnet-outdated tool.

Install dotnet-outdated tool:

```sh
dotnet tool install --global dotnet-outdated-tool
```

Upgrade only to new minor versions of packages

```sh
dotnet outdated --upgrade --version-lock Major
```

Upgrade to all new versions of packages (more likely to include breaking API changes)

```sh
dotnet outdated --upgrade
```

### Run

Linux/OS X:

```sh
sh build.sh
```

Windows:

```sh
build.bat
```

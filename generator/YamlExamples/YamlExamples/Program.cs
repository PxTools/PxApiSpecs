// See https://aka.ms/new-console-template for more information
using yamlGen;

Console.WriteLine("Hello, World!");

bool dryRun = true;

//basepath should be the repo-root folder (where the spec-file is).
DoIt doingIt = new DoIt(basePath: @"..\..\..\..\..\..\", dryRun);


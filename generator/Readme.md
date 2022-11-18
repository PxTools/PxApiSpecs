Steps in changing openapi yaml 

```mermaid      
 graph TD;
      A0[Make feature branch]-->A
      A[Change and Commit spec-file: Paths and schema]-->B[run openapi-generator via myGen.bat];
      B-->C5[Open YamlExamples/YamlExamples.sln solution.<br>Rebuild after you chech path and dryRun parameter in Program.cs.];
      
      C5-->C10[Open YamlExamples/YamlExamples.sln solution.<br>Rebuild after you chech path and dryRun parameter in Program.cs.];
    
      C10-->C15[Add data if needed. If you add a new example, a change is needed in the spec-file];
      C15-->D[Run:<br>Updates external example files and<br> and the examples in spec-file or spec-file+'2' if dryRun];

      D-->D20[Look at diffs and be happy before commit push ];
      D20-->F[Merge back to master, create nuget];
      
```

(When adding a new example, remember to change the spec-file both in the paths and examples section)


The files in the examplesAsJson folder are not needed by the system, and adds no info to the end user, but coping 
from the examples in swagger gui is somewhat tricky so these files migth be usefull for that.
(The files in the examples folder will become obsolete) 

The instruction to use myGen.bat to generate is just to make sure the output code ends up where YamlExamples.sln expect it. 


 

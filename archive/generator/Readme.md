Steps in changing openapi yaml 

```mermaid      
 graph TD;
      A0[Make feature branch]-->A
      A[Change spec-file]-->B[run openapi-generator via myGen.bat];
      B-->C15[Open YamlExamples/YamlExamples.sln solution.<br>If path causes problems it is set in Program.cs.<br>Add data if needed. If you add a new example, a change is needed in the spec-file];
    
      
      C15-->D[Run:<br>Updates external example files in folders <br> examplesAsJson and examplesAsYml];

      D-->D20[Look at diffs and be happy before commit push ];
      D20-->F[Merge back to master, create nuget];
      
```

The files in the examplesAsJson folder are not needed by the system, and adds no info to the end user, but coping 
from the examples in swagger gui is somewhat tricky so these files migth be usefull for that.
(The files in the examples folder will become obsolete) 

The instruction to use myGen.bat to generate is just to make sure the output code ends up where YamlExamples.sln expect it. 


 

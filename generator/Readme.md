Steps in changing openapi yaml 

```mermaid      
 graph TD;
      A0[Make feature branch]-->A
      A[Change in spec-yaml-file: Paths and schema]-->B[Generate code aka modell];
      B-->C[Change code that adds data to the model];
      C-->D[Run:<br>Updates external example files <br> and the examples in spec-yaml-file];
     
      D-->D20[Look at diffs before commit push ];
      D20-->F[Merge back to master, create nuget];

      
```

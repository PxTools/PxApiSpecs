Steps in changing openapi yaml 

The yaml files has external examples which are files on master. These urls should not be changed, so if they cannot be relative, they should always show master. This means the swagger gui form a feature branch will have "old" examples. 
```mermaid      
 graph TD;
      A0[make feature branch]-->A
      A[Change in yaml-file: Paths and schema]-->B[generate code aka modell];
      B-->C[Change code that adds data to the model];
      C-->D[Run:<br> Updates external example files <br> and 'inSpec' examples];
     
      D-->D20[Look at diffs before commit push ];
      D20-->F[ merge back to master, create nuget];

      
```

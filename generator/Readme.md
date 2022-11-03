Steps in changing openapi yaml 

The yaml files has external examples which are files on master. These urls should not be changed, so if they cannot be relative, they should always show master. This means the swagger gui form a feature branch will have "old" examples. 
```mermaid      
  graph TD;
      A0[make feature branch]-->A
      A[Change in yaml-file: Paths and schema]-->B[generate code];
      B-->C[add data to model manually];
      C-->D[update external examples];
      D-->D1[correct and run tests];
      D-->E[push, make beta-nuget];
      E-->F[ merge back to master, create nuget];
```

## How to merge multiple menutrees?
There are 2 usecases:

## Use case 1  
In the "new" pxweb the database is removed and the idea is to have multiple instances of pxweb instead.
In the "old" pxweb menu navigation has two screens, first select database, then select table.
The databases may be viewed as nodes on root in a bigger menu.
This may be solved by an api that merges the content of two or more instances, so that the old databases are now nodes on root in the new menu.

## Use case 2
Statistics Norway have been asked to make a portal for official statistics, where, for instance, statistics on unemployment from both the NSI and 
the unemployment office should be presented together.
In this case, we will probably need some sort of master. 

The procedure for merging the 2 usecases will probably be different.  

A Folder in the menutree has a list of subfolders, a list of table and perhaps some headings.
The id of a folder or a table is unique in the instance, but the same folder(id) may be present in two instances.   

```mermaid
flowchart TB
  subgraph DB2["Menutree in instance 2"]
    direction TB
      subgraph DB2_A01[A]
         subgraph DB2_A0101[C]
           DB2_tab01[/tab../]:::tab
           DB2_tab02[/tab../]:::tab
         end
      end
   end

   subgraph DB1["Menutree in instance 1"]
    direction TB
     subgraph DB1_A[A]
       subgraph DB1_A01[B]
         subgraph DB1_A0101[C]
           DB1_tab01[/tab../]:::tab
           DB1_tab02[/tab../]:::tab
         end
       end
     end
   end

 
   
classDef tab fill:lightgreen,stroke:#fff;
classDef folder fill:#ffffde,stroke:#aaaa33;

```
What should the result of merging these be?


```mermaid
flowchart TB
   subgraph DB1["For usecase 2, with instance 1 as master"]
    direction TB
     subgraph DB1_A[A from master]
       subgraph DB1_A01[B from master]
         subgraph DB1_A0101[C from master]
           DB1_tab01[/tab from instance 1../]:::tab
           DB1_tab02[/tab from instance 2../]:::tab
           DB1_tab03[/tab from instance 1../]:::tab
           DB1_tab04[/tab from instance 2../]:::tab
         end
       end
     end
   end

  subgraph DB2["For usecase 1"]
    direction LR
    subgraph DB3_A[A from instance 1]
       subgraph DB3_A01[B from instance 1]
         subgraph DB3_A0101[C from instance 1]
           DB3_tab01[/tab from instance 1../]:::tab
           DB3_tab02[/tab from instance 1../]:::tab
         end
       end
     end
    subgraph DB4_A01[A from instance 2]
         subgraph DB4_A0101[C from instance 2]
           DB4_tab01[/tab from instance 2../]:::tab
           DB4_tab02[/tab from instance 2../]:::tab
         end
      end 
     
   end 
   
classDef tab fill:lightgreen,stroke:#fff;
classDef folder fill:#ffffde,stroke:#aaaa33;

```
For usecase 1 the id-string in ..Portal_instance/navigate/{id} calls will probably have to contain both the instanceId and the folderId.
The config endpoint should probably have a list of instances and the "navigational" endpoints should have an "exclude these instances" parameter.
The "navigational" endpoints will probably have to manipulate some of the returned urls.  
 

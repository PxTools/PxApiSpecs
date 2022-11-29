## Who/How will we use the generated code.
Is the model usefull outside the api?  (A copy of the model was used to upgraded the "old" jsonstat2 serializer, but this is not "live". It is (soon 🙂 ) used to generate examples, but this happens as part of the spec-editing. So they don't count)
The GUI will be a client, but its language is yet to be decided. Will the same model be created for the client?  The proxy-api(aka portal.api) is both client and server. 

Will we create clients for Python or R?  

## Partial files for the model
Why do we need partial files: Classes with ObjectType like the subclasses of FolderContentItem (heading, table ,, )
should have a constructor setting Objecttype correctly so that instances of the Heading class always has objectType heading.
We also "need" util methods for adding a contact , making sure updated is UTC and so on. 
If these things are included in the nugets, many traps are avoided.
We will try to use extensions methods and Factory-classes instead of partial classes. That way we can go ahead and generate the code as a class library.

### Issues
How to store and maintain:
- Could they be (commited/stored) in /Partial_files and be copied (by myGen.bat and Gitaction) just after generation? This would mean they are created and edited (Which I asume 
will be easiest done in the generated snl) a different place than are are stored. 
- Generation with "buildTarget: library" option, does not make the generated modelclasses partial (in oct. 2022), and generation without ( which adds "partial" to the model classes) 
  changes the other classes. Is that OK/ worth it?  



##  Missing PX Keywords

### These we do need
discontinued  boolean on table

#### These are suggentions for future debate
aggregAllowed on each content, or did I get that wrong?

## Partial files for the model
Why do we need partial files: Classes with ObjectType like the subclasses of FolderContentItem (heading, table ,, )
should have a constructor setting Objecttype correctly so that instances of the Heading class always has objectType heading.
We also "need" util methods for adding a contact , making sure updated is UTC and so on. 
If these things are included in the nugets, many traps are avoided.   

### Issues
How to store and maintain:
- Could they be (commited/stored) in /Partial_files and be copied (by myGen.bat and Gitaction) just after generation? This would mean they are created and edited (Which I asume 
will be easiest done in the generated snl) a different place than are are stored. 
- Generation with "buildTarget: library" option, does not make the generated modelclasses partial, and generation without ( which adds "partial" to the model classes) 
  changes the other classes. Is that OK/ worth it?  



##  Missing PX Keywords

### These we do need
discontinued  boolean on table

#### These are suggentions for future debate
aggregAllowed on each content, or did I get that wrong?

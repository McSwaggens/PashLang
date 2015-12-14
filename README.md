# What is PashLang?
'Pash' or 'PashLang' is the projects name, and contains all the fundamental languages and libraries.

Pash is currently split into 4 projects

# PASM (Pash Assembly)
This is the core language of the project, and is the only language that is interpreted.
Each line is ment to do one (small) job at a time, and the result is each line is super fast and tasks span accross long amounts of code.
##### Why? 
The original goal behind PASM was to have many processes running on 1 CPU thread, and if we had one line of code taking more time than another line of code, it would make 1 process take more time than another.
So the goal is to make every line of PASM code the exact same amount of time.
##### How do I run a PASM script?
PASM (Opposed to other scripting languages) is very easy setup and run
###### Import PASM
```
using PASM;
```
###### Create an Engine
```
Engine engine = new engine();
```
###### Import a Standard library
Note that the Standard library is a seperate class and the methods inside of it are ```public static```
```
engine.ReferenceLibrary(typeof(Standard));
```
###### Load the code
```PASMCode``` is a string array
```
engine.Load(PASMCode);
```

###### Execute the code
```
engine.Execute();
```


# Croc Script
'Croc Script' or 'Crocodile Script' is a C/# like language, and compiles into PASM code
The end goal of Croc Script is not to be the fastest to compile by any means, but to give a feature rich programming language that an actual programmer can write code in (Opposed to PASM which is very hard to learn)
###### What does the code look like?
Sorta like C# and C/++ had a baby.
###### What kind of name is that?
Look at the source code, and find out.

# Pash Runtime
This is a terminal program to run PASM code only.

# Croc Runtime
This is also a terminal program, but will compile the Croc Code and run the PASM code straight after.



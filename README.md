<a href="http://www.github.com/McSwaggens/PashLang/tree/master/PashLang/"><img src="http://www.pashlang.com/PashLang_Icon_P_White.png" Width="128" Height="128"></a>
<a href="http://www.github.com/McSwaggens/PashLang/tree/master/PashLang/PASM"><img src="http://www.pashlang.com/PashLang_Icon_ASM_White.png" Width="128" Height="128"></a>
<a href="http://www.github.com/McSwaggens/PashLang/tree/master/PashLang/CrocodileScript/"><img src="http://www.pashlang.com/PashLang_Icon_Snap_White.png" Width="128" Height="128"></a>
<a href="http://www.github.com/McSwaggens/PashLang/wiki"><img src="http://www.pashlang.com/PashLang_Icon_Wiki_White.png" Width="128" Height="128"></a>
<a href="http://www.github.com/McSwaggens/PashLang/tree/master/PashLang/PashIDE"><img src="http://www.pashlang.com/FireBird_ICON_BLACK.png" Width="128" Height="128"></a>
<a href="http://www.pashlang.com"><img src="http://www.pashlang.com/Heart_ICON_BLACK.png" Width="128" Height="128"></a>


# What is PashLang?
PashLang is a set of tools allowing you to execute interpreted code.

###Performance
In a test done recently comparing Jint and PASM with essentually the same code, JavaScript completed the test in 73ms, PASM on the otherhand finished in around 7ms. (10 times the speed) 

Performance was always the goal of this language and always will be.

Pash is currently split into 4 projects

# PASM (Pash Assembly)
This is the core language of the project, and is the only language that is interpreted.
Each line is ment to do one (small) job at a time, and the result is each line is super fast and tasks span accross long amounts of code.
##### Why? 
The original goal behind PASM was to have many processes running on 1 CPU thread, and if we had one line of code taking more time than another line of code, it would make 1 process take more time than another.
So the goal is to make every line of PASM code the exact same amount of time.

##### Why use PASM opposed to another language like JavaScript or LUA?
###### You're in full control.
PASM is designed from the ground up ready for low, medium and hight trust environments.
Whether you want to limit the amount of RAM it can use, do threading on low specs or use your own standard library, everything is up to you.


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



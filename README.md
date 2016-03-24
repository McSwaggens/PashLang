<a href="http://www.github.com/McSwaggens/PashLang/tree/master/PashLang/"><img src="http://www.pashlang.com/PashLang_Icon_P_White.png" Width="128" Height="128"></a>
<a href="http://www.github.com/McSwaggens/PashLang/tree/master/PashLang/PASM"><img src="http://www.pashlang.com/PashLang_Icon_ASM_White.png" Width="128" Height="128"></a>
<a href="http://www.github.com/McSwaggens/PashLang/tree/master/PashLang/CrocodileScript/"><img src="http://www.pashlang.com/PashLang_Icon_Snap_White.png" Width="128" Height="128"></a>
<a href="http://www.github.com/McSwaggens/PashLang/wiki"><img src="http://www.pashlang.com/PashLang_Icon_Wiki_White.png" Width="128" Height="128"></a>
<a href="http://www.github.com/McSwaggens/PashLang/tree/master/PashLang/PashIDE"><img src="http://www.pashlang.com/FireBird_ICON_BLACK.png" Width="128" Height="128"></a>
<a href="http://www.pashlang.com"><img src="http://www.pashlang.com/Heart_ICON_BLACK.png" Width="128" Height="128"></a>

###What is the PashLang project?
Pashlang is a combination of projects like an IDE, Compiler and Runtimes revolving around 1 interpreted language called PASM (Pash Assembly), which is the core language of this project.

###What is PASM?
Put simply, PASM is a lightning fast, extendable and configurable interpreted programming language.
The language design is similar to regular Assembly in the sense that every line is short and to the point, meaning every line does one small action at a time.

###Why not use another language like JavaScript or LUA?
#####You're in full control.
- The PASM engine allows you to change how much memory (RAM) it's allowed to use.
- All though a Standard library is provided called ```stdlib```, by default there is no library referenced to the engine, meaning the PASM code is suitable for low, medium and high trust environments.
- Libraries are easy to create, all you have to do is create a normal C# class, reference it and start writing methods!

#####You probably don't want to be writing assembly all day, so we made a compiler: Puffin


###What is Puffin?
Puffin is a programming language that compiles into PASM.
The language design looks like a combination between C, C# and Java.

####Unique features
*__dataset__* - Allows you to allocate data and assign variables inside of it.
```C++
//Initialize the dataset variable
dataset data = { int myInt = 123, char[100] charArray, long myLong = 1337, bool myBool = false }
//Access the variables inside of the dataset
print(data->myInt);
print(charArray);
print(data->myLong);
print(data->myBool);
```
The advantage of a dataset is having a single block of data that can be passed around.

*__new__* - Keyword that can be used to copy data.
```C++
int a = 123;
int b = 0;
//Copy a to b
b = new a;
//Change a
a = 0
//A should now be 0 and b should now be 123 
print (b);
```

*__\__pasm__* - Allows you to write native PASM code
```C++
int i = 0;
__pasm {
  set i INT32 123
}
print(i);
```

*__omitable__* - Allows for the creation of new datatypes without wrappers (classes)
```C++
omitable string char[] {
  //This will be finished... Soon.... TM
}
```


###Does PashLang have an IDE?
Sure does!

<img src="http://www.pashlang.com/idescreenshot.png">

# CodeBlockBuilder
Inline code formatter or something ¯\_(ツ)_/¯

# Why
I've been developing an extension for Visual Studio that can insert code snippets with given declaration, so I needed to get rid of this:
![image_2022-08-30_08-35-02](https://user-images.githubusercontent.com/20230176/187426674-797a7439-2344-485a-9b35-bd54fae03704.png)
so i decided to quickly make this thing.
# Usage
```C#
CodeTextBuilder b = new CodeTextBuilder() 
{ 
    "//Comment",
    new CodeTextBlock("if(true)")
    {
       "return 0;"
    }
};
string result = b.Build(0);
```
Output:

![VsDebugConsole_JBtMp6yVaN](https://user-images.githubusercontent.com/20230176/187428782-3a5d7664-5595-4411-993d-8d89a1a61ed7.png)

String view:
```C#
"//Comment\nif(true)\n{\n\treturn 0;\n}"
```
To create a line you can use strings or __CodeTextLine__ with custom indent value:
```C#
CodeTextBuilder b = new CodeTextBuilder() 
{ 
    new CodeTextLine("This is a line with indent of 2", 2),
    "//Comment",
    new CodeTextBlock("if(true)")
    {
       "return 0;"
    }
};
string a = b.Build(0);
```
String view:
```
"\t\tThis is a line with indent of 2\n//Comment\nif(true)\n{\n\treturn 0;\n}"
```
Output:

![VsDebugConsole_EczeSLmWK2](https://user-images.githubusercontent.com/20230176/187430437-b1fe417a-ce17-41dd-be7b-a38b118cf2ae.png)

## Build method argument is used for defining an "initial" indent e.g.:
```C#
CodeTextBuilder b = new CodeTextBuilder() 
{ 
    new CodeTextLine("All the code is two tabs right", 0),
    "//Comment",
    new CodeTextBlock("if(true)")
    {
       "return 0;"
    }
};
string a = b.Build(2);
```
Output:

![VsDebugConsole_bAiXWoXLWa](https://user-images.githubusercontent.com/20230176/187430852-db374f6d-3896-4814-9425-c8d360d97cac.png)

## Demo

![46gNxBA31z](https://user-images.githubusercontent.com/20230176/187433283-624f44a6-63f4-4e32-ba02-b5daaf1682d8.gif)
